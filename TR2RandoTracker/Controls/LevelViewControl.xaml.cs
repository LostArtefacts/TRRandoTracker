using System.Windows;
using System.Windows.Controls;
using TR2RandoTracker.Model;

namespace TR2RandoTracker.Controls
{
    /// <summary>
    /// Interaction logic for LevelViewControl.xaml
    /// </summary>
    public partial class LevelViewControl : UserControl
    {
        public static readonly DependencyProperty LevelReachedProperty = DependencyProperty.Register
        (
            "LevelReached", typeof(bool), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty LevelNameProperty = DependencyProperty.Register
        (
            "LevelName", typeof(string), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty LevelSequenceProperty = DependencyProperty.Register
        (
            "LevelSequence", typeof(int), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty UnarmedProperty = DependencyProperty.Register
        (
            "Unarmed", typeof(bool), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty AmmolessProperty = DependencyProperty.Register
        (
            "Ammoless", typeof(bool), typeof(LevelViewControl)
        );

        public bool LevelReached
        {
            get => (bool)GetValue(LevelReachedProperty);
            set => SetValue(LevelReachedProperty, value);
        }

        public string LevelName
        {
            get => (string)GetValue(LevelNameProperty);
            set => SetValue(LevelNameProperty, value);
        }

        public int LevelSequence
        {
            get => (int)GetValue(LevelSequenceProperty);
            set => SetValue(LevelSequenceProperty, value);
        }

        public bool Unarmed
        {
            get => (bool)GetValue(UnarmedProperty);
            set => SetValue(UnarmedProperty, value);
        }

        public bool Ammoless
        {
            get => (bool)GetValue(AmmolessProperty);
            set => SetValue(AmmolessProperty, value);
        }

        public LevelViewControl()
        {
            InitializeComponent();
            _border.DataContext = Settings.Instance;
            _content.DataContext = this;
            _visIndexCol.DataContext = _hidIndexCol.DataContext = _placeholderBox.DataContext = Settings.Instance;
        }
    }
}
