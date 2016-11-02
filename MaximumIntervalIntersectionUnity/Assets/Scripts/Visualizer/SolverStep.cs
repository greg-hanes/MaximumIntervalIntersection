using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SolverStep
{
    protected SolverVisualizer SolverVisualizer
    {
        get;
        private set;
    }

    public SolverStep(SolverVisualizer visualizer)
    {
        SolverVisualizer = visualizer;
    }

    public void DrawControls()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width, 200));
        if (GUILayout.Button("Restart", GUILayout.ExpandWidth(false)))
        {
            SolverVisualizer.RestartDemo();
        }
        OnDrawControls();
        GUILayout.EndArea();
    }

    public void DrawCanvas()
    {
        GUI.BeginGroup(new Rect(10, 225, Screen.width, Screen.height - 225));
        OnDrawCanvas();
        GUI.EndGroup();
    }

    protected abstract void OnDrawControls();
    protected abstract void OnDrawCanvas();
}

