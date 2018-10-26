using LeakysBlueprinter.Model.IO;
using System.IO;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Represents a resource that is ready to load, absolving consumers of the need to depend on file paths, streams, serializers
    /// </summary>
    /// <typeparam name="T">The type of the resource</typeparam>
    internal class LoadableResource<T> : ILoadableResource<T>
    {
        protected ISerializer<T> _serializer;
        protected IReadingStream _stream;

        public LoadableResource(ISerializer<T> serializer, IReadingStream stream)
        {
            _serializer = serializer;
            _stream = stream;
        }

        public T Load()
        {
            using (Stream s = _stream.Open())
            {
                return _serializer.Deserialize(s);
            }
        }
    }

}
