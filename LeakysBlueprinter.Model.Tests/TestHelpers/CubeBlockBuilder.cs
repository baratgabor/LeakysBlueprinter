using System;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        /// <summary>
        /// Fluent builder for building XElement structures that serve as a single CubeBlock.
        /// </summary>
        /// <typeparam name="TParent">Parent builder class or <see cref="DataBuilder.BuilderRoot"/></typeparam>
        public class CubeBlockBuilder<TParent> : BuilderBase<TParent, CubeBlockBuilder<TParent>>
            where TParent : IXElementBuilder
        {
            protected const string _dataTemplate =
                @"<MyObjectBuilder_CubeBlock type=""MyObjectBuilder_CubeBlock"">
                <SubtypeName>LargeBlockArmorBlock</SubtypeName>
                <Min x=""0"" y=""0"" z=""0""/>
                <ColorMaskHSV x=""0.875"" y=""0.2"" z=""0.55""/>
                <BuiltBy>123123123123123</BuiltBy>
            </MyObjectBuilder_CubeBlock>";

            public CubeBlockBuilder(TParent parent) : base(parent, _dataTemplate)
            { }

            public CubeBlockBuilder<TParent> CustomName(string customName)
            {
                UpdateOrAddElement("CustomName", customName);
                return this;
            }

            public CubeBlockBuilder<TParent> SubtypeName(string subtypeName)
            {
                UpdateOrAddElement("SubtypeName", subtypeName);
                return this;
            }

            public CubeBlockBuilder<TParent> Integrity(double integrityPercent)
            {
                if (integrityPercent < 0 || integrityPercent > 1) throw new ArgumentException($"Argument '{nameof(integrityPercent)}' must be between 0 and 1.");
                UpdateOrAddElement("IntegrityPercent", integrityPercent.ToString());
                return this;
            }

            public CubeBlockBuilder<TParent> Build(double buildPercent)
            {
                if (buildPercent < 0 || buildPercent > 1) throw new ArgumentException($"Argument '{nameof(buildPercent)}' must be between 0 and 1.");
                UpdateOrAddElement("BuildPercent", buildPercent.ToString());
                return this;
            }

            public CubeBlockBuilder<TParent> GridPosition(int x, int y, int z)
            {
                AddOrUpdateAttribute("Min", "x", x.ToString());
                AddOrUpdateAttribute("Min", "y", y.ToString());
                AddOrUpdateAttribute("Min", "z", z.ToString());
                return this;
            }
        }
    }
}
