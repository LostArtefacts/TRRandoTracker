using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core.Tomp.Exes;

public class Tomb1Main : AbstractTompExe
{
    public override string Name => "TR1X";

    internal override byte[] Identifier => new byte[] { 0,0,0 };

    internal override int TitleAddress => 0x11BD90;

    private int _levelAddress, _completeAddress;
    internal override int LevelAddress => _levelAddress;
    internal override int LevelCompleteAddress => _completeAddress;

    internal override int CreditsFlag => 0x11A1FC;

    internal override string ScriptPath => @"cfg\TR1X_gameflow.json5";

    internal override ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
    {
        // 3.0 minimum version
        _levelAddress = 0x80F58A;
        _completeAddress = 0xC66629;
        
        Version version = CalculateProductVersion(process.MainModule.FileName);
        if (version != null)
        {
            if (version >= new Version(3, 1))
            {
                _levelAddress = 0x80F5AA;
                _completeAddress = 0xC66629;
            }
        }
        return new Tomb1MainTracker(this, process , levels);
    }
}