namespace TRRandoTracker.Core.Tomp.Exes
{
    public class Tomp2EPC : AbstractTompExe
    {
        public override string Name => "EPC";

        internal override byte[] Identifier => new byte[] { 0x03, 0xC4, 0x8F };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;

        internal override int CreditsFlag => 0x11A1FC;
        internal override string ScriptPath => @"data\tombpc.dat";
    }
}