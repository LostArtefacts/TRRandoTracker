using System.Collections.Generic;
using TRGE.Core;

namespace TRRandoTracker.Model;

public class LevelViewList : List<LevelView>
{
    public static LevelViewList Get(IReadOnlyList<AbstractTRScriptedLevel> allLevels, int currentLevel)
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