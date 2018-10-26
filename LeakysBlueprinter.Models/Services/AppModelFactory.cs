using System.IO;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Thin factory for application core instantiation / graph composition
    /// </summary>
    public static class AppModelFactory
    {
        /// <summary>
        /// Creates a new <see cref="ApplicationService"/> instance.
        /// </summary>
        public static ApplicationService Create(Stream resxStream, params Stream[] definitionStreams)
            => new ApplicationService(
                new DefinitionsRepository(
                    new DefinitionsTranslator(
                        new DefinitionsLoader(
                            new XmlStreamSerializer<MyObjectBuilder_Definitions>(),
                            definitionStreams
                        ),
                        new ResxRepository(resxStream)
                    ),
                    new DefinitionsBaseRepoFactory()
                )
            );
    }
}
