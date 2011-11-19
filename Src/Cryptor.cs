/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-19 Created 
 * */

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Src
{
    /// <summary>
    /// 加解密类
    /// </summary>
    public class Cryptor : ICryptor
    {
        private const string DefaultKey = "kI@F(9wLt^";
        private readonly byte[] key;

        private Cryptor(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            var keyBytes = Encoding.Unicode.GetBytes(key);
            this.key = Hash(keyBytes);
        }

        /// <summary>
        /// 创建一个加解密实例
        /// </summary>
        /// <returns>返回 <see cref="ICryptor"/> 的实例</returns>
        public static ICryptor Create()
        {
            return Create(DefaultKey);
        }

        /// <summary>
        /// 创建一个加解密实例
        /// </summary>
        /// <param name="key">对称加密的密钥</param>
        /// <returns>返回 <see cref="ICryptor"/> 的实例</returns>
        public static ICryptor Create(string key)
        {
            return new Cryptor(key);
        }
        #region Implementation of ICryptor

        public byte[] Encrypt(byte[] data)
        {
            return Crypt(data, delegate(TripleDES des) { return des.CreateEncryptor(); });
        }

        private byte[] Crypt(byte[] data, Transform transform)
        {
            using (TripleDES des = TripleDES.Create())
            {
                des.Key = GetKey();
                des.IV = GetIV();
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, transform(des), CryptoStreamMode.Write))
                    {
                        cStream.Write(data, 0, data.Length);
                        cStream.FlushFinalBlock();
                    }
                    return mStream.ToArray();
                }
            }
        }

        private delegate ICryptoTransform Transform(TripleDES des);


        private byte[] GetIV()
        {
            const int IVLength = 8;
            byte[] result = new byte[IVLength];
            for (int i = 0; i < IVLength; i++)
            {
                result[i] = (byte)(key[i] ^ key[i + 8]);
            }
            return result;
        }
        private byte[] GetKey()
        {
            const int KeyLength = 24;
            byte[] result = new byte[KeyLength];
            for (int i = 0; i < key.Length; i++)
            {
                result[i] = key[i];
            }
            for (int i = key.Length; i < KeyLength; i++)
            {
                int mod = (i % key.Length) * 2;
                result[i] = (byte)(key[mod] ^ key[mod + 1]);
            }
            return result;
        }

        public string EncryptToBase64(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] cryptedBytes = Encrypt(bytes);
            return Convert.ToBase64String(cryptedBytes);
        }

        public byte[] Decrypt(byte[] data)
        {
            return Crypt(data, delegate(TripleDES des) { return des.CreateDecryptor(); });
        }

        public string DecryptFromBase64(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            byte[] decryptedBytes = Decrypt(bytes);
            return Encoding.Unicode.GetString(decryptedBytes);
        }

        public byte[] Hash(byte[] data)
        {
            MD5 md5 = MD5.Create();
            return md5.ComputeHash(data);
        }

        public string HashToBase64(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] hashedBytes = Hash(bytes);
            return Convert.ToBase64String(hashedBytes);
        }

        #endregion
    }
}