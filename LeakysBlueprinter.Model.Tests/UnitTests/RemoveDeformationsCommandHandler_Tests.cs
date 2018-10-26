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
    class RemoveDeformationsCommandHandler_Tests
    {
        // Test subject
        RemoveDeformationsCommandHandler RemoveDeformationsCommandHandler;

        RemoveDeformationsCommand ValidCommand = new RemoveDeformationsCommand()
        {
            GridEntityId = "123456"
        };

        Mock<IBlueprintDataContext> MockDataContext;
        XElement TestGrid;

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

            RemoveDeformationsCommandHandler = new RemoveDeformationsCommandHandler(MockDataContext.Object);
        }

        [Test]
        public void Handle_OnGridWithSkeletonNode_ShouldRemoveSkeletonNode_AndNoOtherChanges()
        {
            // Create test grid with some simple blocks and a Skeleton node
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith().ThatsAll()
                .AndBlockWith().ThatsAll()
                .DummySkeleton()
                .Build();
            
            // Affirm test grid
            Assert.That(TestGrid.Descendants("Skeleton").Count(), Is.EqualTo(1), "Test grid should contain a Skeleton node.");

            // Create expected grid, which is equivalent to test grid but without Skeleton node
            var ExpectedGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith().ThatsAll()
                .AndBlockWith().ThatsAll()
                .Build();

            // Affirm expected grid
            Assert.That(ExpectedGrid.Descendants("Skeleton").Count(), Is.Zero, "Comparison grid shouldn't contain Skeleton node.");

            RemoveDeformationsCommandHandler.Handle(ValidCommand);

            //TestGrid.Descendants("EntityId").First().Value = "changed value"; // Trigger assert fail

            Assert.Multiple(() =>
            {
                // Assert that Skeleton node is indeed deleted
                Assert.That(TestGrid.Descendants("Skeleton").Count(), Is.Zero, "Grid shouldn't contain Skeleton node.");

                // Assert that full grid equals to the same grid without Skeleton node
                // I.e. that nothing else changed besides the removal of the Skeleton node
                Assert.That(XNode.DeepEquals(TestGrid, ExpectedGrid), "Nothing else should be changed on the grid besides the removal of the Skeleton node.");
            });
        }

        [Test]
        public void HandleMethod_ShouldThrowWhenSkeletonNodeNotFound()
        {
            // Test grid with a few blocks and no Skeleton node
            TestGrid = TestHelpers.DataBuilder.BuildCubeGrid()
                .AndBlockWith().ThatsAll()
                .AndBlockWith().ThatsAll()
                .Build();

            // Affirm test grid
            Assert.That(TestGrid.Descendants("Skeleton").Count(), Is.Zero, "Test grid should not contain Skeleton node.");

            Assert.Multiple(() =>
            {
                // assert that it throws
                var ex = Assert.Throws<AppException>(
                    () => RemoveDeformationsCommandHandler.Handle(ValidCommand)
                );

                // assert that exception type is correct
                Assert.That(ex.ExceptionKind, Is.EqualTo(ExceptionKind.Blueprint_NoDeformationToDelete));
            });
        }     
    }
}
