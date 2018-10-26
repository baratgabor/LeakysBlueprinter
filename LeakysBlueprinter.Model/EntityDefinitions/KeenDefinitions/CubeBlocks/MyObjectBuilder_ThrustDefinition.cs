using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyFuelConverterInfo
    {
        public SerializableDefinitionId FuelId = new SerializableDefinitionId();

        public float Efficiency = 1f;
    }

    public class MyObjectBuilder_ThrustDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        //static readonly Vector4 DefaultThrustColor = new Vector4(Color.CornflowerBlue.ToVector3() * 0.7f, 0.75f);
        static readonly Vector4 DefaultThrustColor = new Vector4(1,1,1,1);

        public string ResourceSinkGroup;

        public MyFuelConverterInfo FuelConverter = new MyFuelConverterInfo();

        public float SlowdownFactor = 10;

        public string ThrusterType = "Ion";

        public float ForceMagnitude;

        public float MaxPowerConsumption;

        public float MinPowerConsumption;

        public float FlameDamageLengthScale = 0.6f;

        public float FlameLengthScale = 1.15f;

        public Vector4 FlameFullColor = DefaultThrustColor;

        public Vector4 FlameIdleColor = DefaultThrustColor;

        public string FlamePointMaterial = "EngineThrustMiddle";

        public string FlameLengthMaterial = "EngineThrustMiddle";

        public string FlameGlareMaterial = "GlareSsThrustSmall";

        public float FlameVisibilityDistance = 200;

        public float FlameGlareSize = 0.391f;

        public float FlameGlareQuerySize = 1;

        public float FlameDamage = 0.5f;

        public float MinPlanetaryInfluence = 0f;

        public float MaxPlanetaryInfluence = 1f;

        public float EffectivenessAtMaxInfluence = 1f;

        public float EffectivenessAtMinInfluence = 1f;

        public bool NeedsAtmosphereForInfluence = false;

        public float ConsumptionFactorPerG = 0f;

        public bool PropellerUsesPropellerSystem = false;

        public string PropellerSubpartEntityName = "Propeller";

        public float PropellerRoundsPerSecondOnFullSpeed = 1.9f;

        public float PropellerRoundsPerSecondOnIdleSpeed = 0.3f;

        public float PropellerAccelerationTime = 3f;

        public float PropellerDecelerationTime = 6f;

        public float PropellerMaxVisibleDistance = 20f;
    }

}
