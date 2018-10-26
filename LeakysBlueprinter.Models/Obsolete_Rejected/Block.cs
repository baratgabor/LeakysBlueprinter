using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.Obsolete
{
    public enum CubeSize
    {
        Large,
        Small
    }

    public enum BlockTopology
    {
        Cube,
        TriangleMesh
    }

    public class Block
    {
        public ItemId Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Icon { get; private set; }
        public CubeSize CubeSize { get; private set; }
        public BlockTopology BlockTopology { get; private set; }
        public BlockSize Size { get; private set; }
        public BlockModelOffset ModelOffset { get; private set; }
        public string Model { get; private set; }
        public List<(string Subtype, int Count)> Components { get; private set; }

        public Block(ItemId id, string displayName, string icon, CubeSize cubeSize, BlockTopology blockTopology, BlockSize size, BlockModelOffset modelOffset, string model, List<(string Subtype, int Count)> components)
        {
            Id = id;
            DisplayName = displayName;
            Icon = icon;
            CubeSize = cubeSize;
            BlockTopology = blockTopology;
            Size = size;
            ModelOffset = modelOffset;
            Model = model;
            Components = components;
        }
    }


    public struct BlockSize
    {
        public readonly byte X;
        public readonly byte Y;
        public readonly byte Z;

        public BlockSize(byte x, byte y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct BlockModelOffset
    {
        public readonly byte X;
        public readonly byte Y;
        public readonly byte Z;

        public BlockModelOffset(byte x, byte y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }




}
