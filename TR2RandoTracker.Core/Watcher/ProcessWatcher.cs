using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace TR2RandoTracker.Core.Watcher
{
    public class ProcessWatcher
    {
        public event EventHandler<ProcessEventArgs> ProcessStarted;
        public event EventHandler<ProcessEventArgs> ProcessStopped;

        private readonly string _processName;
        private Process _currentProcess;
        private volatile bool _running;

        public ProcessWatcher(string processName)
        {
            _processName = processName;
        }

        public void Start()
        {
            _running = true;
            new Thread(Run).Start();
        }

        public void Stop()
        {
            _running = false;
        }

        private void Run()
        {
            while (_running)
            {
                if (_currentProcess == null)
                {
                    Process[] processes = Process.GetProcessesByName(_processName);
                    if (processes.Length > 0)
                    {
                        _currentProcess = processes[0];
                        ProcessStarted?.Invoke(this, new ProcessEventArgs
                        {
                            Process = _currentProcess
                        });
                    }
                }

                if (_currentProcess != null)
                {
                    if (_currentProcess.HasExited)
                    {
                        ProcessStopped?.Invoke(this, new ProcessEventArgs
                        {
                            Process = _currentProcess
                        });
                        _currentProcess = null;
                    }
                    else
                    {

                    }
                }

                Thread.Sleep(500);
            }
        }
    }
}