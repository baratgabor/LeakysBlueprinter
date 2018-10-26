using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_AirtightDoorGenericDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;
        public float PowerConsumptionIdle;
        public float PowerConsumptionMoving;
        public float OpeningSpeed;
        public string Sound;
        public float SubpartMovementDistance = 2.5f;
    }
}
