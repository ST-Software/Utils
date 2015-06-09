# Utils
Collection of utility functions written in C#

## DateTimeUtils
[code]https://github.com/ST-Software/Utils/blob/master/src/DateTimeUtils.cs

### DateTimeUtils.GetIntervalIntersection()  
Calculates the intersection (overlap) between two datetime intervals.

Example:
```cs
DateTime now = DateTime.Now;
DateTimeInterval testInterval = new DateTimeInterval { From = now.AddDays(-10), To = now.AddDays(3) };
DateTimeInterval allowedInterval = new DateTimeInterval { From = now, To = now.AddDays(6) };

DateTimeInterval result = DateTimeUtils.GetIntervalIntersection(testInterval, allowedInterval);
Assert.AreEqual(now, result.From);
Assert.AreEqual(now.AddDays(3), result.To);
```

### DateTimeUtils.GetIntervalDuration()
Calculates duration of an interval. Single day intervals have duration equal to 1, not 0.
```cs
var now = DateTime.Now;

var interval = new DateTimeInterval { From = null, To = null };
Assert.IsNull(DateTimeUtils.GetIntervalDuration(interval));

interval = new DateTimeInterval {From = now, To = now};
Assert.AreEqual(1, DateTimeUtils.GetIntervalDuration(interval));
```
