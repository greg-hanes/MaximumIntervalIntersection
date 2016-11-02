using System.Collections.Generic;
using UnityEngine;

public class SolverVisualizer : MonoBehaviour
{
    private int m_currentStep = 0;
    private List<SolverStep> m_solverSteps = new List<SolverStep>();

    public Interval[] InputData
    {
        get;
        private set;
    }

    internal IntervalEndPoint[] IntervalEndPoints
    {
        get;
        private set;
    }

    public Texture2D WhiteTexture1x1
    {
        get;
        private set;
    }

    public Texture2D RedTexture1x1
    {
        get;
        private set;
    }
    
    void Awake()
    {
        WhiteTexture1x1 = new Texture2D(1, 1);
        WhiteTexture1x1.SetPixel(0, 0, Color.white);
        WhiteTexture1x1.Apply();

        RedTexture1x1 = new Texture2D(1, 1);
        RedTexture1x1.SetPixel(0, 0, Color.red);
        RedTexture1x1.Apply();

        RestartDemo();
    }

    public void RestartDemo()
    {
        m_currentStep = 0;
        m_solverSteps.Clear();

        GenerateData();

        m_solverSteps.Add(new DisplayInputData(this));
        m_solverSteps.Add(new DisplaySortedData(this));
        m_solverSteps.Add(new DisplayFlattenedData(this));
        m_solverSteps.Add(new SearchForMaximaStep(this));
    }

    public void MoveNextStep()
    {
        m_currentStep++;
        m_currentStep = Mathf.Clamp(m_currentStep, 0, m_solverSteps.Count - 1);
    }

    public void MovePreviousStep()
    {
        m_currentStep--;
        m_currentStep = Mathf.Clamp(m_currentStep, 0, m_solverSteps.Count - 1);
    }

    void OnGUI()
    {
        m_solverSteps[m_currentStep].DrawControls();
        m_solverSteps[m_currentStep].DrawCanvas();
    }
    
    private void GenerateData()
    {
        InputData = new Interval[10];
        int minimumStartingEndpoint = int.MaxValue;
        for (int i = 0; i < 10; i++)
        {
            int start = UnityEngine.Random.Range(0, 500);
            int end = start + UnityEngine.Random.Range(0, 500);
            InputData[i] = new Interval(start, end);

            // Record the minimum starting end point so we can
            // shift everything over and not have a big gap on the left side.
            if (start < minimumStartingEndpoint)
                minimumStartingEndpoint = start;
        }

        for (int i = 0; i < 10; i++)
        {
            InputData[i] = new Interval(InputData[i].Minimum - minimumStartingEndpoint, InputData[i].Maximum - minimumStartingEndpoint);
        }

        IntervalEndPoints = IntervalIntersectionSolver.SortAndFlattenIntervals(InputData);
    }
}
