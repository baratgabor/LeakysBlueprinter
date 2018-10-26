using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        /// <summary>
        /// Data builder facade for building test data through multiple nested fluent builders.
        /// </summary>
        public static class DataBuilder
        {
            /// <summary>
            /// Starts building data with a CubeBlock as root element
            /// </summary>
            public static CubeBlockBuilder<BuilderRoot> BuildCubeBlock()
                => new BuilderRoot().CubeBlock();

            /// <summary>
            /// Starts building data with a CubeGrid as root element
            /// </summary>
            public static CubeGridBuilder<BuilderRoot> BuildCubeGrid()
                => new BuilderRoot().CubeGrid();

            /// <summary>
            /// Starts building data with a full Blueprint as root element
            /// </summary>
            public static BlueprintBuilder<BuilderRoot> BuildBlueprint()
                => new BuilderRoot().Blueprint();

            /// <summary>
            /// Caps the chain of parentable builders.
            /// </summary>
            public class BuilderRoot : IXElementBuilder
            {
                private IXElementBuilder _builder;
                public XElement Build() => _builder.BuildCurrent();
                public XElement BuildCurrent() => _builder.BuildCurrent();
                public static implicit operator XElement(BuilderRoot b) => b.Build();

                public BlueprintBuilder<BuilderRoot> Blueprint() => SetAndReturn(new BlueprintBuilder<BuilderRoot>(this));
                public CubeGridBuilder<BuilderRoot> CubeGrid() => SetAndReturn(new CubeGridBuilder<BuilderRoot>(this));
                public CubeBlockBuilder<BuilderRoot> CubeBlock() => SetAndReturn(new CubeBlockBuilder<BuilderRoot>(this));

                private T SetAndReturn<T>(T t) where T : IXElementBuilder
                {
                    _builder = t;
                    return t;
                }
            }
        }
    }
}
