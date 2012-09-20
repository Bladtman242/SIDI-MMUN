using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmark {
    public static class QueueExtensions {
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
