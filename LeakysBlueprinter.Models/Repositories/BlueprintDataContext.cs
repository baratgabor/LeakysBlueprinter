using LeakysBlueprinter.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    internal class BlueprintDataContext : IBlueprintDataContext
    {
        public XElement Context { get; }

        public BlueprintDataContext(XElement context)
        {
            Context = context;
        }

        public XElement GetBlockByCoordinates(int x, int y, int z, XElement grid = null)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        public IEnumerable<XElement> GetBlocksByType(Type blockType, XElement grid = null)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        public IEnumerable<XElement> GetBlocksBySubtypeName(string subtypeName, XElement grid = null)
            => GetBlockList(grid).Where(b => b.Element("SubtypeName")?.Value == subtypeName);

        // TODO: Consider if "property" is a misnomer here
        /// <summary>
        /// Returns all elements that contain a child node with the specified node name.
        /// If <see cref="grid"/> is specified, the scope of search will include only blocks from the given grid.
        /// </summary>
        /// <param name="propertyNodeName">The node name to search for.</param>
        /// <param name="grid">The grid to search on.</param>
        /// <returns>The list of matching blocks.</returns>
        public IEnumerable<XElement> GetBlocksWithProperty(string propertyNodeName, XElement grid = null)
            => GetBlockList(grid).Where(e => e.Element(propertyNodeName) != null);

        /// <summary>
        /// Returns the grid that has the provided display name.
        /// Returns null if no grid found with the given display name.
        /// <para>
        /// If multiple grids found with the same display name, returns the first. To return multiple, use <see cref="GetGridsByDisplayName(string)"/>.
        /// </para>
        /// </summary>
        /// <param name="displayName">DisplayName of grid.</param>
        public XElement GetGridByDisplayName(string displayName)
            => GetGridsByDisplayName(displayName).FirstOrDefault();

        /// <summary>
        /// Returns all grids that has the provided display name.
        /// Returns empty collection if no grid found with the given display name.
        /// </summary>
        /// <param name="displayName">DisplayName of grid.</param>
        public IEnumerable<XElement> GetGridsByDisplayName(string displayName)
            => (from c in Context.Descendants("CubeGrids").First().Elements("CubeGrid")
                where c.Element("DisplayName").Value == displayName
                select c);

        /// <summary>
        /// Returns the grid corresponding to the provided Entity ID.
        /// Returns null if no grid found with the given Entity ID.
        /// </summary>
        /// <param name="entityId">Entity ID of grid.</param>
        /// <exception cref="InvalidOperationException">Thrown when more than one grid is found with the given Entity ID.</exception>
        public XElement GetGridByEntityId(string entityId)
        {
            var res = (from c in Context.Descendants("CubeGrids").First().Elements("CubeGrid")
                       where c.Element("EntityId").Value == entityId
                       select c).ToList();
            if (res.Count > 1)
                throw new AppException(ExceptionKind.Blueprint_GridEntityIdNotUnique);
            return res.SingleOrDefault();
        }

        // TODO: Implement proper structure validity test
        public bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Returns the list of all blocks, either from a single grid, or from all grids, 
        /// depending on the provided parameter.
        /// </summary>
        /// <param name="grid">Optional parameter that, if provided, limits search to a single grid.</param>
        /// <returns>The list of blocks.</returns>
        protected IEnumerable<XElement> GetBlockList(XElement grid = null)
        {
            if (grid != null)
                // All cubeblocks of the given grid
                return grid.Element("CubeBlocks").Elements();
            else
                // All cubeblocks across all grids
                return Context.Descendants("CubeGrids").First().Elements("CubeGrid").Elements("CubeBlocks").Elements();
        }
    }

}
