using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_BatteryBlockDefinition : MyObjectBuilder_PowerProducerDefinition
    {
        public float MaxStoredPower;

        public float InitialStoredPowerRatio = 0.3f;

        public string ResourceSinkGroup;

        public float RequiredPowerInput;

        public bool AdaptibleInput = true;
    }
}
