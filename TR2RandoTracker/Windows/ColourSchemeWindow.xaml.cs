using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using TR2RandoTracker.Model;
using TR2RandoTracker.Utils;

namespace TR2RandoTracker.Windows
{
    /// <summary>
    /// Interaction logic for ColourSchemeWindow.xaml
    /// </summary>
    public partial class ColourSchemeWindow : Window
    {
        private readonly SolidColorBrush _originalBackground, _originalForeground, _originalSeparator;

        public ColourSchemeWindow()
        {
            InitializeComponent();
            Owner = WindowUtils.GetActiveWindow();

            _backgroundPicker.SelectedColor = (_originalBackground = Settings.Instance.Background).Color;
            _foregroundPicker.SelectedColor = (_originalForeground = Settings.Instance.Foreground).Color;
            _separatorPicker.SelectedColor = (_originalSeparator = Settings.Instance.Separator).Color;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowUtils.TidyMenu(this);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!DialogResult.HasValue)
            {
                Cancel();
            }
        }

        private void Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                Settings.Instance.Background = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void Foreground_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
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
            Settings.Instance.Background = _originalBackground;
            Settings.Instance.Foreground = _originalForeground;
            Settings.Instance.Separator = _originalSeparator;
            DialogResult = false;
        }
    }
}