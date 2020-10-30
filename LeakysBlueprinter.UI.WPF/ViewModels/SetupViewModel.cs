using LeakysBlueprinter.Model.Exceptions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeakysBlueprinter.UI.WPF.Utilities;
using System.Windows.Input;
using Easy.MessageHub;
using LeakysBlueprinter.UI.WPF.EventPayloads;
using LeakysBlueprinter.UI.WPF.Setup.StaticConfig;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    public class SetupViewModel : MainContentViewModelBase, IMainContent, ISkippableTransient
    {
        public List<ResourceViewModel> ResourceStatuses { get; protected set; } = new List<ResourceViewModel>();

        public string IntroTitle { get; protected set; } = "Welcome!";
        public string IntroMessage { get; protected set; }

        public Func<IMainContent> Successor { get; }

        public ICommand ContinueCommand { get; protected set; }
        public ICommand ExitAppCommand { get; protected set; }
        public ICommand SelectResourceRootFolderCommand { get; protected set; }
        public ICommand AutodetectResourceRootFolderCommand { get; protected set; }
      
        public bool CanContinue
        {
            get => _canContinue;
            protected set => Set(ref _canContinue, value);
        }

        public string TargetAppFolder
        {
            get => _targetAppFolder;
            set
            {
                Set(ref _targetAppFolder, value);
                ResourceStatuses.ForEach((r) => r.FileUri = r.MakeUri(TargetAppFolder));
            }
        }

        public bool Skip { get; protected set; } = false;

        protected const string _intro_NoCachedSettingsFound = "It looks like you're running the app for the first time. We'll need to set up access to a few resources.";
        protected const string _intro_CachedSettingsInvalid_AutodetectFail = "We've run into trouble locating one or more resources. Maybe the location of the files changed. Could you help us out a bit?";
        protected const string _intro_CachedSettingsInvalid_AutodetectSuccess = "We didn't find one or two resources at the previously set location, but we managed to autodetect them. Could you confirm that these are correct?";
        protected const char _settingsDelimiter = '|';

        protected bool _canContinue;
        protected int _resourceCount;
        protected bool _canAutodetect = true;
        protected const string _targetAppName = "Space Engineers";
        protected string _targetAppFolder;

        protected List<string> resources_CachedUris;

        // message part depending on autodetections status:
        // if not set for any, i.e. autodetect failed: "Could you help us a bit?"
        // if not set for some: "Some of the files were successfully autodetected, but some weren't. Could you help us a bit?"
        // if set for all: "Luckily we managed to autodetect everything. Could you confirm that the following are correct?"

        // if file uri not set: "It looks like you're running this app for the first time. We'll need to set up access to a few resources."
        // if any file uri not valid: "It seems the location of one or more resource files changed."

        /// <summary>
        /// Parameterless constructor for Designer functionality
        /// </summary>
        public SetupViewModel()
        {
            _messageHub = MessageHub.Instance;
            SetupViewModel_Inner();
        }

        public SetupViewModel(IMessageHub messageHub, Func<IMainContent> successor) : base(messageHub)
        {
            Successor = successor;
            SetupViewModel_Inner();
        }

        protected void SetupViewModel_Inner()
        {
            DisplayName = "Setup";
            PopulateRequiredResources();

            if (RunSkippable())
            {
                Skip = true;
                OnFinished(this);
            }

            CreateResourceStatuses();
            CanContinue = ResourceStatuses.All(s => s.FileUriValid);

            IntroMessage = GetMessage();
            SetupCommands();
        }

        protected bool RunSkippable() =>
            resources_CachedUris != null
            && resources_CachedUris.Count == _resourceCount
            && resources_CachedUris.All(uri => ResourceViewModel.IsValid(uri));

        protected string GetMessage()
        {
            string message;

            if (Skip)
                return "We're ready to go, everything is already set up correctly.";
            else if (ResourceStatuses.Any(r => string.IsNullOrEmpty(r.FileUri)))
                message = "Looks like you're running this app for the first time. This means we need to set up access to a few resources. \r\n\r\nYou can try to autodetect their location, set the game folder manually, or even provide the location of each resource individually.";
            else // this means at least one of them it not valid; otherwise Skip would have been true
                message = "It seems one or more resources are not accessible anymore. \r\n\r\nYou can try to autodetect their location, set the game folder manually, or even provide the location of each resource individually.";

            return message;
        }

        private void CreateResourceStatuses()
        {
            for (int i = 0; i < _resourceCount; i++)
            {
                ResourceStatuses.Add(
                    new ResourceViewModel(
                        messageHub: _messageHub,
                        targetFileName: ResourceConfig.ResourceDefaults[i].TargetFileName,
                        relativePath: ResourceConfig.ResourceDefaults[i].RelativePath,
                        displayName: ResourceConfig.ResourceDefaults[i].DisplayName,
                        description: ResourceConfig.ResourceDefaults[i].Description,
                        existingFileUri: resources_CachedUris[i]
                ));
                ResourceStatuses[i].PropertyChanged += HandlePropertyChanged;
            }
        }

        private void PopulateRequiredResources()
        {
            resources_CachedUris = new List<string>(Properties.Settings.Default.Resources_CachedUris.Split(_settingsDelimiter));

            _resourceCount = ResourceConfig.ResourceDefaults.Count;

            if (resources_CachedUris == null || resources_CachedUris.Count != _resourceCount)
                resources_CachedUris = new List<string>(new string[_resourceCount]);
        }

        protected void SetupCommands()
        {
            ContinueCommand = new RelayCommand<object>(ContinueCommandHandler, (_) => CanContinue);
            ExitAppCommand = new RelayCommand<object>((_) => OnRequestExitApp(this));
            SelectResourceRootFolderCommand = new RelayCommand<object>(SelectResourceRootFolder);
            AutodetectResourceRootFolderCommand = new RelayCommand<object>(AutodetectResourceRootFolder, (_) => _canAutodetect);
        }

        private void AutodetectResourceRootFolder(object obj)
        {
            string result = GetAppFolder();
            if (string.IsNullOrEmpty(result))
                _canAutodetect = false;
            else
                TargetAppFolder = result;
        }

        private void SelectResourceRootFolder(object obj)
        {
            var message = new SelectFolderThroughDialog()
            {
                Sender = this
            };

            Publish(message);

            if (!string.IsNullOrEmpty(message.FolderSelected))
                TargetAppFolder = message.FolderSelected;
        }

        protected bool TryDetectAllResources()
            => ResourceStatuses.All((resource) => resource.DetectUri(TargetAppFolder));

        /// <summary>
        /// Saves settings and signals that this viewmodel finished executing
        /// </summary>
        protected void ContinueCommandHandler(object o)
        {
            var uriList = from r in ResourceStatuses
                    select r.FileUri;
            var delimitedUriList = String.Join(_settingsDelimiter.ToString(), uriList);
            Properties.Settings.Default.Resources_CachedUris = delimitedUriList;
            Properties.Settings.Default.Save();

            OnFinished(this);
        }

        protected string GetAppFolder()
        {
            if (_canAutodetect == false)
                return null;
            else
                // TODO: replace reference with abstraction
                return InstalledApplicationsFinder.GetApplictionInstallPath(_targetAppName);
        }

        protected void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileUriValid")
                CanContinue = ResourceStatuses.All(s => s.FileUriValid);
        }
    }
}
 