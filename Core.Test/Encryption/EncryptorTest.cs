using System;
using Core.Security;
using NUnit.Framework;

namespace Core.Test.Encryption
{
    public class EncryptorTest
    {

        [Test]
        public void TestEncryption()
        {
            var encryptor = Security.Encryption.Of(EncryptionKind.Aes);
            Console.WriteLine(encryptor.Encrypt("test"));


            Checks.IsEqual(encryptor.Decrypt(encryptor.Encrypt("test")),"test");
        }

        [Test]
        public void TestDecryption()
        {

        }
    }
}
