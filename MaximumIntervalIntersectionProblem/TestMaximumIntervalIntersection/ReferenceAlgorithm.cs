using System.Collections.Generic;
using MaximumIntervalIntersectionProblem;

namespace TestMaximumIntervalIntersection
{
    /// <summary>
    /// Limited reference algorithm to compare to optimized solution.
    /// 
    /// Reference solution only works on intervals between 0 and kMaxLength.
    /// 
    /// The algorithm is quite slow and has O(L) memory footprint where L is the range of input values,
    /// but provides a simple reference solution to compare.
    /// 
    /// There is a very low probability that both the reference algorithm
    /// and the real algorithm will provide identically incorrect solutions,
    /// barring systematic flaws in reasoning about the problem.
    /// </summary>
    public class ReferenceAlgorithm
    {
        public const int kMaxLength = 1000;
        public static Solution Solve(Interval[] intervals)
        {
            int[] counters = new int[kMaxLength];

            foreach (var interval in intervals)
            {
                for (int i = interval.Minimum; i <= interval.Maximum; i++)
                {
                    counters[i]++;
                }
            }

            int max = 0;
            for (int i = 0; i < kMaxLength; i++)
            {
                if (counters[i] > max)
                    max = counters[i];
            }

            List<Interval> maxIntervals = new List<Interval>();

            bool onInterval = false;
            int startInterval = 0;
            for (int i = 0; i < kMaxLength; i++)
            {
                bool wasOnInterval = onInterval;
                onInterval = counters[i] == max;

                if (!wasOnInterval && onInterval)
                {
                    startInterval = i;
                }
                else if (wasOnInterval && !onInterval)
                {
                    maxIntervals.Add(new Interval(startInterval, i - 1));
                }
            }


            return new Solution(max, maxIntervals.ToArray());
        }
    }
}
