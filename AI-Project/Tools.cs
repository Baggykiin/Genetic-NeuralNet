using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    static class Tools
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private static long[] intervals = new long[10];
        private static int index = 0;
        public static void StartMeasuring()
        {
            stopwatch.Restart();
        }

        public static void Interval()
        {
            intervals[index] = stopwatch.ElapsedTicks;
            index++;
        }

        public static float StopMeasuring(bool accurate = false)
        {
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMsAccurate();
            if (accurate)
            {
                for (var i = 0; i < index; i++)
                {
                    Console.WriteLine("Interval at ns: " + stopwatch.CalculateTicks(intervals[index]) * 1000);
                }
                index = 0;
                Console.WriteLine("Done measuring. Elapsed ns: " + elapsed * 1000);
            }
            else
            {
                for (var i = 0; i < index; i++)
                {
                    Console.WriteLine("Interval at ms: " + stopwatch.CalculateTicks(intervals[index]));
                }
                Console.WriteLine("Done measuring. Elapsed ms: " + elapsed);
            }
            return elapsed;
        }
    }
}
