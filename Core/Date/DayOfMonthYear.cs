using System;

namespace Core.Date
{

    public sealed class DayOfMonthYear : IDateTime, IComparable<DayOfMonthYear>, IComparable,IEquatable<DayOfMonthYear>
    {

        private int _day;
        private int _month;
        private int _year;

        internal DayOfMonthYear(int day, int month, int year) {
            this._day   = day;
            this._month = month;
            this._year  = year;
        }

        public static DayOfMonthYear Of(int day, Month month) {
            return new DayOfMonthYear(day, month.GetMonth(), 0);
        }

        public int CompareTo(DayOfMonthYear other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<DayOfMonthYear>.Equals(DayOfMonthYear other)
        {
            throw new NotImplementedException();
        }

    }

}
