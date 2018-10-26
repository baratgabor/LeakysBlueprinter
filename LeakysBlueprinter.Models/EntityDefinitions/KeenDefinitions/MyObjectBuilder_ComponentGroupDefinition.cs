using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ComponentGroupDefinition : MyObjectBuilder_DefinitionBase
    {
        public struct Component
        {
            [XmlAttribute]
            public string SubtypeId;

            [XmlAttribute]
            public int Amount;
        }

        [XmlArrayItem("Component")]
        public Component[] Components;
    }
}
