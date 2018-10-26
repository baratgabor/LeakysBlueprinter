using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_LightingBlockDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public SerializableBounds LightRadius = new SerializableBounds(2, 10, 2.8f);

        public SerializableBounds LightReflectorRadius = new SerializableBounds(2, 120, 120.0f);

        public SerializableBounds LightFalloff = new SerializableBounds(1, 3, 1.5f);

        public SerializableBounds LightIntensity = new SerializableBounds(0.5f, 5, 2);

        public string ResourceSinkGroup;

        public float RequiredPowerInput = 0.001f;

        public string LightGlare = "GlareLsLight";

        public SerializableBounds LightBlinkIntervalSeconds = new SerializableBounds(0.0f, 30.0f, 0);

        public SerializableBounds LightBlinkLenght = new SerializableBounds(0.0f, 100.0f, 10.0f);

        public SerializableBounds LightBlinkOffset = new SerializableBounds(0.0f, 100.0f, 0);

        public bool HasPhysics = false;
    }
}
