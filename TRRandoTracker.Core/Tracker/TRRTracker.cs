using Newtonsoft.Json;
using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Extensions;

namespace TRRandoTracker.Core.Tracker;

internal class TRRTracker : DefaultTracker
{
    private Dictionary<int, int> _cutscenes;
    private int _currentGame;
    private IntPtr _dllBase;
    private static readonly Dictionary<int, int> _levelComplete = new()
    {
        [0] = 0xF7560,
        [1] = 0x12EF44,
    };

    public TRRTracker(AbstractTompExe trackingExe, Process process)
        : base(trackingExe, process, new())
    {
        _currentGame = -1;
        LoadLevels();
    }

    private void LoadLevels()
    {
        string module;
        while (true)
        {
            try
            {
                module = Path.GetDirectoryName(_process.MainModule.FileName);
                break;
            }
            catch
            {
                Thread.Sleep(100);
            }
        }

        _levels.Clear();
        string trgeFile = null;
        if (_currentGame == -1)
        {
            while (_currentGame++ < 2)
            {
                trgeFile = Path.Combine(module, $@"{_currentGame + 1}\data\trge.json");
                if (File.Exists(trgeFile))
                {
                    break;
                }
            }
        }
        else
        {
            trgeFile = Path.Combine(module, $@"{_currentGame + 1}\data\trge.json");
            if (!File.Exists(trgeFile))
            {
                trgeFile = null;
            }
        }

        if (trgeFile == null || !File.Exists(trgeFile))
        {
            _currentGame = -1;
            return;
        }
        

        List<TRRSequence> data = JsonConvert.DeserializeObject<List<TRRSequence>>(File.ReadAllText(trgeFile));
        _levels.AddRange(data.Select(l => new TRRScriptedLevel(TRVersion.TR1)
        {
            Sequence = l.Index,
            Name = l.Name,
            RemovesAmmo = l.Ammoless,
            RemovesWeapons = l.Unarmed,
            CutSceneLevel = l.HasCutScene ? new TRRScriptedLevel(TRVersion.TR1) : null,
        }));

        _cutscenes = new Dictionary<int, int>();
        foreach (AbstractTRScriptedLevel level in _levels.Where(l => l.HasCutScene))
        {
            if (_currentGame == 0)
            {
                _cutscenes[_levels.Count + _cutscenes.Count + 5] = level.Sequence;
            }
            else
            {
                //33,36,38,47
                _cutscenes[level.Sequence + 32] = level.Sequence;
            }
        }
    }

    private int _gameChangeTracker = 0;

    public override void Track()
    {
        if (_process.HasExited)
        {
            return;
        }

        int game = _process.Read<int>(_process.MainModule.BaseAddress, 0x2F3DB0);
        if (_currentGame != game)
        {
            _gameChangeTracker++;
            if (_gameChangeTracker < 10)
            {
                return;
            }

            _gameChangeTracker = 0;
            _currentGame = game;
            LoadLevels();
            _dllBase = _process.GetDLLBase(_currentGame == 0 ? "tomb1.dll" : "tomb2.dll");
            TriggerGameChanged();
        }

        if (_levels.Count == 0)
        {
            return;
        }

        if (_process.Read<short>(_process.MainModule.BaseAddress, 0x25EF44) == 1)
        {
            // Loading screen
            return;
        }

        Level = _process.Read<short>(_process.MainModule.BaseAddress, 0x25EA64);
        if (_currentGame == 1 && Level == 63)
        {
            IsTitle = true;
            Level = 0;
        }
        else if (_currentGame == 0 && (Level < 0 || Level > (_levels.Count + _cutscenes.Count + 4)))
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
            
            IsCredits = Level == _levels.Count && _process.Read<short>(_dllBase, _levelComplete[_currentGame]) == 1;
        }
    }

    public override bool InterpretLevel(int level)
    {
        return true;
    }

    public override TrackingEventArgs GetLevelArgs(int currentLevel, bool titleScreen)
    {
        TrackingEventArgs e = new()
        {
            Exe = _trackingExe
        };

        if (titleScreen || currentLevel == -1)
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

    private class TRRSequence
    {
        public ushort Index { get; set; }
        public string Name { get; set; }
        public bool Unarmed { get; set; }
        public bool Ammoless { get; set; }
        public bool HasCutScene { get; set; }
    }
}
