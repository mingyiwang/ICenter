using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Core.Collection;

namespace Core.Security
{

    public class Base64String : IEnumerable<char>, IComparable<string>, IEquatable<string>, IConvertible, ICloneable
    {

        private readonly byte[] _store;

        private Base64String(byte[] data)
        {
            _store = data;
        }

        public static Base64String FromString(string content)
        {
            return new Base64String(Encoding.UTF8.GetBytes(content));
        }

        public static Base64String FromBytes(byte[] data)
        {
            return new Base64String(data);
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


        public IEnumerator<char> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }

}