using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        public interface IXElementBuilder
        {
            XElement Build();
            XElement BuildCurrent();
        }
    }
}
