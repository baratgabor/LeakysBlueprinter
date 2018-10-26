using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public class MyObjectBuilder_WarheadDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        public float ExplosionRadius = 0.0f;

        public float WarheadExplosionDamage = 15000f;
    }
}
