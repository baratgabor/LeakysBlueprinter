using System.Collections.Generic;

namespace LeakysBlueprinter.Model
{
    internal interface IDefinitionBaseRepository<TDefinitionBase> : IRepository<TDefinitionBase, string>
        where TDefinitionBase : MyObjectBuilder_DefinitionBase
    {
        new TDefinitionBase GetById(string subtypeId); 
        IEnumerable<TDefinitionBase> GetByDisplayNameContains(string value);
        IEnumerable<TDefinitionBase> GetAll();
    }
}
