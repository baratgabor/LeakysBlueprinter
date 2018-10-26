using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ProjectorDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;
        public float RequiredPowerInput;
        public string IdleSound;
    }
}
