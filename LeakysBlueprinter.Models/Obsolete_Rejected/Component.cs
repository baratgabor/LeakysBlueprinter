using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LeakysBlueprinter.Model.Obsolete
{
    [DataContract(Namespace ="")]
    public class Component
    {
        [DataMember(Order = 0)]
        public ItemId Id { get; private set; }
        [DataMember(Order = 1)]
        public string DisplayName { get; private set; }
        [DataMember(Order = 2)]
        public string Icon { get; private set; }
        [DataMember(Order = 3)]
        public ItemSize Size { get; private set; }
        [DataMember(Order = 4)]
        public float Mass { get; private set; }
        [DataMember(Order = 5)]
        public float? Volume { get; private set; }
        [DataMember(Order = 6)]
        public string Model { get; private set; }
        [DataMember(Order = 7)]
        public string PhysicalMaterial { get; private set; }
        [DataMember(Order = 8)]
        public int MaxIntegrity { get; private set; }
        [DataMember(Order = 9)]
        public float DropProbability { get; private set; }
        [DataMember(Order = 10)]
        public int Health { get; private set; }

        public Component(
            ItemId id,
            string displayName,
            string icon,
            ItemSize size,
            int mass,
            int volume,
            string model,
            string physicalMaterial,
            int maxIntegrity,
            float dropProbability,
            int health)
        {
            Id = id;
            DisplayName = displayName;
            Icon = icon;
            Size = size;
            Mass = mass;
            Volume = volume;
            Model = model;
            PhysicalMaterial = physicalMaterial;
            MaxIntegrity = maxIntegrity;
            DropProbability = dropProbability;
            Health = health;
        }
    }
}
