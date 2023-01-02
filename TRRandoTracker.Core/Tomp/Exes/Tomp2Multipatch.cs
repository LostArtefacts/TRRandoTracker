namespace TRRandoTracker.Core.Tomp.Exes
{
    public class Tomp2Multipatch : AbstractTompExe
    {
        public override string Name => "Multipatch";

        internal override byte[] Identifier => new byte[] { 0xE3, 0xA8, 0x8E };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;

        internal override int CreditsFlag => 0x11A1FC;
        internal override string ScriptPath => @"data\tombpc.dat";
    }
}