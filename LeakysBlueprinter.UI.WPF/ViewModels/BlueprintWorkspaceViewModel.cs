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
        public string Creator => _blueprint.CreatorName;

        public string Mass { get; private set; }
        public int GridCount => _blueprint.GridCount;
        public int BlockCount => _blueprint.BlockCount;
        public int DamagedBlockCount => _blueprint.DamagedBlockCount;
        public int IncompleteBlockCount => _blueprint.IncompleteBlockCount;

        public BlueprintWorkspaceViewModel()
        {
            counter++;

            Init(null);
        }

        ~BlueprintWorkspaceViewModel()
        {
            counter--;
        }

        private readonly BlueprintService _blueprint;

        public BlueprintWorkspaceViewModel(string blueprintURI, BlueprintService service)
        {
            counter++;

            _blueprint = service;
            Init(blueprintURI);

            DisplayName = _blueprint.BlueprintName;
            Mass = _blueprint.Mass > 100000 ? $"{(_blueprint.Mass / 1000):#,##0.00} tons" : $"{_blueprint.Mass:#,##0.00} kg";
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
