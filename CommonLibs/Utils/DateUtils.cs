using System;

namespace CommonLibs.Utils
{
    public class DateUtils
    {
        public static string GetCurrentDateAndTime()
        {
            return DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’-’mm’-’ss");

        }

        public static string GetCurrentDateAndTime(string dateFormat)
        {
            return DateTime.Now.ToString(dateFormat);

        }
    }
}
