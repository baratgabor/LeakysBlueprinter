using Easy.MessageHub;
using LeakysBlueprinter.EventPayloads;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.ViewModels
{
    /// <summary>
    /// Application's main viewmodel that exposes another viewmodels as content, facilitates switching between them, and delegates their UI-dependant requests to the View.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Events exposed for View
        public event EventHandler RequestExitApp;
        public event EventHandler<SelectFileUriThroughDialog> RequestFileUri;
        public event EventHandler<SelectFolderThroughDialog> RequestFolderPath;
        #endregion

        public bool Ready { get; protected set; }

        public IMainContent CurrentViewModel
        {
            get => _currentViewModel;
            protected set => Set(ref _currentViewModel, value);
        }

        protected const string _appName = "Leaky's Blueprinter";
        protected List<IMainContent> _viewModels = new List<IMainContent>();
        protected IMainContent _currentViewModel;
        
        /// <summary>
        /// Parameterless constructor for Designer functionality
        /// </summary>
        public MainViewModel()
        {
            Init(new SetupViewModel());
            //Init(new MainContentViewModel());
        }

        /// <summary>
        /// Parametered constructor for actual execution
        /// </summary>
        public MainViewModel(IMessageHub messageHub, IMainContent startupContent) : base (messageHub)
        {
            SetupSubscriptions();
            Init(startupContent);
        }

        /// <summary>
        /// Sets up subscriptions to message bus messages, routing them to an event invocation (with the View as intended subscriber).
        /// Essentially, these are UI-dependent requests that are delegated to the view.
        /// </summary>
        protected void SetupSubscriptions()
        {
            // Acquires a file uri 
            Subscribe<SelectFileUriThroughDialog>(
                (message) => RequestFileUri?.Invoke(message.Sender, message)
            );

            // Acquires a folder path
            Subscribe<SelectFolderThroughDialog>(
                (message) => RequestFolderPath?.Invoke(message.Sender, message)
            );
        }

        protected void Init(IMainContent startupContent)
        {
            base.DisplayName = _appName;

            startupContent = GetFirstActual(startupContent);

            ChangeViewModel(startupContent);
            Ready = true;

            IMainContent GetFirstActual(IMainContent content)
            {
                if (content is ISkippableTransient skippable && skippable.Skip == true)
                    content = GetFirstActual(skippable.Successor());

                return content;
            }
        }

        protected void ChangeViewModel(IMainContent viewModel, bool discardPrevious = false)
        {
            if (!_viewModels.Contains(viewModel))
                _viewModels.Add(viewModel);

            Unsubscribe(CurrentViewModel); // Event-based operations are supported only on the active content
            if (discardPrevious)
                _viewModels.Remove(CurrentViewModel);

            Subscribe(viewModel);
            CurrentViewModel = viewModel;
        }

        protected void Subscribe(IMainContent viewModel)
        {
            if (viewModel == null) return;

            viewModel.RequestExitApp += (_, __) => this.RequestExitApp?.Invoke(this, EventArgs.Empty);
            if (viewModel is ITransient transient)
                transient.ContinueWithSuccessor += (_, __) => ChangeViewModel(transient.Successor?.Invoke(), discardPrevious: true);
        }

        protected void Unsubscribe(IMainContent viewModel)
        {
            if (viewModel == null) return;

            viewModel.RequestExitApp -= (_, __) => this.RequestExitApp?.Invoke(this, EventArgs.Empty);
            if (viewModel is ITransient transient)
                transient.ContinueWithSuccessor -= (_, __) => ChangeViewModel(transient.Successor?.Invoke(), discardPrevious: true);
        }
    }
}
