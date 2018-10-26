using System.IO;

namespace LeakysBlueprinter.Model.IO
{
    public interface IReadingStream
    {
        Stream Open();
    }

    public interface IWritingStream
    {
        Stream Open();
    }

    public interface ICreatingStream
    {
        Stream Open();
    }
}
