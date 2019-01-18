using System;

namespace Core.Date.Extension
{

    public static class DateTimeExtension
    {

        public static DateTime ToZonedTime(this DateTime dateTime, TimeZoneInfo zoneInfo)
        {
            return TimeZoneInfo.ConvertTime(dateTime, zoneInfo);
        }

    }

}