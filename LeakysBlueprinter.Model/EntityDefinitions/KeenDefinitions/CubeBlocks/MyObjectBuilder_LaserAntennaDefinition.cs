using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_LaserAntennaDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public string ResourceSinkGroup;
        public float PowerInputIdle = 0.001f;
        public float PowerInputTurning = 0.01f;
        public float PowerInputLasing = 2f;
        public float RotationRate = (float)Math.PI / 20000.0f;
        public float MaxRange = 40000;
        public bool RequireLineOfSight = true;
        public int MinElevationDegrees = -180;
        public int MaxElevationDegrees = 180;
        public int MinAzimuthDegrees = -180;
        public int MaxAzimuthDegrees = 180;
    }
}
