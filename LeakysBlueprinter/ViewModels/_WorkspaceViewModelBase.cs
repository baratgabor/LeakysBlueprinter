using LeakysBlueprinter.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeakysBlueprinter.ViewModels
{
    /// <summary>
    /// Viewmodel base of usercontrols appearing as a workspace inside another viewmodel
    /// </summary>
    public abstract class WorkspaceViewModelBase : ViewModelBase
    {
        public event EventHandler RequestClose;
        public ICommand CloseCommand { get; protected set; }

        public WorkspaceViewModelBase() 
            => CloseCommand = new RelayCommand<object>(Close);

        public void Close(object obj)
            => RequestClose?.Invoke(this, EventArgs.Empty);

        public ObservableCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                    _commands = new ObservableCollection<CommandViewModel>();
                return _commands;
            }
        }
        protected ObservableCollection<CommandViewModel> _commands;
        
    }
}
