using System.ComponentModel;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_PhysicalItemDefinition : MyObjectBuilder_DefinitionBase
    {
        public Vector3 Size; // in meters

        public float Mass; // in Kg

        public string Model = @"Models\Components\Sphere.mwm";

        [XmlArrayItem("Model")]
        public string[] Models = null;

        [DefaultValue(null)]
        public string IconSymbol = null;
        public bool ShouldSerializeIconSymbol() { return IconSymbol != null; }

        [DefaultValue(null)]
        public float? Volume = null; // in liters

        [DefaultValue(null)]
        public float? ModelVolume = null; // in liters

        public string PhysicalMaterial;

        public string VoxelMaterial;

        [DefaultValue(true)]
        public bool CanSpawnFromScreen = true;

        // Adding these members to allow chaning the default orientation of the model on spawn
        public bool RotateOnSpawnX = false;

        public bool RotateOnSpawnY = false;

        public bool RotateOnSpawnZ = false;

        public int Health = 100;

        [DefaultValue(null)]
        public SerializableDefinitionId? DestroyedPieceId = null;

        public int DestroyedPieces = 0;

        [DefaultValue(null)]
        public string ExtraInventoryTooltipLine = null;

        //public MyFixedPoint MaxStackAmount = MyFixedPoint.MaxValue;
    }

}
