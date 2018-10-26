using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct SerializableDefinitionId
    {
        [XmlAttribute("Type")]
        public string TypeIdStringAttribute
        {
            get; set;
        }

        [XmlElement("TypeId")]
        public string TypeIdString
        {
            get; set;
        }

        [XmlAttribute("Subtype")]
        public string SubtypeIdAttribute
        {
            get; set;
        }

        [XmlIgnore]
        public string SubtypeName;

        public string SubtypeId
        {
            get; set;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", TypeIdString, SubtypeName);
        }

        public SerializableDefinitionId(string typeId, string subtypeName)
        {
            
            TypeIdString = TypeIdStringAttribute = typeId;
            SubtypeName = SubtypeId = SubtypeIdAttribute = subtypeName;
        }

    }
}
