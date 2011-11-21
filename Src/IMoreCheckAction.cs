namespace Src
{
    /// <summary>
    /// 更多检查动作接口
    /// </summary>
    public interface IMoreCheckAction
    {
        /// <summary>
        /// 或
        /// </summary>
        /// <param name="anotherTarget">另一个需要检查的对象</param>
        /// <param name="pName">参数名（用于验证未通过时，进行提示）</param>
        /// <returns><see cref="IParameterChecker"/></returns>
        IParameterChecker Or(object anotherTarget, string pName);

        /// <summary>
        /// 或
        /// </summary>
        /// <returns><see cref="IParameterChecker"/></returns>
        IParameterChecker Or();
    }
}