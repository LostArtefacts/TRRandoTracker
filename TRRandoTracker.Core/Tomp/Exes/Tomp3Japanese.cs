using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core.Tomp.Exes;

public class Tomp3Japanese : AbstractTompExe
{
    private static readonly string[] _hashes = new string[]
    {
        "66404f58bb5dbf30707abfd245692cd2",
        "1c9bdf6b998b34752cb0c7d315129af6"
    };

    public override string Name => "Japanese";

    internal override string[] Hashes => _hashes;

    internal override byte[] Identifier => new byte[] { 0x7F, 0x76, 0x6A };

    internal override int TitleAddress => 0x2A1C60;

    internal override int LevelAddress => 0xC561C;

    internal override int CreditsFlag => 0x11A20C;

    internal override string ScriptPath => @"data\tombpc.dat";

    internal override ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
    {
        return new Tomb3Tracker(this, process, levels);
    }
}