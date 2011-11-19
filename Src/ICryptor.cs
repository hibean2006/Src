/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-19 Created 
 * */
namespace Src
{
    /// <summary>
    /// 加解密接口
    /// </summary>
    public interface ICryptor
    {
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data">需要加密的数据</param>
        /// <returns>返回加密内容</returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// 加密字符串，并转换为 Base64
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <returns>返回加密数据</returns>
        string EncryptToBase64(string data);

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data">需要解密的内容</param>
        /// <returns>返回解密数据</returns>
        byte[] Decrypt(byte[] data);

        /// <summary>
        /// 从 Base64 的数据，进行解密
        /// </summary>
        /// <param name="data">需要加密的Base64字符串</param>
        /// <returns>返回解密数据</returns>
        string DecryptFromBase64(string data);

        /// <summary>
        /// 将数据进行哈希
        /// </summary>
        /// <param name="data">需要哈希的数据</param>
        /// <returns>返回哈希值</returns>
        byte[] Hash(byte[] data);

        /// <summary>
        /// 将字符串数据进行哈希，并转换为 Base64
        /// </summary>
        /// <param name="data">需要哈希的数据</param>
        /// <returns>返回哈希值</returns>
        string HashToBase64(string data);
    }
}