using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmarksystem {
    /// <summary>
    /// Enumeration of different job states.
    /// </summary>
    enum State {
        New, Submitted, Cancelled, Running, Terminated, Failed
    }
}
