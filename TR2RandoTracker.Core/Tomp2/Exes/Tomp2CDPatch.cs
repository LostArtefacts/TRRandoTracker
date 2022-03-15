namespace TR2RandoTracker.Core.Tomp2.Exes
{
    public class Tomp2CDPatch : AbstractTomp2Exe
    {
        public override string Name => "CD Patch";

        internal override byte[] Identifer => new byte[] { 0xBC, 0xAB, 0x82 };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;

        internal override int CreditsFlag => 0x11A1FC;
    }
}