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
    class GridQueryHandlerBase_Tests
    {
        // Fake class extending the base class that is the actual test subject
        public class FakeGridQueryHandler : GridQueryHandlerBase<IGridQuery<bool>,bool>
        {
            public int DoHandleCalls { get; private set; } = 0;
            public XElement TargetGrid { get; private set; }

            public FakeGridQueryHandler(IBlueprintDataContext dataContext) : base(dataContext)
            { }

            protected override bool DoHandleOnGrid(IGridQuery<bool> query, XElement grid)
            {
                DoHandleCalls++;
                TargetGrid = grid;
                return true;
            }
        }

        Mock<IBlueprintDataContext> MockDataContext;
        FakeGridQueryHandler GridQueryHandler;

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

            GridQueryHandler = new FakeGridQueryHandler(MockDataContext.Object);
        }

        [Test]
        public void Handle_OnEmptyGridQuery_ShouldThrow()
        {
            // Empty query, GridEntityId not set
            Mock<IGridQuery<bool>> MockEmptyGridQuery = new Mock<IGridQuery<bool>>();

            Assert.Multiple(() =>
            {
                    // assert that it throws
                    var ex = Assert.Throws<AppException>(
                    () => GridQueryHandler.Handle(MockEmptyGridQuery.Object)
                );

                    // assert that exception type is correct
                    Assert.That(ex.ExceptionKind, Is.EqualTo(ExceptionKind.GridOperationFailed_TargetGridNotSpecified));
            });
        }

        [Test]
        public void Handle_OnNonEmptyGridQueryButNullGrid_ShouldThrow()
        {
            // Non-Empty query, GridEntityId is set
            Mock<IGridQuery<bool>> MockNonEmptyGridQuery = new Mock<IGridQuery<bool>>();
            MockNonEmptyGridQuery.SetupGet(c => c.GridEntityId).Returns("123123");

            Assert.Multiple(() =>
            {
                    // assert that it throws
                    var ex = Assert.Throws<AppException>(
                    () => GridQueryHandler.Handle(MockNonEmptyGridQuery.Object)
                );

                    // assert that exception type is correct
                    Assert.That(ex.ExceptionKind, Is.EqualTo(ExceptionKind.Blueprint_GridNotFound));
            });
        }

        [Test]
        public void Handle_OnValidGridAndQuery_DoHandleShouldBeCalledOnce_AndGridShouldBeSet()
        {
            XElement TestGrid = TestHelpers.DataBuilder.BuildCubeGrid().
                AndBlockWith()
                    .ThatsAll().
                AndBlockWith()
                    .ThatsAll();

            // Valid query, GridEntityId is set
            Mock<IGridQuery<bool>> MockNonEmptyGridQuery = new Mock<IGridQuery<bool>>();
            MockNonEmptyGridQuery.SetupGet(c => c.GridEntityId).Returns("123123");

            // Return a valid grid
            MockDataContext
                .Setup(dc => dc.GetGridByEntityId(It.IsAny<string>()))
                .Returns(TestGrid);

            GridQueryHandler.Handle(MockNonEmptyGridQuery.Object);

            Assert.Multiple(() =>
            {
                Assert.That(GridQueryHandler.DoHandleCalls, Is.EqualTo(1),
                    "DoHandleOnGrid should be called once.");
                Assert.That(GridQueryHandler.TargetGrid, Is.EqualTo(TestGrid),
                    "The grid passed to DoHandleOnGrid should be set correctly.");
            });
        }
    }
}