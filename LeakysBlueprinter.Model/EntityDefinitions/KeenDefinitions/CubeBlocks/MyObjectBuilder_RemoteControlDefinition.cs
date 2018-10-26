using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_RemoteControlDefinition : MyObjectBuilder_ShipControllerDefinition
    {
        public string ResourceSinkGroup;
        public float RequiredPowerInput;
    }
}
