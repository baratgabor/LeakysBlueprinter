using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_MedicalRoomDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public string IdleSound;

        public string ProgressSound;

        public bool RespawnAllowed = true;

        public bool HealingAllowed = true;

        public bool RefuelAllowed = true;

        public bool SuitChangeAllowed = true;

        public bool CustomWardrobesEnabled = false;

        public bool ForceSuitChangeOnRespawn = false;

        public bool SpawnWithoutOxygenEnabled = true;

        public string RespawnSuitName = null;

        [XmlArrayItem("Name")]
        public string[] CustomWardRobeNames = null;
    }
}
