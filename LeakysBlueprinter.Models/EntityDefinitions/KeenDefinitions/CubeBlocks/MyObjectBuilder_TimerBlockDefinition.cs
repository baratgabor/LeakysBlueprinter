using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_TimerBlockDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;

        public string TimerSoundStart = "";

        public string TimerSoundMid = "";

        public string TimerSoundEnd = "";
    }
}
