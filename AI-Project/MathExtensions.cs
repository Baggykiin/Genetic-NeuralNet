using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project
{
    static class MathExtensions
    {
        public static double Sigmoid(double t)
        {
            return 1 / (1 + Math.Pow(Math.E, -t / GlobalParameters.SigmoidP));
        }

        public static double RandomClamped()
        {
            return RNG.NextDouble() * 2 - 1;
        }
    }
}
