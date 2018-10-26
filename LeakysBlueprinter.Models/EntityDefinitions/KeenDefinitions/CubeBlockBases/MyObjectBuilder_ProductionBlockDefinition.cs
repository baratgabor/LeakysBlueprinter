using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ProductionBlockDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public float InventoryMaxVolume;

        public Vector3 InventorySize;

        public string ResourceSinkGroup;

        public float StandbyPowerConsumption;

        public float OperationalPowerConsumption;

        [XmlArrayItem("Class")]
        public string[] BlueprintClasses;
    }
}
