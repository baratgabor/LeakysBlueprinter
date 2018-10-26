using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model.Obsolete
{
    /// <summary>
    /// Id used for both components and blocks.
    /// </summary>
    [DataContract(Namespace ="")]
    public class ItemId
    {
        [DataMember(Order = 0)]
        public string TypeId { get; private set; }
        [DataMember(Order = 1)]
        public string SubtypeId { get; private set; }

        public ItemId(string typeID, string subtypeID)
        {
            TypeId = typeID;
            SubtypeId = subtypeID;
        }
    }
}
