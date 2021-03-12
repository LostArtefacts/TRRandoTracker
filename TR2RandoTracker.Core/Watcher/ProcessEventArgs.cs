using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TR2RandoTracker.Core.Watcher
{
    public class ProcessEventArgs : EventArgs
    {
        public Process Process { get; set; }
    }
}