using System.Collections.Generic;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Specifies a generic factory method for the creation of <see cref="IDefinitionBaseRepository"> instances.
    /// </summary>
    internal interface IDefinitionBaseRepositoryFactory
    {
        IDefinitionBaseRepository<TDefinitionBase> Create<TDefinitionBase>(IEnumerable<TDefinitionBase> entities)
            where TDefinitionBase : MyObjectBuilder_DefinitionBase;
    }
}
