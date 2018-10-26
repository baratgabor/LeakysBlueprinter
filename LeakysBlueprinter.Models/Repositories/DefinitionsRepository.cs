namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Exposes the root definitions object and a sub-repository for each used entity type.
    /// </summary>
    internal class DefinitionsRepository : IDefinitionsRepository
    {
        public MyObjectBuilder_Definitions Root { get; private set; }
        public IDefinitionBaseRepository<MyObjectBuilder_ComponentDefinition> Components { get; private set; }
        public IDefinitionBaseRepository<MyObjectBuilder_BlueprintDefinition> Recipes { get; private set; }
        public IDefinitionBaseRepository<MyObjectBuilder_CubeBlockDefinition> CubeBlocks { get; private set; }

        public DefinitionsRepository(
            IDefinitionsProvider definitionsProvider,
            IDefinitionBaseRepositoryFactory definitionBaseRepositoryFactory)
        {
            Root = definitionsProvider.Definitions;
            Components = definitionBaseRepositoryFactory.Create(Root.Components);
            Recipes = definitionBaseRepositoryFactory.Create(Root.Blueprints);
            CubeBlocks = definitionBaseRepositoryFactory.Create(Root.CubeBlocks);
        }
    }
}
