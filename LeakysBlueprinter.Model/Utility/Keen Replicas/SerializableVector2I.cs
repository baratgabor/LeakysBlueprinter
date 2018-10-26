using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct SerializableVector2I
    {
        public int X;
        public int Y;

        public bool ShouldSerializeX() { return false; }
        public bool ShouldSerializeY() { return false; }

        public SerializableVector2I(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        [XmlAttribute]
        public int x { get { return X; } set { X = value; } }

        [XmlAttribute]
        public int y { get { return Y; } set { Y = value; } }

        public static implicit operator Vector2I(SerializableVector2I v)
        {
            return new Vector2I(v.X, v.Y);
        }

        public static implicit operator SerializableVector2I(Vector2I v)
        {
            return new SerializableVector2I(v.X, v.Y);
        }
    }
}
