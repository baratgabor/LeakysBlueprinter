using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.Obsolete
{
    /// <summary>
    /// Size attributes used for both components and blocks.
    /// </summary>
    [DataContract(Namespace = "")]
    public class ItemSize
    {
        [DataMember(Order = 0)]
        public float X { get; private set; }
        [DataMember(Order = 1)]
        public float Y { get; private set; }
        [DataMember(Order = 2)]
        public float Z { get; private set; }

        public ItemSize(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
