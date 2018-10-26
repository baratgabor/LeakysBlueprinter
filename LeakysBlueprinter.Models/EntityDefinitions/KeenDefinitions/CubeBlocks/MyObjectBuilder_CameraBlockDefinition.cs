using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_CameraBlockDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;
        public float RequiredPowerInput;
        public string OverlayTexture;
        public float MinFov = 0.1f;
        public float MaxFov = 1.04719755f;
    }
}
