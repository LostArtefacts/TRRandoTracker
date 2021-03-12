using System;
using System.Collections.Generic;
using TRGE.Core;

namespace TR2RandoTracker.Core.Tracker
{
    public class TrackingEventArgs : EventArgs
    {
        public AbstractTomp2Exe Exe { get; internal set; }
        public TrackingStatus Status { get; internal set; }
        public IReadOnlyList<TR23ScriptedLevel> Levels { get; internal set; }
        public int CurrentSequence { get; internal set; }
        public TR23ScriptedLevel CurrentLevel { get; internal set; }

        public override string ToString()
        {
            return Status.ToString() + ", " + CurrentSequence + ", " + (CurrentLevel == null ? string.Empty : CurrentLevel.Name);
        }
    }
}