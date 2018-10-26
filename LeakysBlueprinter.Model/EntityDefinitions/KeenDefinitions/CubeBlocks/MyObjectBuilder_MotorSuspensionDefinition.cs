using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_MotorSuspensionDefinition : MyObjectBuilder_MotorStatorDefinition
    {
        public float MaxSteer = 0.8f;

        public float SteeringSpeed = 0.1f;

        public float PropulsionForce = 10000;

        public float MinHeight = -0.32f;

        public float MaxHeight = 0.26f;
    }
}
