using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model
{
    // TODO: One-to-one interface/class mapping with BlueprintDataContext - segregate this interface into finer-grained role-based interfaces
    internal interface IBlueprintDataContext
    {
        XElement Context { get; }

        bool IsValid();

        XElement GetGridByEntityId(string entityId);
        XElement GetGridByDisplayName(string displayName);

        XElement GetBlockByCoordinates(int x, int y, int z, XElement grid = null);
        IEnumerable<XElement> GetBlocksByType(Type blockType, XElement grid = null);
        IEnumerable<XElement> GetBlocksBySubtypeName(string subtypeName, XElement grid = null);
        IEnumerable<XElement> GetBlocksWithProperty(string propertyNodeName, XElement grid = null);
    }
}
