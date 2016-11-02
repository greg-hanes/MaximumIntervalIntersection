using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MaximumIntervalIntersectionProblem
{
    public static class IntervalIntersectionSolver
    {
        /// <summary>
        /// Given a set of input intervals, finds the maximum number of overlapping intervals,
        /// in addition to the intervals over which the number of intersections is maximum.
        /// 
        /// Note: The intervals returned are not a subset of the input.
        /// 
        /// For example, given the intervals [0, 10] and [5, 12], the maximum overlap is 2,
        /// ocurring on the interval [5, 10]
        /// 
        /// All interval end-points are inclusive. That is, [0, 1] overlaps [1, 2] on the interval [1, 1].
        /// 
        /// The runtime is O(n*log(n)). The algorithm requires a pre-sort, which dominates the runtime.
        /// Without the sort, the algorithm is O(n).
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if intervals is null.</exception>
        /// <param name="intervals">A non-null list of intervals.</param>
        /// <returns>The solution containing the maximum number of overlapping intervals and the intervals in which the overlap count is maximum.</returns>
        public static Solution Solve(Interval[] intervals)
        {
            if (intervals == null)
                throw new InvalidOperationException("argument intervals cannot be null");

            if (intervals.Length == 0)
                return new Solution(0, new Interval[0]);


            IntervalEndPoint[] intervalEndPoints = SortAndFlattenIntervals(intervals);
            
            int intersectionCounter = 0;
            int maximumIntersectionCount = 0;
            int maximumIntervalStart = 0;
            int maximumIntervalEnd = 0;
            bool onMaximumInterval = false;
            List<Interval> maximumIntervals = new List<Interval>();

            // Algorithm is roughly as follows:
            //  Iterate over all interval end points.
            //  When a start interval endpoint is encountered, increment the intersection counter.
            //  When an end interval endpoint is encountered, decrement the intersection counter.
            //  If the intersection counter exceeds the maximum we've seen, then we know we've
            //      found a preliminary maximum intersection.
            //  So we set a flag to remember that we've started a new maximum intersection interval
            //  If the intersection counter decreases and the flag is set,
            //      then the maximum intersection interval has ended
            //      So we save it in the maximumIntervals list.
            //  If we find a maximum intersection interval that exceeds previous ones,
            //      we clear out the list and reset the flag.
            //  If the intersection counter is equal to the previous maximum, then we've found
            //      a second interval that has a maximum number of intersections.
            for (int i = 0; i < intervalEndPoints.Length; i++)
            {
                IntervalEndPoint currentEndPoint = intervalEndPoints[i];

                switch (currentEndPoint.IntervalEndPointType)
                {
                case IntervalEndPointType.Start:
                    intersectionCounter++;
                    break;
                case IntervalEndPointType.End:
                    intersectionCounter--;
                    break;
                default:
                    Debug.Assert(false, "Unknown value for enum type IntervalEndPointType: " + currentEndPoint.IntervalEndPointType);
                    break;
                }

                if (intersectionCounter > maximumIntersectionCount)
                {
                    // New maximum! Clear previous intervals.
                    maximumIntervals.Clear();
                    onMaximumInterval = true;
                    maximumIntersectionCount = intersectionCounter;
                    maximumIntervalStart = currentEndPoint.Value;
                }
                else if (intersectionCounter == maximumIntersectionCount && !onMaximumInterval)
                {
                    // Equal to maximum, but not on the interval of the previous maximum. Start a new interval!
                    onMaximumInterval = true;
                    maximumIntervalStart = currentEndPoint.Value;
                }
                else if (intersectionCounter < maximumIntersectionCount && onMaximumInterval)
                {
                    // Less than maximum, and on an interval, so we're done with this interval.
                    maximumIntervalEnd = currentEndPoint.Value;
                    onMaximumInterval = false;

                    // This is somewhat of a hack.
                    // In some cases, bordering intervals with maximum intersections (i.e., [0, 9], [10, 20]) can occur.
                    // This is erroneous, as all interval endpoints are inclusive.
                    // There's no easy way to handle this situation without looking ahead and determining the next interval without
                    //      committing to the current interval, which could get messy.
                    // So, we just check the last interval and if the new interval borders it, we combine the intervals in lieu of
                    //      adding a new one.
                    if (maximumIntervals.Count != 0 && maximumIntervals[maximumIntervals.Count - 1].Maximum + 1 == maximumIntervalStart)
                    {
                        Interval lastInterval = maximumIntervals[maximumIntervals.Count - 1];
                        Interval updatedInterval = new Interval(lastInterval.Minimum, maximumIntervalEnd);
                        maximumIntervals[maximumIntervals.Count - 1] = updatedInterval;
                    }
                    else
                    {
                        maximumIntervals.Add(new Interval(maximumIntervalStart, maximumIntervalEnd));
                    }
                }
            }

            return new Solution(maximumIntersectionCount, maximumIntervals.ToArray());
        }
        
        /// <summary>
        /// Utility method to convert an array of Interval to a sorted array of IntervalEndPoints
        /// </summary>
        /// <param name="intervals">Array of Interval to convert and sort.</param>
        /// <returns>Sorted array of IntervalEndPoints.</returns>
        private static IntervalEndPoint[] SortAndFlattenIntervals(Interval[] intervals)
        {
            IntervalEndPoint[] intervalEndPoints = new IntervalEndPoint[intervals.Length * 2];

            for (int i = 0; i < intervals.Length; i++)
            {
                intervalEndPoints[2 * i] = new IntervalEndPoint(intervals[i].Minimum, IntervalEndPointType.Start);
                intervalEndPoints[2 * i + 1] = new IntervalEndPoint(intervals[i].Maximum, IntervalEndPointType.End);
            }
            Array.Sort(intervalEndPoints, IntervalEndPoint.Comparison);
            return intervalEndPoints;
        }
    }
}
