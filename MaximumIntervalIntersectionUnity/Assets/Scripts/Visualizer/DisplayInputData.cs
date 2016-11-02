using System.Collections.Generic;
using UnityEngine;

public class DisplayInputData : SolverStep
{
    public DisplayInputData(SolverVisualizer visualizer) : base(visualizer)
    {

    }

    protected override void OnDrawControls()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", GUILayout.ExpandWidth(false)))
            SolverVisualizer.MovePreviousStep();

        if (GUILayout.Button(">", GUILayout.ExpandWidth(false)))
            SolverVisualizer.MoveNextStep();

        GUILayout.EndHorizontal();

        GUILayout.Label("This is the input data. It is a set of intervals. The intervals are arranged vertically for visualization purposes.");
        GUILayout.Label("The goal is to find the maximum number of intervals that overlap.");
    }

    protected override void OnDrawCanvas()
    {
        for (int i = 0; i < SolverVisualizer.InputData.Length; i++)
        {
            Interval interval = SolverVisualizer.InputData[i];
            Rect r = new Rect(interval.Minimum, i * 35, interval.Maximum - interval.Minimum, 30);
            GUI.DrawTexture(r, SolverVisualizer.WhiteTexture1x1, ScaleMode.StretchToFill);
        }
    }
}

