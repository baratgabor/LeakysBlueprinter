using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.Tests
{
    public static partial class TestHelpers
    {
        /// <summary>
        /// Fluent builder for building XElement structures that serve as a full Blueprint.
        /// </summary>
        /// <typeparam name="TParent">Parent builder class or <see cref="DataBuilder.BuilderRoot"/></typeparam>
        public class BlueprintBuilder<TParent> : BuilderBase<TParent, BlueprintBuilder<TParent>>
            where TParent : IXElementBuilder
        {
            protected const string _dataTemplate =
                @"<?xml version=""1.0""?>
            <Definitions xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
              <ShipBlueprints>
                <ShipBlueprint xsi:type=""MyObjectBuilder_ShipBlueprintDefinition"">
                  <Id Type = ""MyObjectBuilder_ShipBlueprintDefinition"" Subtype=""BlueprintTestGrid"" />
                  <DisplayName>CreatorName</DisplayName>
                  <CubeGrids>
                  </CubeGrids>
                  <WorkshopId>0</WorkshopId>
                  <OwnerSteamId>00000000000000000</OwnerSteamId>
                  <Points>0</Points>
                </ShipBlueprint>
              </ShipBlueprints>
            </Definitions>";

            protected const string _creatorNamePath = "./ShipBlueprints/ShipBlueprint";
            protected const string _gridInsertionPath = "./ShipBlueprints/ShipBlueprint/CubeGrids";

            public BlueprintBuilder(TParent parent) : base(parent, _dataTemplate)
            { }

            public BlueprintBuilder<TParent> CreatorName(string creatorName)
            {
                UpdateOrAddElementAt(_creatorNamePath, "DisplayName", creatorName);
                return this;
            }

            public CubeGridBuilder<BlueprintBuilder<TParent>> AndGridWith()
            {
                var cubegridBuilder = new CubeGridBuilder<BlueprintBuilder<TParent>>(this);
                AddNewChildAt(_gridInsertionPath, cubegridBuilder.BuildCurrent());
                return cubegridBuilder;
            }
        }
    }
}
