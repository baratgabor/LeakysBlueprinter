using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.ViewModels
{
    interface IResourceViewModelFactory
    {
        IResourceStatus Create();
    }

    public interface IResourceStatus
    {

    }
}
