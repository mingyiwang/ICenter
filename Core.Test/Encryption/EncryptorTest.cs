using System;
using Core.Encryption;
using NUnit.Framework;

namespace Core.Test.Encryption
{
    public class EncryptorTest
    {

        [Test]
        public void TestEncryption()
        {
            var encryptor = Encryptor.Of(EncryptionKind.Aes);
            Checks.IsEqual(encryptor.Decrypt(encryptor.Encrypt("")),"");
        }

        [Test]
        public void TestDecryption()
        {

        }
    }
}
