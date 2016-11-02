namespace MaximumIntervalIntersectionProblem
{
    /// <summary>
    /// Represents a solution of the maximum interval intersection problem.
    /// 
    /// Contains the maximum number of intersections found, and an array of Interval
    /// which contains the intervals over which the maximum intersections were found.
    /// </summary>
    public struct Solution
    {
        /// <summary>
        /// The maximum number of intersections of the input Intervals.
        /// </summary>
        public int MaximumIntersections
        {
            get;
            private set;
        }

        /// <summary>
        /// The intervals over which the maximum number of intersections was found.
        /// </summary>
        public Interval[] MaximumIntervals
        {
            get;
            private set;
        }
        
        internal Solution(int maximumIntersections, Interval[] maximumIntervals)
        {
            MaximumIntersections = maximumIntersections;
            MaximumIntervals = maximumIntervals;
        }
    }
}
