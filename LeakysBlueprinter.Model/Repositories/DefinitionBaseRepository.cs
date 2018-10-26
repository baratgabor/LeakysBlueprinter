using System.Collections.Generic;
using System.Linq;

namespace LeakysBlueprinter.Model
{
    public class DefinitionBaseRepository<TDefinitionBase> : IDefinitionBaseRepository<TDefinitionBase>
        where TDefinitionBase : MyObjectBuilder_DefinitionBase
    {
        protected IEnumerable<TDefinitionBase> _entities;

        public DefinitionBaseRepository(IEnumerable<TDefinitionBase> entities)
        {
            _entities = entities;
        }

        public TDefinitionBase GetById(string subtypeId)
        {
            return (from e in _entities
                    where e.Id.SubtypeId == subtypeId
                    select e).First();
        }

        public IEnumerable<TDefinitionBase> GetByDisplayNameContains(string value)
        {
            return from e in _entities
                   where e.DisplayName.Contains(value)
                   select e;
        }

        public IEnumerable<TDefinitionBase> GetAll()
        {
            return _entities;
        }
    }
    
}
