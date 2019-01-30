using System;
using Core.Collection;

namespace Core.IO
{
    /// <summary>
    /// This class is used to store a sequence of data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Chain<T> : IEquatable<Chain<T>>
    {
        private const int MarkUnset = -1;

        private readonly T[] _store;
        private readonly int _capacity;
        private int _position;
        private int _limit;
        private int _mark;
        
        public T Get()
        {
            return _store[NextIndex()];
        }

        public void Put(T data)
        {
            _store[NextPutIndex()] = data;
        }

        public T[] Get(T[] buffer, int offset, int length)
        {
            return null;
        }

        

        private Chain(int capacity)
        {
            _limit    = _capacity = capacity;
            _position = 0;
            _mark     = MarkUnset;
            _store    = new T[_capacity];
        }

        public int Remaining => _limit - _position;
        public int Position  => _position;
        public int Limit     => _limit;
        public int Capacity  => _capacity;
         
        /// <summary>
        /// Sets this buffer's position.
        /// If the mark is defined and larger than the new position then it is discarded.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Chain<T> SetPosition(int pos)
        {
            if (pos < 0 || pos > _limit)
            {
                throw new ArgumentException();
            }

            _position = pos;
            if (_mark != MarkUnset && _mark > pos)
            {
                _mark = MarkUnset;
            }
            return this;
        }

        /// <summary>
        /// Sets the buffer's limit.If the position is larger than the new limit
        /// then it is set to the new limit.  If the mark is defined and larger than
        /// the new limit then it is discarded.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Chain<T> SetLimit(int limit)
        {
            if (limit < 0 || limit > _capacity)
            {
                throw new ArgumentException("Bad limit (capacity " + _capacity + "): " + limit);
            }

            _limit = limit;
            if (_position > limit)
            {
                _position = limit;
            }

            if (_mark > limit)
            {
                _mark = MarkUnset;
            }

            return this;
        }

        public Chain<T> Flip()
        {
            _limit    = _position;
            _position = 0;
            _mark     = MarkUnset;
            return this;
        }

        public Chain<T> Mark()
        {
            _mark = _position;
            return this;
        }

        public Chain<T> Reset()
        {
            _position = _mark;
            return this;
        }

        public Chain<T> Clear()
        {
            _position = 0;
            _limit = _capacity;
            _mark  = MarkUnset;
            return this;
        }

        internal int NextIndex()
        {
            if (_position >= _limit)
            {
                throw new OverflowException();
            }
            return _position++;
        }

        internal int NextIndex(int nb)
        {                    
            if (_limit - _position < nb)
                throw new OverflowException();
            int p = _position;
            _position += nb;
            return p;
        }

        internal int NextPutIndex()
        {                          
            if (_position >= _limit)
                throw new OverflowException();
            return _position++;
        }

        internal int NextPutIndex(int nb)
        {                    
            if (_limit - _position < nb)
                throw new OverflowException();
            int p = _position;
            _position += nb;
            return p;
        }

        public virtual bool Equals(Chain<T> other)
        {
            return Arrays.IsEqual(_store, other._store) 
                && _position == other._position
                && _capacity == other._capacity
                && _limit    == other._limit
                && _mark     == other._mark
                ;
        }

        public sealed override bool Equals(object other)
        {
            if(other == null)
            {
                return false;
            }

            if(ReferenceEquals(this, other))
            {
                return true;
            }

            return other is Chain<T> buf && Equals(buf);
        }

    }

}