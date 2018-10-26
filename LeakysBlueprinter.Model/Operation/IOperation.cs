using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Model
{
    public interface IOperation
    {
        /// <summary>
        /// Unique name without spaces
        /// </summary>
        string IdentifierName { get; }

        /// <summary>
        /// Name in a GUI-compatible format
        /// </summary>
        string FriendlyName { get; }

        // TODO: Idea: define complexity to help consumer decide if Task.Run is needed
        // Technically it would be a bit leaky, but still, consumers end up making assumptions to this end all the time
        // ExecutionComplexity ExecutionComplexity { get; }
    }
}
