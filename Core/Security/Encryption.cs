using System;
using System.Security.Cryptography;
using Core.Collection;
using Core.IO;
using Core.Primitive;

namespace Core.Security
{

    public sealed class Encryption
    {
        private EncryptionKind _kind = EncryptionKind.Aes;
        private EncryptionKey  _key;

        // Todo: Wrap this into a class
        private static readonly HashMap<EncryptionKind, SymmetricAlgorithm> Algorithms =
            new HashMap<EncryptionKind, SymmetricAlgorithm>
            {
                { EncryptionKind.Aes, new AesCryptoServiceProvider()}
                // Todo: Add more SymmetricAlgorithm here
            };

        internal Encryption(EncryptionKind kind)
        {
            _kind = kind;
        }

        public static Encryption Of(EncryptionKind kind)
        {
            return new Encryption(kind);
        }

        public bool TryEncrypt(string content, out Base64String result)
        {
            try
            {
                result = Encrypt(content);
                return true;
            }
            catch
            {
                result = default(Base64String);
                return false;
            }
        }

        public Base64String Encrypt(string content)
        {
            var succeed = Algorithms.TryGetValue(_kind, out var symmetricAlgorithm);
            if (!succeed)
            {
                 throw new ArgumentException("{Kind} is not found.");
            }

            using (var ms = Streams.Empty())
            {
                using (var crypto =
                    new CryptoStream(ms, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    var data = Strings.GetBytes(content);
                    crypto.Write(data, 0, data.Length);
                    crypto.FlushFinalBlock();

                    return new Base64String(Streams.GetBytes(ms));
                }
            }
        }

        public bool TryDecrypt(Base64String content, out string result)
        {
            result = string.Empty;
            try
            {
                result = Decrypt(content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Decrypt(Base64String content)
        {
            var succeed = Algorithms.TryGetValue(_kind, out var symmetricAlgorithm);
            if (!succeed)
            {
                throw new ArgumentException("{Kind} is not found.");
            }

            using (var ms = Streams.Of(content.ToBytes()))
            {
                using (var crypto =
                    new CryptoStream(ms,symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    return Streams.GetString(crypto);
                }
            }
        }

    }

    public enum EncryptionKind
    {
       Aes, Des
    }

    

}
