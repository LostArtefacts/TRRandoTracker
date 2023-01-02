using System.Collections.Generic;
using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core.Tomp.Exes
{
    public class Tomb1Main : AbstractTompExe
    {
        public override string Name => "Tomb1Main";

        internal override byte[] Identifier => new byte[] { 0,0,0 };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0x805FAA; //2.12
        internal override int LevelCompleteAddress => 0xC57449;

        internal override int CreditsFlag => 0x11A1FC;

        internal override string ScriptPath => @"cfg\Tomb1Main_gameflow.json5";

        internal override ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
        {
            return new Tomb1MainTracker(this, process , levels);
        }
    }
}