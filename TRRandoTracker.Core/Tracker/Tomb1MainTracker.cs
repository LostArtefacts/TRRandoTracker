using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Extensions;

namespace TRRandoTracker.Core.Tracker;

internal class Tomb1MainTracker : DefaultTracker
{
    private readonly Dictionary<int, int> _cutscenes;

    public Tomb1MainTracker(AbstractTompExe trackingExe, Process process, List<AbstractTRScriptedLevel> levels)
        : base(trackingExe, process, levels)
    {
        _cutscenes = new Dictionary<int, int>();
        foreach (AbstractTRScriptedLevel level in levels)
        {
            if (level.HasCutScene)
            {
                _cutscenes[levels.Count + _cutscenes.Count + 1] = level.Sequence;
            }
        }
    }

    public override void Track()
    {
        if (_process.HasExited)
        {
            return;
        }

        Level = _process.Read<short>(_trackingExe.LevelAddress);
        if (Level < 0 || Level > (_levels.Count + _cutscenes.Count))
        {
            IsTitle = true;
            Level = 0;
        }
        else
        {
            if (_cutscenes.ContainsKey(Level))
            {
                Level = _cutscenes[Level];
            }
            
            IsTitle = false;
            IsCredits = Level == _levels.Count && _process.Read<byte>(_trackingExe.LevelCompleteAddress) == 1;
        }
    }

    public override bool InterpretLevel(int level)
    {
        return true;
    }

    public override TrackingEventArgs GetLevelArgs(int currentLevel, bool titleScreen)
    {
        TrackingEventArgs e = new TrackingEventArgs
        {
            Exe = _trackingExe
        };

        if (titleScreen)
        {
            e.Status = TrackingStatus.TitleScreen;
        }
        else
        {
            e.Status = TrackingStatus.InLevel;
        }

        e.Levels = _levels;
        currentLevel--;
        e.CurrentLevel = currentLevel >= 0 && currentLevel < _levels.Count ? _levels[currentLevel] : null;
        e.CurrentSequence = currentLevel;

        return e;
    }
}
