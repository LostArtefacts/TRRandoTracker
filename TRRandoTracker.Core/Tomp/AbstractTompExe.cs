using System.Diagnostics;
using TRGE.Core;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Core;

public abstract class AbstractTompExe
{
    internal virtual string[] Hashes { get; }
    internal abstract byte[] Identifier { get; }
    internal abstract int TitleAddress { get; }
    internal abstract int LevelAddress { get; }
    internal virtual int LevelCompleteAddress => 0;
    internal abstract int CreditsFlag { get; }
    public abstract string Name { get; }
    internal abstract string ScriptPath { get; }
    internal virtual ITracker CreateTracker(Process process, List<AbstractTRScriptedLevel> levels)
    {
        return new DefaultTracker(this, process, levels);
    }

    protected Version CalculateProductVersion(string exePath)
    {
        try
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(exePath);
            int[] parts = new int[] { 0, 0, 0 };
            string[] productParts = versionInfo.ProductVersion.Split('.');
            for (int i = 0; i < productParts.Length; i++)
            {
                int j = 0;
                string part = string.Empty;
                while (j < productParts[i].Length && char.IsDigit(productParts[i][j]))
                {
                    part += productParts[i][j++];
                }
                parts[i] = int.Parse(part);
            }
            return new Version(parts[0], parts[1], parts[2]);
        }
        catch
        {
            return null;
        }
    }
}