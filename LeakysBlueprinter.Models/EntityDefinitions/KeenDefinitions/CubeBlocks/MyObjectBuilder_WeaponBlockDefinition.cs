using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_WeaponBlockDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public class WeaponBlockWeaponDefinition
        {
            //[XmlIgnore]
            //public MyObjectBuilderType Type = typeof(MyObjectBuilder_WeaponDefinition);

            [XmlAttribute]
            public string Subtype;
        }

        public WeaponBlockWeaponDefinition WeaponDefinitionId;

        public string ResourceSinkGroup;

        public float InventoryMaxVolume;
    }
}
