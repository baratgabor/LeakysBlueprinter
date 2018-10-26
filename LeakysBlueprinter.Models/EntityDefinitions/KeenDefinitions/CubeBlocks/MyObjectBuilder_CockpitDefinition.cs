using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public enum MyCockpitType
    {
        Closed, //First Person enabled, needs Interior and Glass model
        Open, //First Person disabled
        OpenFP //First Person enabled
    }

    public class MyObjectBuilder_CockpitDefinition : MyObjectBuilder_ShipControllerDefinition
    {
        [XmlIgnore]
        private string m_characterAnimation;

        public string GlassModel;

        public string InteriorModel;

        public string CharacterAnimation
        {
            get; set;
        }

        [XmlIgnore]
        public string CharacterAnimationFile;

        public float OxygenCapacity;
        public bool IsPressurized;
    }
}
