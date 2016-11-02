using System;
using MaximumIntervalIntersectionProblem;
using NUnit.Framework;

namespace TestMaximumIntervalIntersection
{
    [TestFixture]
    public class TestMaximumIntervalIntersectionSolver
    {
        [Test]
        public void TestNullInput()
        {
            Assert.Catch<InvalidOperationException>(() => IntervalIntersectionSolver.Solve(null));
        }

        [Test]
        public void TestZeroLengthInput()
        {
            var solution = IntervalIntersectionSolver.Solve(new Interval[0]);
            Assert.AreEqual(0, solution.MaximumIntersections);
            Assert.AreEqual(0, solution.MaximumIntervals.Length);
        }

        [Test]
        public void TestOneInterval()
        {
            Interval[] testInterval = new Interval[1];
            testInterval[0] = new Interval(0, 100);
            var solution = IntervalIntersectionSolver.Solve(testInterval);

            Assert.AreEqual(1, solution.MaximumIntersections);
            Assert.AreEqual(1, solution.MaximumIntervals.Length);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(100, solution.MaximumIntervals[0].Maximum);
        }

        [Test]
        public void TestTwoNonOverlappingIntervals()
        {
            Interval[] testIntervals = new Interval[2];
            testIntervals[0] = new Interval(0, 10);
            testIntervals[1] = new Interval(20, 40);

            var solution = IntervalIntersectionSolver.Solve(testIntervals);

            Assert.AreEqual(1, solution.MaximumIntersections);
            Assert.AreEqual(2, solution.MaximumIntervals.Length);

            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(10, solution.MaximumIntervals[0].Maximum);
            Assert.AreEqual(20, solution.MaximumIntervals[1].Minimum);
            Assert.AreEqual(40, solution.MaximumIntervals[1].Maximum);
        }

