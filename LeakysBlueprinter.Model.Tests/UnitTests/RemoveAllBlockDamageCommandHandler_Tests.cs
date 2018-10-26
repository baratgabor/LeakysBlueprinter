using LeakysBlueprinter.Model.Commands;
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
    class RemoveAllBlockDamageCommandHandler_Tests
    {
        // Test subject
        RemoveAllBlockDamageCommandHandler CommandHandler;

        Mock<IBlueprintDataContext> MockDataContext;
        XElement TestGrid;

        RemoveAllBlockDamageCommand ValidCommand = new RemoveAllBlockDamageCommand
        {
            GridEntityId = "123456"
        };

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

            CommandHandler = new RemoveAllBlockDamageCommandHandler(MockDataContext.Object);
        }

        [Test]
        public void Handle_OnGridWithOnlyBlockDamage_AllDamageShouldBeRepaired_AndNoOtherChanges()
        {
            // Create test data that contains damaged blocks that are otherwise complete (i.e. only Integrity is set)
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Integrity(0.3)
                    .ThatsAll()
                .AndBlockWith()
                    .Integrity(0.1)
                    .ThatsAll();

            // Expected result grid containing blocks with full integrity (full integrity = Integrity setting not present)
            var ExpectedResultGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .ThatsAll()
                .AndBlockWith()
                    .ThatsAll();

            Assert.Multiple(() =>
            {
                Assert.That(TestGrid.Descendants("IntegrityPercent").Count, Is.Not.Zero,
                    "Test grid should have damaged blocks.");
                Assert.That(TestGrid.Descendants("BuildPercent").Count, Is.Zero,
                    "Test grid should not have incomplete blocks.");
            });

            CommandHandler.Handle(ValidCommand);

            // TestGrid.Descendants("MyObjectBuilder_CubeBlock").First().Add(XElement.Parse("<IntegrityPercent>0.495782</IntegrityPercent>")); // Trigger assert fail

            Assert.Multiple(() =>
            {
                Assert.That(TestGrid.Descendants("IntegrityPercent").Count, Is.Zero,
                    "Resulting grid should not contain block damage, i.e. no IntegrityPercent nodes should be present.");
                Assert.That(XNode.DeepEquals(TestGrid, ExpectedResultGrid),
                    "Resulting grid shouldn't contain any other changes besides the removal of damage.");
            });
        }

        [Test]
        public void Handle_OnGridWithOnlyIncompleteBlocks_NothingShouldChange()
        {
            // Create test data that contains incomplete blocks without additional damage (i.e. build = integrity)
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Build(0.5)
                    .Integrity(0.5)
                    .ThatsAll()
                .AndBlockWith()
                    .Build(0.4)
                    .Integrity(0.4)
                    .ThatsAll();

            var ExpectedResultGrid = TestHelpers.DeepCopy(TestGrid);

            Assert.Multiple(() =>
            {
                Assert.That(TestGrid.Descendants("BuildPercent").Count, Is.Not.Zero,
                    "Test grid should have incomplete blocks.");
                Assert.That(TestGrid.Descendants("IntegrityPercent").Count, Is.EqualTo(TestGrid.Descendants("BuildPercent").Count()),
                    "Test grid should have correctly set incomplete blocks, with IntegrityPercent and BuildPercent both present.");
                Assert.That(TestGrid.Descendants("IntegrityPercent").Select(e => e.Value), Is.EquivalentTo(TestGrid.Descendants("BuildPercent").Select(e => e.Value)),
                    "Test grid should have identical values for IntegrityPercent and BuildPercent, to represent the state of incomplete blocks without additional damage.");
            });

            CommandHandler.Handle(ValidCommand);

            // TestGrid.Descendants("IntegrityPercent").First().Value = "0.0101"; // Trigger assert fail

            Assert.That(XNode.DeepEquals(TestGrid, ExpectedResultGrid), Is.True,
                "Grid should not be changed in any way.");
        }

        [Test]
        public void Handle_OnGridWithIncompleteAndDamagedBlocks_ShouldRepairOnlyUpToIncompletion_AndNoOtherChanges()
        {
            // Create test data that contains incomplete blocks without additional damage (i.e. build = integrity)
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Build(0.5)
                    .Integrity(0.3)
                    .ThatsAll()
                .AndBlockWith()
                    .Build(0.9)
                    .Integrity(0.1)
                    .ThatsAll();

            // Expected result grid containing equal Build and Integrity settings
            var ExpectedResultGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith()
                    .Build(0.5)
                    .Integrity(0.5)
                    .ThatsAll()
                .AndBlockWith()
                    .Build(0.9)
                    .Integrity(0.9)
                    .ThatsAll();

            var IntegrityPercents_Initial = TestGrid.Descendants("IntegrityPercent").Select(e => double.Parse(e.Value)).ToList();
            var BuildPercents_Initial = TestGrid.Descendants("BuildPercent").Select(e => double.Parse(e.Value)).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(TestGrid.Descendants("BuildPercent").Count, Is.Not.Zero,
                    "Test grid should have incomplete blocks.");
                int i = 0;
                Assert.That(IntegrityPercents_Initial.All(integrityPercent => integrityPercent < BuildPercents_Initial[i++]), Is.True,
                    "Test grid should have smaller IntegrityPercent values than BuildPercent values.");
            });

            CommandHandler.Handle(ValidCommand);

            // TestGrid.Descendants("IntegrityPercent").First().Value = "0.969696"; // Trigger assert fail

            var IntegrityPercents_Result = TestGrid.Descendants("IntegrityPercent").Select(e => double.Parse(e.Value));

            Assert.Multiple(() =>
            {
                Assert.That(IntegrityPercents_Result, Is.EquivalentTo(BuildPercents_Initial),
                    "Blocks in result grid should have integrity values set equal to the original corresponding BuildPercent value.");
                Assert.That(XNode.DeepEquals(TestGrid, ExpectedResultGrid), Is.True,
                    "Result grid should not contain any other modifications.");
            });
        }
    }
}
