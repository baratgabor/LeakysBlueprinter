using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    [XmlType("VR.PhysicalModelDefinition")]
    public class MyObjectBuilder_PhysicalModelDefinition : MyObjectBuilder_DefinitionBase
    {
        public string Model;

        public string PhysicalMaterial;

        public float Mass = 0;
    }
}
