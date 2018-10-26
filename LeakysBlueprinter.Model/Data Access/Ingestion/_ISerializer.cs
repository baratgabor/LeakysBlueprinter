using System.IO;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Provides abstraction layer for serializer implementations
    /// </summary>
    internal interface ISerializer<T>
    {
        void Serialize(Stream stream, T @object);
        T Deserialize(Stream stream);
    }

    internal interface ISerializerAsync<T>
    {
        Task SerializeAsync(Stream stream, T @object);
        Task<T> DeserializeAsync(Stream stream);
    }

    internal interface ISerializerComposite<T> : ISerializer<T>, ISerializerAsync<T>
    { }
}
