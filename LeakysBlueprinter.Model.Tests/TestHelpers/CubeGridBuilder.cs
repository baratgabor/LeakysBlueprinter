using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        /// <summary>
        /// Fluent builder for building XElement structures that serve as a single CubeGrid.
        /// </summary>
        /// <typeparam name="TParent">Parent builder class or <see cref="DataBuilder.BuilderRoot"/></typeparam>    
        public class CubeGridBuilder<TParent> : BuilderBase<TParent, CubeGridBuilder<TParent>>
            where TParent : IXElementBuilder
        {
            protected const string _dataTemplate =
                @"<CubeGrid>
                <SubtypeName/>
                <EntityId>FakeEntityId</EntityId>
                <PersistentFlags>CastShadows InScene</PersistentFlags>
                <PositionAndOrientation>
                    <Position x=""0.0"" y=""0.0"" z=""0.0""/>
                    <Forward x=""0.0"" y=""0.0"" z=""0.0""/>
                    <Up x=""0.0"" y=""0.0"" z=""0.0""/>
                    <Orientation>
                        <X>0.0</X>
                        <Y>0.0</Y>
                        <Z>0.0</Z>
                        <W>0.0</W>
                    </Orientation>
                </PositionAndOrientation>
                <GridSizeEnum>Large</GridSizeEnum>
                <CubeBlocks>
                </CubeBlocks>
                <DisplayName>BlueprintTestGrid</DisplayName>
                <OxygenAmount>
                    <float>100.0</float>
                </OxygenAmount>
                <DestructibleBlocks>true</DestructibleBlocks>
                <IsRespawnGrid>false</IsRespawnGrid>
                <LocalCoordSys>0</LocalCoordSys>
                <TargetingTargets/>
            </CubeGrid>";

            protected const string _dummySkeletonNodeTemplate =
                @"<Skeleton>
                    <BoneInfo>
                      <BonePosition x=""1"" y=""2"" z=""3"" />
                      <BoneOffset x=""114"" y=""124"" z=""126"" />
                    </BoneInfo>
                  </Skeleton>";

            protected const string _blockInsertionPath = "./CubeBlocks";

            public CubeGridBuilder(TParent parent) : base(parent, _dataTemplate)
            { }

            public CubeGridBuilder<TParent> EntityId(string entityId)
            {
                UpdateOrAddElement("EntityId", entityId);
                return this;
            }

            public CubeGridBuilder<TParent> DisplayName(string displayName)
            {
                UpdateOrAddElement("DisplayName", displayName);
                return this;
            }

            public CubeGridBuilder<TParent> DummySkeleton()
            {
                AddNewChild(XElement.Parse(_dummySkeletonNodeTemplate));
                return this;
            }

            public CubeBlockBuilder<CubeGridBuilder<TParent>> AndBlockWith()
            {
                var cubeblockBuilder = new CubeBlockBuilder<CubeGridBuilder<TParent>>(this);
                AddNewChildAt(_blockInsertionPath, cubeblockBuilder.BuildCurrent());
                return cubeblockBuilder;
            }
        }
    }
}
