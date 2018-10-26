using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_RefineryDefinition : MyObjectBuilder_ProductionBlockDefinition
    {
        public float RefineSpeed;

        public float MaterialEfficiency;
    }
}
