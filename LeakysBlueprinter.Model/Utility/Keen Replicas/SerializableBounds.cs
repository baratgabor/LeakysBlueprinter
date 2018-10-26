using System.Linq;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct SerializableBounds
    {
        [XmlAttribute]
        public float Min;

        [XmlAttribute]
        public float Max;

        [XmlAttribute]
        public float Default;

        public SerializableBounds(float min, float max, float def)
        {
            Min = min;
            Max = max;
            Default = def;
        }

        //public static implicit operator MyBounds(SerializableBounds v)
        //{
        //    return new MyBounds(v.Min, v.Max, v.Default);
        //}

        //public static implicit operator SerializableBounds(MyBounds v)
        //{
        //    return new SerializableBounds(v.Min, v.Max, v.Default);
        //}

    }
}
