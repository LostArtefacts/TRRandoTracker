using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Extensions;
using TRRandoTracker.Core.Tomp;
using TRRandoTracker.Core.Watcher;

namespace TRRandoTracker.Core.Tracker;

public class TompTracker
{
    private static readonly int _exeIDAddress = 0x88;

    private readonly ProcessWatcher _watcher;

    private ITracker _tracker;
    private AbstractTompExe _trackingExe;
    private Process _trackingProcess;
    private volatile bool _tracking;

    public event EventHandler<TrackingEventArgs> TrackingChanged;

    private List<AbstractTRScriptedLevel> _scriptLevels;

    public TompTracker()
    {
        _watcher = new ProcessWatcher("tr1x", "tomb2"/*, "tomb3"*/, "tomb123");
        _watcher.ProcessStarted += Watcher_ProcessStarted;
        _watcher.ProcessStopped += Watcher_ProcessStopped;

        _tracking = false;
        _watcher.Start();
    }

    public void Stop()
    {
        _tracking = false;
        _watcher.Stop();
    }

    private void Watcher_ProcessStarted(object sender, ProcessEventArgs e)
    {
        string checksum = null;
        try
        {
            checksum = new FileInfo(e.Process.MainModule.FileName).Checksum();
        }
        catch { }

        AbstractTompExe exe = TompExeFactory.Get(e.Process.ProcessName, e.Process.ProcessName.ToLower() == "tomb123" ?  null : e.Process.ReadBytes(_exeIDAddress, 3), checksum);
        if (exe != null)
        {
            if (!LoadScriptLevels(e.Process, exe))
            {
                return;
            }

            TrackingChanged?.Invoke(this, new TrackingEventArgs
            {
                Exe = _trackingExe = exe,
                Status = TrackingStatus.ExeStarted,
                Levels = _scriptLevels
            });

            _trackingProcess = e.Process;
            _tracking = true;
            new Thread(TrackLevels).Start();
        }
    }

    private void Watcher_ProcessStopped(object sender, ProcessEventArgs e)
    {
        if (_trackingExe != null)
        {
            TrackingChanged?.Invoke(this, new TrackingEventArgs
            {
                Exe = _trackingExe,
                Status = TrackingStatus.ExeStopped
            });

            _trackingProcess = null;
            _tracking = false;
            _trackingExe = null;
        }
    }

    private bool LoadScriptLevels(Process process, AbstractTompExe exe)
    {
        if (exe.ScriptPath == null)
        {
            _tracker = exe.CreateTracker(process, null);
            _tracker.GameChanged += (o, e) =>
            {
                _scriptLevels = new(e.Levels);
                TrackingChanged?.Invoke(this, new TrackingEventArgs
                {
                    Exe = _trackingExe = exe,
                    Status = TrackingStatus.GameChanged,
                    Levels = _scriptLevels,
                    CurrentSequence = -1,
                });
            };
            return true;
        }

        string datFile = Path.Combine(Path.GetDirectoryName(process.MainModule.FileName), exe.ScriptPath);
        if (File.Exists(datFile))
        {
            string tmpDatFile = Path.GetTempFileName() + Path.GetExtension(datFile);
            try
            {
                Thread.Sleep(1000); // try to allow the game to access the dat file first
                File.Copy(datFile, tmpDatFile, true);
                _scriptLevels = TRScriptFactory.OpenScript(tmpDatFile).Levels;
            }
            finally
            {
                if (File.Exists(tmpDatFile))
                {
                    File.Delete(tmpDatFile);
                }
            }

            _tracker = exe.CreateTracker(process, _scriptLevels);

            return true;
        }
        return false;
    }

    private void TrackLevels()
    {
        int currentLevel = -1, level;
        bool currentTitle = false, titleScreen, creditsScreen;

        while (_tracking && !_trackingProcess.HasExited)
        {
            _tracker.Track();

            level = _tracker.Level;
            titleScreen = _tracker.IsTitle;
            creditsScreen = _tracker.IsCredits;

            if (_tracker.InterpretLevel(level))
            {
                if (level != currentLevel || titleScreen != currentTitle)
                {
                    FireLevelChanged(currentLevel = level, currentTitle = titleScreen);
                }
            }

            if (currentLevel != -1 && creditsScreen)
            {
                FireCreditsReached();
                do
                {
                    Thread.Sleep(500);
                    _tracker.Track();
                    creditsScreen = _tracker.IsCredits;
                }
                while (creditsScreen && _tracking && !_trackingProcess.HasExited);
            }

            Thread.Sleep(100);
        }
    }

    private void FireLevelChanged(int currentLevel, bool titleScreen)
    {
        TrackingChanged?.Invoke(this, _tracker.GetLevelArgs(currentLevel, titleScreen));
    }

    private void FireCreditsReached()
    {
        TrackingChanged.Invoke(this, new TrackingEventArgs
        {
            Exe = _trackingExe,
            Status = TrackingStatus.Credits
        });
    }
}