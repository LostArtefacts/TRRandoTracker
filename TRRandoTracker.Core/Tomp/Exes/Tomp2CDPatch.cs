namespace TRRandoTracker.Core.Tomp.Exes
{
    public class Tomp2CDPatch : AbstractTompExe
    {
        public override string Name => "CD Patch";

        internal override byte[] Identifier => new byte[] { 0xBC, 0xAB, 0x82 };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;

        internal override int CreditsFlag => 0x11A1FC;
        internal override string ScriptPath => @"data\tombpc.dat";
    }
}