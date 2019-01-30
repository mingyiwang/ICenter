using System;
using System.Security.Cryptography;
using Core.Collection;
using Core.IO;
using Core.Primitive;

namespace Core.Encryption
{

    public sealed class Encryptor
    {
        private EncryptionKind _kind = EncryptionKind.Aes;
        private ISymmetricAlgorithmKey _key;

        // Todo: Wrap this into a class
        private static readonly HashMap<EncryptionKind, SymmetricAlgorithm> Algorithms =
            new HashMap<EncryptionKind, SymmetricAlgorithm>
            {
                { EncryptionKind.Aes, new AesCryptoServiceProvider()}
                // Todo: Add more SymmetricAlgorithm here
            };

        internal Encryptor(EncryptionKind kind)
        {
            _kind = kind;
        }

        public static Encryptor Of(EncryptionKind kind)
        {
            return new Encryptor(kind);
        }

        public bool TryEncrypt(string content, out string result)
        {
            result = string.Empty;
            try
            {
                result = Encrypt(content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Encrypt(string content)
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
                    return Convert.ToBase64String(Streams.GetBytes(ms));
                }
            }
        }

        public bool TryDecrypt(string content, out string result)
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

        public string Decrypt(string content)
        {
            var succeed = Algorithms.TryGetValue(_kind, out var symmetricAlgorithm);
            if (!succeed)
            {
                throw new ArgumentException("{Kind} is not found.");
            }

            using (var ms = Streams.Of(Convert.FromBase64String(content)))
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

    public interface ISymmetricAlgorithmKey
    {
        
    }

}
