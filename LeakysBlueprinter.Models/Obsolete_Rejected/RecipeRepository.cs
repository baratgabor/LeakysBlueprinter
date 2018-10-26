using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.Obsolete
{
    public class RecipeRepository : DefinitionBaseRepository<MyObjectBuilder_BlueprintDefinition>
    {
        private const string TypeId_Ingot = "Ingot";
        private const string TypeId_Component = "Component";


        public RecipeRepository(IEnumerable<MyObjectBuilder_BlueprintDefinition> entities) : base(entities)
        {
        }

        public IEnumerable<MyObjectBuilder_BlueprintDefinition> GetByResultId(string resultSubtypeId)
        {
            return (from e in _entities
                    where e.Result.SubtypeId == resultSubtypeId
                    select e);
        }

        public IEnumerable<MyObjectBuilder_BlueprintDefinition> GetByPrerequisiteId(string prerequisiteSubtypeId)
        {
            return (from e in _entities
                    where e.Prerequisites.Any(p => p.SubtypeId == prerequisiteSubtypeId)
                    select e);
        }

        public IEnumerable<MyObjectBuilder_BlueprintDefinition> GetAllResultTypeIngot()
        {
            return (from e in _entities
                    where e.Result.TypeId == TypeId_Ingot
                    select e);
        }

        public IEnumerable<MyObjectBuilder_BlueprintDefinition> GetAllResultTypeComponent()
        {
            return (from e in _entities
                    where e.Result.TypeId == TypeId_Component
                    select e);
        }

    }
}
