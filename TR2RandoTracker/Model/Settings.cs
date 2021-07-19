using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace TR2RandoTracker.Model
{
    public class Settings : INotifyPropertyChanged
    {
        private static Settings _instance;

        private static readonly FontFamily[] _fontFamilyOptions;
        private static readonly TextAlignment[] _textAlignmentOptions;

        static Settings()
        {
            List<FontFamily> families = Fonts.SystemFontFamilies.ToList();
            families.Sort(delegate (FontFamily ff1, FontFamily ff2)
            {
                return ff1.Source.CompareTo(ff2.Source);
            });
            _fontFamilyOptions = families.ToArray();

            _textAlignmentOptions = Enum.GetValues(typeof(TextAlignment)).Cast<TextAlignment>().ToArray();
        }

        public FontFamily[] FontFamilyOptions => _fontFamilyOptions;
        public TextAlignment[] AlignmentOptions => _textAlignmentOptions;

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

        #region Main Window
        
        private const bool _defaultAllowTransparency = false;
        private bool _allowTransparency;
        public bool AllowTransparency
        {
            get => _allowTransparency;
            set
            {
                _allowTransparency = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultBackground = "#B3151515";
        private SolidColorBrush _background;
        public SolidColorBrush Background
        {
            get => _background;
            set
            {
                _background = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultUseBackgroundImage = false;
        private bool _useBackgroundImage;
        public bool UseBackgroundImage
        {
            get => _useBackgroundImage;
            set
            {
                _useBackgroundImage = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultBackgroundImage = "";
        private string _backgroundImage;
        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                FirePropertyChanged();
            }
        }

        private const double _defaultTop = 0;
        private double _top;
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                FirePropertyChanged();
            }
        }

        private const double _defaultLeft = 0;
        private double _left;
        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                FirePropertyChanged();
            }
        }

        private const double _defaultWidth = 300;
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                FirePropertyChanged();
            }
        }

        private const double _defaultHeight = 586;
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultOnTop = false;
        private bool _onTop;
        public bool AlwaysOnTop
        {
            get => _onTop;
            set
            {
                _onTop = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultResizable = true;
        private bool _resizable;
        public bool Resizable
        {
            get => _resizable;
            set
            {
                _resizable = value;
                FirePropertyChanged();
            }
        }

        #endregion

        #region Title

        private const bool _defaultShowTitle = true;
        private bool _showTitle;
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                _showTitle = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultTitle = "TR II Randomizer";
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultTitleForeground = "#FFF";
        private SolidColorBrush _titleForeground;
        public SolidColorBrush TitleForeground
        {
            get => _titleForeground;
            set
            {
                _titleForeground = value;
                FirePropertyChanged();
            }
        }

        private const int _defaultTitleSize = 18;
        private int _titleSize;
        public int TitleSize
        {
            get => _titleSize;
            set
            {
                _titleSize = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultTitleBold = true;
        private bool _titleBold;

        public bool BoldTitle
        {
            get => _titleBold;
            set
            {
                _titleBold = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultTitleItalic = false;
        private bool _titleItalic;
        public bool ItalicTitle
        {
            get => _titleItalic;
            set
            {
                _titleItalic = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultTitleUnderline = false;
        private bool _titleUnderline;
        public bool UnderlineTitle
        {
            get => _titleUnderline;
            set
            {
                _titleUnderline = value;
                FirePropertyChanged();
            }
        }

        private static readonly FontFamily _defaultTitleFontFamily = new FontFamily("Segoe UI");
        private FontFamily _titleFontFamily;
        public FontFamily TitleFontFamily
        {
            get => _titleFontFamily;
            set
            {
                _titleFontFamily = value;
                FirePropertyChanged();
            }
        }

        private const TextAlignment _defaultTitleAlignment = TextAlignment.Center;
        private TextAlignment _titleAlignment;
        public TextAlignment TitleAlignment
        {
            get => _titleAlignment;
            set
            {
                _titleAlignment = value;
                FirePropertyChanged();
            }
        }

        #endregion

        #region Level List

        private const string _defaultForeground = "#FFF";
        private SolidColorBrush _foreground;
        public SolidColorBrush Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                FirePropertyChanged();
            }
        }

        private const int _defaultIndexWidth = 21;
        private int _indexWidth;
        public int IndexWidth
        {
            get => _indexWidth;
            set
            {
                _indexWidth = value;
                FirePropertyChanged();
            }
        }

        private const int _defaultLevelSize = 15;
        private int _levelSize;
        public int LevelSize
        {
            get => _levelSize;
            set
            {
                _levelSize = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultLevelBold = false;
        private bool _levelBold;

        public bool BoldLevel
        {
            get => _levelBold;
            set
            {
                _levelBold = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultLevelItalic = false;
        private bool _levelItalic;
        public bool ItalicLevel
        {
            get => _levelItalic;
            set
            {
                _levelItalic = value;
                FirePropertyChanged();
            }
        }

        private static readonly FontFamily _defaultLevelFontFamily = new FontFamily("Segoe UI");
        private FontFamily _levelFontFamily;
        public FontFamily LevelFontFamily
        {
            get => _levelFontFamily;
            set
            {
                _levelFontFamily = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultPlaceholder = "?";
        private string _placeholder;
        public string LevelPlaceholder
        {
            get => _placeholder;
            set
            {
                _placeholder = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultSeparator = "#FF353535";
        private SolidColorBrush _separator;
        public SolidColorBrush Separator
        {
            get => _separator;
            set
            {
                _separator = value;
                FirePropertyChanged();
            }
        }

        #endregion

        #region Timer

        private const bool _defaultShowTimer = true;
        private bool _showTimer;
        public bool ShowTimer
        {
            get => _showTimer;
            set
            {
                _showTimer = value;
                FirePropertyChanged();
            }
        }

        private const string _defaultTimerForeground = "#FFF";
        private SolidColorBrush _timerForeground;
        public SolidColorBrush TimerForeground
        {
            get => _timerForeground;
            set
            {
                _timerForeground = value;
                FirePropertyChanged();
            }
        }

        private const int _defaultTimerSize = 30;
        private int _timerSize;
        public int TimerSize
        {
            get => _timerSize;
            set
            {
                _timerSize = value;
                FirePropertyChanged();
            }
        }

        private const int _defaultTimerAltSize = 18;
        private int _timerAltSize;
        public int TimerAltSize
        {
            get => _timerAltSize;
            set
            {
                _timerAltSize = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultTimerBold = false;
        private bool _timerBold;
        public bool BoldTimer
        {
            get => _timerBold;
            set
            {
                _timerBold = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultTimerItalic = false;
        private bool _timerItalic;
        public bool ItalicTimer
        {
            get => _timerItalic;
            set
            {
                _timerItalic = value;
                FirePropertyChanged();
            }
        }

        private const bool _defaultTimerUnderline = false;
        private bool _timerUnderline;
        public bool UnderlineTimer
        {
            get => _timerUnderline;
            set
            {
                _timerUnderline = value;
                FirePropertyChanged();
            }
        }

        private static readonly FontFamily _defaultTimerFontFamily = new FontFamily("Segoe UI");
        private FontFamily _timerFontFamily;
        public FontFamily TimerFontFamily
        {
            get => _timerFontFamily;
            set
            {
                _timerFontFamily = value;
                FirePropertyChanged();
            }
        }

        private const TextAlignment _defaultTimerAlignment = TextAlignment.Right;
        private TextAlignment _timerAlignment;
        public TextAlignment TimerAlignment
        {
            get => _timerAlignment;
            set
            {
                _timerAlignment = value;
                FirePropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private Settings()
        {
            AllowTransparency = _defaultAllowTransparency;
            Background = CreateBrush(_defaultBackground);
            UseBackgroundImage = _defaultUseBackgroundImage;
            BackgroundImage = _defaultBackgroundImage;
            Top = _defaultTop;
            Left = _defaultLeft;
            Width = _defaultWidth;
            Height = _defaultHeight;
            AlwaysOnTop = _defaultOnTop;
            Resizable = _defaultResizable;

            ShowTitle = _defaultShowTitle;
            Title = _defaultTitle;
            TitleForeground = CreateBrush(_defaultTitleForeground);
            TitleSize = _defaultTitleSize;
            BoldTitle = _defaultTitleBold;
            ItalicTitle = _defaultTitleItalic;
            UnderlineTitle = _defaultTitleUnderline;
            TitleFontFamily = _defaultTitleFontFamily;
            TitleAlignment = _defaultTitleAlignment;

            Foreground = CreateBrush(_defaultForeground);
            IndexWidth = _defaultIndexWidth;
            LevelSize = _defaultLevelSize;
            BoldLevel = _defaultLevelBold;
            ItalicLevel = _defaultLevelItalic;
            LevelFontFamily = _defaultLevelFontFamily;
            LevelPlaceholder = _defaultPlaceholder;
            Separator = CreateBrush(_defaultSeparator);

            ShowTimer = _defaultShowTimer;
            TimerForeground = CreateBrush(_defaultTimerForeground);
            TimerSize = _defaultTimerSize;
            TimerAltSize = _defaultTimerAltSize;
            BoldTimer = _defaultTimerBold;
            ItalicTimer = _defaultTimerItalic;
            UnderlineTimer = _defaultTimerUnderline;
            TimerFontFamily = _defaultTimerFontFamily;
            TimerAlignment = _defaultTimerAlignment;

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

                #region Main Window
                if (data.ContainsKey(nameof(AllowTransparency)))
                {
                    AllowTransparency = bool.Parse(data[nameof(AllowTransparency)].ToString());
                }
                if (data.ContainsKey(nameof(Background)))
                {
                    Background = CreateBrush(data[nameof(Background)].ToString());
                }
                if (data.ContainsKey(nameof(UseBackgroundImage)))
                {
                    UseBackgroundImage = bool.Parse(data[nameof(UseBackgroundImage)].ToString());
                }
                if (data.ContainsKey(nameof(BackgroundImage)))
                {
                    BackgroundImage = data[nameof(BackgroundImage)].ToString();
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
                #endregion

                #region Title
                if (data.ContainsKey(nameof(ShowTitle)))
                {
                    ShowTitle = bool.Parse(data[nameof(ShowTitle)].ToString());
                }
                if (data.ContainsKey(nameof(Title)))
                {
                    Title = data[nameof(Title)].ToString();
                }
                if (data.ContainsKey(nameof(TitleForeground)))
                {
                    TitleForeground = CreateBrush(data[nameof(TitleForeground)].ToString());
                }
                else if(data.ContainsKey(nameof(Foreground)))
                {
                    TitleForeground = CreateBrush(data[nameof(Foreground)].ToString());
                }
                if (data.ContainsKey(nameof(TitleSize)))
                {
                    TitleSize = int.Parse(data[nameof(TitleSize)].ToString());
                }
                if (data.ContainsKey(nameof(BoldTitle)))
                {
                    BoldTitle = bool.Parse(data[nameof(BoldTitle)].ToString());
                }
                if (data.ContainsKey(nameof(ItalicTitle)))
                {
                    ItalicTitle = bool.Parse(data[nameof(ItalicTitle)].ToString());
                }
                if (data.ContainsKey(nameof(UnderlineTitle)))
                {
                    UnderlineTitle = bool.Parse(data[nameof(UnderlineTitle)].ToString());
                }
                if (data.ContainsKey(nameof(TitleFontFamily)))
                {
                    TitleFontFamily = new FontFamily(data[nameof(TitleFontFamily)].ToString());
                }
                if (data.ContainsKey(nameof(TitleAlignment)))
                {
                    TitleAlignment = (TextAlignment)Enum.Parse(typeof(TextAlignment), data[nameof(TitleAlignment)].ToString());
                }
                #endregion

                #region Level List
                if (data.ContainsKey(nameof(Foreground)))
                {
                    Foreground = CreateBrush(data[nameof(Foreground)].ToString());
                }
                if (data.ContainsKey(nameof(IndexWidth)))
                {
                    IndexWidth = int.Parse(data[nameof(IndexWidth)].ToString());
                }
                if (data.ContainsKey(nameof(LevelSize)))
                {
                    LevelSize = int.Parse(data[nameof(LevelSize)].ToString());
                }
                if (data.ContainsKey(nameof(BoldLevel)))
                {
                    BoldLevel = bool.Parse(data[nameof(BoldLevel)].ToString());
                }
                if (data.ContainsKey(nameof(ItalicLevel)))
                {
                    ItalicLevel = bool.Parse(data[nameof(ItalicLevel)].ToString());
                }
                if (data.ContainsKey(nameof(LevelFontFamily)))
                {
                    LevelFontFamily = new FontFamily(data[nameof(LevelFontFamily)].ToString());
                }
                if (data.ContainsKey(nameof(LevelPlaceholder)))
                {
                    LevelPlaceholder = data[nameof(LevelPlaceholder)].ToString();
                }
                if (data.ContainsKey(nameof(Separator)))
                {
                    Separator = CreateBrush(data[nameof(Separator)].ToString());
                }
                #endregion

                #region Timer
                if (data.ContainsKey(nameof(ShowTimer)))
                {
                    ShowTimer = bool.Parse(data[nameof(ShowTimer)].ToString());
                }
                if (data.ContainsKey(nameof(TimerForeground)))
                {
                    TimerForeground = CreateBrush(data[nameof(TimerForeground)].ToString());
                }
                else if (data.ContainsKey(nameof(Foreground)))
                {
                    TimerForeground = CreateBrush(data[nameof(Foreground)].ToString());
                }
                if (data.ContainsKey(nameof(TimerSize)))
                {
                    TimerSize = int.Parse(data[nameof(TimerSize)].ToString());
                }
                if (data.ContainsKey(nameof(TimerAltSize)))
                {
                    TimerAltSize = int.Parse(data[nameof(TimerAltSize)].ToString());
                }
                if (data.ContainsKey(nameof(BoldTimer)))
                {
                    BoldTimer = bool.Parse(data[nameof(BoldTimer)].ToString());
                }
                if (data.ContainsKey(nameof(ItalicTimer)))
                {
                    ItalicTimer = bool.Parse(data[nameof(ItalicTimer)].ToString());
                }
                if (data.ContainsKey(nameof(UnderlineTimer)))
                {
                    UnderlineTimer = bool.Parse(data[nameof(UnderlineTimer)].ToString());
                }
                if (data.ContainsKey(nameof(TimerFontFamily)))
                {
                    TimerFontFamily = new FontFamily(data[nameof(TimerFontFamily)].ToString());
                }
                if (data.ContainsKey(nameof(TimerAlignment)))
                {
                    TimerAlignment = (TextAlignment)Enum.Parse(typeof(TextAlignment), data[nameof(TimerAlignment)].ToString());
                }
                #endregion
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
                [nameof(UseBackgroundImage)] = UseBackgroundImage,
                [nameof(BackgroundImage)] = BackgroundImage,
                [nameof(Top)] = Top,
                [nameof(Left)] = Left,
                [nameof(Width)] = Width,
                [nameof(Height)] = Height,
                [nameof(AlwaysOnTop)] = AlwaysOnTop,
                [nameof(Resizable)] = Resizable,

                [nameof(ShowTitle)] = ShowTitle,
                [nameof(Title)] = Title,
                [nameof(TitleForeground)] = TitleForeground,
                [nameof(TitleSize)] = TitleSize,
                [nameof(BoldTitle)] = BoldTitle,
                [nameof(ItalicTitle)] = ItalicTitle,
                [nameof(UnderlineTitle)] = UnderlineTitle,
                [nameof(TitleFontFamily)] = TitleFontFamily.Source,
                [nameof(TitleAlignment)] = TitleAlignment,

                [nameof(Foreground)] = cc.ConvertToString(Foreground.Color),
                [nameof(IndexWidth)] = IndexWidth,
                [nameof(LevelSize)] = LevelSize,
                [nameof(BoldLevel)] = BoldLevel,
                [nameof(ItalicLevel)] = ItalicLevel,
                [nameof(LevelFontFamily)] = LevelFontFamily.Source,
                [nameof(LevelPlaceholder)] = LevelPlaceholder,
                [nameof(Separator)] = cc.ConvertToString(Separator.Color),

                [nameof(ShowTimer)] = ShowTimer,
                [nameof(TimerForeground)] = cc.ConvertToString(TimerForeground.Color),
                [nameof(TimerSize)] = TimerSize,
                [nameof(TimerAltSize)] = TimerAltSize,
                [nameof(BoldTimer)] = BoldTimer,
                [nameof(ItalicTimer)] = ItalicTimer,
                [nameof(UnderlineTimer)] = UnderlineTimer,
                [nameof(TimerFontFamily)] = TimerFontFamily.Source,
                [nameof(TimerAlignment)] = TimerAlignment

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