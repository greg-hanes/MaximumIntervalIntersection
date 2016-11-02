

namespace MaximumIntervalIntersectionProblem
{
    internal enum IntervalEndPointType
    {
        Start,
        End,
    }

    /// <summary>
    /// Represents an end-point of an interval. That is, it can be either the start
    /// of an interval, or the end of an interval, and contains a value representing where
    /// the start or end of the interval is.
    /// </summary>
    internal struct IntervalEndPoint
    {
        public int Value
        {
            get;
            private set;
        }

        public IntervalEndPointType IntervalEndPointType
        {
            get;
            private set;
        }

        public IntervalEndPoint(int value, IntervalEndPointType endpointType)
        {
            Value = value;
            IntervalEndPointType = endpointType;
        }

        /// <summary>
        /// Comparison for sorting interval endpoints.
        /// </summary>
        /// <param name="endpoint1">First IntervalEndPoint to compare.</param>
        /// <param name="endpoint2">Second IntervalEndPoint to compare.</param>
        /// <returns>A negative number of endpoint1 should come before endpoint2,
        /// a positive number if endpoint2 should come before endpoint1, or zero if they
        /// are equivalent.</returns>
        public static int Comparison(IntervalEndPoint endpoint1, IntervalEndPoint endpoint2)
        {
            // Sort ascending, but given same values, put start endpoints first.
            if (endpoint1.Value == endpoint2.Value)
            {
                if (endpoint1.IntervalEndPointType == endpoint2.IntervalEndPointType)
                    return 0;
                else if (endpoint1.IntervalEndPointType == IntervalEndPointType.Start)
                    return -1;
                else
                    return 1;
            }
            return endpoint1.Value - endpoint2.Value;
        }
    }
}
