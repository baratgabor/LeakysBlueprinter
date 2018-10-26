using System.ComponentModel;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public abstract class MyObjectBuilder_DefinitionBase : MyObjectBuilder_Base
    {
        public SerializableDefinitionId Id;

        [DefaultValue("")]
        public string DisplayName;

        [DefaultValue("")]
        public string Description;

        [DefaultValue(new string[] { "" })]
        [XmlElement("Icon")]
        public string[] Icons;

        [DefaultValue(true)]
        public bool Public = true;

        [DefaultValue(true), XmlAttribute(AttributeName = "Enabled")]
        public bool Enabled = true;

        [DefaultValue(true)]
        public bool AvailableInSurvival = true;
    }
}
