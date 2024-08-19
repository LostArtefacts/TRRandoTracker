using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core.Tomp.Exes;

public class TRR : AbstractTompExe
{
    public override string Name => "TRR";

    internal override byte[] Identifier => new byte[] { 0, 0, 0 };

    internal override int TitleAddress => throw new NotImplementedException();

    internal override int LevelAddress => throw new NotImplementedException();

    internal override int CreditsFlag => throw new NotImplementedException();

    internal override string ScriptPath => null;

    internal override ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
    {
        return new TRRTracker(this, process);
    }
}
