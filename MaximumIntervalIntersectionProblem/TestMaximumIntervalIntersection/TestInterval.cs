using System;
using MaximumIntervalIntersectionProblem;
using NUnit.Framework;

namespace TestMaximumIntervalIntersection
{
    [TestFixture]
    public class TestInterval
    {
        [Test]
        public void TestConstructor()
        {
            Interval interval = new Interval(0, 1);
            Assert.AreEqual(0, interval.Minimum);
            Assert.AreEqual(1, interval.Maximum);
        }

        [Test]
        public void TestInvalidConstructorArguments()
        {
            Assert.Catch<InvalidOperationException>(() => { Interval interval = new Interval(1, 0); });
        }
    }
}
