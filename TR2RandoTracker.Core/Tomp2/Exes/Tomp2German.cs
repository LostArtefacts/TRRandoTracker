namespace TR2RandoTracker.Core.Tomp2.Exes
{
    class Tomp2German : AbstractTomp2Exe
    {
        public override string Name => "German";

        internal override byte[] Identifer => new byte[] { 0x54, 0xC3, 0x8F };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;
        
        internal override int CreditsFlag => 0x11A1FC;
    }
}