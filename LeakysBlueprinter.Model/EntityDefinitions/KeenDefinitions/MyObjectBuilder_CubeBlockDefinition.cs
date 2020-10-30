using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace LeakysBlueprinter.Model
{
    public struct VoxelPlacementSettings
    {
        public enum VoxelPlacementMode
        {
            None,
            InVoxel,
            OutsideVoxel,
            Both,
            Volumetric
        }

        public VoxelPlacementMode PlacementMode;
        /// <summary>
        /// Maximum amount in % of block being inside voxel (where 1 - 100% to 0 - 0%)
        /// </summary>
        public float MaxAllowed;
        /// <summary>
        /// Minimum amount in % of block being inside voxel (where 1 - 100% to 0 - 0%)
        /// </summary>
        public float MinAllowed;
    }

    public enum MyCubeSize : byte
    {
        Large = 0,
        Small = 1,
    }

    public enum MyBlockTopology : byte
    {
        Cube = 0,
        TriangleMesh = 1,
    }

    public struct BoneInfo
    {
        public SerializableVector3I BonePosition;

        public SerializableVector3UByte BoneOffset;
    }

    public enum MyCubeTopology
    {
        Box,
        Slope,
        Corner,
        InvCorner,
        StandaloneBox,
        //This should have been called RoundedBlock or something similar, since it means it was derived from a full block,
        //but because of modding we cannot change this now.
        RoundedSlope,
        RoundSlope,
        RoundCorner,
        RoundInvCorner,
        RotatedSlope,
        RotatedCorner,

        //Slopes
        //We need separate definition for each block because of edges and physics
        Slope2Base,
        Slope2Tip,

        Corner2Base,
        Corner2Tip,

        InvCorner2Base,
        InvCorner2Tip,

        //Additions for new half height armor blocks (probably)
        HalfBox,
        HalfSlopeBox,
    }

    public enum MyFractureMaterial
    {
        Stone,
        Wood,
    }

    public enum MyPhysicsOption
    {
        None,
        Box,
        Convex,
    }

    public enum BlockSideEnum
    {
        Right = 0,
        Top = 1,
        Front = 2,
        Left = 3,
        Bottom = 4,
        Back = 5,
    }

    public enum MySymmetryAxisEnum
    {
        None,
        X,
        Y,
        Z,
        XHalfY,
        YHalfY,
        ZHalfY,
        XHalfX,
        YHalfX,
        ZHalfX,
        XHalfZ,
        YHalfZ,
        ZHalfZ,
        MinusHalfX,
        MinusHalfY,
        MinusHalfZ,
        HalfX,
        HalfY,
        HalfZ,
        XMinusHalfZ,
        YMinusHalfZ,
        ZMinusHalfZ,
        XMinusHalfX,
        YMinusHalfX,
        ZMinusHalfX,
        ZThenOffsetX,
        YThenOffsetX,
        OffsetX,
        ZThenOffsetXOdd,
        YThenOffsetXOdd
    }

    public enum MyAutorotateMode
    {
        /// <summary>
        /// When block has mount points only on one side, it will autorotate so that side is touching the surface.
        /// Otherwise, full range of rotations is allowed.
        /// </summary>
        OneDirection,

        /// <summary>
        /// When block has mount points only on two sides and those sides are opposite each other (eg. Top and Bottom),
        /// it will autorotate so that one of these sides is touching the surface. Otherwise, full range of rotations
        /// is allowed.
        /// </summary>
        OppositeDirections,

        /// <summary>
        /// When block has mountpoint on at least one side, it will autorotate so that this side is touching the surface.
        /// Otherwise, full range of rotations is allowed.
        /// </summary>
        FirstDirection,
    }

    [Flags]
    public enum MyBlockDirection
    {
        Horizontal = 1 << 0,
        Vertical = 1 << 1,
        Both = Horizontal | Vertical,
    }

    [Flags]
    public enum MyBlockRotation
    {
        None = 0,
        Horizontal = 1 << 0,
        Vertical = 1 << 1,
        Both = Horizontal | Vertical,
    }

    public struct VoxelPlacementOverride
    {
        public VoxelPlacementSettings StaticMode;
        public VoxelPlacementSettings DynamicMode;
    }


    [XmlInclude(typeof(MyObjectBuilder_DebugSphere1Definition))]
    [XmlInclude(typeof(MyObjectBuilder_DebugSphere2Definition))]
    [XmlInclude(typeof(MyObjectBuilder_DebugSphere3Definition))]
    [XmlInclude(typeof(MyObjectBuilder_KitchenDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_PlanterDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_VendingMachineDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_JukeboxDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_LCDPanelsBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_StoreBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_SafeZoneBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ContractBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_HydrogenEngineDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_WindTurbineDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ShipConnectorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_SurvivalKitDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_DecoyDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ParachuteDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_AirtightSlideDoorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_AirtightHangarDoorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_LaserAntennaDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_MergeBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ShipWelderDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ShipGrinderDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_SpaceBallDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_VirtualMassDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ConveyorSorterDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_PoweredCargoContainerDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_OxygenFarmDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_SolarPanelDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_TimerBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ButtonPanelDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_MotorAdvancedStatorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_MotorSuspensionDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_MotorStatorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ExtendedPistonBaseDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_PistonBaseDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ReactorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_GyroDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_CameraBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ThrustDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_CargoContainerDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_UpgradeModuleDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_AirVentDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_RemoteControlDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_GasTankDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_TextPanelDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_SoundBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_SensorBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ShipDrillDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_CryoChamberDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_CockpitDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_JumpDriveDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_GravityGeneratorSphereDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_GravityGeneratorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_MedicalRoomDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_OreDetectorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_AssemblerDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_OxygenGeneratorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_RefineryDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ProjectorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_LandingGearDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_WarheadDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ReflectorBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_RadioAntennaDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_LightingBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_LargeTurretBaseDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_DoorDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_BeaconDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_ProgrammableBlockDefinition))]
    [XmlInclude(typeof(MyObjectBuilder_BatteryBlockDefinition))]
    public class MyObjectBuilder_CubeBlockDefinition : MyObjectBuilder_PhysicalModelDefinition
    {

        #region Properties Definitions

        
        public class MountPoint
        {
            [XmlIgnore]
            public SerializableVector2 Start;

            [XmlIgnore]
            public SerializableVector2 End;

            [XmlAttribute]
            public BlockSideEnum Side;

            [XmlAttribute]
            public float StartX
            {
                get { return Start.X; }
                set { Start.X = value; }
            }
            [XmlAttribute]
            public float StartY
            {
                get { return Start.Y; }
                set { Start.Y = value; }
            }

            [XmlAttribute]
            public float EndX
            {
                get { return End.X; }
                set { End.X = value; }
            }
            [XmlAttribute]
            public float EndY
            {
                get { return End.Y; }
                set { End.Y = value; }
            }

            [XmlAttribute, DefaultValue(0)]
            public byte ExclusionMask = 0;

            [XmlAttribute, DefaultValue(0)]
            public byte PropertiesMask = 0;

            [XmlAttribute, DefaultValue(true)]
            public bool Enabled = true;

            [XmlAttribute, DefaultValue(false)]
            public bool Default = false;

        }

        
        public class CubeBlockComponent
        {
            [XmlAttribute]        
            public string Subtype;

            [XmlAttribute] 
            public UInt16 Count;
          
            public SerializableDefinitionId DeconstructId;
        }

        
        public class CriticalPart
        {
            [XmlAttribute]         
            public string Subtype;

            [XmlAttribute]
            public int Index = 0;
        }

        
        public class Variant
        {
            /// <summary>
            /// Color is used to get Color(4 bytes) as well as
            /// MyStringId value for localization.
            /// </summary>
            [XmlAttribute]
            public string Color;

            [XmlAttribute]
            public string Suffix;
        }

        
        public class PatternDefinition
        {
            public MyCubeTopology CubeTopology;
            
            public Side[] Sides;
            
            public bool ShowEdges;
        }

        
        public class Side
        {
            [XmlIgnore]
            public SerializableVector2I PatternSize;

            [XmlAttribute]      
            public string Model;

            [XmlAttribute]
            public int PatternWidth
            {
                get { return PatternSize.X; }
                set { PatternSize.X = value; }
            }

            [XmlAttribute]
            public int PatternHeight
            {
                get { return PatternSize.Y; }
                set { PatternSize.Y = value; }
            }
        }

        
        public class BuildProgressModel
        {
            [XmlAttribute]
            public float BuildPercentUpperBound;

            [XmlAttribute]
            public string File;

            [XmlAttribute]
            [DefaultValue(false)]
            public bool RandomOrientation;
            
            [XmlArray("MountPointOverrides")]
            [XmlArrayItem("MountPoint"), DefaultValue(null)]
            public MountPoint[] MountPoints;

            [XmlAttribute]
            [DefaultValue(true)]
            public bool Visible = true;
        }

        
        public class MySubBlockDefinition
        {
            [XmlAttribute]
            public string SubBlock;

            public SerializableDefinitionId Id;
        }

        
        public class EntityComponentDefinition
        {
            [XmlAttribute]
            public string ComponentType;

            [XmlAttribute]
            public string BuilderType;
        }

        
        public class CubeBlockEffectBase
        {
            [XmlAttribute]
            public string Name = "";

            [XmlAttribute]
            public float ParameterMin = float.MinValue;

            [XmlAttribute]
            public float ParameterMax = float.MaxValue;

            [XmlArrayItem("ParticleEffect")]
            public CubeBlockEffect[] ParticleEffects;
        }

        
        public class CubeBlockEffect
        {
            [XmlAttribute]
            public string Name = "";

            [XmlAttribute]
            public string Origin = "";

            [XmlAttribute]
            public float Delay = 0f;

            [XmlAttribute]        
            public float Duration = 0f;

            [XmlAttribute]
            public bool Loop = false;

            [XmlAttribute]
            public float SpawnTimeMin = 0f;

            [XmlAttribute]
            public float SpawnTimeMax = 0f;
        }

        #endregion

        public VoxelPlacementOverride? VoxelPlacement = null;

        [DefaultValue(false)]
        public bool SilenceableByShipSoundSystem;
        
        public MyCubeSize CubeSize;
        
        public MyBlockTopology BlockTopology;
        
        public SerializableVector3I Size;
        
        public SerializableVector3 ModelOffset;
        
        public PatternDefinition CubeDefinition;

        [XmlArrayItem("Component")]
        public CubeBlockComponent[] Components;

        [XmlArrayItem("Effect")]
        public CubeBlockEffectBase[] Effects;
        
        public CriticalPart CriticalComponent;
        
        public MountPoint[] MountPoints;
        
        public Variant[] Variants;

        [XmlArrayItem("Component")]
        public EntityComponentDefinition[] EntityComponents;

        [DefaultValue(MyPhysicsOption.Box)]
        public MyPhysicsOption PhysicsOption = MyPhysicsOption.Box;

        [XmlArrayItem("Model")]
        [DefaultValue(null)]
        public List<BuildProgressModel> BuildProgressModels = null;
        
        public string BlockPairName;
        
        public SerializableVector3I? Center;
        public bool ShouldSerializeCenter() { return Center.HasValue; }

        [DefaultValue(MySymmetryAxisEnum.None)]
        public MySymmetryAxisEnum MirroringX = MySymmetryAxisEnum.None;

        [DefaultValue(MySymmetryAxisEnum.None)]
        public MySymmetryAxisEnum MirroringY = MySymmetryAxisEnum.None;

        [DefaultValue(MySymmetryAxisEnum.None)]
        public MySymmetryAxisEnum MirroringZ = MySymmetryAxisEnum.None;

        [DefaultValue(1.0f)]
        public float DeformationRatio = 1.0f;
        
        public string EdgeType;

        [DefaultValue(10.0f)]
        public float BuildTimeSeconds = 10.0f;

        [DefaultValue(1.0f)]
        public float DisassembleRatio = 1.0f;
        
        public MyAutorotateMode AutorotateMode = MyAutorotateMode.OneDirection;
      
        public string MirroringBlock;
        
        public bool UseModelIntersection = false;
        
        public string PrimarySound;
     
        public string ActionSound;

        [DefaultValue(null)]
        public string BuildType = null;

        [DefaultValue(null)]
        public string BuildMaterial = null;

        [XmlArrayItem("Template")]
        [DefaultValue(null)]
        public string[] CompoundTemplates = null;

        [DefaultValue(true)]
        public bool CompoundEnabled = true;

        [XmlArrayItem("Definition")]
        [DefaultValue(null)]
        public MySubBlockDefinition[] SubBlockDefinitions = null;

        [DefaultValue(null)]
        public string MultiBlock = null;

        [DefaultValue(null)]
        public string NavigationDefinition = null;

        [DefaultValue(true)]
        public bool GuiVisible = true;

        [XmlArrayItem("BlockVariant")]
        [DefaultValue(null)]
        public SerializableDefinitionId[] BlockVariants = null;

        // Forward direction - can be horizontal and horizontal+vertical (vertical only not supported)
        [DefaultValue(MyBlockDirection.Both)]
        public MyBlockDirection Direction = MyBlockDirection.Both;

        // Allowed rotation
        [DefaultValue(MyBlockRotation.Both)]
        public MyBlockRotation Rotation = MyBlockRotation.Both;

        [XmlArrayItem("GeneratedBlock")]
        [DefaultValue(null)]
        public SerializableDefinitionId[] GeneratedBlocks;

        [DefaultValue(null)]
        public string GeneratedBlockType;

        // Defines if the block is mirrored version of some other block (mirrored block is usually used as block variant)
        [DefaultValue(false)]
        public bool Mirrored;

        [DefaultValue(0)]
        public int DamageEffectId;

        [DefaultValue("")]
        public string DestroyEffect;

        [DefaultValue("")]
        public string DestroySound;

        // Defines if the block is deformed by a skeleton by default (round blocks)
        [DefaultValue(null)]
        public List<BoneInfo> Skeleton;

        // Defines if the block can be randomly rotated when line/plane building is applied to it.
        [DefaultValue(false)]
        public bool RandomRotation;

        // Temporary flag that tells the oxygen system to treat this block as a full block
        [DefaultValue(false)]
        public bool IsAirTight;

        [DefaultValue(1)]
        public int Points;

        [DefaultValue(0)]
        public int MaxIntegrity = 0;

        [DefaultValue(1)]
        public float BuildProgressToPlaceGeneratedBlocks = 1;

        [DefaultValue(null)]
        public string DamagedSound = null;

        [DefaultValue(true)]
        public bool CreateFracturedPieces = true;
    }
}
