using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_PoweredCargoContainerDefinition : MyObjectBuilder_CargoContainerDefinition
    {
        public string ResourceSinkGroup;

        public float RequiredPowerInput;
    }
}
