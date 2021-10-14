using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chinese_checkers.Helpers
{
    /// <summary>
    /// Static class with a static property used to track the status of debugmode
    /// </summary>
    static class DebugHelper
    {
        public static bool DebugEnabled { get; set; } = false;
    }
}
