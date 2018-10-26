using System.Collections.Generic;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Thin factory for the concrete type <see cref="DefinitionBaseRepository"/>. No logic inside.
    /// </summary>
    internal class DefinitionsBaseRepoFactory : IDefinitionBaseRepositoryFactory
    {
        public IDefinitionBaseRepository<TDefinitionBase> Create<TDefinitionBase>(IEnumerable<TDefinitionBase> entities)
            where TDefinitionBase : MyObjectBuilder_DefinitionBase
                => new DefinitionBaseRepository<TDefinitionBase>(entities);
    }
}
