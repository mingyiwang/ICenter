using System;
using Core.Date.Extension;

namespace Core.Date
{

    public sealed class MonthOfYear : IDateTime, IComparable, IComparable<MonthOfYear>, IEquatable<MonthOfYear>
    {

        private readonly Month _month;
        private readonly Year  _year;

        private MonthOfYear(Month month, Year year)
        {
            _month = month;
            _year  = year;
        }

        /// <summary>
        /// Get Current MonthOfYear on Local Time Zone
        /// </summary>
        public static MonthOfYear Now
        {
            get
            {
                var dateTime = DateTime.Now;
                return new MonthOfYear(Month.Of(dateTime.Month), Year.Of(dateTime.Year));
            }
        }

        /// <summary>
        /// Get MonthOfYear by Sepcified DateTime
        /// If DateTime is not in Local then it will be converted to local DateTime
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>MonthOfYear</returns>
        public static MonthOfYear Of(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
                return Of(localDateTime.Month, localDateTime.Year);
            }
            
            return Of(dateTime.Month, dateTime.Year);

        }

        /// <summary>
        /// Get MonthOfYear by DateTimeOffset Object
        /// The DateTimeOffset will be converted to Local
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static MonthOfYear Of(DateTimeOffset dateTime)
        {
            var localDateTime = dateTime.LocalDateTime;
            return Of((int) localDateTime.Month, localDateTime.Year);
        }

        /// <summary>
        /// Construct MonthOfYear By Month and Year
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public static MonthOfYear Of(int month, int year)
        {
            return Of(Month.Of(month), Year.Of(year));
        }

        /// <summary>
        /// Construct MonthOfYear By Month and Year
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public static MonthOfYear Of(Month month, Year year)
        {
            return new MonthOfYear(month, year);
        }

        /// <summary>
        /// Get The Total Days in This Month of This Year
        /// </summary>
        public int TotalDays => _month.GetTotalDaysOfMonthInYear(_year.GetYear());

        /// <summary>
        /// Get Total Days From 1/1/0001 To the End of This Month
        /// </summary>
        public int TotalDaysToEndOfMonth => _year.TotalDaysBeforeStartOfYear + _month.GetDaysToEndOfMonthInYear(_year.GetYear());

        /// <summary>
        /// Get Total Days From 1/1/0001 To the End of Previous Month
        /// </summary>
        public int TotalDaysBeforeStartOfMonth
        {
            get {
                return TotalDaysToEndOfMonth - _month.GetTotalDaysOfMonthInYear(_year.GetYear());
            }
        }

        /// <summary>
        /// Get First DateTime of This Month
        /// </summary>
        public DateTime FirstDateOfMonth => new DateTime(_year.GetYear(), _month.GetMonth(), day: 1);

        /// <summary>
        /// Get Last DateTime of This Month
        /// </summary>
        public DateTime LastDateOfMonth
        {
            get
            {
                var day = _month.GetTotalDaysOfMonthInYear(_year.GetYear());
                return new DateTime(_year.GetYear(), _month.GetMonth(), day);
            }

        }

        public DateTime GetDayOfWeekDateForWeek(int week, DayOfWeek dayOfWeek)
        {
            Checks.InRange(1, 5, week, $"{week} is out of range in month {_month.GetName()} of Year {_year.GetYear()}");
            
            var daysUtilFirstDayOfThisWeek = 1 + (week - 1) * 7;
            var dateTime = new DateTime(_year.GetYear(), _month.GetMonth(), daysUtilFirstDayOfThisWeek);
            var firstDayOfThisWeek = dateTime.DayOfWeek;

            var intOfDayOfWeek = DayOfWeekExtension.GetInt(dayOfWeek);
            var intOfFirstDayOfThisWeek = DayOfWeekExtension.GetInt(firstDayOfThisWeek);

            var days = intOfDayOfWeek >= intOfFirstDayOfThisWeek
                     ? intOfDayOfWeek - intOfFirstDayOfThisWeek
                     : intOfDayOfWeek - intOfFirstDayOfThisWeek + 7
                     ;

            Checks.LessThanOrEqual(days++, TotalDays, $"{dayOfWeek} doesn't exist in {week} week(s) of {_month.GetName()} {_year}");
            return dateTime.AddDays(days);
        }

        /// <summary>
        /// Get Month
        /// </summary>
        public Month Month => Month.Of(_month.GetMonth());

        /// <summary>
        /// Get Year
        /// </summary>
        public Year Year => Year.Of(_year.GetYear());

        /// <summary>
        /// Check the Day is a valid day or not
        /// </summary>
        /// <param name="day"><code>int</code></param>
        /// <returns><code>bool</code></returns>
        public bool IsValidDay(int day)
        {
            return _month.IsValid(day, _year.GetYear());
        }

        public bool IsAfter(MonthOfYear other)
        {
            return CompareTo(other) > 0;
        }

        public bool IsBefore(MonthOfYear other)
        {
            return CompareTo(other) < 0;
        }

        public int CompareTo(MonthOfYear other)
        {
            return _year.Equals(other._year) 
                 ? _month.CompareTo(other._month) 
                 : _year.CompareTo(other._year)
                 ;
        }

        public int CompareTo(object obj)
        {
            Checks.NotNull(obj);
            Checks.Is(obj, expected: typeof(MonthOfYear));
            return CompareTo(obj as MonthOfYear);
        }

        public bool Equals(MonthOfYear other)
        {
            return _year.Equals(other._year) && _month.Equals(other._month);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is MonthOfYear my && Equals(my);
        }

        public override int GetHashCode()
        {
            return  _year.GetYear() ^ (_month.GetMonth() << 27);
        }

    }

}