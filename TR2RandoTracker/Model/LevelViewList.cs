using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TRGE.Core;

namespace TR2RandoTracker.Model
{
    public class LevelViewList : List<LevelView>
    {
        public static LevelViewList Get(IReadOnlyList<TR23ScriptedLevel> allLevels, int currentLevel)
        {
            LevelViewList list = new LevelViewList();
            for (int i = 0; i < allLevels.Count; i++)
            {
                list.Add(new LevelView(allLevels[i])
                {
                    Visible = i <= currentLevel
                });
            }
            return list;
        }
    }
}