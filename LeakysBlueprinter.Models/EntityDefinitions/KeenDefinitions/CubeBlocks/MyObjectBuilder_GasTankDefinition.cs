using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_GasTankDefinition : MyObjectBuilder_ProductionBlockDefinition
    {
        public float Capacity;

        public SerializableDefinitionId StoredGasId;

        public string ResourceSourceGroup;
    }
}
