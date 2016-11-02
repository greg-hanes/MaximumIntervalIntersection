using System.Collections.Generic;
using UnityEngine;

public class DisplayFlattenedData : SolverStep
{
    private struct FlattenedIntervalData
    {
        public int Depth;
        public Interval Interval;
    }

    private List<FlattenedIntervalData> m_flattenedIntervals = new List<FlattenedIntervalData>();

    public DisplayFlattenedData(SolverVisualizer visualizer) : base(visualizer)
    {
        IntervalEndPoint[] intervalEndPoints = IntervalIntersectionSolver.SortAndFlattenIntervals(visualizer.InputData);
        Stack<int> openIntervals = new Stack<int>();
        for (int i = 0; i < intervalEndPoints.Length; i++)
        {
            if (intervalEndPoints[i].IntervalEndPointType == IntervalEndPointType.Start)
            {
                openIntervals.Push(intervalEndPoints[i].Value);
            }
            else
            {
                int start = openIntervals.Pop();
                int end = intervalEndPoints[i].Value;
                int height = openIntervals.Count;
                m_flattenedIntervals.Add(new FlattenedIntervalData()
                {
                    Depth = height,
                    Interval = new Interval(start, end)
                });
            }
        }
    }

    protected override void OnDrawControls()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", GUILayout.ExpandWidth(false)))
            SolverVisualizer.MovePreviousStep();

        if (GUILayout.Button(">", GUILayout.ExpandWidth(false)))
            SolverVisualizer.MoveNextStep();

        GUILayout.EndHorizontal();

        GUILayout.Label(@"We're going to rearrange the intervals a bit.");
        GUILayout.Label(@"Now the data is organized so that each level represents the start of a new interval. As we step through the data, we increase the level at any interval starting endpoint, and we decrease the level at any interval end point. 
Visually, it's now much more clear what the answer is.");

        GUILayout.Label("We can just count how tall the stack is, and that is the maximum number of overlapping intervals.");
    }

    protected override void OnDrawCanvas()
    {
        for (int i = 0; i < m_flattenedIntervals.Count; i++)
        {
            FlattenedIntervalData data = m_flattenedIntervals[i];
            Rect r = new Rect(data.Interval.Minimum, data.Depth * 35, data.Interval.Maximum - data.Interval.Minimum, 30);
            GUI.DrawTexture(r, SolverVisualizer.WhiteTexture1x1, ScaleMode.StretchToFill);
        }
    }
}
