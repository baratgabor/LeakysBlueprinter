using FontAwesome.WPF;
using LeakysBlueprinter.Model.Queries;
using LeakysBlueprinter.Model;
using LeakysBlueprinter.UI.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    /* Ideas to implement:
        - Show blueprint picture
        - Show path
        - Show grid mass (won't be easy - we need to pull data from cubeblocks file, import data for component masses, and calculate it manually)
        - Show grid flight capabilities, based on grid mass, thrusters and inventory
        - Ability to change inventory content in blocks with inventory
        - Detect damaged blocks, repair everything with a single click
        - Detect incomplete blocks, offer to complete (some blocks are intentionally incomplete for visual purposes)
        - Replace all armor blocks to heavy or light
        - Empty all inventory
        - Put given amount of Uranium into all reactor, or fill tanks and batteries to set amount
        - Undo feature for modifications
        - Save feature to save modified blueprint (obviously)
        - Paint features... e.g. automatic camo paint?
    */

    public class BlueprintWorkspaceViewModel : WorkspaceViewModelBase, IWorkspace
    {
        private static int counter = 0;

        public string FilePath { get; private set; }
        public string Creator { get; private set; } = "Unknown";

        public BlueprintWorkspaceViewModel()
        {
            counter++;

            Init(null);
        }

        ~BlueprintWorkspaceViewModel()
        {
            counter--;
        }


        private BlueprintService _service;
        public BlueprintWorkspaceViewModel(string blueprintURI, BlueprintService service)
        {
            counter++;

            _service = service;
            Init(blueprintURI);
            var query = new GetGridMassQuery()
            {
                GridEntityId = _service.Blueprint.Descendants("CubeGrid").First().Element("EntityId").Value
            };
        }

        private void Init(string blueprintURI)
        {
            base.Commands.Add(new CommandViewModel("Close Blueprint", String.Empty, new RelayCommand<object>(Close), FontAwesomeIcon.Close));
            base.Commands.Add(new CommandViewModel("Save Blueprint", String.Empty, new RelayCommand<object>(Save, (_) => false), FontAwesomeIcon.Save));
        }

        private void Save(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
