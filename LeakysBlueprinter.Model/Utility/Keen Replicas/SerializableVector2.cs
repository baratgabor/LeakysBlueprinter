using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct SerializableVector2
    {
        public float X;
        public float Y;

        public bool ShouldSerializeX() { return false; }
        public bool ShouldSerializeY() { return false; }

        public SerializableVector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        [XmlAttribute]
        public float x { get { return X; } set { X = value; } }

        [XmlAttribute]
        public float y { get { return Y; } set { Y = value; } }

        //public static implicit operator Vector2(SerializableVector2 v)
        //{
        //    return new Vector2(v.X, v.Y);
        //}

        public static implicit operator SerializableVector2(Vector2 v)
        {
            return new SerializableVector2(v.X, v.Y);
        }
    }
}
