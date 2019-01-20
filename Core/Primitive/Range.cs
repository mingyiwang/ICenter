namespace Core.Primitive
{

    public sealed class Range<T>
    {
        private readonly T _start;
        private readonly T _end;

        private Range(T start, T end)
        {
            _start = start;
            _end = end;
        }

        public static Range<T> Of(T start, T end)
        {
            return new Range<T>(start, end);
        }

        public T GetStart()
        {
            return _start;
        }

        public T GetEnd()
        {
            return _end;
        }

    }
}