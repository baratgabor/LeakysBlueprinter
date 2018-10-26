using LeakysBlueprinter.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Loads definitions from the specified streams, and deserializes them with the specified serializer.
    /// </summary>
    internal class DefinitionsLoader : IDefinitionsProvider
    {
        public MyObjectBuilder_Definitions Definitions { get; private set; }

        public DefinitionsLoader(ISerializer<MyObjectBuilder_Definitions> serializer, params Stream[] definitionStreams)
            => Definitions = LoadDefinitions(serializer, definitionStreams);

        public MyObjectBuilder_Definitions LoadDefinitions(ISerializer<MyObjectBuilder_Definitions> serializer, Stream[] definitionStreams)
        {
            List<MyObjectBuilder_Definitions> fragmentedDefinitions = new List<MyObjectBuilder_Definitions>();

            foreach (var stream in definitionStreams) using (stream)
                    fragmentedDefinitions.Add(serializer.Deserialize(stream));

            try
            {
                return new MyObjectBuilder_Definitions()
                {
                    // TODO: Would be neater not to reference the members in this hard-coded way
                    Blueprints = fragmentedDefinitions.Where(d => d.Blueprints != null).Single().Blueprints,
                    Components = fragmentedDefinitions.Where(d => d.Components != null).Single().Components,
                    CubeBlocks = fragmentedDefinitions.Where(d => d.CubeBlocks != null).Single().CubeBlocks
                };
            }
            catch (Exception ex)
            {
                throw new AppException(ExceptionKind.DefinitionsLoadError_DuplicateOrMissingDataTypes, ex);
            }
        }
    }
}
