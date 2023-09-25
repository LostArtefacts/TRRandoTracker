namespace TRRandoTracker.Core.Tomp.Exes;

public class Tomp2Soul : AbstractTompExe
{
    public override string Name => "Soul";

    internal override byte[] Identifier => new byte[] { 0x70, 0x7C, 0x6A };

    internal override int TitleAddress => 0x11BDB0;

    internal override int LevelAddress => 0xD9ED0;

    internal override int CreditsFlag => 0x11A21C;
    internal override string ScriptPath => @"data\tombpc.dat";
}