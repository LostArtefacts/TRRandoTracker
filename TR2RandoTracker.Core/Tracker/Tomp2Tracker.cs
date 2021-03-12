using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TR2RandoTracker.Core.Extensions;
using TR2RandoTracker.Core.Tomp2;
using TR2RandoTracker.Core.Watcher;
using TRGE.Core;

namespace TR2RandoTracker.Core.Tracker
{
    public class Tomp2Tracker
    {
        private static readonly int _exeIDAddress = 0x88;

        private readonly ProcessWatcher _watcher;

        private AbstractTomp2Exe _trackingExe;
        private Process _trackingProcess;
        private volatile bool _tracking;

        public event EventHandler<TrackingEventArgs> TrackingChanged;

        private List<TR23ScriptedLevel> _scriptLevels;

        public Tomp2Tracker()
        {
            _watcher = new ProcessWatcher("tomb2");
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
            //byte[] multipatch = new byte[] { 0xE3, 0xA8, 0x8E };
            //byte[] epc = new byte[] { 0x03, 0xC4, 0x8F };
            //byte[] ukbox = new byte[] { 0x7F, 0x76, 0x6A };
            //byte[] german = new byte[] { 0x54, 0xC3, 0x8F };
            //byte[] soul = new byte[] { 0x70, 0x7C, 0x6A };
            //byte[] jpn = new byte[] { 0x6F, 0xB6, 0xA3 };

            AbstractTomp2Exe exe = Tomp2ExeFactory.Get(e.Process.ReadBytes(_exeIDAddress, 3));
            if (exe != null)
            {
                if (!LoadScriptLevels(e.Process))
                {
                    return;
                }

                TrackingChanged?.Invoke(this, new TrackingEventArgs
                {
                    Exe = (_trackingExe = exe),
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

        private bool LoadScriptLevels(Process process)
        {
            string datFile = Path.Combine(Path.GetDirectoryName(process.MainModule.FileName), "data", "tombpc.dat");
            if (File.Exists(datFile))
            {
                string tmpDatFile = Path.GetTempFileName();
                try
                {
                    Thread.Sleep(1000); // try to allow the game to access the dat file first
                    File.Copy(datFile, tmpDatFile, true);
                    TR23Script script = TRScriptFactory.OpenScript(tmpDatFile) as TR23Script;
                    _scriptLevels = script.Levels.Cast<TR23ScriptedLevel>().ToList();
                }
                finally
                {
                    if (File.Exists(tmpDatFile))
                    {
                        File.Delete(tmpDatFile);
                    }
                }
                return true;
            }
            return false;
        }

        private void TrackLevels()
        {
            int currentLevel = -1;
            bool currentTitle = false;

            while (_tracking && !_trackingProcess.HasExited)
            {
                int level = _trackingProcess.Read<int>(_trackingExe.LevelAddress);
                bool titleScreen = _trackingProcess.Read<int>(_trackingExe.TitleAddress) == 1;

                if ((level - 1) < _scriptLevels.Count) //ignore demos altogether
                {
                    if (level != currentLevel || titleScreen != currentTitle)
                    {
                        FireLevelChanged(currentLevel = level, currentTitle = titleScreen);
                    }
                }
                

                //if (titleScreen)
                //{
                //    if (currentLevel != 0)
                //    {
                //        FireLevelChanged(currentLevel = 0);
                //    }
                //}
                //else if (currentLevel != level)
                //{
                //    FireLevelChanged(currentLevel = level);
                //}

                Thread.Sleep(500);
            }
        }

        private void FireLevelChanged(int currentLevel, bool titleScreen)
        {
            TrackingEventArgs e = new TrackingEventArgs
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
                if (currentLevel >= _scriptLevels.Count)
                {
                    e.Status = TrackingStatus.InDemo;
                }
                else
                {
                    e.Status = TrackingStatus.InLevel;
                }
            }

            e.Levels = _scriptLevels;
            e.CurrentLevel = currentLevel >= 0 && currentLevel < _scriptLevels.Count ? _scriptLevels[currentLevel] : null;
            e.CurrentSequence = currentLevel;

            //if (currentLevel == 0)
            //{
            //    e.Status = TrackingStatus.TitleScreen;
            //    e.Levels = _scriptLevels;
            //    e.CurrentSequence = -1;
            //}
            //else
            //{
            //    currentLevel--;
            //    if (currentLevel >= _scriptLevels.Count)
            //    {
            //        e.Status = TrackingStatus.InDemo;
            //    }
            //    else
            //    {
            //        e.Status = TrackingStatus.InLevel;
            //        e.Levels = _scriptLevels;
            //        e.CurrentLevel = _scriptLevels[currentLevel];
            //        e.CurrentSequence = currentLevel;
            //    }
            //}

            TrackingChanged?.Invoke(this, e);
        }
    }
}