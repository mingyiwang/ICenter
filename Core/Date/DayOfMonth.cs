namespace Core.Date
{

    public sealed class DayOfMonth : IDateTime, IComparable<DayOfMonth>, IComparable,IEquatable<DayOfMonth>
    {

        private int _day;
        private int _month;
        private int _year;

        internal DayOfMonth(int day, int month, int year) {
            this._day   = day;
            this._month = month;
            this._year  = year;
        }

        public static DayOfMonth Of(int day, Month month) {
            return new DayOfMonth(day, month.GetMonth(), 0);
        }

        public int CompareTo(DayOfMonth other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<DayOfMonth>.Equals(DayOfMonth other)
        {
            throw new NotImplementedException();
        }

    }

}
