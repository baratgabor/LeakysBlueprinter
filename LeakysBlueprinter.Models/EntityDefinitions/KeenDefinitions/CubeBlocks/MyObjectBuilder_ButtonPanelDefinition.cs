using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_ButtonPanelDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public int ButtonCount;

        public string[] ButtonSymbols;

        public Vector4[] ButtonColors;

        public Vector4 UnassignedButtonColor;
    }
}
