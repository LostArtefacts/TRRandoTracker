using TRRandoTracker.Core.Extensions;
using TRGE.Core;
using System.ComponentModel;

namespace TRRandoTracker.Model
{
    public class LevelView : BaseNotifyPropertyChanged
    {
        private bool _visible;
        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    NotifyPropertyChanged();
                    SetLevelName();
                }
            }
        }

        private string _levelName;
        public string LevelName
        {
            get => _levelName;
            private set
            {
                if (value != _levelName)
                {
                    _levelName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _levelSequence;
        public int LevelSequence
        {
            get => _levelSequence;
            private set
            {
                if (value != _levelSequence)
                {
                    _levelSequence = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _isFinalLevel;
        public bool IsFinalLevel
        {
            get => _isFinalLevel;
            private set
            {
                if (_isFinalLevel != value)
                {
                    _isFinalLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _unarmed;
        public bool Unarmed
        {
            get => _unarmed;
            private set
            {
                if (value != _unarmed)
                {
                    _unarmed = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _ammoless;
        public bool Ammoless
        {
            get => _ammoless;
            private set
            {
                if (value != _ammoless)
                {
                    _ammoless = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _mediless;
        public bool Mediless
        {
            get => _mediless;
            private set
            {
                if (value != _mediless)
                {
                    _mediless = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool MedilessSupported => _level is TR1ScriptedLevel;

        private readonly AbstractTRScriptedLevel _level;

        public LevelView(AbstractTRScriptedLevel level)
        {
            _level = level;

            LevelSequence = level.Sequence;
            IsFinalLevel = level.IsFinalLevel;
            Unarmed = level.RemovesWeapons;
            Ammoless = level.RemovesAmmo;
            Mediless = level is TR1ScriptedLevel lvl && lvl.RemovesMedis;

            SetLevelName();

            Settings.Instance.PropertyChanged += SettingsChanged;
        }

        private void SettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Instance.LevelPlaceholder))
            {
                SetLevelName();
            }
        }

        private void SetLevelName()
        {
            if (_visible)
            {
                if (_level is TR2ScriptedLevel)
                {
                    LevelName = (_level as TR2ScriptedLevel).GetDecodedName();
                }
                else
                {
                    LevelName = _level.Name;
                }
            }
            else
            {
                LevelName = Settings.Instance.LevelPlaceholder;
            }
        }
    }
}