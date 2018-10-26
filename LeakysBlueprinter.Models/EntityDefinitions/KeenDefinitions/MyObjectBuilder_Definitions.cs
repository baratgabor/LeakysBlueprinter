using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    [XmlRoot("Definitions")]
    public class MyObjectBuilder_Definitions : MyObjectBuilder_Base
    {
        [XmlArrayItem("Blueprint", typeof(MyObjectBuilder_BlueprintDefinition))]
        public MyObjectBuilder_BlueprintDefinition[] Blueprints;

        [XmlArrayItem("Component", typeof(MyObjectBuilder_ComponentDefinition))]
        public MyObjectBuilder_ComponentDefinition[] Components;

        //[XmlArrayItem("ContainerType", Type = typeof(MyAbstractXmlSerializer<MyObjectBuilder_ContainerTypeDefinition>))]
        //[ProtoMember]
        //public MyObjectBuilder_ContainerTypeDefinition[] ContainerTypes;

        [XmlArrayItem("Definition", typeof(MyObjectBuilder_CubeBlockDefinition))]
        public MyObjectBuilder_CubeBlockDefinition[] CubeBlocks;

        [XmlArrayItem("PhysicalItem", typeof(MyObjectBuilder_PhysicalItemDefinition))]
        public MyObjectBuilder_PhysicalItemDefinition[] PhysicalItems;

        //[XmlArrayItem("ShipBlueprint", Type = typeof(MyAbstractXmlSerializer<MyObjectBuilder_ShipBlueprintDefinition>))]
        //[ProtoMember]
        //public MyObjectBuilder_ShipBlueprintDefinition[] ShipBlueprints;

    }
}
