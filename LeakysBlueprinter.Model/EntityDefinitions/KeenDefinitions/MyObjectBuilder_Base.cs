using System.ComponentModel;

namespace LeakysBlueprinter.Model
{
    public abstract class MyObjectBuilder_Base
    {
        [DefaultValue(null)]
        public string SubtypeName
        {
            get; set;
        }

        public string TypeId;
        public string SubtypeId;
    }
}
