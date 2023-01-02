namespace TRRandoTracker.Core.Tracker
{
    internal interface ITracker
    {
        int Level { get; set; }
        bool IsTitle { get; set; }
        bool IsCredits { get; set; }
        void Track();
        bool InterpretLevel(int level);
        TrackingEventArgs GetLevelArgs(int currentLevel, bool titleScreen);
    }
}
