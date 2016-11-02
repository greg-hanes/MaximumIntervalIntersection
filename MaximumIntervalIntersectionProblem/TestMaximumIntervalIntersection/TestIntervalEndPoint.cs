using MaximumIntervalIntersectionProblem;
using NUnit.Framework;

namespace TestMaximumIntervalIntersection
{
    [TestFixture]
    public class TestIntervalEndPoint
    {
        [Test]
        public void TestIntervalEndPointComparisonOne()
        {
            IntervalEndPoint ep1 = new IntervalEndPoint(0, IntervalEndPointType.Start);
            IntervalEndPoint ep2 = new IntervalEndPoint(1, IntervalEndPointType.Start);

            Assert.Greater(0, IntervalEndPoint.Comparison(ep1, ep2));
            Assert.Less(0, IntervalEndPoint.Comparison(ep2, ep1));
        }

        [Test]
        public void TestIntervalEndPointComparisonTwo()
        {
            IntervalEndPoint ep1 = new IntervalEndPoint(0, IntervalEndPointType.End);
            IntervalEndPoint ep2 = new IntervalEndPoint(1, IntervalEndPointType.End);

            Assert.Greater(0, IntervalEndPoint.Comparison(ep1, ep2));
            Assert.Less(0, IntervalEndPoint.Comparison(ep2, ep1));
        }

        [Test]
        public void TestIntervalEndPointComparisonThree()
        {
            IntervalEndPoint ep1 = new IntervalEndPoint(0, IntervalEndPointType.Start);
            IntervalEndPoint ep2 = new IntervalEndPoint(0, IntervalEndPointType.Start);

            Assert.AreEqual(0, IntervalEndPoint.Comparison(ep1, ep2));
            Assert.AreEqual(0, IntervalEndPoint.Comparison(ep2, ep1));
        }

        [Test]
        public void TestIntervalEndPointComparisonFour()
        {
            IntervalEndPoint ep1 = new IntervalEndPoint(0, IntervalEndPointType.End);
            IntervalEndPoint ep2 = new IntervalEndPoint(0, IntervalEndPointType.End);

            Assert.AreEqual(0, IntervalEndPoint.Comparison(ep1, ep2));
            Assert.AreEqual(0, IntervalEndPoint.Comparison(ep2, ep1));
        }

        [Test]
        public void TestIntervalEndPointComparisonFive()
        {
            IntervalEndPoint ep1 = new IntervalEndPoint(0, IntervalEndPointType.Start);
            IntervalEndPoint ep2 = new IntervalEndPoint(0, IntervalEndPointType.End);

            Assert.Greater(0, IntervalEndPoint.Comparison(ep1, ep2));
            Assert.Less(0, IntervalEndPoint.Comparison(ep2, ep1));
        }
    }
}
