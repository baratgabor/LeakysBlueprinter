using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_OxygenFarmDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public struct MyProducedGasInfo
        {
            public SerializableDefinitionId Id;

            public float MaxOutputPerSecond;
        }

        public string ResourceSinkGroup;

        public string ResourceSourceGroup;

        public Vector3 PanelOrientation = new Vector3(0, 0, 0);

        public bool TwoSidedPanel = true;

        public float PanelOffset = 1;

        public MyProducedGasInfo ProducedGas;

        public float OperationalPowerConsumption = 0.001f;
    }
}
