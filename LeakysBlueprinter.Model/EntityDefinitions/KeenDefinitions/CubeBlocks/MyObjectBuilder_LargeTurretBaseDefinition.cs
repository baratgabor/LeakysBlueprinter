namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_LargeTurretBaseDefinition : MyObjectBuilder_WeaponBlockDefinition
    {
        public string OverlayTexture;
        public bool AiEnabled = true;
        public int MinElevationDegrees = -180;
        public int MaxElevationDegrees = 180;
        public int MinAzimuthDegrees = -180;
        public int MaxAzimuthDegrees = 180;
        public bool IdleRotation = true;
        public float MaxRangeMeters = 800.0f;
        public float RotationSpeed = 0.005f;
        public float ElevationSpeed = 0.005f;
    }
}
