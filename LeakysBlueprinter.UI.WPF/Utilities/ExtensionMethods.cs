using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.UI.WPF.Utilities
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Tells if integer is a valid index for the collection.
        /// </summary>
        /// <param name="collection">The collection to check in</param>
        /// <param name="index">The index to check</param>
        /// <returns></returns>
        public static bool IndexValid<T>(this IEnumerable<T> collection, int index)
        {
            if (index < 0 || index > collection.Count())
                return false;

            return true;
        }
    }
}
