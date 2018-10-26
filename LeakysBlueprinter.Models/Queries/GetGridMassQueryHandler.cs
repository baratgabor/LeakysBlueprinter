using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Queries
{
    /// <summary>
    /// Calculates the mass of a full grid, by adding up all blocks' all components.
    /// </summary>
    internal class GetGridMassQueryHandler : GridQueryHandlerBase<GetGridMassQuery, float>
    {
        protected IDefinitionsRepository _definitions;

        public GetGridMassQueryHandler(IDefinitionsRepository definitions, IBlueprintDataContext dataContext) : base(dataContext)
        {
            _definitions = definitions;
        }

        /// <summary>
        /// Calculates grid mass. Grid mass is the sum of all blocks' mass on the grid, and block mass is the sum of all of their components' mass.
        /// </summary>
        protected override float DoHandleOnGrid(GetGridMassQuery query, XElement grid)
        {
            var cubeBlocks = grid.Element("CubeBlocks").Elements("MyObjectBuilder_CubeBlock");

            float gridMass = 0;
            foreach(var cubeBlock in cubeBlocks)
            {
                string blockId = cubeBlock.Element("SubtypeName").Value;

                var blockComponents = _definitions.CubeBlocks.GetById(blockId).Components;

                float blockMass = 0;
                // Iterate all components of current block, look up the given component's mass, and add that mass into a running total of block mass
                foreach (var component in blockComponents)
                    blockMass += _definitions.Components.GetById(component.Subtype).Mass * component.Count;

                // Running total of grid mass, increased by current block's mass
                gridMass += blockMass;
            }

            return gridMass;
        }
    }
}
