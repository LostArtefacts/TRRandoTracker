namespace TR2RandoTracker.Core.Tomp2.Exes
{
    public class Tomp2EPC : AbstractTomp2Exe
    {
        public override string Name => "EPC";

        internal override byte[] Identifer => new byte[] { 0x03, 0xC4, 0x8F };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;
    }
}