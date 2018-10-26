using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_AirVentDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;
        public string ResourceSourceGroup;
        public float OperationalPowerConsumption;
        public float StandbyPowerConsumption;
        public float VentilationCapacityPerSecond;

        public string PressurizeSound;
        public string DepressurizeSound;
        public string IdleSound;
    }
}
