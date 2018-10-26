using Easy.MessageHub;
using LeakysBlueprinter.UI.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    public class MainContentViewModelBase : ViewModelBase
    {
        public event EventHandler RequestExitApp;
        public ICommand RequestExitAppCommand { get; protected set; }

        public event EventHandler ContinueWithSuccessor;
        public ICommand FinishedCommand { get; protected set; }

        public MainContentViewModelBase()
            => Ctor_Inner();

        public MainContentViewModelBase(IMessageHub messageHub) : base(messageHub)
            => Ctor_Inner();

        protected void Ctor_Inner()
        {
            RequestExitAppCommand = new RelayCommand<object>(OnRequestExitApp);
            FinishedCommand = new RelayCommand<object>(OnFinished);
        }

        public void OnRequestExitApp(object sender)
            => RequestExitApp?.Invoke(sender, EventArgs.Empty);

        public void OnFinished(object sender)
            => ContinueWithSuccessor?.Invoke(sender, EventArgs.Empty);
    }
}
