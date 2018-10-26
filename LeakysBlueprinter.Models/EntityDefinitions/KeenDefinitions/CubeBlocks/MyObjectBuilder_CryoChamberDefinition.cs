using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_CryoChamberDefinition : MyObjectBuilder_CockpitDefinition
    {
        public string OverlayTexture;
        public string ResourceSinkGroup;
        public float IdlePowerConsumption = 0.001f;
        public string OutsideSound;
        public string InsideSound;
    }
}
