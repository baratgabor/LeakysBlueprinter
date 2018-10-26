using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model.Obsolete
{
    [DataContract]
    public class Recipe
    {
        [DataMember(Order = 0)]
        public ItemId Id { get; private set; }
        [DataMember(Order = 1)]
        public string DisplayName { get; private set; }
        [DataMember(Order = 2)]
        public string Icon { get; private set; }

        public Ingredient[] Prerequisites { get; private set; }
    }

    [DataContract]
    public class Ingredient
    {
        public float Amount { get; private set; }
        public string TypeId { get; private set; }
        public string SubtypeId { get; private set; }

        public Ingredient(float amount, string typeId, string subtypeId)
        {
            Amount = amount;
            TypeId = typeId;
            SubtypeId = subtypeId;
        }
    }

}
