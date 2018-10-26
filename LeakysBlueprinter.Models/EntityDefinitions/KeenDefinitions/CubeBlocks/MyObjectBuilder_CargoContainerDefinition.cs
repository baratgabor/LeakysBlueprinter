using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_CargoContainerDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        //TODO: remove - this is obsolete and should not be used, instead MyObjectBuilder_InventoryComponentDefinition should be used together with entity container definition.
        public Vector3 InventorySize;
    }
}
