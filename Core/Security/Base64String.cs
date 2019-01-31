using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Core.Collection;
using Core.Primitive;

namespace Core.Security
{

    public sealed class Base64String : IComparable, IEnumerable<char>, IComparable<string>, IEquatable<string>, IConvertible, ICloneable
    {

        private readonly byte[] _store;
        private readonly string _base64String;

        public Base64String(byte[] data)
        {
            _store = data;
        }

        /// <summary>
        /// Not safe
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            var buffer = Arrays.Make<byte>(_store.Length);
            Buffer.BlockCopy(_store, 0, buffer,0, _store.Length);
            return buffer;
        }

        /// <summary>
        /// Not safe
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return ToString();
        }

        public override string ToString()
        {
            var base64String = _base64String;
            LazyInitializer.EnsureInitialized(ref base64String, () => Strings.Of(_base64String));
            return base64String;
        }

        public int CompareTo(object obj)
        {
            return _base64String.CompareTo(obj);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return _base64String.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(string other)
        {
            return String.Compare(_base64String, other, StringComparison.Ordinal);
        }

        public bool Equals(string other)
        {
            return _base64String.Equals(other);
        }

        public TypeCode GetTypeCode()
        {
            return _base64String.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_base64String, provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_base64String, provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_base64String, provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_base64String, provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_base64String, provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_base64String, provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_base64String, provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_base64String, provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_base64String, provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_base64String, provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_base64String, provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return _base64String.ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return null;
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_base64String, provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_base64String, provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_base64String, provider);
        }

        public object Clone()
        {
            return _base64String.Clone();
        }

        

    }

}