using System;

namespace AI_Project
{
    static class RNG
    {
        private static readonly Random rand;

        static RNG()
        {
            rand = new Random();
        }

        public static int Next(int max)
        {
            return rand.Next(max);
        }

        public static int Next(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static double NextDouble(double max)
        {
            return rand.NextDouble() * max;
        }

        public static Double NextDouble()
        {
            return rand.NextDouble();
        }

        public static int[] NextInts(int count, int max)
        {
            return NextInts(count, 0, max);
        }
        public static int[] NextInts(int count, int min, int max)
        {
            int[] items = new int[count];
            for (var i = 0; i < count; i++)
            {
                items[i] = rand.Next(min, max);
            }
            return items;
        }

        public static double[] NextIntsD(int count, int max)
        {
            return NextIntsD(count, 0, max);
        }
        public static double[] NextIntsD(int count, int min, int max)
        {
            var items = new double[count];
            for (var i = 0; i < count; i++)
            {
                items[i] = rand.Next(min, max);
            }
            return items;
        }
    }
}
