using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_MotorStatorDefinition : MyObjectBuilder_MechanicalConnectionBlockBaseDefinition
    {
        public string ResourceSinkGroup;

        public float RequiredPowerInput;

        public float MaxForceMagnitude;

        public float RotorDisplacementMin;

        public float RotorDisplacementMax;

        public float RotorDisplacementInModel;
    }
}
