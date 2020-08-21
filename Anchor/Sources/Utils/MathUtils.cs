using System;
using System.Collections.Generic;

namespace Anchor.Utils
{
    public class MathUtils
    {
        // Cache
        private static Dictionary<int, double> FibonacciCache = new Dictionary<int, double>();
        public static double Fibonacci(int index)
        {

            if (FibonacciCache.ContainsKey(index))
            {
                return FibonacciCache[index];
            }

            if (index == 0 || index == 1)
            {
                FibonacciCache.Add(index, 0.1);
                return 0.1;
            }
            else
            {
                var value = Fibonacci(index - 1) + Fibonacci(index - 2);
                FibonacciCache.Add(index, value);
                return value;
            }
        }

        public static double NearestFibonacci(double value)
        {
            // Get best scaleStep
            var fibIndex = 0;
            var scaleStep = value;
            while (scaleStep > Fibonacci(fibIndex))
            {
                ++fibIndex;
            }
            scaleStep = Fibonacci(fibIndex);
            return scaleStep;
        }

    }
}
