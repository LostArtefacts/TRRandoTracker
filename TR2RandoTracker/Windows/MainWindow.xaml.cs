using System;
using System.Windows;
using System.Windows.Input;
using TR2RandoTracker.Core.Tracker;
using TR2RandoTracker.Model;
using TR2RandoTracker.Updates;

namespace TR2RandoTracker.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Tomp2Tracker _tracker;
        private bool _autoUpdateMessageShown, _gameInProgress;

        public MainWindow()
        {
            InitializeComponent();

            _listView.ItemsSource = new LevelViewList();
            _listView.DataContext = DataContext = Settings.Instance;

            AllowsTransparency = _transparencyMenu.IsChecked = Settings.Instance.AllowTransparency;

            Top = Settings.Instance.Top;
            Left = Settings.Instance.Left;
            Width = Settings.Instance.Width;
            Height = Settings.Instance.Height;

            Topmost = _onTopMenu.IsChecked = Settings.Instance.AlwaysOnTop;
            ResizeMode = Settings.Instance.Resizable ? ResizeMode.CanResizeWithGrip : ResizeMode.NoResize;
            _resizeMenu.IsChecked = ResizeMode == ResizeMode.CanResizeWithGrip;

            _tracker = new Tomp2Tracker();
            _tracker.TrackingChanged += Tracker_TrackingChanged;

            _autoUpdateMessageShown = _gameInProgress = false;
            UpdateChecker.Instance.UpdateAvailable += UpdateChecker_UpdateAvailable;

            Application.Current.Exit += Application_Exit;
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
                    _gameInProgress = true;
                    _resetMenu.IsEnabled = true;
                    _listView.ItemsSource = LevelViewList.Get(e.Levels, e.CurrentSequence);
                    break;
                case TrackingStatus.InLevel:
                    _gameInProgress = true;
                    _resetMenu.IsEnabled = false;
                    _listView.ItemsSource = LevelViewList.Get(e.Levels, e.CurrentSequence);
                    break;
                case TrackingStatus.ExeStopped:
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
                    DragMove();
                    StoreWindowState();
                }
                catch { }
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
                    MessageWindow.ShowMessage("The current version of TR2 Rando Tracker is up to date.");
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

        private void ColourSchemeMenu_Click(object sender, RoutedEventArgs e)
        {
            ColourSchemeWindow csw = new ColourSchemeWindow();
            if (csw.ShowDialog() ?? false)
            {
                Settings.Instance.Save();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StoreWindowState();
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

        private void TransparencyMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MessageWindow.ShowMessageWithCancel("This setting will take effect after the application is restarted."))
            {
                _transparencyMenu.IsChecked = Settings.Instance.AllowTransparency = !Settings.Instance.AllowTransparency;
                StoreWindowState();

                _transparencyMenu.IsEnabled = false;
                _transparencyMenu.Header += " (pending restart)";                
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
}