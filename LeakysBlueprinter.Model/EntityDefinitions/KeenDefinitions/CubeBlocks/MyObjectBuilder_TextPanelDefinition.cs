using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_TextPanelDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public float RequiredPowerInput = 0.001f;

        public int TextureResolution = 512;

        public int TextureAspectRadio = 1;
    }
}
