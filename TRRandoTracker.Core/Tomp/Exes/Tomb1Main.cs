using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core.Tomp.Exes;

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
        // 2.11 minimum version
        _levelAddress = 0x805FAA;
        _completeAddress = 0xC57449;
        
        Version version = CalculateProductVersion(process.MainModule.FileName);
        if (version != null)
        {
            if (version >= new Version(2, 15))
            {
                _levelAddress = 0x80CFEA;
                _completeAddress = 0xC5F509;
            }
            else if (version >= new Version(2, 14))
            {
                _levelAddress = 0x809F8A;
                _completeAddress = 0xC5C509;
            }
            else if (version >= new Version(2, 13))
            {
                _levelAddress = 0x808F8A;
                _completeAddress = 0xC5A509;
            }
        }
        return new Tomb1MainTracker(this, process , levels);
    }
}