using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.UI.WPF.EventPayloads
{
    public class SelectFileUriThroughDialog
    {
        public object Sender { get; set; }
        public string FileNameFilter { get; set; }
        public string InitialDirectory { get; set; }

        // Result
        public string FileUriSelected { get; set; }
    }
}
