using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ConveyorSorterDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public float PowerInput = 0.001f;

        public Vector3 InventorySize;
    }
}
