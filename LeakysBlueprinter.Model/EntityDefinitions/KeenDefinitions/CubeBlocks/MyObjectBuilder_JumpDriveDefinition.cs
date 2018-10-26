using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_JumpDriveDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public float RequiredPowerInput = 4.0f;

        public float PowerNeededForJump = 1.0f;

        public double MaxJumpDistance = 500000.0;

        public double MaxJumpMass = 1250000.0;

        public float JumpDelay = 10.0f;
    }
}
