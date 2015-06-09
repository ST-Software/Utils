using System;

namespace FinaDb.Common.Utils
{
    public class DateTimeInterval
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public static class DateTimeUtils
    {
        /// <summary>
        /// Get the intersection between testInterval and allowedInterval.
        /// </summary>
        public static DateTimeInterval GetIntervalIntersection(DateTimeInterval testInterval, DateTimeInterval allowedInterval)
        {
            #region sanity check

            const string errorMessage = "DateTimeUtils.GetIntervalIntersection - argument can not be null";
            
            if (testInterval == null)
            {
                throw new ArgumentNullException("testInterval", errorMessage);
            }

            if (allowedInterval == null)
            {
                throw new ArgumentNullException("allowedInterval", errorMessage);
            }

            if (testInterval.From == null || testInterval.To == null || allowedInterval.From == null || allowedInterval.To == null)
            {
                throw new ArgumentException("DateTimeUtils.GetIntervalIntersection - argument can not be null");
            }

            #endregion sanity check

            //Is testInterval totally outside the allowedInterval?
            if (testInterval.From.Value < allowedInterval.From.Value && testInterval.To.Value < allowedInterval.From.Value
                || testInterval.From.Value > allowedInterval.To.Value && testInterval.To.Value > allowedInterval.To.Value)
            {
                return new DateTimeInterval();
            }

            DateTime from = testInterval.From < allowedInterval.From
                ? allowedInterval.From.Value
                : testInterval.From.Value;

            DateTime to = testInterval.To > allowedInterval.To
                ? allowedInterval.To.Value
                : testInterval.To.Value;

            var result = new DateTimeInterval
            {
                From = from,
                To = to
            };

            return result;
        }

        public static double? GetIntervalDuration(DateTimeInterval interval)
        {
            if (interval.To == null || interval.From == null)
            {
                return null;
            }

            TimeSpan? difference = interval.To.Value.Date - interval.From.Value.Date;
            double days = Math.Floor(difference.Value.TotalDays + 1);
            return days;
        }
    }
}
