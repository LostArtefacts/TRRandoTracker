using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TR2RandoTracker.Core.Tracker;

namespace TR2RandoTracker.Test
{
    class Program
    {
        static void Main()
        {
            Tomp2Tracker tt = new Tomp2Tracker();
            tt.TrackingChanged += Tt_TrackingChanged;
            Console.Read();
        }

        private static void Tt_TrackingChanged(object sender, TrackingEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}