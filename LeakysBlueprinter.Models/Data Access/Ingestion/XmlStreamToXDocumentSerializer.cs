using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Well, it's not really "serializing", but still
    /// </summary>
    internal class XmlStreamToXDocumentSerializer : ISerializer<XDocument>
    {
        public XDocument Deserialize(Stream stream)
            => XDocument.Load(stream);

        public void Serialize(Stream stream, XDocument @object)
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(stream))
            {
                @object.WriteTo(xw);
            }
        }
    }

}
