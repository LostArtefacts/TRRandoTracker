using System.Diagnostics;

namespace TRRandoTracker.Core.Watcher;

public class ProcessEventArgs : EventArgs
{
    public Process Process { get; set; }
}