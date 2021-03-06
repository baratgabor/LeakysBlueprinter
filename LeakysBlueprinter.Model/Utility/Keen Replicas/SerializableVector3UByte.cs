﻿using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct SerializableVector3UByte
    {
        public byte X;
        public byte Y;
        public byte Z;

        public bool ShouldSerializeX() { return false; }
        public bool ShouldSerializeY() { return false; }
        public bool ShouldSerializeZ() { return false; }

        public SerializableVector3UByte(byte x, byte y, byte z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        [XmlAttribute]
        public byte x { get { return X; } set { X = value; } }

        [XmlAttribute]
        public byte y { get { return Y; } set { Y = value; } }

        [XmlAttribute]
        public byte z { get { return Z; } set { Z = value; } }

        //public static implicit operator Vector3UByte(SerializableVector3UByte v)
        //{
        //    return new Vector3UByte(v.X, v.Y, v.Z);
        //}

        //public static implicit operator SerializableVector3UByte(Vector3UByte v)
        //{
        //    return new SerializableVector3UByte(v.X, v.Y, v.Z);
        //}
    }
}
