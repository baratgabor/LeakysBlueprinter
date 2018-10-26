using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_BlueprintDefinition : MyObjectBuilder_DefinitionBase
    {
        [XmlArrayItem("Item")]
        public BlueprintItem[] Prerequisites;

        //[XmlArrayItem("Item")]
        //public BlueprintItem[] Results;

        public BlueprintItem Result;

        public float BaseProductionTimeInSeconds = 1.0f;
    }
}
