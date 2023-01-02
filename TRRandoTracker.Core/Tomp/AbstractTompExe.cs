using System.Collections.Generic;
using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core
{
    public abstract class AbstractTompExe
    {
        internal virtual string[] Hashes { get; }
        internal abstract byte[] Identifier { get; }
        internal abstract int TitleAddress { get; }
        internal abstract int LevelAddress { get; }
        internal virtual int LevelCompleteAddress => 0;
        internal abstract int CreditsFlag { get; }
        public abstract string Name { get; }
        internal abstract string ScriptPath { get; }
        internal virtual ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
        {
            return new DefaultTracker(this, process, levels);
        }
    }
}