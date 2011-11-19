// <copyright file="CryptorTest.cs" company="Microsoft">Copyright ?Microsoft 2011</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Src;

namespace Src
{
    /// <summary>This class contains parameterized unit tests for Cryptor</summary>
    [PexClass(typeof(Cryptor))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CryptorTest
    {
        /// <summary>Test stub for Create()</summary>
        [PexMethod]
        public ICryptor Create()
        {
            ICryptor result = Cryptor.Create();
            return result;
            // TODO: add assertions to method CryptorTest.Create()
        }

        /// <summary>Test stub for Create(String)</summary>
        [PexMethod]
        public ICryptor Create01(string key)
        {
            ICryptor result = Cryptor.Create(key);
            return result;
            // TODO: add assertions to method CryptorTest.Create01(String)
        }

        /// <summary>Test stub for Decrypt(Byte[])</summary>
        [PexMethod]
        public byte[] Decrypt([PexAssumeUnderTest]Cryptor target, byte[] data)
        {
            byte[] result = target.Decrypt(data);
            return result;
            // TODO: add assertions to method CryptorTest.Decrypt(Cryptor, Byte[])
        }

        /// <summary>Test stub for DecryptFromBase64(String)</summary>
        [PexMethod]
        public string DecryptFromBase64([PexAssumeUnderTest]Cryptor target, string data)
        {
            string result = target.DecryptFromBase64(data);
            return result;
            // TODO: add assertions to method CryptorTest.DecryptFromBase64(Cryptor, String)
        }

        /// <summary>Test stub for Encrypt(Byte[])</summary>
        [PexMethod]
        public byte[] Encrypt([PexAssumeUnderTest]Cryptor target, byte[] data)
        {
            byte[] result = target.Encrypt(data);
            return result;
            // TODO: add assertions to method CryptorTest.Encrypt(Cryptor, Byte[])
        }

        /// <summary>Test stub for EncryptToBase64(String)</summary>
        [PexMethod]
        public string EncryptToBase64([PexAssumeUnderTest]Cryptor target, string data)
        {
            string result = target.EncryptToBase64(data);
            return result;
            // TODO: add assertions to method CryptorTest.EncryptToBase64(Cryptor, String)
        }

        /// <summary>Test stub for Hash(Byte[])</summary>
        [PexMethod]
        public byte[] Hash([PexAssumeUnderTest]Cryptor target, byte[] data)
        {
            byte[] result = target.Hash(data);
            return result;
            // TODO: add assertions to method CryptorTest.Hash(Cryptor, Byte[])
        }

        /// <summary>Test stub for HashToBase64(String)</summary>
        [PexMethod]
        public string HashToBase64([PexAssumeUnderTest]Cryptor target, string data)
        {
            string result = target.HashToBase64(data);
            return result;
            // TODO: add assertions to method CryptorTest.HashToBase64(Cryptor, String)
        }

        [TestMethod]
        public void TestEncryptToBase64()
        {
            string data = "需要加密的数据";
            string key = "passw0rd";
            ICryptor cryptor = Cryptor.Create(key);
            string output = cryptor.EncryptToBase64(data);
            string input = cryptor.DecryptFromBase64(output);
            string hash = cryptor.HashToBase64(key);
            Assert.AreEqual("需要加密的数据", input);
        }
    }
}
