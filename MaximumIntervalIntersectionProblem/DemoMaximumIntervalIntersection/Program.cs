using System;
using System.Collections.Generic;
using System.IO;
using MaximumIntervalIntersectionProblem;

namespace DemoMaximumIntervalIntersection
{
    class Program
    {
        /// <summary>
        /// Simple, quick and dirty console demo of maximum interval intersection solver.
        /// </summary>
        /// <param name="args">Unused</param>
        public static void Main(string[] args)
        {
            List<Interval> inputDataList = new List<Interval>();
            using (StreamReader reader = new StreamReader(File.OpenRead("DemoData.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    int start = int.Parse(values[0]);
                    int end = int.Parse(values[1]);

                    inputDataList.Add(new Interval(start, end));
                }
            }

            Interval[] inputData = inputDataList.ToArray();
            var solution = IntervalIntersectionSolver.Solve(inputData);

            Console.WriteLine("Maximum number of people alive: " + solution.MaximumIntersections);
            Console.WriteLine("Years during which " + solution.MaximumIntersections + " people were alive:");
            for (int i = 0; i < solution.MaximumIntervals.Length; i++)
            {
                Console.WriteLine(solution.MaximumIntervals[i].Minimum + " to " + solution.MaximumIntervals[i].Maximum);
            }

            Console.ReadLine();
        }
    }
}
