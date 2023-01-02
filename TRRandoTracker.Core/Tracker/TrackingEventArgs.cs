using System;
using System.Collections.Generic;
using TRGE.Core;

namespace TRRandoTracker.Core.Tracker
{
    public class TrackingEventArgs : EventArgs
    {
        public AbstractTompExe Exe { get; internal set; }
        public TrackingStatus Status { get; internal set; }
        public IReadOnlyList<AbstractTRScriptedLevel> Levels { get; internal set; }
        public int CurrentSequence { get; internal set; }
        public AbstractTRScriptedLevel CurrentLevel { get; internal set; }

        public override string ToString()
        {
            return Status.ToString() + ", " + CurrentSequence + ", " + (CurrentLevel == null ? string.Empty : CurrentLevel.Name);
        }
    }
}