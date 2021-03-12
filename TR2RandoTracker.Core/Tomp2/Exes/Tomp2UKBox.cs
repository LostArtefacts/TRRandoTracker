namespace TR2RandoTracker.Core.Tomp2.Exes
{
    public class Tomp2UKBox : AbstractTomp2Exe
    {
        public override string Name => "UK Box";

        internal override byte[] Identifer => new byte[] { 0x7F, 0x76, 0x6A };

        internal override int TitleAddress => 0x11BDA0;

        internal override int LevelAddress => 0xD9EC0;
    }
}