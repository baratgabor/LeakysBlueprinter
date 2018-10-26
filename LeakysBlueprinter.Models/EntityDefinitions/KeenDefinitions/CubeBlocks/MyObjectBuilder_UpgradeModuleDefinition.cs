using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    /// <summary>
    /// Upgrade modifier type
    /// </summary>
    public enum MyUpgradeModifierType
    {
        /// <summary>
        /// Multiplies base value of block - to increase value by 50% set <see cref="MyUpgradeModuleInfo.Modifier"/> to 1.5
        /// </summary>
        Multiplicative,
        /// <summary>
        /// Adds to base value of block - to increase value by 50% set <see cref="MyUpgradeModuleInfo.Modifier"/> to 0.5
        /// </summary>
        Additive,
    }

    /// <summary>
    /// Module upgrade information
    /// </summary>
    public struct MyUpgradeModuleInfo
    {
        /// <summary>
        /// Name of upgrade
        /// </summary>
        public string UpgradeType { get; set; }
        /// <summary>
        /// Modifier for upgrade (as decimal - 1 = 100%)
        /// </summary>
        public float Modifier { get; set; }
        /// <summary>
        /// Type of modifier as <see cref="MyUpgradeModifierType"/>
        /// </summary>
        public MyUpgradeModifierType ModifierType { get; set; }
    }

    /// <summary>
    /// Upgrade module base definition
    /// </summary>
    public class MyObjectBuilder_UpgradeModuleDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        /// <summary>
        /// List of upgrades provided by block <see cref="MyUpgradeModuleInfo"/>
        /// </summary>
        public MyUpgradeModuleInfo[] Upgrades;
    }
}
