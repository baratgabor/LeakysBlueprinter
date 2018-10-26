using System.Collections.Generic;
using System.Linq;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Translating decorator that translates all useful entities inside a pre-populated definitions instance.
    /// All dependencies are transient and go out of scope after construction; only the resulting definitions instance is kept.
    /// </summary>
    internal class DefinitionsTranslator : IDefinitionsProvider
    {
        public MyObjectBuilder_Definitions Definitions { get; private set; }

        public DefinitionsTranslator(IDefinitionsProvider definitionsProvider, IRepository<string, string> resxRepository)
            => Definitions = TranslateAll(definitionsProvider.Definitions, resxRepository);

        protected MyObjectBuilder_Definitions TranslateAll(MyObjectBuilder_Definitions definitions, IRepository<string, string> repository)
        {
            // TODO: Would be neater not to reference the members in this hard-coded way
            DoOne(definitions.Blueprints);
            DoOne(definitions.Components);
            DoOne(definitions.CubeBlocks);
            return definitions;

            void DoOne(IEnumerable<MyObjectBuilder_DefinitionBase> list)
                => list.All(definition => { definition.DisplayName = repository.GetById(definition.DisplayName); return true; });
        }
    }
}
