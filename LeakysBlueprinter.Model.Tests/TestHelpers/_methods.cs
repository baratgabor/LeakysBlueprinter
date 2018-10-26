using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using static LeakysBlueprinter.Model.MyObjectBuilder_CubeBlockDefinition;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        public static XElement DeepCopy(XElement elementToCopy)
            => XElement.Parse(elementToCopy.ToString());
    }
}
