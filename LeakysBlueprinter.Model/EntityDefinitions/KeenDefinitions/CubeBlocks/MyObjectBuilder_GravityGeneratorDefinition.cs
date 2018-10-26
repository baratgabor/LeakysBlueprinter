using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_GravityGeneratorDefinition : MyObjectBuilder_GravityGeneratorBaseDefinition
    {
        public float RequiredPowerInput;
        public SerializableVector3 MinFieldSize = new SerializableVector3(1, 1, 1);
        public SerializableVector3 MaxFieldSize = new SerializableVector3(150, 150, 150);
    }
}
