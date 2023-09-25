using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core.Tomp.Exes;

public class Tomp3International : AbstractTompExe
{
    private static readonly string[] _hashes = new string[]
    {
        "4044dc2c58f02bfea2572e80dd8f2abb",
        "46a780f8f5314d5284f1d1b3ab468ab2"
    };

    public override string Name => "International";

    internal override string[] Hashes => _hashes;

    internal override byte[] Identifier => new byte[] { 0x7F, 0x76, 0x6A };

    internal override int TitleAddress => 0x2A1C58;

    internal override int LevelAddress => 0xC561C;

    internal override int CreditsFlag => 0x11A20C;
    internal override string ScriptPath => @"data\tombpc.dat";

    internal override ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
    {
        return new Tomb3Tracker(this, process, levels);
    }
}