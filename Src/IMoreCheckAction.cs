namespace Src
{
    /// <summary>
    /// �����鶯���ӿ�
    /// </summary>
    public interface IMoreCheckAction
    {
        /// <summary>
        /// ��
        /// </summary>
        /// <param name="anotherTarget">��һ����Ҫ���Ķ���</param>
        /// <param name="pName">��������������֤δͨ��ʱ��������ʾ��</param>
        /// <returns><see cref="IParameterChecker"/></returns>
        IParameterChecker Or(object anotherTarget, string pName);

        /// <summary>
        /// ��
        /// </summary>
        /// <returns><see cref="IParameterChecker"/></returns>
        IParameterChecker Or();
    }
}