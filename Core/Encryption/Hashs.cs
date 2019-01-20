using System;
using System.Security.Cryptography;
using System.Text;
using Core.IO;

namespace Core.Encryption
{

    public sealed class Hashing
    {
        // Used for MD5 Hex Hash
        public static string MD5Hex(string input)
        {
            using (var md5Hash = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input)))
                                   .Replace("-", string.Empty);
            }
        }

        // Used for MD5 Base64 Hash
        public static string MD5Base64(string input)
        {
            using (var md5Hash = new MD5CryptoServiceProvider())
            {
                return Convert.ToBase64String(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input)))
                              .Replace("+", string.Empty)
                              .Replace("/", string.Empty)
                              .Replace("-", string.Empty)
                              .Replace("=", string.Empty);
            }
        }

        // Used for password hash cos the speed is fastest
        public static string SHA1Hex(string input)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)))
                                   .Replace("-", string.Empty);
            }
        }

        // Used for password hash cos the speed is fastest
        public static string SHA1Base64(string input)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)))
                              .Replace("+", string.Empty)
                              .Replace("/", string.Empty)
                              .Replace("-", string.Empty)
                              .Replace("=", string.Empty);
            }
        }

        // Used for password hash for high security
        public static string SHA256Hex(string input)
        {
            using (var mySha256 = new SHA256CryptoServiceProvider())
            {
                return BitConverter.ToString(mySha256.ComputeHash(Encoding.UTF8.GetBytes(input)))
                                   .Replace("-", string.Empty);
            }
        }

        // Used for password hash for high security
        public static string SHA256Base64(string input)
        {
            using (var mySha256 = new SHA256CryptoServiceProvider())
            {
                return Convert.ToBase64String(mySha256.ComputeHash(Encoding.UTF8.GetBytes(input)))
                              .Replace("+", string.Empty)
                              .Replace("/", string.Empty)
                              .Replace("-", string.Empty)
                              .Replace("=", string.Empty);
            }
        }

        // Used for password hash for highest security
        public static string SHA512Hex(string input)
        {
            using (var mySha512 = new SHA512CryptoServiceProvider())
            {
                return BitConverter.ToString(mySha512.ComputeHash(Encoding.UTF8.GetBytes(input)))
                                   .Replace("-", string.Empty);
            }
        }

        // Used for password hash for highest security
        public static string SHA512Base64(string input)
        {
            using (var mySha512 = new SHA512CryptoServiceProvider())
            {
                return Convert.ToBase64String(mySha512.ComputeHash(Encoding.UTF8.GetBytes(input)))
                              .Replace("+", string.Empty)
                              .Replace("/", string.Empty)
                              .Replace("-", string.Empty)
                              .Replace("=", string.Empty);
            }
        }
    }

}