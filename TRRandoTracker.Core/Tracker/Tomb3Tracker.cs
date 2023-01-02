using System.Collections.Generic;
using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Extensions;

namespace TRRandoTracker.Core.Tracker
{
    internal class Tomb3Tracker : DefaultTracker
    {
        private readonly Dictionary<int, int> _cutscenes;

        public Tomb3Tracker(AbstractTompExe trackingExe, Process process, List<AbstractTRScriptedLevel> levels)
            : base(trackingExe, process, levels)
        {
            _cutscenes = new Dictionary<int, int>();
            foreach (AbstractTRScriptedLevel level in levels)
            {
                if (level.HasCutScene)
                {
                    _cutscenes[levels.Count + _cutscenes.Count + 1] = level.Sequence;
                }
            }
        }

        public override void Track()
        {
            if (_process.HasExited)
            {
                return;
            }

            Level = _process.Read<int>(_trackingExe.LevelAddress);
            if (Level < 0 || Level > _levels.Count)
            {
                IsTitle = true;
                Level = 0;
            }
            else
            {
                if (_cutscenes.ContainsKey(Level))
                {
                    Level = _cutscenes[Level];
                }

                IsTitle = false;
                IsCredits = Level != -1 && _process.Read<int>(_trackingExe.CreditsFlag) == 1;
            }
        }

        public override bool InterpretLevel(int level)
        {
            return true;
        }
    }
}
