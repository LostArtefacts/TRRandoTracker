using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRRandoTracker.Core.Tracker;

namespace TRRandoTracker.Test
{
    class Program
    {
        static void Main()
        {
            TompTracker tt = new TompTracker();
            tt.TrackingChanged += Tt_TrackingChanged;
            Console.Read();
        }

        private static void Tt_TrackingChanged(object sender, TrackingEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}