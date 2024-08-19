using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Extensions;

namespace TRRandoTracker.Core.Tracker;

internal class DefaultTracker : ITracker
{
    protected readonly AbstractTompExe _trackingExe;
    protected readonly Process _process;
    protected readonly List<AbstractTRScriptedLevel> _levels;

    public event EventHandler<TrackingEventArgs> GameChanged;

    public int Level { get; set; }
    public bool IsTitle { get; set; }
    public bool IsCredits { get; set; }

    public DefaultTracker(AbstractTompExe trackingExe, Process process, List<AbstractTRScriptedLevel> levels)
    {
        _trackingExe = trackingExe;
        _process = process;
        _levels = levels;
    }

    protected void TriggerGameChanged()
    {
        GameChanged?.Invoke(this, new()
        {
            Levels = _levels,
        });
    }

    public virtual void Track()
    {
        Level = _process.Read<int>(_trackingExe.LevelAddress);
        IsTitle = _process.Read<int>(_trackingExe.TitleAddress) == 1;
        IsCredits = Level != -1 && _process.Read<int>(_trackingExe.CreditsFlag) == 1;            
    }

    public virtual bool InterpretLevel(int level)
    {
        return level - 1 < _levels.Count;
    }

    public virtual TrackingEventArgs GetLevelArgs(int currentLevel, bool titleScreen)
    {
        TrackingEventArgs e = new()
        {
            Exe = _trackingExe
        };

        if (titleScreen)
        {
            e.Status = TrackingStatus.TitleScreen;
            currentLevel--;
        }
        else
        {
            currentLevel--;
            if (currentLevel >= _levels.Count)
            {
                e.Status = TrackingStatus.InDemo;
            }
            else
            {
                e.Status = TrackingStatus.InLevel;
            }
        }

        e.Levels = _levels;
        e.CurrentLevel = currentLevel >= 0 && currentLevel < _levels.Count ? _levels[currentLevel] : null;
        e.CurrentSequence = currentLevel;

        return e;
    }
}
