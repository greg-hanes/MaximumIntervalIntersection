using System.Collections.Generic;
using UnityEngine;

public class SearchForMaximaStep : DisplayFlattenedData
{
    private int m_currentMaximaIndex = 0;
    private int m_maximumIntersectionCount;
    private int m_currentIntersectionCount;
    private List<Interval> m_maximumIntervals = new List<Interval>();
    
    public SearchForMaximaStep(SolverVisualizer visualizer) : base(visualizer)
    {
        m_currentIntersectionCount = 1;
        m_maximumIntersectionCount = 1;
    }

    protected override void OnDrawControls()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", GUILayout.ExpandWidth(false)))
        {
            if (m_currentMaximaIndex == 0)
                SolverVisualizer.MovePreviousStep();
            else
            {
                m_currentMaximaIndex--;
                UpdateIntersectionCounts();
            }
        }

        if (GUILayout.Button(">", GUILayout.ExpandWidth(false)))
        {
            if (m_currentMaximaIndex == SolverVisualizer.IntervalEndPoints.Length - 1)
                SolverVisualizer.MoveNextStep();
            else
            {
                m_currentMaximaIndex++;
                UpdateIntersectionCounts();
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Label(@"The way we count the maximum number of overlaps is the same as how you would count the depth of nested parenthesis. When you encounter a starting parenthesis, you add one to the depth. When you encounter a closing parenthesis, you subtract one.");

        GUILayout.Label(@"Now we step through the end points and keep a running total of both the maximum depth we've found so far, as well as the start and end of the intervals.");
        GUILayout.Label(@"When we count a higher number than we have before, we can get rid of the previously maximum intervals.");
        GUILayout.Label("Current Intersection Count: " + m_currentIntersectionCount);
        GUILayout.Label("Maximum Intersection Count: " + m_maximumIntersectionCount);
    }

    protected override void OnDrawCanvas()
    {
        base.OnDrawCanvas();

        Stack<int> openIntervals = new Stack<int>();
        for (int i = 0; i < SolverVisualizer.IntervalEndPoints.Length; i++)
        {
            IntervalEndPoint intervalEndPoint = SolverVisualizer.IntervalEndPoints[i];
            if (i == m_currentMaximaIndex)
            {
                Rect line = new Rect(intervalEndPoint.Value, 0, 1, 600);
                GUI.DrawTexture(line, SolverVisualizer.RedTexture1x1, ScaleMode.StretchToFill);
            }
        }

        for (int i = 0; i < m_maximumIntervals.Count; i++)
        {
            Interval interval = m_maximumIntervals[i];

            Rect line = new Rect(interval.Minimum, 0, interval.Maximum - interval.Minimum, 30);
            GUI.DrawTexture(line, SolverVisualizer.RedTexture1x1, ScaleMode.StretchToFill);
        }
    }

    private void UpdateIntersectionCounts()
    {
        m_currentIntersectionCount = 0;
        m_maximumIntersectionCount = 0;
        m_maximumIntervals.Clear();

        int maximumIntervalStart = 0;
        int maximumIntervalEnd = 0;
        bool onMaximumInterval = false;

        // Copied and pasted from the actual solver.
        // The original code isn't easily adapted to producing partial solutions.
        // Since this is just a demo, I'm not terribly concerned with the duplication of code.
        for (int i = 0; i <= m_currentMaximaIndex; i++)
        {
            IntervalEndPoint endpoint = SolverVisualizer.IntervalEndPoints[i];

            switch (endpoint.IntervalEndPointType)
            {
            case IntervalEndPointType.Start:
                m_currentIntersectionCount++;
                break;
            case IntervalEndPointType.End:
                m_currentIntersectionCount--;
                break;
            }

            if (m_currentIntersectionCount > m_maximumIntersectionCount)
            {
                // New maximum! Clear previous intervals.
                m_maximumIntervals.Clear();
                onMaximumInterval = true;
                m_maximumIntersectionCount = m_currentIntersectionCount;
                maximumIntervalStart = endpoint.Value;
            }
            else if (m_currentIntersectionCount == m_maximumIntersectionCount && !onMaximumInterval)
            {
                // Equal to maximum, but not on the interval of the previous maximum. Start a new interval!
                onMaximumInterval = true;
                maximumIntervalStart = endpoint.Value;
            }
            else if (m_currentIntersectionCount < m_maximumIntersectionCount && onMaximumInterval)
            {
                // Less than maximum, and on an interval, so we're done with this interval.
                maximumIntervalEnd = endpoint.Value;
                onMaximumInterval = false;

                // This is somewhat of a hack.
                // In some cases, bordering intervals with maximum intersections (i.e., [0, 9], [10, 20]) can occur.
                // This is erroneous, as all interval endpoints are inclusive.
                // There's no easy way to handle this situation without looking ahead and determining the next interval without
                //      committing to the current interval, which could get messy.
                // So, we just check the last interval and if the new interval borders it, we combine the intervals in lieu of
                //      adding a new one.
                if (m_maximumIntervals.Count != 0 && m_maximumIntervals[m_maximumIntervals.Count - 1].Maximum + 1 == maximumIntervalStart)
                {
                    Interval lastInterval = m_maximumIntervals[m_maximumIntervals.Count - 1];
                    Interval updatedInterval = new Interval(lastInterval.Minimum, maximumIntervalEnd);
                    m_maximumIntervals[m_maximumIntervals.Count - 1] = updatedInterval;
                }
                else
                {
                    m_maximumIntervals.Add(new Interval(maximumIntervalStart, maximumIntervalEnd));
                }
            }
        }
    }
}

