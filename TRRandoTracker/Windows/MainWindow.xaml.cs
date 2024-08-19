using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TRRandoTracker.Core.Tracker;
using TRRandoTracker.Model;
using TRRandoTracker.Updates;

namespace TRRandoTracker.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static readonly string _timerFormat = @"hh\:mm\:ss";
    private static readonly string _timerAltFormat = @"\.ff";

    private readonly TompTracker _tracker;
    private bool _autoUpdateMessageShown, _gameInProgress;
    private readonly DispatcherTimer _timer;
    private readonly Stopwatch _stopwatch;

    public MainWindow()
    {
        InitializeComponent();

        _listView.ItemsSource = new LevelViewList();
        _listView.DataContext = DataContext = Settings.Instance;

        Settings.Instance.PropertyChanged += Settings_PropertyChanged;
        // Must remain in constructor
        InitialiseLayout();
        
        _timer = new DispatcherTimer();
        _timer.Tick += Timer_Tick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        _stopwatch = new Stopwatch();

        _tracker = new TompTracker();
        _tracker.TrackingChanged += Tracker_TrackingChanged;

        _autoUpdateMessageShown = _gameInProgress = false;
        UpdateChecker.Instance.UpdateAvailable += UpdateChecker_UpdateAvailable;

        Application.Current.Exit += Application_Exit;
    }

    private void InitialiseLayout()
    {
        AllowsTransparency = Settings.Instance.AllowTransparency;
        Topmost = _onTopMenu.IsChecked = Settings.Instance.AlwaysOnTop;
        ResizeMode = Settings.Instance.Resizable ? ResizeMode.CanResizeWithGrip : ResizeMode.NoResize;
        _resizeMenu.IsChecked = ResizeMode == ResizeMode.CanResizeWithGrip;
        Top = Settings.Instance.Top;
        Left = Settings.Instance.Left;
        Width = Settings.Instance.Width;
        Height = Settings.Instance.Height;

        SetBackgroundImage();
    }

    private void SetBackgroundImage()
    {
        if (Settings.Instance.UseBackgroundImage)
        {
            try
            {
                string path = Path.GetFullPath(Settings.Instance.BackgroundImage);
                if (File.Exists(path))
                {
                    _mainGrid.Background = new ImageBrush(new BitmapImage(new Uri(path)))
                    {
                        Stretch = Stretch.None
                    };
                    return;
                }
            }
            catch { }
        }
        
        _mainGrid.Background = null;
    }

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Settings.Instance.AlwaysOnTop):
                Topmost = _onTopMenu.IsChecked = Settings.Instance.AlwaysOnTop;
                break;
            case nameof(Settings.Instance.Resizable):
                ResizeMode = Settings.Instance.Resizable ? ResizeMode.CanResizeWithGrip : ResizeMode.NoResize;
                _resizeMenu.IsChecked = ResizeMode == ResizeMode.CanResizeWithGrip;
                break;
            case nameof(Settings.Instance.UseBackgroundImage):
            case nameof(Settings.Instance.BackgroundImage):
                SetBackgroundImage();
                break;
        }            
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        _timerLabel.Text = _stopwatch.Elapsed.ToString(_timerFormat);
        _timerAltLabel.Text = _stopwatch.Elapsed.ToString(_timerAltFormat);
    }

    private void UpdateChecker_UpdateAvailable(object sender, UpdateEventArgs e)
    {
        if (Dispatcher.CheckAccess())
        {
            if (!_gameInProgress)
            {
                ShowUpdateWindow();
            }
        }
        else
        {
            Dispatcher.Invoke(() => UpdateChecker_UpdateAvailable(sender, e));
        }
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        _tracker.Stop();
    }

    private void Tracker_TrackingChanged(object sender, TrackingEventArgs e)
    {
        if (Dispatcher.CheckAccess())
        {
            TrackingChanged(e);
        }
        else
        {
            Dispatcher.Invoke(new Action(() => TrackingChanged(e))); 
        }
    }

    private void TrackingChanged(TrackingEventArgs e)
    {
        switch (e.Status)
        {
            case TrackingStatus.TitleScreen:
            case TrackingStatus.GameChanged:
                _timer.Stop();
                _stopwatch.Stop();
                _gameInProgress = true;
                _resetMenu.IsEnabled = true;
                _listView.ItemsSource = LevelViewList.Get(e.Levels, e.CurrentSequence);
                break;

            case TrackingStatus.InLevel:
                if (!_timer.IsEnabled && e.CurrentSequence != -1)
                {
                    _timer.Start();
                    _stopwatch.Reset();
                    _stopwatch.Start();
                }
                _gameInProgress = true;
                _resetMenu.IsEnabled = false;
                _listView.ItemsSource = LevelViewList.Get(e.Levels, e.CurrentSequence);

                if (e.CurrentSequence >= 0 && e.CurrentSequence < _listView.Items.Count)
                {
                    object focusItem = _listView.Items.GetItemAt(e.CurrentSequence == _listView.Items.Count - 1
                        ? e.CurrentSequence : e.CurrentSequence + 1);
                    if (focusItem != null)
                    {
                        _listView.ScrollIntoView(focusItem);
                    }
                }
                break;

            case TrackingStatus.Credits:
                _timer.Stop();
                _stopwatch.Stop();
                break;

            case TrackingStatus.ExeStopped:
                _timer.Stop();
                _stopwatch.Stop();
                _gameInProgress = false;
                _resetMenu.IsEnabled = true;
                _listView.ItemsSource = new LevelViewList();
                break;
        }
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            try
            {
                Cursor = _timerBox.Cursor = Cursors.SizeAll;
                DragMove();
                StoreWindowState();
            }
            catch { }
            finally
            {
                Cursor = _timerBox.Cursor = Cursors.Arrow;
            }
        }
    }

    private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void UpdatesMenuItem_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (UpdateChecker.Instance.CheckForUpdates())
            {
                _autoUpdateMessageShown = false;
                ShowUpdateWindow();
            }
            else
            {
                MessageWindow.ShowMessage("The current version of TR Rando Tracker is up to date.");
            }
        }
        catch (Exception ex)
        {
            MessageWindow.ShowError(ex.Message);
        }
    }

    private void ShowUpdateWindow()
    {
        if (!_autoUpdateMessageShown)
        {
            _autoUpdateMessageShown = true;
            new UpdateAvailableWindow().ShowDialog();
        }
    }

    private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
    {
        new AboutWindow().ShowDialog();
    }

    private void ResetMenuItem_Click(object sender, RoutedEventArgs e)
    {
        _listView.ItemsSource = new LevelViewList();
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        StoreWindowState();
        System.Diagnostics.Debug.WriteLine(Width + " " + Height);
    }

    private void AlwaysOnTopMenuItem_Click(object sender, RoutedEventArgs e)
    {
        _onTopMenu.IsChecked = Topmost = !Topmost;
        StoreWindowState();
    }

    private void ResizeMenuItem_Click(object sender, RoutedEventArgs e)
    {
        ResizeMode = ResizeMode == ResizeMode.CanResizeWithGrip ? ResizeMode.NoResize : ResizeMode.CanResizeWithGrip;
        _resizeMenu.IsChecked = ResizeMode == ResizeMode.CanResizeWithGrip;
        StoreWindowState();
    }

    private void SettingsMenu_Click(object sender, RoutedEventArgs e)
    {
        SettingsWindow sw = new();
        if (sw.ShowDialog() ?? false)
        {
            Settings.Instance.Save();
        }
    }

    private void ResetTimerMenuItem_Click(object sender, RoutedEventArgs e)
    {
        bool isRunning = _stopwatch.IsRunning;
        _stopwatch.Reset();            
        if (isRunning)
        {
            _stopwatch.Start();
        }
        else
        {
            _timerLabel.Text = "00:00:00";
            _timerAltLabel.Text = ".00";
        }
    }

    private void StoreWindowState()
    {
        Settings.Instance.Top = Top;
        Settings.Instance.Left = Left;
        Settings.Instance.Width = Width;
        Settings.Instance.Height = Height;
        Settings.Instance.AlwaysOnTop = Topmost;
        Settings.Instance.Resizable = ResizeMode == ResizeMode.CanResizeWithGrip;
        Settings.Instance.Save();
    }
}