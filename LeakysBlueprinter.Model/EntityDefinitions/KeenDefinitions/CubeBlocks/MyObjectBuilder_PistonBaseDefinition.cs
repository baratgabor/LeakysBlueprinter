using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_PistonBaseDefinition : MyObjectBuilder_MechanicalConnectionBlockBaseDefinition
    {
        public float Minimum = 0f;

        public float Maximum = 10f;

        public float MaxVelocity = 5;

        public string ResourceSinkGroup;

        public float RequiredPowerInput;
    }
}
