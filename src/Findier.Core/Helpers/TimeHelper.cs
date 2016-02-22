using System;

namespace Findier.Core.Helpers
{
    public static class TimeHelper
    {
        public static DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static DateTime FromUnixTimeStamp(double time)
        {
            return EpochTime.AddSeconds(time);
        }

        public static string ToTimeSince(DateTime time)
        {
            return ToTimeSince(ToUnixTimeStamp(time));
        }

        public static string ToTimeSince(double time)
        {
            var now = DateTime.UtcNow;
            var date = FromUnixTimeStamp(time);

            var since = now - date;

            if ((int)since.TotalSeconds == 0)
            {
                return "just now";
            }
            if (since.TotalSeconds < 60)
            {
                //return "just now";
                return Math.Floor(since.TotalSeconds) + "s ago";
            }
            if (since.TotalMinutes < 60)
            {
                return Math.Floor(since.TotalMinutes) + "m ago";
            }
            if (since.TotalHours < 24)
            {
                return Math.Floor(since.TotalHours) + "h ago";
            }
            return Math.Floor(since.TotalDays) + "d ago";
        }

        public static double ToUnixTimeStamp(DateTime time)
        {
            if (time.Kind == DateTimeKind.Local)
            {
                time = time.ToUniversalTime();
            }
            return (time - EpochTime).TotalSeconds;
        }
    }
}