using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    public interface IWorkspace
    {
        string DisplayName { get; }
        event EventHandler RequestClose;


    }
}
