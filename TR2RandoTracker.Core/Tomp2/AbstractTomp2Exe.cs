namespace TR2RandoTracker.Core
{
    public abstract class AbstractTomp2Exe
    {
        internal abstract byte[] Identifer { get; }
        internal abstract int TitleAddress { get; }
        internal abstract int LevelAddress { get; }
        internal abstract int CreditsFlag { get; }
        public abstract string Name { get; }
    }
}