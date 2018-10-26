using Easy.MessageHub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Displayable name of component.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
        #endregion

        #region Message Bus Stuff
        protected IMessageHub _messageHub;

        public ViewModelBase()
        { }

        public ViewModelBase(IMessageHub messageHub)
            => _messageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));

        protected Guid Subscribe<T>(Action<T> action)
            => _messageHub.Subscribe(action);

        protected void Unsubscribe(Guid token)
            => _messageHub.Unsubscribe(token);

        protected void Publish<T>(T message)
            => _messageHub.Publish(message);
        #endregion
    }

}
