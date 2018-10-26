using LeakysBlueprinter.Model.Commands;
using LeakysBlueprinter.Model.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Tests.UnitTests
{
    [TestFixture]
    class CompleteAllBlocksCommandHandler_Tests
    {
        // Test subject
        CompleteAllBlocksCommandHandler CompleteAllBlocksHandler;

        XElement TestGrid;

        CompleteAllBlocksCommand ValidCommand = new CompleteAllBlocksCommand
        {
            GridEntityId = "123456"
        };

        Mock<IBlueprintDataContext> MockDataContext;

        [SetUp]
        public void SetUp()
        {
            MockDataContext = new Mock<IBlueprintDataContext>();
            
            //Validity check should always return true
            MockDataContext
                .Setup(dc => dc.IsValid())
                .Returns(true);

            // Grid query should always return our test grid
            MockDataContext
                .Setup(dc => dc.GetGridByEntityId(It.IsAny<string>()))
                .Returns(() => TestGrid); // delegate for lazy eval

            // Block query should always return all blocks in our test grid
            MockDataContext
                .Setup(dc => dc.GetBlocksWithProperty(It.IsAny<string>(), It.IsAny<XElement>()))
                .Returns(() => TestGrid.Descendants("MyObjectBuilder_CubeBlock")); // delegate for lazy eval

            CompleteAllBlocksHandler = new CompleteAllBlocksCommandHandler(MockDataContext.Object);
        }

        [Test]
        [TestCase(0.6d, 0.5d, 0.9d)]
        [TestCase(0.9d, 0.1d, 0.2d)]
        public void Handle_OnGridWithIncompleteBlocksAndAdditionalDamage_IntegrityShouldBeRecalculated(double testBuildPercent, double testIntegrityPercent, double expectedResultIntegrity)
        {
            // Create test data that contains blocks set to the specified values
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Build(testBuildPercent)
                    .Integrity(testIntegrityPercent)
                    .ThatsAll()
                .AndBlockWith()
                    .Build(testBuildPercent)
                    .Integrity(testIntegrityPercent)
                    .ThatsAll();

            // Ensure initial conditions met
            Assert.Multiple(() =>
            {
                Assert.That(testIntegrityPercent < testBuildPercent,
                    "Test argument invalid: The specified test IntegrityPercent must be lower than the specified test BuildPercent.");

                Assert.That(1 - (testBuildPercent - testIntegrityPercent), Is.EqualTo(expectedResultIntegrity).Within(0.0001d),
                    "Test argument invalid: The specified expected result integrity is supposed to reflect the difference between the specified BuildPercent and IntegrityPercent, " +
                    "e.g. BuildPercent of 0.6 and IntegrityPercent of 0.5 should result in IntegrityPercent of 0.9 (i.e. 1-(0.6-0.5) after full integrity is restored.");

                Assert.That(
                    TestGrid.Descendants("MyObjectBuilder_CubeBlock").All(b 
                        => double.Parse(b.Element("BuildPercent").Value) == testBuildPercent
                        && double.Parse(b.Element("IntegrityPercent").Value) == testIntegrityPercent),
                    $"All blocks in test grid must have the specified BuildPercent value ({testBuildPercent}) and IntegrityPercent value ({testIntegrityPercent}) set.");
            });

            CompleteAllBlocksHandler.Handle(ValidCommand);

            // TestGrid.Descendants("IntegrityPercent").First().Value = "0.01"; // Trigger assertion fail

            Assert.That(TestGrid.Descendants("IntegrityPercent").All(i => double.Parse(i.Value) == expectedResultIntegrity),
                $"Resulting grid should contain blocks set to the specified expected result integrity of {expectedResultIntegrity}");
        }

        [Test]
        public void Handle_OnGridWithIncompleteBlocks_BlocksShouldBeCompleted()
        {
            // Create test data that contains incomplete blocks
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Build(0.5)
                    .Integrity(0.5)
                    .ThatsAll()
                .AndBlockWith()
                    .Build(0.4)
                    .Integrity(0.4)
                    .ThatsAll();

            Assert.That(TestGrid.Descendants("BuildPercent").Count, Is.Not.Zero,
                "Test grid should contain incomplete blocks.");

            CompleteAllBlocksHandler.Handle(ValidCommand);

            Assert.That(TestGrid.Descendants("BuildPercent").Count, Is.Zero,
                "Result grid shouldn't contain incomplete blocks. I.e. no BuildPercent nodes can be present.");
        }

        [Test]
        public void Handle_OnGridWithIncompleteBlocks_IntegrityShouldBeRaisedToFull()
        {
            // Create test data that contains incomplete blocks - without additional damage
            // Since IntegrityPercent=1 is the default value, fully integrity = no integrity present
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Build(0.5)
                    .Integrity(0.5)
                    .ThatsAll()
                .AndBlockWith()
                    .Build(0.4)
                    .Integrity(0.4)
                    .ThatsAll();

            Assert.Multiple(() =>
            {
                Assert.That(TestGrid.Descendants("BuildPercent").Count, Is.Not.Zero,
                    "Test grid should contain blocks with incomplete build.");
                Assert.That(TestGrid.Descendants("IntegrityPercent").Count, Is.Not.Zero,
                    "Test grid should contain blocks with integrity set.");
                Assert.That(TestGrid.Descendants("BuildPercent").Select(i => i.Value), Is.EquivalentTo(TestGrid.Descendants("IntegrityPercent").Select(i => i.Value)),
                    "Test grid should contain blocks with equal BuildPercent and IntegrityPercent values."
                );
            });

            CompleteAllBlocksHandler.Handle(ValidCommand);

            Assert.That(TestGrid.Descendants("IntegrityPercent").Count, Is.Zero,
                "Result grid shouldn't contain blocks with IntegrityPercent nodes.");
        }
    }
}
