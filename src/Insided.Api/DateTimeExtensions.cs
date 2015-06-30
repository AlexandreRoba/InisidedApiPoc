using System;
using System.Globalization;

namespace Insided.Api
{
    public static class DateTimeExtensions
    {
        ///<summary>
        ///Converts a regular DateTime to a RFC822 date string.
        ///</summary>
        ///<returns>The specified date formatted as a RFC822 date string.</returns>
        public static string ToRfc2822Date(this DateTime date,TimeZoneInfo timeZoneInfo)
        {
            int offset = timeZoneInfo.GetUtcOffset(date).Hours;
            string timeZone = "+" + offset.ToString().PadLeft(2, '0');

            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }

            return date.ToString("ddd, dd MMM yyyy HH:mm:ss" + timeZone.PadRight(5, '0'), CultureInfo.InvariantCulture);
        }

        public static string ToRfc2822Date(this DateTime date)
        {
            return date.ToRfc2822Date(TimeZoneInfo.Local);
        }
    }
}