using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TR2RandoTracker.Model;
using TR2RandoTracker.Utils;

namespace TR2RandoTracker.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly Dictionary<string, object> _defaultSettings;

        public SettingsWindow()
        {
            InitializeComponent();
            Owner = WindowUtils.GetActiveWindow();
            _content.DataContext = Settings.Instance;
            _defaultSettings = Settings.Instance.ToDictionary();

            _backgroundPicker.SelectedColor = Settings.Instance.Background.Color;
            _titlePicker.SelectedColor = Settings.Instance.TitleForeground.Color;
            _foregroundPicker.SelectedColor = Settings.Instance.Foreground.Color;
            _separatorPicker.SelectedColor = Settings.Instance.Separator.Color;
            _timerPicker.SelectedColor = Settings.Instance.TimerForeground.Color;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowUtils.TidyMenu(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DialogResult.HasValue)
            {
                Cancel();
            }
        }

        private void BackgroundPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Settings.Instance.Background = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void TitlePicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Settings.Instance.TitleForeground = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void ForegroundPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Settings.Instance.Foreground = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void SeparatorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Settings.Instance.Separator = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void TimerPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Settings.Instance.TimerForeground = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            Settings.Instance.SetFromDictionary(_defaultSettings);
            DialogResult = false;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Settings.Instance.BackgroundImage;
            string initDir;
            if (System.IO.File.Exists(path))
            {
                initDir = System.IO.Path.GetDirectoryName(Settings.Instance.BackgroundImage);
            }
            else
            {
                initDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif",
                InitialDirectory = initDir
            };
            if (ofd.ShowDialog(this) ?? false)
            {
                _imageBox.Text = ofd.FileName;
            }
        }
    }
}