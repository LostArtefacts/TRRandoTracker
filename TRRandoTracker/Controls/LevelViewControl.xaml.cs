using System.Windows;
using System.Windows.Controls;
using TRRandoTracker.Model;

namespace TRRandoTracker.Controls
{
    public partial class LevelViewControl : UserControl
    {
        public static readonly DependencyProperty LevelReachedProperty = DependencyProperty.Register
        (
            nameof(LevelReached), typeof(bool), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty LevelNameProperty = DependencyProperty.Register
        (
            nameof(LevelName), typeof(string), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty LevelSequenceProperty = DependencyProperty.Register
        (
            nameof(LevelSequence), typeof(int), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty UnarmedProperty = DependencyProperty.Register
        (
            nameof(Unarmed), typeof(bool), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty AmmolessProperty = DependencyProperty.Register
        (
            nameof(Ammoless), typeof(bool), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty MedilessProperty = DependencyProperty.Register
        (
            nameof(Mediless), typeof(bool), typeof(LevelViewControl)
        );

        public static readonly DependencyProperty MedilessSupportedProperty = DependencyProperty.Register
        (
            nameof(MedilessSupported), typeof(bool), typeof(LevelViewControl)
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

        public bool Mediless
        {
            get => (bool)GetValue(MedilessProperty);
            set => SetValue(MedilessProperty, value);
        }

        public bool MedilessSupported
        {
            get => (bool)GetValue(MedilessProperty);
            set => SetValue(MedilessProperty, value);
        }

        public LevelViewControl()
        {
            InitializeComponent();
            _border.DataContext = Settings.Instance;
            _content.DataContext = this;
            _visIndexCol.DataContext = Settings.Instance;
        }
    }
}
