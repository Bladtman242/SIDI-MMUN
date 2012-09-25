using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    public static class QueueExtensions {
        /// <summary>
        /// Remove a single element from a queue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="element">The element to be removed</param>
        /// <returns>The queue without the removed element</returns>
        public static Queue<T> Remove<T>(this Queue<T> queue, T element) where T : class {
            Queue<T> tmp = new Queue<T>();
            while (queue.Count > 0) {
                T tmpElement = queue.Dequeue();
                if (tmpElement != element) {
                    tmp.Enqueue(tmpElement);
                }
            }
            return tmp;
        }
    }
}
