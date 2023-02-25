using System;
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

        private int _levelAddress, _completeAddress;
        internal override int LevelAddress => _levelAddress;
        internal override int LevelCompleteAddress => _completeAddress;

        internal override int CreditsFlag => 0x11A1FC;

        internal override string ScriptPath => @"cfg\Tomb1Main_gameflow.json5";

        internal override ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
        {
            Version version = CalculateProductVersion(process.MainModule.FileName);
            if (version != null && version >= new Version(2, 13))
            {
                _levelAddress = 0x808F8A;
                _completeAddress = 0xC5A509;
            }
            else
            {
                _levelAddress = 0x805FAA;
                _completeAddress = 0xC57449;
            }
            return new Tomb1MainTracker(this, process , levels);
        }
    }
}