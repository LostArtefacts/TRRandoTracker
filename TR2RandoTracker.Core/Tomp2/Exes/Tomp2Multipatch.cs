namespace TR2RandoTracker.Core.Tomp2.Exes
{
    public class Tomp2Multipatch : AbstractTomp2Exe
    {
        public override string Name => "Multipatch";

        internal override byte[] Identifer => new byte[] { 0xE3, 0xA8, 0x8E };

        internal override int TitleAddress => 0x11BD90;

        internal override int LevelAddress => 0xD9EB0;
    }
}