using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_PhysicalObject : MyObjectBuilder_Base
    {
        [DefaultValue(MyItemFlags.None)]
        public MyItemFlags Flags = MyItemFlags.None;

        /// <summary>
        /// This is used for GUI to show the amount of health points (durability) of the weapons and tools. This is updated through Durability entity component if entity exists..
        /// </summary>
        [DefaultValue(null)]
        public float? DurabilityHP = null;
        public bool ShouldSerializeDurabilityHP()
        {
            return DurabilityHP.HasValue;
        }

        public virtual bool CanStack(MyObjectBuilder_PhysicalObject a)
        {
            if (a == null) return false;
            return CanStack(a.TypeId, a.SubtypeId, a.Flags);
        }

        public virtual bool CanStack(string typeId, string subtypeId, MyItemFlags flags)
        {
            if (flags == Flags &&
                typeId == TypeId &&
                subtypeId == SubtypeId)
            {
                return true;
            }
            return false;
        }

        public MyObjectBuilder_PhysicalObject() : this(MyItemFlags.None) { }

        public MyObjectBuilder_PhysicalObject(MyItemFlags flags)
        {
            Flags = flags;
        }

        //public virtual MyDefinitionId GetObjectId()
        //{
        //    return this.GetId();
        //}
    }

    [Flags]
    public enum MyItemFlags : byte
    {
        None = 0,
        Damaged = 1 << 1,
    }
}
