namespace TRRandoTracker.Core.Tomp.Exes
{
    public class Tomp2Japanese : AbstractTompExe
    {
        public override string Name => "Japanese";

        internal override byte[] Identifier => new byte[] { 0x6F, 0xB6, 0xA3 };

        internal override int TitleAddress => 0x124730;

        internal override int LevelAddress => 0xE2850;

        internal override int CreditsFlag => 0x122B9C;
        internal override string ScriptPath => @"data\tombpc.dat";
    }
}