using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.IO
{

    /// <summary>
    /// Provides access to a stream, based on an initially set file path.
    /// Cast to interface to express intent, and call Open().
    /// Dispose() the returned stream after finished.
    /// </summary>
    public class FileStreamProvider : IReadingStream, IWritingStream, ICreatingStream
    {
        protected string _filePath;

        public FileStreamProvider(string filePath)
            => _filePath = filePath;

        Stream IReadingStream.Open()
            => File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        Stream IWritingStream.Open()
            => File.Open(_filePath, FileMode.Open, FileAccess.Write, FileShare.Read);

        Stream ICreatingStream.Open()
            => File.Open(_filePath, FileMode.Create, FileAccess.Write, FileShare.None);




    }

}
