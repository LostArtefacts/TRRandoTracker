using System.Collections.Generic;
using System.Linq;
using TR2RandoTracker.Core.Tomp2.Exes;

namespace TR2RandoTracker.Core.Tomp2
{
    public static class Tomp2ExeFactory
    {
        private static readonly List<AbstractTomp2Exe> _exes = new List<AbstractTomp2Exe>
        {
            new Tomp2EPC(), new Tomp2German(), new Tomp2Japanese(), new Tomp2Multipatch(), new Tomp2UKBox(), new Tomp2CDPatch()
        };

        public static AbstractTomp2Exe Get(byte[] identifier)
        {
            foreach (AbstractTomp2Exe exe in _exes)
            {
                if (exe.Identifer.SequenceEqual(identifier))
                {
                    return exe;
                }
            }
            return null;
        }
    }
}