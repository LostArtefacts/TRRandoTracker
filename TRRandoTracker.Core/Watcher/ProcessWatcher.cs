using System.Diagnostics;

namespace TRRandoTracker.Core.Watcher;

public class ProcessWatcher
{
    public event EventHandler<ProcessEventArgs> ProcessStarted;
    public event EventHandler<ProcessEventArgs> ProcessStopped;

    private readonly string[] _processNames;
    private Process _currentProcess;
    private volatile bool _running;

    public ProcessWatcher(params string[] processNames)
    {
        _processNames = processNames;
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
                foreach (string processName in _processNames)
                {
                    Process[] processes = Process.GetProcessesByName(processName);
                    if (processes.Length > 0)
                    {
                        _currentProcess = processes[0];
                        ProcessStarted?.Invoke(this, new ProcessEventArgs
                        {
                            Process = _currentProcess
                        });
                        break;
                    }
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
            }

            Thread.Sleep(500);
        }
    }
}