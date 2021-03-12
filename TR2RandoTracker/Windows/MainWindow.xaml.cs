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

        public MainWindow()
        {
            InitializeComponent();
            _listView.ItemsSource = new LevelViewList();

            _tracker = new Tomp2Tracker();
            _tracker.TrackingChanged += Tracker_TrackingChanged;

            UpdateChecker.Instance.UpdateAvailable += UpdateChecker_UpdateAvailable;

            Application.Current.Exit += Application_Exit;
        }

        private void UpdateChecker_UpdateAvailable(object sender, UpdateEventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                ShowUpdateWindow();
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
                case TrackingStatus.InLevel:
                    _listView.ItemsSource = LevelViewList.Get(e.Levels, e.CurrentSequence);
                    break;
                case TrackingStatus.ExeStopped:
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
            new UpdateAvailableWindow().ShowDialog();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }
    }
}