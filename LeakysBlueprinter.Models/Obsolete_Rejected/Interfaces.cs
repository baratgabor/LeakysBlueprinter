
using System.IO;

namespace LeakysBlueprinter.Model.Obsolete
{

    public interface IBlockDefinitionsProvider
    {
        Block GetBlock(string blockId);
    }

    public interface IComponentDefinitionsProvider
    {
        Component GetComponent(string componentSubtypeId);
    }

    public interface IIconProvider
    {
        string GetIconPath(ItemId id);
    }

    public interface IFileConfig
    {
        string BlockDefinitionsPath { get; }
        string ComponentDefinitionsPath { get; }
        string ComponentIconsPath { get; }
        string BlockIconsPath { get; }
    }

}
