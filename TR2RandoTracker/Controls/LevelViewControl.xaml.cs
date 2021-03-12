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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            _content.DataContext = this;
        }
    }
}
