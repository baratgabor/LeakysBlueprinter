namespace LeakysBlueprinter.Model.Queries
{
    /// <summary>
    /// Calculates the total mass of a blueprint.
    /// </summary>
    internal class GetTotalMassQueryHandler : QueryHandlerBase<GetTotalMassQuery, float>
    {
        public GetTotalMassQueryHandler(IDefinitionsRepository definitions, IBlueprintDataContext dataContext) : base(definitions, dataContext)
        {
        }

        protected override float DoHandle(GetTotalMassQuery query)
        {
            float totalMass = 0;
            foreach (var block in _dataContext.GetAllBlocks())
            {
                var blockId = block.Element("SubtypeName").Value;
                var blockComponents = _definitions.CubeBlocks.GetById(blockId).Components;

                float blockMass = 0;

                foreach (var component in blockComponents)
                {
                    blockMass += _definitions.Components.GetById(component.Subtype).Mass * component.Count;
                }

                totalMass += blockMass;
            }

            return totalMass;
        }
    }
}
