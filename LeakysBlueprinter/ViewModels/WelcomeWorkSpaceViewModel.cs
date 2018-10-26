using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.ViewModels
{
    class WelcomeWorkSpaceViewModel : WorkspaceViewModelBase, IWorkspace
    {
        public WelcomeWorkSpaceViewModel()
        {
            DisplayName = "Welcome";
        }
    }
}
