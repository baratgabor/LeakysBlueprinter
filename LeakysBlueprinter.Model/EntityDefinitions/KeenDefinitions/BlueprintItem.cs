using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Definition file for the corresponding entity. Replicated from Space Engineers, adapted for own use.
    /// </summary>
    public class BlueprintItem
    {
        [XmlAttribute]
        public string TypeId { get; set; }

        [XmlAttribute]
        public string SubtypeId { get; set; }

        [XmlAttribute]
        public string Amount;
    }
}
