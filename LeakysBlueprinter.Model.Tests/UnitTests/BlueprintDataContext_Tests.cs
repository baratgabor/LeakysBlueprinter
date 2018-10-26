using LeakysBlueprinter.Model;
using LeakysBlueprinter.Model.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LeakysBlueprinter.Model.Tests.UnitTests
{
    [TestFixture]
    class BlueprintDataContext_Tests
    {
        // TODO: Check if repetitive code can be safely replaced with parameterized test cases or helper methods

        [SetUp]
        public void SetUp()
        {
            // TODO: Identify invariant parts (if any) and move them here
        }

        [Test]
        public void GetGridByEntityId_OnNotFound_ShouldReturnNull()
        {
            string TestEntityId = "testid";

            // Test data not containing the tested entity ID
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .EntityId("SomethingSomethingInTheMonthOfMay")
                    .ThatsAll()
                .AndGridWith()
                    .EntityId("ArbitraryId")
                    .ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("EntityId").Where(e => e.Value == TestEntityId).Count, Is.Zero,
                "Test blueprint shouldn't contain the tested EntityId.");

            var res = DataContext.GetGridByEntityId(TestEntityId);

            Assert.That(res, Is.Null,
                "Return value should be null when looking up non-existing entity ID.");
        }

        [Test]
        public void GetGridByEntityId_OnMultipleFound_ShouldThrow()
        {
            string TestEntityId = "testid";

            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .EntityId(TestEntityId)
                    .ThatsAll()
                .AndGridWith()
                    .EntityId(TestEntityId)
                    .ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            Assert.That(TestBlueprint.Descendants("EntityId").Where(e => e.Value == TestEntityId).Count, Is.GreaterThan(1),
                "Test blueprint should contain more than one instance of the tested EntityId.");

            Assert.Multiple(() =>
            {
                // assert that it throws internal exception
                var ex = Assert.Throws<AppException>(
                    () => DataContext.GetGridByEntityId(TestEntityId),
                    $"Tested method should throw {nameof(AppException)}.");

                // assert proper internal exception kind
                Assert.That(ex.ExceptionKind, Is.EqualTo(ExceptionKind.Blueprint_GridEntityIdNotUnique),
                    $"Threw method should be of kind {nameof(ExceptionKind.Blueprint_GridEntityIdNotUnique)}.");
            });
        }

        [Test]
        public void GetGridByEntityId_OnSingleFound_ShouldReturnGrid()
        {
            string TestEntityId = "123456";

            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .EntityId(TestEntityId)
                    .ExportThis(out XElement ExpectedResultGrid) // Note out ExpectedResultGrid
                    .ThatsAll()
                .AndGridWith()
                    .EntityId("arbitraryid")
                    .ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("EntityId").Where(e => e.Value == TestEntityId).Count, Is.EqualTo(1),
                "Test blueprint should contain a single instance of the tested EntityId.");

            var res = DataContext.GetGridByEntityId(TestEntityId);

            Assert.That(res, Is.EqualTo(ExpectedResultGrid));
        }

        [Test]
        public void GetGridByDisplayName_OnNotFound_ShouldReturnNull()
        {
            string TestDisplayName = "TestDisplayName";

            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .DisplayName("arbitraryNameOne")
                    .ThatsAll()
                .AndGridWith()
                    .DisplayName("arbitraryNameTwo")
                    .ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("DisplayName").Where(e => e.Value == TestDisplayName).Count, Is.Zero,
                "Test blueprint should not contain the tested DisplayName.");

            var res = DataContext.GetGridByDisplayName(TestDisplayName);

            Assert.That(res, Is.Null);
        }

        [Test]
        public void GetGridByDisplayName_OnMultipleFound_ShouldReturnFirst()
        {
            string TestDisplayName = "TestDisplayName";

            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .DisplayName(TestDisplayName)
                    .ExportThis(out XElement ExpectedResultGrid) // Note the declaration/assignment
                    .ThatsAll()
                .AndGridWith()
                    .DisplayName(TestDisplayName)
                    .ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("DisplayName").Where(e => e.Value == TestDisplayName).Count, Is.GreaterThan(1),
                "Test blueprint should contain multiple instances of the tested DisplayName.");

            var res = DataContext.GetGridByDisplayName(TestDisplayName);

            Assert.That(res, Is.EqualTo(ExpectedResultGrid));
        }

        [Test]
        public void GetGridsByDisplayName_OnNoneFound_ShouldReturnEmptyCollection()
        {
            string TestDisplayName = "TestDisplayName";

            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .DisplayName("arbitraryNameOne")
                    .ThatsAll()
                .AndGridWith()
                    .DisplayName("arbitraryNameTwo")
                    .ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("DisplayName").Where(e => e.Value == TestDisplayName).Count, Is.Zero,
                "Test blueprint should not contain the tested DisplayName.");

            var res = DataContext.GetGridsByDisplayName(TestDisplayName);

            Assert.That(res, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetGridsByDisplayName_OnMultipleFound_ShouldReturnAll_AndOnlyTheMatches()
        {
            string TestDisplayName = "TestDisplayName";

            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .DisplayName(TestDisplayName)
                    .ExportThis(out var Res1) // Note out Res1
                    .ThatsAll()
                .AndGridWith()
                    .DisplayName(TestDisplayName)
                    .ExportThis(out var Res2) // Note out Res2
                    .ThatsAll()
                .AndGridWith()
                    .DisplayName("DifferentName")
                    .ThatsAll();

            List<XElement> ExpectedResultList = new List<XElement>() { Res1, Res2 };

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("DisplayName").Where(e => e.Value == TestDisplayName).Count, Is.EqualTo(2),
                "Test blueprint should contain two instances of the tested DisplayName.");

            var res = DataContext.GetGridsByDisplayName(TestDisplayName);

            Assert.That(res, Is.EquivalentTo(ExpectedResultList),
                "Tested method should return two grids with matching display names, and nothing else.");
        }


        [Test]
        public void GetBlocksWithProperty_OnAllGridsNoneFound_ShouldReturnEmptyCollection()
        {
            string TestPropertyName = "IntegrityPercent";

            // Test data without any matching element
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().ThatsAll()
                    .ThatsAll()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("IntegrityPercent").Count, Is.Zero,
                $"Test blueprint shouldn't contain any {TestPropertyName} nodes.");

            var res = DataContext.GetBlocksWithProperty(TestPropertyName, null);

            Assert.That(res, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetBlocksWithProperty_OnOneGridNoneFound_ShouldReturnEmptyCollection()
        {
            string TestPropertyName = "IntegrityPercent";

            // Test data that doesn't contain any matching element on target grid, but contains on another grid
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().ThatsAll()
                    .ExportThis(out var TargetGrid) // Note out TargetGrid
                    .ThatsAll()
                .AndGridWith()
                    .AndBlockWith().Integrity(0.5).ThatsAll() // Note Integrity set
                    .AndBlockWith().ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.Multiple(() =>
            {
                Assert.That(TargetGrid.Descendants("IntegrityPercent").Count, Is.Zero,
                    $"Test blueprints's target grid shouldn't contain any {TestPropertyName} nodes.");

                Assert.That(TestBlueprint.Descendants("IntegrityPercent").Count, Is.Not.Zero,
                    $"Test blueprint should contain at least one {TestPropertyName} node on a non-target grid.");
            });

            var res = DataContext.GetBlocksWithProperty(TestPropertyName, TargetGrid);

            Assert.That(res, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetBlocksWithProperty_OnAllGridsFound_ShouldReturnMatchesFromMultipleGrids()
        {
            string TestPropertyName = "IntegrityPercent";

            // Test data that contains matching elements on multiple grids
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().Integrity(0.3).ExportThis(out var res1).ThatsAll() // Note Integrity && out res1
                    .ThatsAll()
                .AndGridWith()
                    .AndBlockWith().Integrity(0.6).ExportThis(out var res2).ThatsAll() // Note Integrity && out res2
                    .AndBlockWith().ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);
            var ExpectedResultList = new List<XElement>() { res1, res2 };

            // Affirm preconditions
            Assert.Multiple(() =>
            {
                Assert.That(TestBlueprint.Descendants("CubeGrid").Count, Is.AtLeast(2),
                    "Test blueprint should contain multiple grids");

                Assert.That(TestBlueprint.Descendants("IntegrityPercent").SelectMany(i => i.Ancestors("CubeGrid")).Distinct().Count, Is.AtLeast(2),
                    $"Test blueprint should contain {TestPropertyName} nodes in multiple grids");
            });

            var res = DataContext.GetBlocksWithProperty(TestPropertyName, null);

            Assert.That(res, Is.EquivalentTo(ExpectedResultList));
        }

        [Test]
        public void GetBlocksBySubtypeName_OnAllGridNotFound_ShouldReturnEmptyCollection()
        {
            string TestSubtypeName = "TestSubtypeName";

            // Test data without any matching element
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().ThatsAll()
                    .ThatsAll()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.That(TestBlueprint.Descendants("SubtypeName").Where(e => e.Value == TestSubtypeName).Count, Is.Zero,
                $"Test blueprint shouldn't contain any nodes with SubtypeName of {TestSubtypeName}.");

            var res = DataContext.GetBlocksBySubtypeName(TestSubtypeName, null);

            Assert.That(res, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetBlocksBySubtypeName_OnOneGridNotFound_ShouldReturnEmptyCollection()
        {
            string TestSubtypeName = "TestSubtypeName";

            // Test data that doesn't contain any matching element on target grid, but contains on another grid
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().ThatsAll()
                    .ExportThis(out var TargetGrid) // Note out TargetGrid
                    .ThatsAll()
                .AndGridWith()
                    .AndBlockWith().SubtypeName(TestSubtypeName).ThatsAll() // Note SubtypeName set
                    .AndBlockWith().ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);

            // Affirm preconditions
            Assert.Multiple(() =>
            {
                Assert.That(TargetGrid.Descendants("SubtypeName").Where(e => e.Value == TestSubtypeName).Count, Is.Zero,
                    $"Test blueprints's target grid shouldn't contain any nodes with SubtypeName of {TestSubtypeName}.");

                Assert.That(TestBlueprint.Descendants("SubtypeName").Where(e => e.Value == TestSubtypeName).Count, Is.Not.Zero,
                    $"Test blueprint should contain at least one node with SubtypeName of {TestSubtypeName} on a non-target grid.");
            });

            var res = DataContext.GetBlocksBySubtypeName(TestSubtypeName, TargetGrid);

            Assert.That(res, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetBlocksBySubtypeName_OnAllGridsFound_ShouldReturnMatchesFromMultipleGrids()
        {
            string TestSubtypeName = "TestSubtypeName";

            // Test data that contains matching elements on multiple grids
            XElement TestBlueprint = TestHelpers.DataBuilder.BuildBlueprint()
                .AndGridWith()
                    .AndBlockWith().ThatsAll()
                    .AndBlockWith().SubtypeName(TestSubtypeName).ExportThis(out var res1).ThatsAll() // Note SubtypeName set & out res1
                    .ThatsAll()
                .AndGridWith()
                    .AndBlockWith().SubtypeName(TestSubtypeName).ExportThis(out var res2).ThatsAll() // Note SubtypeName set & out res2
                    .AndBlockWith().ThatsAll();

            BlueprintDataContext DataContext = new BlueprintDataContext(TestBlueprint);
            var ExpectedResultList = new List<XElement>() { res1, res2 };

            Console.WriteLine(TestBlueprint);

            // Affirm preconditions
            Assert.Multiple(() =>
            {
                Assert.That(TestBlueprint.Descendants("CubeGrid").Count, Is.AtLeast(2),
                    "Test blueprint should contain multiple grids");

                Assert.That(TestBlueprint.Descendants("SubtypeName").Where(e => e.Value == TestSubtypeName).SelectMany(i => i.Ancestors("CubeGrid")).Distinct().Count, Is.AtLeast(2),
                    $"Test blueprint should contain nodes with SubtypeName of {TestSubtypeName} in multiple grids");
            });

            var res = DataContext.GetBlocksBySubtypeName(TestSubtypeName, null);

            Assert.That(res, Is.EquivalentTo(ExpectedResultList));
        }


    }
}
