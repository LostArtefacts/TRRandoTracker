using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace TR2RandoTracker.Model
{
    public class Settings : INotifyPropertyChanged
    {
        private static Settings _instance;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        private const string _defaultBackground = "#B3151515";
        private const string _defaultForeground = "#FFF";
        private const string _defaultSeparator = "#FF353535";
        private const double _defaultTop = 0;
        private const double _defaultLeft = 0;
        private const double _defaultWidth = 300;
        private const double _defaultHeight = 486;
        private const bool _defaultOnTop = false;
        private const bool _defaultResizable = true;
        private const bool _defaultAllowTransparency = false;

        private bool _allowTransparency;
        private SolidColorBrush _background, _foreground, _separator;
        private double _top, _left, _width, _height;
        private bool _onTop, _resizable;

        public bool AllowTransparency
        {
            get => _allowTransparency;
            set
            {
                _allowTransparency = value;
                FirePropertyChanged();
            }
        }

        public SolidColorBrush Background
        { 
            get => _background; 
            set
            {
                _background = value;
                FirePropertyChanged();
            }
        }

        public SolidColorBrush Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                FirePropertyChanged();
            }
        }

        public SolidColorBrush Separator
        {
            get => _separator;
            set
            {
                _separator = value;
                FirePropertyChanged();
            }
        }

        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                FirePropertyChanged();
            }
        }

        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                FirePropertyChanged();
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                FirePropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                FirePropertyChanged();
            }
        }

        public bool AlwaysOnTop
        {
            get => _onTop;
            set
            {
                _onTop = value;
                FirePropertyChanged();
            }
        }

        public bool Resizable
        {
            get => _resizable;
            set
            {
                _resizable = value;
                FirePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Settings()
        {
            AllowTransparency = _defaultAllowTransparency;
            Background = CreateBrush(_defaultBackground);
            Foreground = CreateBrush(_defaultForeground);
            Separator = CreateBrush(_defaultSeparator);
            Top = _defaultTop;
            Left = _defaultLeft;
            Width = _defaultWidth;
            Height = _defaultHeight;
            AlwaysOnTop = _defaultOnTop;
            Resizable = _defaultResizable;

            Load();
        }

        protected void FirePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static SolidColorBrush CreateBrush(string hex)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        }

        private void Load()
        {
            string configPath = GetConfigPath();

            if (File.Exists(configPath))
            {
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(configPath));
                if (data.ContainsKey(nameof(AllowTransparency)))
                {
                    AllowTransparency = bool.Parse(data[nameof(AllowTransparency)].ToString());
                }
                if (data.ContainsKey(nameof(Background)))
                {
                    Background = CreateBrush(data[nameof(Background)].ToString());
                }
                if (data.ContainsKey(nameof(Foreground)))
                {
                    Foreground = CreateBrush(data[nameof(Foreground)].ToString());
                }
                if (data.ContainsKey(nameof(Separator)))
                {
                    Separator = CreateBrush(data[nameof(Separator)].ToString());
                }
                if (data.ContainsKey(nameof(Top)))
                {
                    Top = double.Parse(data[nameof(Top)].ToString());
                }
                if (data.ContainsKey(nameof(Left)))
                {
                    Left = double.Parse(data[nameof(Left)].ToString());
                }
                if (data.ContainsKey(nameof(Width)))
                {
                    Width = double.Parse(data[nameof(Width)].ToString());
                }
                if (data.ContainsKey(nameof(Height)))
                {
                    Height = double.Parse(data[nameof(Height)].ToString());
                }
                if (data.ContainsKey(nameof(AlwaysOnTop)))
                {
                    AlwaysOnTop = bool.Parse(data[nameof(AlwaysOnTop)].ToString());
                }
                if (data.ContainsKey(nameof(Resizable)))
                {
                    Resizable = bool.Parse(data[nameof(Resizable)].ToString());
                }
            }
        }

        public void Save()
        {
            string configPath = GetConfigPath();
            Directory.CreateDirectory(Path.GetDirectoryName(configPath));

            ColorConverter cc = new ColorConverter();
            File.WriteAllText(configPath, JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                [nameof(AllowTransparency)] = AllowTransparency,
                [nameof(Background)] = cc.ConvertToString(Background.Color),
                [nameof(Foreground)] = cc.ConvertToString(Foreground.Color),
                [nameof(Separator)] = cc.ConvertToString(Separator.Color),
                [nameof(Top)] = Top,
                [nameof(Left)] = Left,
                [nameof(Width)] = Width,
                [nameof(Height)] = Height,
                [nameof(AlwaysOnTop)] = AlwaysOnTop,
                [nameof(Resizable)] = Resizable
            }, Formatting.Indented));
        }

        private string GetConfigPath()
        {
            return Path.Combine
            (
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TR2RandoTracker",
                "settings.json"
            );
        }
    }
}