using LeakysBlueprinter.Model.Exceptions;
using LeakysBlueprinter.Model.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    public sealed class ApplicationService
    {
        private IDefinitionsRepository _definitionsRepository;

        // TODO: Expose app status info, log data, etc.

        internal ApplicationService(IDefinitionsRepository definitionsRepository)
        {
            _definitionsRepository = definitionsRepository;
        }

        public BlueprintService CreateBlueprintService(string blueprintFilePath)
        {
            return new BlueprintService(
                _definitionsRepository,
                new LoadableResource<XDocument>(
                    new Temp(),
                    new FileStreamProvider(blueprintFilePath)));
        }
    }

    // TODO: Remove temporary implementation, refactor serializer consumption
    class Temp : ISerializer<XDocument>
    {
        public XDocument Deserialize(Stream stream)
        {
            return XDocument.Load(stream);
        }

        public void Serialize(Stream stream, XDocument @object)
        {
            throw new NotImplementedException();
        }
    }
}
