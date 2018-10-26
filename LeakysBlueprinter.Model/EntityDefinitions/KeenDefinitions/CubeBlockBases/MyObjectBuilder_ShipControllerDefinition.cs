using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ShipControllerDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public bool EnableFirstPerson;
        public bool EnableShipControl;
        public bool EnableBuilderCockpit;

        public string GetInSound;
        public string GetOutSound;
    }
}
