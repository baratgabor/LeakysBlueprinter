using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    /// <summary>
    /// Represents a command with additional attributes for displaying on user interface.
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        private ICommand _command;
        public ICommand Command
        {
            get => _command;
            private set => Set(ref _command, value);
        }

        private string _commandParameter;
        public string CommandParameter
        {
            get => _commandParameter;
            private set => Set(ref _commandParameter, value);
        }

        private FontAwesomeIcon _icon;
        public FontAwesomeIcon Icon
        {
            get => _icon;
            private set => Set(ref _icon, value);
        }

        private string _toolTip;
        public string ToolTip
        {
            get => _toolTip;
            private set => Set(ref _toolTip, value);
        }

        // TODO: Consider making setter public to utilize property change synchronization

        public CommandViewModel(string displayName, string commandParameter, ICommand command, FontAwesomeIcon icon, string toolTip)
        {
            base.DisplayName = displayName;
            CommandParameter = commandParameter;
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Icon = icon;
            ToolTip = toolTip;
        }

        public CommandViewModel(string displayName, string commandParameter, ICommand command, FontAwesomeIcon icon)
        {
            base.DisplayName = displayName;
            CommandParameter = commandParameter;
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Icon = icon;
        }

        public CommandViewModel(string displayName, string commandParameter, ICommand command)
        {
            base.DisplayName = displayName;
            CommandParameter = commandParameter;
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }
    }

}
