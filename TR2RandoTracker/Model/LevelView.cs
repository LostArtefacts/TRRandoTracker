using TR2RandoTracker.Core.Extensions;
using TRGE.Core;

namespace TR2RandoTracker.Model
{
    public class LevelView
    {
        public bool Visible { get; set; }
        public string LevelName { get; private set; }
        public int LevelSequence => _level.Sequence;
        public bool IsFinalLevel => _level.IsFinalLevel;
        public bool Unarmed => _level.RemovesWeapons;
        public bool Ammoless => _level.RemovesAmmo;

        private readonly TR23ScriptedLevel _level;

        public LevelView(TR23ScriptedLevel level)
        {
            _level = level;
            LevelName = _level.GetDecodedName();
        }
    }
}