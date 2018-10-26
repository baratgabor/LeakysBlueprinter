namespace LeakysBlueprinter.Model
{
    internal interface IDefinitionsRepository
    {
        MyObjectBuilder_Definitions Root { get; }

        IDefinitionBaseRepository<MyObjectBuilder_ComponentDefinition> Components { get; }
        IDefinitionBaseRepository<MyObjectBuilder_BlueprintDefinition> Recipes { get; }
        IDefinitionBaseRepository<MyObjectBuilder_CubeBlockDefinition> CubeBlocks { get; }
    }
}
