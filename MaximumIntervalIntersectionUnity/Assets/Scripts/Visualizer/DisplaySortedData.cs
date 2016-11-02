using System.Collections.Generic;
using UnityEngine;

public class DisplaySortedData : SolverStep
{
    private List<Interval> m_sortedIntervals;

    public DisplaySortedData(SolverVisualizer visualizer) : base(visualizer)
    {
        m_sortedIntervals = new List<Interval>(visualizer.InputData);
        m_sortedIntervals.Sort((i1, i2) => i1.Minimum - i2.Minimum);
    }

    protected override void OnDrawControls()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", GUILayout.ExpandWidth(false)))
            SolverVisualizer.MovePreviousStep();

        if (GUILayout.Button(">", GUILayout.ExpandWidth(false)))
            SolverVisualizer.MoveNextStep();

        GUILayout.EndHorizontal();

        GUILayout.Label("For the first step, let's sort them by their starting point. It's probably not obvious why, yet, but it will make the next step easier.");
        GUILayout.Label(@"The key is that we don't care about where each specific interval starts and ends. We only care about the ordering of the end points.
So we can collapse the intervals into a list of end points, where each end point is either the start of an interval, or the end of the interval.");
    }

    protected override void OnDrawCanvas()
    {
        for (int i = 0; i < m_sortedIntervals.Count; i++)
        {
            Interval interval = m_sortedIntervals[i];
            Rect r = new Rect(interval.Minimum, i * 35, interval.Maximum - interval.Minimum, 30);
            GUI.DrawTexture(r, SolverVisualizer.WhiteTexture1x1, ScaleMode.StretchToFill);
        }
    }
}
