using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.ViewModels
{
    public interface ITransient
    {
        event EventHandler ContinueWithSuccessor;
        Func<IMainContent> Successor { get; }
    }
}
