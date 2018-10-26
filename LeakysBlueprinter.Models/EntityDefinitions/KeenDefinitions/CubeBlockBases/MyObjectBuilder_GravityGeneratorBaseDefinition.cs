using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_GravityGeneratorBaseDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;
        public float MinGravityAcceleration = -9.81f;
        public float MaxGravityAcceleration = 9.81f;
    }
}
