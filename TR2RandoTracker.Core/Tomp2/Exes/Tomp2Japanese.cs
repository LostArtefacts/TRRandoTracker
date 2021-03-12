namespace TR2RandoTracker.Core.Tomp2.Exes
{
    public class Tomp2Japanese : AbstractTomp2Exe
    {
        public override string Name => "Japanese";

        internal override byte[] Identifer => new byte[] { 0x6F, 0xB6, 0xA3 };

        internal override int TitleAddress => 0x124730;

        internal override int LevelAddress => 0xE2850;
    }
}