using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakysBlueprinter.Utilities
{
    /// <summary>
    /// Adds simple Queue features to ObservableCollection. NOTE that this Dequeue() is not efficient; O(n) complexity.
    /// </summary>
    public class ObservableQueue<T> : ObservableCollection<T>
    {
        public ObservableQueue<T> Enqueue(T item)
        {
            Add(item);
            return this;
        }

        public ObservableQueue<T> Dequeue()
        {
            RemoveAt(0);
            return this;
        }

        public ObservableQueue<T> Dequeue(out T item)
        {
            item = base[0];
            RemoveAt(0);
            return this;
        }

        public ObservableQueue<T> AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (collection is ICollection<T> countable && countable.Count == 0)
                goto Return;

            foreach (var item in collection)
                Add(item);

            Return:
            return this;
        }
    }
}
