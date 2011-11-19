/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-19 Created 
 * */
namespace Src
{
    /// <summary>
    /// �ӽ��ܽӿ�
    /// </summary>
    public interface ICryptor
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="data">��Ҫ���ܵ�����</param>
        /// <returns>���ؼ�������</returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// �����ַ�������ת��Ϊ Base64
        /// </summary>
        /// <param name="data">��Ҫ���ܵ��ַ���</param>
        /// <returns>���ؼ�������</returns>
        string EncryptToBase64(string data);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="data">��Ҫ���ܵ�����</param>
        /// <returns>���ؽ�������</returns>
        byte[] Decrypt(byte[] data);

        /// <summary>
        /// �� Base64 �����ݣ����н���
        /// </summary>
        /// <param name="data">��Ҫ���ܵ�Base64�ַ���</param>
        /// <returns>���ؽ�������</returns>
        string DecryptFromBase64(string data);

        /// <summary>
        /// �����ݽ��й�ϣ
        /// </summary>
        /// <param name="data">��Ҫ��ϣ������</param>
        /// <returns>���ع�ϣֵ</returns>
        byte[] Hash(byte[] data);

        /// <summary>
        /// ���ַ������ݽ��й�ϣ����ת��Ϊ Base64
        /// </summary>
        /// <param name="data">��Ҫ��ϣ������</param>
        /// <returns>���ع�ϣֵ</returns>
        string HashToBase64(string data);
    }
}