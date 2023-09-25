using TRRandoTracker.Core.Tomp.Exes;

namespace TRRandoTracker.Core.Tomp;

public static class TompExeFactory
{
    private static readonly List<AbstractTompExe> _exes = new List<AbstractTompExe>
    {
        new Tomp2EPC(), new Tomp2German(), new Tomp2Japanese(), new Tomp2Multipatch(), new Tomp2UKBox(), new Tomp2CDPatch(),
        new Tomp3International(), new Tomp3Japanese()
    };

    public static AbstractTompExe Get(string processName, byte[] identifier, string checksum)
    {
        foreach (AbstractTompExe exe in _exes)
        {
            if (exe.Identifier.SequenceEqual(identifier))
            {
                return exe;
            }

            if (exe.Hashes != null)
            {
                foreach (string hash in exe.Hashes)
                {
                    if (hash == checksum)
                    {
                        return exe;
                    }
                }
            }
        }

        if (processName.ToLower() == "tomb1main")
        {
            return new Tomb1Main();
        }

        return null;
    }
}