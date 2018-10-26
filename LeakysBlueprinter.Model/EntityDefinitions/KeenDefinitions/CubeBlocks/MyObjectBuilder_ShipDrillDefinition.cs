using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ShipDrillDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public float SensorRadius = 1.5f;

        public float SensorOffset;

        public float CutOutRadius = 2.5f;

        public float CutOutOffset;
    }
}
