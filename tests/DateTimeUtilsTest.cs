using System;
using FinaDb.Common.Utils;
using NUnit.Framework;

namespace FinaDb.Tests.Common.Utils
{
    [TestFixture]
    public class DateTimeUtilsTest
    {
        #region GetIntervalIntersection Tests
        [Test]
        // ReSharper disable once InconsistentNaming
        public void Result_should_be_empty_when_there_is_no_intersection_because_test_interval_is_before_allowed_interval()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval{From = now.AddDays(-10), To = now.AddDays(-5)};
            DateTimeInterval allowedInterval = new DateTimeInterval{From = now, To = now.AddDays(3)};

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.IsNull(result.From);
            Assert.IsNull(result.To);
        }

        [Test]
        // ReSharper disable once InconsistentNaming
        public void Result_should_be_empty_when_there_is_no_intersection_because_test_interval_is_after_allowed_interval()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval { From = now.AddDays(10), To = now.AddDays(5) };
            DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(3) };

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.IsNull(result.From);
            Assert.IsNull(result.To);
        }

        [Test]
        // ReSharper disable once InconsistentNaming
        public void Test_interval_starts_before_allowed_interval_and_ends_within_it()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval { From = now.AddDays(-10), To = now.AddDays(3) };
            DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(6) };

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.AreEqual(now, result.From);
            Assert.AreEqual(now.AddDays(3), result.To);
        }
        
        [Test]
        // ReSharper disable once InconsistentNaming
        public void Test_interval_starts_within_allowed_interval_and_ends_after_it()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval { From = now.AddDays(1), To = now.AddDays(30) };
            DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(6) };

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.AreEqual(now.AddDays(1), result.From);
            Assert.AreEqual(now.AddDays(6), result.To);
        }

        [Test]
        // ReSharper disable once InconsistentNaming
        public void Test_interval_equals_the_allowed_interval()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval { From = now, To = now.AddDays(6) };
            DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(6) };

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.AreEqual(now, result.From);
            Assert.AreEqual(now.AddDays(6), result.To);
        }

        [Test]
        // ReSharper disable once InconsistentNaming
        public void Test_interval_starts_before_allowed_interval_and_ends_after_it()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval { From = now.AddDays(-10), To = now.AddDays(30) };
            DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(6) };

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.AreEqual(now, result.From);
            Assert.AreEqual(now.AddDays(6), result.To);
        }

        [Test]
        // ReSharper disable once InconsistentNaming
        public void Test_interval_starts_within_allowed_interval_and_ends_within_it()
        {
            DateTime now = DateTime.Now;
            DateTimeInterval testInterval = new DateTimeInterval { From = now.AddDays(2), To = now.AddDays(5) };
            DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(10) };

            DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
            Assert.AreEqual(now.AddDays(2), result.From);
            Assert.AreEqual(now.AddDays(5), result.To);
        }

        #endregion GetIntervalIntersection Tests

        #region GetIntervalDuration Tests

        [Test]
        // ReSharper disable once InconsistentNaming
        public void Get_duration()
        {
            var now = DateTime.Now;
            
            var interval = new DateTimeInterval { From = null, To = null };
            Assert.IsNull(DateTimeUtils.GetIntervalDuration(interval));

            interval = new DateTimeInterval {From = now, To = now};
            Assert.AreEqual(1, DateTimeUtils.GetIntervalDuration(interval));

            interval = new DateTimeInterval { From = now, To = now.AddDays(1) };
            Assert.AreEqual(2, DateTimeUtils.GetIntervalDuration(interval));
        }
        #endregion GetIntervalDuration Tests
    }
}
