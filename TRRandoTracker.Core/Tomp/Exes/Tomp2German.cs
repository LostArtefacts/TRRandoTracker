namespace TRRandoTracker.Core.Tomp.Exes;

class Tomp2German : AbstractTompExe
{
    public override string Name => "German";

    internal override byte[] Identifier => new byte[] { 0x54, 0xC3, 0x8F };

    internal override int TitleAddress => 0x11BD90;

    internal override int LevelAddress => 0xD9EB0;
    
    internal override int CreditsFlag => 0x11A1FC;
    internal override string ScriptPath => @"data\tombpc.dat";
}