using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_SolarPanelDefinition : MyObjectBuilder_PowerProducerDefinition
    {
        public Vector3 PanelOrientation = new Vector3(0, 0, 0);

        public bool TwoSidedPanel = true;

        public float PanelOffset = 1;
    }
}
