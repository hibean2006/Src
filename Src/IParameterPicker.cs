namespace Src
{
    /// <summary>
    /// ����ץȡ�ӿڣ�ץȡ��Ҫ���Ĳ�����
    /// </summary>
    public interface IParameterPicker
    {
        /// <summary>
        /// ��Ҫ������
        /// </summary>
        /// <param name="target">Ŀ�����</param>
        /// <param name="pName">��������������֤δͨ��ʱ��������ʾ��</param>
        /// <returns><see cref="IParameterChecker"/></returns>
        IParameterChecker When(object target, string pName);
    }
}