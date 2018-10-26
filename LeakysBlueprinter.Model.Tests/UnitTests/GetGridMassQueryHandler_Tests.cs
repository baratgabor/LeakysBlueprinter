using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LeakysBlueprinter.Model.Queries;
using Moq;
using LeakysBlueprinter.Model.Exceptions;
using static LeakysBlueprinter.Model.MyObjectBuilder_CubeBlockDefinition;

namespace LeakysBlueprinter.Model.Tests
{
    [TestFixture]
    public class GetGridMassQueryHandler_Tests
    {
        // Test subject
        GetGridMassQueryHandler GetMassQueryHandler;

        GetGridMassQuery ValidQuery = new GetGridMassQuery()
        {
            GridEntityId = "123456"
        };

        Mock<IDefinitionsRepository> MockDefinitionsRepository;
        Mock<IBlueprintDataContext> MockDataContext;

        MyObjectBuilder_CubeBlockDefinition BlockDefinition_WithOneComponentTypeAndTenCount = new MyObjectBuilder_CubeBlockDefinition()
        {
            Components = new[]
            {
                new CubeBlockComponent()
                {
                    Count = 10,
                    Subtype = "Mock"
                }
            }
        };

        MyObjectBuilder_ComponentDefinition ComponentDefinition_WithMassOfTen = new MyObjectBuilder_ComponentDefinition()
        {
            Mass = 10f
        };

        [SetUp]
        public void SetUp()
        {
            MockDefinitionsRepository = new Mock<IDefinitionsRepository>();

            MockDataContext = new Mock<IBlueprintDataContext>();

            //Validity check should always return true
            MockDataContext
                .Setup(dc => dc.IsValid())
                .Returns(true);

            GetMassQueryHandler = new GetGridMassQueryHandler(MockDefinitionsRepository.Object, MockDataContext.Object);
        }

        // TODO: Refactor into something less primitive; test more variations
        [Test]
        public void Handle_OnTenComponentsOfMassTen_ShouldReturnOneHundred()
        {
            XElement TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                .ThatsAll();

            // Ensure initial conditions met
            Assert.Multiple(() => {
                Assert.That(TestGrid.Descendants("MyObjectBuilder_CubeBlock").Count, Is.EqualTo(1),
                    "Test grid must contain a single block.");
                Assert.That(BlockDefinition_WithOneComponentTypeAndTenCount.Components.Count, Is.EqualTo(1),
                    "Test block must contain a single type of component.");
                Assert.That(BlockDefinition_WithOneComponentTypeAndTenCount.Components.First().Count, Is.EqualTo(10),
                    "Test block's component must have Count of 10.");
                Assert.That(ComponentDefinition_WithMassOfTen.Mass, Is.EqualTo(10),
                    "Test component must have Mass of 10.");
            });

            // 1) Mock grid getter: always returns a grid that contains a single block
            MockDataContext
                .Setup(dc => dc.GetGridByEntityId(It.IsAny<string>()))
                .Returns(TestGrid);

            // 2) Mock block definition getter: always returns a block definition that has 10 pieces of a single component type
            MockDefinitionsRepository
                .Setup(repo => repo.CubeBlocks.GetById(It.IsAny<string>()))
                .Returns(BlockDefinition_WithOneComponentTypeAndTenCount);

            // 3) Mock component definition getter: always returns a component that has the mass of 10
            MockDefinitionsRepository
                .Setup(repo => repo.Components.GetById(It.IsAny<string>()))
                .Returns(ComponentDefinition_WithMassOfTen);

            var result = GetMassQueryHandler.Handle(ValidQuery);

            // 1 grid => 1 block => 10 components, each of mass 10 = 100
            Assert.That(result, Is.EqualTo(100f));
        }
    }
}
