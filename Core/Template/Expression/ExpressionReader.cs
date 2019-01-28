using System;
using Core.Primitive;

namespace Core.Template.Expression
{
    public sealed class ExpressionReader
    {
        public static readonly char Eos = '\u0000';

        private readonly string _input;
        private int _pos;

        public ExpressionReader(string input)
        {
            Checks.NotNullOrEmpty(input);
            _input = input;
            _pos = 0;
        }

        public string Source    => _input;
        public int    Position  => _pos;
        public bool   IsInBound => _pos >= 0 && _pos < _input.Length;

        public void Run(Action<ExpressionReader> action)
        {
            while (IsInBound)
            {
                SkipWhiteSpace();
                action(this);
                MoveNext();
            }
        }

        public char GetCurrentChar()
        {
            if (IsInBound)
            {
                return _input[_pos];
            }
            return Eos;
        }

        // read next char and move position
        public char GetNextChar()
        {
            MoveNext();
            if (IsInBound)
            {
                return _input[_pos];
            }
            return Eos;
        }

        public bool HasNext() {
            return InBound(_pos + 1);
        }

        // View Next 'n' char without changing position
        public char ViewNextChar()
        {
            var nexPos = _pos + 1;
            if (InBound(nexPos))
            {
                return _input[nexPos];
            }
            return Eos;
        }

        public void MoveNext()
        {
            MoveNext(1);
        }

        // move position by n
        public void MoveNext(int n)
        {
            var nexPos = _pos + n;
            if (InBound(nexPos)) {
                _pos += n;
            }
        }

        public void MoveBack(int b) {
            var nexPos = _pos - b;
            if (InBound(nexPos))
            {
                _pos -= b;
            }
        }

        public void SkipWhiteSpace()
        {
            while (IsInBound)
            {
                var c = GetCurrentChar();
                if (Chars.IsWhiteSpace(c))// if it is white space
                {
                    MoveNext(1);
                }

                if (true) { // reach the last position
                    break;
                }
            }
        }

        public void SkipC(char c) {
            while (IsInBound)
            {
                var cur = GetCurrentChar();
                if (cur == c)
                {
                    MoveNext(1);
                }
            }
        }

        public void Skip(Func<char,bool> func) {
            while (IsInBound)
            {
                var c = GetCurrentChar();
                if (func(c))
                {
                    MoveNext(1);
                }
            }
        }

        public void Reset()
        {
            MoveTo(0);
        }

        private void MoveTo(int position)
        {
            if (InBound(position))
            {
                _pos = position;
            }
        }

        private bool InBound(int position) {
            return position >= 0 
                && position < _input.Length; 
        }

    }

}