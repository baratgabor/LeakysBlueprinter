using LeakysBlueprinter.Model.IO;
using System.IO;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Stateless static class for loading resources
    /// </summary>
    internal static class ResourceLoader
    {
        public static T Load<T>(ISerializer<T> serializer, IReadingStream stream)
        {
            using (Stream s = stream.Open())
            {
                return serializer.Deserialize(s);
            }
        }
    }
}
