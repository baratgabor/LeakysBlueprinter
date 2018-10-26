using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using LeakysBlueprinter.UI.WPF.Utilities;
using Easy.MessageHub;
using LeakysBlueprinter.UI.WPF.EventPayloads;
using LeakysBlueprinter.Model;
using FontAwesome.WPF;
using System.Collections.Generic;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    /// <summary>
    /// Application's main content viewmodel that exposes the application's main state and functionality.
    /// Exposes multiple other viewmodels as workspaces.
    /// </summary>
    public class MainContentViewModel : MainContentViewModelBase, IMainContent
    {
        public override string DisplayName => ActiveWorkspace?.DisplayName;

        public ICommand OpenBlueprintCommand { get; private set; }
        public ICommand ResetUserSettingsCommand { get; private set; }

        public RecentFilesCommands RecentFiles { get; private set; }

        private int _selectedWorkspace = 0;
        public int SelectedWorkspace
        {
            get => _selectedWorkspace;
            set
            {
                Set(ref _selectedWorkspace, value);
                RaisePropertyChanged(nameof(ActiveWorkspace));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public ObservableCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                    _commands = new ObservableCollection<CommandViewModel>();
                return _commands;
            }
        }
        private ObservableCollection<CommandViewModel> _commands;

        public IWorkspace ActiveWorkspace
        {
            get
            {
                if (!Workspaces.IndexValid(_selectedWorkspace))
                    return null;
                return Workspaces[_selectedWorkspace];
            }
        }

        /// <summary>
        /// This collection contains all additional viewmodels (aka "workspaces") that we use inside MainViewModel.
        /// The active workspace exposes most of the useful information, and sets the context for most executed commands.
        /// </summary>
        public ObservableCollection<IWorkspace> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<IWorkspace>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                    _workspaces.Add(new WelcomeWorkSpaceViewModel());
                }
                return _workspaces;
            }
        }
        private ObservableCollection<IWorkspace> _workspaces;

        private ApplicationService _applicationService;

        /// <summary>
        /// Parameterless constructor for Designer functionality
        /// </summary>
        public MainContentViewModel()
            => MainContentViewModel_Inner();

        /// <summary>
        /// Parametered constructor for actual execution
        /// </summary>
        public MainContentViewModel(IMessageHub messageHub) : base(messageHub)
        {
            MainContentViewModel_Inner();

            List<Stream> definitionStreams = new List<Stream>();
            Stream resxStream = Stream.Null;
            foreach (var u in new List<string>(Properties.Settings.Default.Resources_CachedUris.Split('|')))
            {
                if (u.Contains(".resx"))
                    resxStream = File.Open(u, FileMode.Open, FileAccess.Read, FileShare.Read);
                else 
                    definitionStreams.Add(File.Open(u, FileMode.Open, FileAccess.Read, FileShare.Read));
            }
            
            _applicationService = AppModelFactory.Create(resxStream, definitionStreams.ToArray());
        }

        private void MainContentViewModel_Inner()
        {
            SetupCommands();
            // TODO: Investigate this settings issue, where setting could throw null exception if wasn't used before
            if (Properties.Settings.Default.RecentFilesList == null)
                Properties.Settings.Default.RecentFilesList = new StringCollection();
            RecentFiles = new RecentFilesCommands(OpenBlueprintCommand, Properties.Settings.Default.RecentFilesList);
        }

        private void SetupCommands()
        {
            OpenBlueprintCommand = new RelayCommand<string>(OpenBlueprint);
            ResetUserSettingsCommand = new RelayCommand<object>(ResetUserSettings);

            Commands.Add(new CommandViewModel("Open Blueprint", String.Empty, OpenBlueprintCommand, FontAwesomeIcon.FolderOpen));
        }

        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModelBase workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModelBase workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            // TODO: Make sure blueprint is not referenced and gets garbage collected, since data size can be large
            this.Workspaces.Remove(sender as IWorkspace);
        }

        private void ResetUserSettings(object obj)
        {
            // TODO: Move to separate settings handler, and reference it there?
            Properties.Settings.Default.Reset();
        }

        // TODO: Too many responsibilities here, make this nice with SRP
        private void OpenBlueprint(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                var message = new SelectFileUriThroughDialog()
                {
                    Sender = this,
                    FileNameFilter = "Blueprint files (*.sbc)|*.sbc",
                    // TODO: Move hardcoded uri to app settings, set up by setupviewmodel
                    InitialDirectory = Environment.SpecialFolder.ApplicationData + @"\Roaming\SpaceEngineers\Blueprints"
                };

                Publish(message);

                if (!string.IsNullOrEmpty(message.FileUriSelected))
                    uri = message.FileUriSelected;
                else
                    return; // code path exit
            }

            if (!File.Exists(uri))
                throw new FileNotFoundException($"File not found at: {uri}");

            // If a blueprint workspace is already open with the give file URI, switch to that tab instead of opening another
            var alreadyOpened = Workspaces.FirstOrDefault((w) => w is BlueprintWorkspaceViewModel && ((BlueprintWorkspaceViewModel)w).FilePath == uri);
            if (alreadyOpened != null)
            {
                SelectedWorkspace = Workspaces.IndexOf(alreadyOpened);
                return;
            }

            // Open a new blueprint workspace instance with the given file URI
            Workspaces.Add(new BlueprintWorkspaceViewModel(uri, _applicationService.CreateBlueprintService(uri)));

            // Make the new workspace the active one
            SelectedWorkspace = Workspaces.Count() - 1;

            // Register URI as a recently opened file
            RecentFiles.AddRecentFile(uri);
        }
    }


    // TODO: Refactor this into a separate viewmodel; consider converting the whole menu into a composite object structure with a data template for visualization
    /// <summary>
    /// Manages the responsibilities related to the list of recent files.
    /// </summary>
    public class RecentFilesCommands : ObservableQueue<CommandViewModel>
    {
        public bool Active { get; set; }
        public CommandViewModel ClearRecentFilesCommand { get; private set; }

        private StringCollection _recentFilesList;
        private System.Windows.Input.ICommand _baseCommand;
        private const string _noFilesLabel = "None";
        private const string _clearListLabel = "Clear List";

        /// <summary>
        /// Creates a new instance of recent files list handler.
        /// </summary>
        /// <param name="openCommand">The command to invoke to open files.</param>
        /// <param name="recentFilesList">The existing list of recent files.</param>
        public RecentFilesCommands(System.Windows.Input.ICommand openCommand, StringCollection recentFilesList) : base()
        {
            _recentFilesList = recentFilesList;
            _baseCommand = openCommand;
            ClearRecentFilesCommand = new CommandViewModel(
                _clearListLabel,
                null,
                new RelayCommand<object>(
                    ClearRecentFiles));

            MakeCommandList();
        }

        /// <summary>
        /// Adds a new file path to the Recent Files list.
        /// </summary>
        /// <param name="uri">Path of the file to be added.</param>
        public void AddRecentFile(string uri)
        {
            if (_recentFilesList.Contains(uri))
                return;

            if (this[0].DisplayName == _noFilesLabel)
                RemoveAt(0);

            Enqueue(MakeCommand(uri));

            if (Count > Properties.Settings.Default.MaxRecentFiles)
                Dequeue();

            SyncAndSaveToSetting();
        }

        private void MakeCommandList()
        {
            if (_recentFilesList.Count > 0)
                foreach (var f in _recentFilesList)
                    Add(MakeCommand(f));
            else
                Add(MakeCommand(_noFilesLabel));
        }

        private bool CanExecute(string uri)
        {
            // To avoid unecessary file exists checks firing
            if (!Active)
                return true;

            return File.Exists(uri);
        }

        private void ClearRecentFiles(object _)
        {
            _recentFilesList.Clear();
            this.Clear();
            MakeCommandList();
            SyncAndSaveToSetting();
        }

        private CommandViewModel MakeCommand(string uri)
            => new CommandViewModel(
                MakeLabel(uri),
                uri,
                new RelayCommand<string>(
                    _baseCommand.Execute,
                    CanExecute));

        private string MakeLabel(string uri)
        {
            char c = Path.DirectorySeparatorChar;

            int m = uri.LastIndexOf(c);
            if (m == -1) goto Return;

            int n = uri.LastIndexOf(c, m - 1);
            if (n == -1) goto Return;

            // Returns last folder name of path. Well, supposed to.
            return uri.Substring(n + 1, m - n - 1);

            Return:
            return uri;
        }

        private void SyncAndSaveToSetting()
        {
            _recentFilesList.Clear();

            foreach (var item in this)
                _recentFilesList.Add(item.CommandParameter);

            // TODO: Refer to a method on a settings handler instead?
            Properties.Settings.Default.Save();
        }
    }

}
