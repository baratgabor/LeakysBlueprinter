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
    class GridCommandHandlerBase_Tests
    {
        // Fake class extending the base class that is the actual test subject
        public class FakeGridCommandHandler : GridCommandHandlerBase<IMyGridCommand>
        {
            public int DoHandleCalls { get; private set; } = 0;
            public XElement TargetGrid { get; private set; }

            public FakeGridCommandHandler(IBlueprintDataContext dataContext) : base(dataContext)
            { }

            protected override void DoHandleOnGrid(IMyGridCommand command, XElement grid)
            {
                DoHandleCalls++;
                TargetGrid = grid;
            }
        }

        Mock<IBlueprintDataContext> MockDataContext;
        FakeGridCommandHandler GridCommandHandler;

        [SetUp]
        public void SetUp()
        {
            MockDataContext = new Mock<IBlueprintDataContext>();
            //Validity check should always return true
            MockDataContext
                .Setup(dc => dc.IsValid())
                .Returns(true);

            MockDataContext
                .Setup(dc => dc.GetGridByEntityId(It.IsAny<string>()))
                .Returns<XElement>(null);

            GridCommandHandler = new FakeGridCommandHandler(MockDataContext.Object);
        }

        [Test]
        public void Handle_OnEmptyGridCommand_ShouldThrow()
        {
            // Empty command, GridEntityId not set
            Mock<IMyGridCommand> MockEmptyGridCommand = new Mock<IMyGridCommand>();

            Assert.Multiple(() =>
            {
                // assert that it throws
                var ex = Assert.Throws<AppException>(
                    () => GridCommandHandler.Handle(MockEmptyGridCommand.Object)
                );

                // assert that exception type is correct
                Assert.That(ex.ExceptionKind, Is.EqualTo(ExceptionKind.GridOperationFailed_TargetGridNotSpecified));
            });
        }

        [Test]
        public void Handle_OnNonEmptyGridCommandButNullGrid_ShouldThrow()
        {
            // Non-Empty command, GridEntityId is set
            Mock<IMyGridCommand> MockNonEmptyGridCommand = new Mock<IMyGridCommand>();
            MockNonEmptyGridCommand.SetupGet(c => c.GridEntityId).Returns("123123");

            Assert.Multiple(() =>
            {
                // assert that it throws
                var ex = Assert.Throws<AppException>(
                    () => GridCommandHandler.Handle(MockNonEmptyGridCommand.Object)
                );

                // assert that exception type is correct
                Assert.That(ex.ExceptionKind, Is.EqualTo(ExceptionKind.Blueprint_GridNotFound));
            });
        }

        [Test]
        public void Handle_OnValidGridAndCommand_DoHandleShouldBeCalledOnce_AndGridShouldBeSet()
        {
            XElement TestGrid = TestHelpers.DataBuilder.BuildCubeGrid().
                AndBlockWith()
                    .ThatsAll().
                AndBlockWith()
                    .ThatsAll();

            // Valid command, GridEntityId is set
            Mock<IMyGridCommand> MockNonEmptyGridCommand = new Mock<IMyGridCommand>();
            MockNonEmptyGridCommand.SetupGet(c => c.GridEntityId).Returns("123123");

            // Return a valid grid
            MockDataContext
                .Setup(dc => dc.GetGridByEntityId(It.IsAny<string>()))
                .Returns(TestGrid);

            GridCommandHandler.Handle(MockNonEmptyGridCommand.Object);

            Assert.Multiple(() =>
            {
                Assert.That(GridCommandHandler.DoHandleCalls, Is.EqualTo(1),
                    "DoHandleOnGrid should be called once.");
                Assert.That(GridCommandHandler.TargetGrid, Is.EqualTo(TestGrid),
                    "The grid passed to DoHandleOnGrid should be set correctly.");
            });
        }
    }
}
