namespace TRRandoTracker.Core.Tomp.Exes
{
    public class Tomp2UKBox : AbstractTompExe
    {
        public override string Name => "UK Box";

        internal override byte[] Identifier => new byte[] { 0x7F, 0x76, 0x6A };

        internal override int TitleAddress => 0x11BDA0;

        internal override int LevelAddress => 0xD9EC0;

        internal override int CreditsFlag => 0x11A20C;

        internal override string ScriptPath => @"data\tombpc.dat";
    }
}