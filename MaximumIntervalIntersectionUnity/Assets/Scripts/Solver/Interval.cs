using System;
using System.Text;

/// <summary>
/// Represents an interval with a minimum value and maximum value.
/// </summary>
public struct Interval
{
    /// <summary>
    /// The minimum value of the interval.
    /// </summary>
    public int Minimum
    {
        get;
        private set;
    }

    /// <summary>
    /// The maximum value of the interval.
    /// </summary>
    public int Maximum
    {
        get;
        private set;
    }

    /// <summary>
    /// Constructs an Interval from the given minimum and maximum.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if minimum is greater than maximum.</exception>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    public Interval(int minimum, int maximum)
    {
        if (minimum > maximum)
        {
            throw new InvalidOperationException("minimum must be less than or equal to maximum");
        }
        Minimum = minimum;
        Maximum = maximum;
    }

    public override string ToString()
    {
        // 20 picked arbitrarily.
        // 4 characters for formatting, ~8 characters for the minimum and maximum each before requiring StringBuilder auto expansion.
        StringBuilder sb = new StringBuilder(20);
        sb.Append("[");
        sb.Append(Minimum);
        sb.Append(", ");
        sb.Append(Maximum);
        sb.Append("]");

        return sb.ToString();
    }
}
