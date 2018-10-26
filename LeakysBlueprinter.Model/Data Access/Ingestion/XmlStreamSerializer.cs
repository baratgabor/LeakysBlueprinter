using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{

    /// <summary>
    /// Wraps an XML serializer, providing both sync and async interface implementations
    /// </summary>
    internal class XmlStreamSerializer<T> : ISerializer<T>, ISerializerAsync<T>
    {
        protected XmlSerializer _serializer;

        public XmlStreamSerializer()
            => _serializer = new XmlSerializer(typeof(T));

        public void Serialize(Stream stream, T @object)
            => _serializer.Serialize(stream, @object);

        public T Deserialize(Stream stream)
            => (T)_serializer.Deserialize(stream);

        public async Task SerializeAsync(Stream stream, T @object)
            => await Task.Run(() => Serialize(stream, @object)).ConfigureAwait(false);

        public async Task<T> DeserializeAsync(Stream stream)
            => await Task.Run(() => Deserialize(stream)).ConfigureAwait(false);
    }

}