        [Test]
        public void TestOverlappingEndpoints()
        {
            Interval[] testInterval = new Interval[2];
            testInterval[0] = new Interval(0, 10);
            testInterval[1] = new Interval(10, 20);

            var solution = IntervalIntersectionSolver.Solve(testInterval);

            Assert.AreEqual(2, solution.MaximumIntersections);
            Assert.AreEqual(1, solution.MaximumIntervals.Length);
            Assert.AreEqual(10, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(10, solution.MaximumIntervals[0].Maximum);
        }

        [Test]
        public void TestBorderingIntervals()
        {
            Interval[] testIntervals = new Interval[6];
            testIntervals[0] = new Interval(0, 9);
            testIntervals[1] = new Interval(0, 20);
            testIntervals[2] = new Interval(10, 20);
            testIntervals[3] = new Interval(30, 39);
            testIntervals[4] = new Interval(30, 50);
            testIntervals[5] = new Interval(40, 50);

            var solution = IntervalIntersectionSolver.Solve(testIntervals);

            Assert.AreEqual(2, solution.MaximumIntersections);
            Assert.AreEqual(2, solution.MaximumIntervals.Length);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(20, solution.MaximumIntervals[0].Maximum);

            Assert.AreEqual(30, solution.MaximumIntervals[1].Minimum);
            Assert.AreEqual(50, solution.MaximumIntervals[1].Maximum);
        }

        [Test]
        public void TestBorderingIntervals2()
        {
            Interval[] testIntervals = new Interval[4];
            testIntervals[0] = new Interval(0, 1);
            testIntervals[1] = new Interval(2, 3);
            testIntervals[2] = new Interval(4, 5);
            testIntervals[3] = new Interval(6, 7);

            var solution = IntervalIntersectionSolver.Solve(testIntervals);

            Assert.AreEqual(1, solution.MaximumIntersections);
            Assert.AreEqual(1, solution.MaximumIntervals.Length);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(7, solution.MaximumIntervals[0].Maximum);
        }

        [Test]
        public void TestOverlappingIntervals()
        {
            Interval[] testIntervals = new Interval[4];
            testIntervals[0] = new Interval(0, 10);
            testIntervals[1] = new Interval(0, 10);
            testIntervals[2] = new Interval(0, 10);
            testIntervals[3] = new Interval(0, 10);

            var solution = IntervalIntersectionSolver.Solve(testIntervals);

            Assert.AreEqual(4, solution.MaximumIntersections);
            Assert.AreEqual(1, solution.MaximumIntervals.Length);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(10, solution.MaximumIntervals[0].Maximum);
        }

        [Test]
        public void TestOrderIndependency()
        {
            const int kTestIntervalCount = 10;


            Interval[] testIntervals1 = new Interval[kTestIntervalCount];
            Interval[] testIntervals2 = new Interval[kTestIntervalCount];

            // Generate some random data.
            Random r = new Random();
            for (int i = 0; i < kTestIntervalCount; i++)
            {
                int start = r.Next(-100, 100);
                int endOffset = r.Next(0, 100);
                testIntervals1[i] = new Interval(start, start + endOffset);
                testIntervals2[i] = testIntervals1[i];
            }

            // Shuffle it via Fisher-Yates
            for (int i = 0; i < kTestIntervalCount - 2; i++)
            {
                int j = r.Next(i, kTestIntervalCount);
                Interval tmp = testIntervals2[i];
                testIntervals2[i] = testIntervals2[j];
                testIntervals2[j] = tmp;
            }


            var solution1 = IntervalIntersectionSolver.Solve(testIntervals1);
            var solution2 = IntervalIntersectionSolver.Solve(testIntervals2);


            Assert.AreEqual(solution1.MaximumIntersections, solution2.MaximumIntersections);
            Assert.AreEqual(solution1.MaximumIntervals.Length, solution2.MaximumIntervals.Length);

            for (int i = 0; i < solution1.MaximumIntervals.Length; i++)
            {
                Assert.AreEqual(solution1.MaximumIntervals[i].Minimum, solution2.MaximumIntervals[i].Minimum);
                Assert.AreEqual(solution1.MaximumIntervals[i].Maximum, solution2.MaximumIntervals[i].Maximum);
            }
        }

        [Test]
        public void TestMeasureZeroInterval()
        {
            Interval[] testInterval = new Interval[1];
            testInterval[0] = new Interval(0, 0);

            var solution = IntervalIntersectionSolver.Solve(testInterval);
            
            Assert.AreEqual(1, solution.MaximumIntersections);
            Assert.AreEqual(1, solution.MaximumIntervals.Length);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Maximum);
        }

        [Test]
        public void TestConsecutiveMeasureZeroIntervals()
        {
            Interval[] testInterval = new Interval[2];
            testInterval[0] = new Interval(0, 0);
            testInterval[1] = new Interval(1, 1);

            var solution = IntervalIntersectionSolver.Solve(testInterval);

            Assert.AreEqual(1, solution.MaximumIntersections);
            Assert.AreEqual(1, solution.MaximumIntervals.Length);
            Assert.AreEqual(0, solution.MaximumIntervals[0].Minimum);
            Assert.AreEqual(1, solution.MaximumIntervals[0].Maximum);
        }

        [Test]
        public void TestAgainstReference()
        {
            Random r = new Random();
            Interval[] testIntervals = new Interval[100000];

            for (int i = 0; i < testIntervals.Length; i++)
            {
                int start = r.Next(0, ReferenceAlgorithm.kMaxLength);
                int end = r.Next(0, ReferenceAlgorithm.kMaxLength - start);
                testIntervals[i] = new Interval(start, start + end);
            }

            var refSolution = ReferenceAlgorithm.Solve(testIntervals);
            var solution = IntervalIntersectionSolver.Solve(testIntervals);

            Assert.AreEqual(refSolution.MaximumIntersections, solution.MaximumIntersections);
            Assert.AreEqual(refSolution.MaximumIntervals.Length, solution.MaximumIntervals.Length);
            for (int i = 0; i < refSolution.MaximumIntervals.Length; i++)
            {
                Assert.AreEqual(refSolution.MaximumIntervals[i].Minimum, solution.MaximumIntervals[i].Minimum);
                Assert.AreEqual(refSolution.MaximumIntervals[i].Maximum, solution.MaximumIntervals[i].Maximum);
            }
        }
    }
}
