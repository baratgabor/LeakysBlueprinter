using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct MyObjectBuilder_GasGeneratorResourceInfo
    {
        public SerializableDefinitionId Id;
        public float IceToGasRatio;
    }

    public class MyObjectBuilder_OxygenGeneratorDefinition : MyObjectBuilder_ProductionBlockDefinition
    {
        public float IceConsumptionPerSecond;
        public string IdleSound;
        public string GenerateSound;
        public string ResourceSourceGroup;

        [XmlArrayItem("GasInfo")]
        public List<MyObjectBuilder_GasGeneratorResourceInfo> ProducedGases;
    }
}
