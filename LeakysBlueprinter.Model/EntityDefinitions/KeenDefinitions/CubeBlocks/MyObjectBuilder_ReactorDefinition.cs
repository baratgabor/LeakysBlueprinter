using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ReactorDefinition : MyObjectBuilder_PowerProducerDefinition
    {
        public Vector3 InventorySize = new Vector3(10, 10, 10);
        
        public SerializableDefinitionId FuelId = new SerializableDefinitionId(typeof(MyObjectBuilder_Ingot).ToString(), "Uranium");
    }
}
