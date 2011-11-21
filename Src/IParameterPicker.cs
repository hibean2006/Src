namespace Src
{
    /// <summary>
    /// 参数抓取接口（抓取需要检查的参数）
    /// </summary>
    public interface IParameterPicker
    {
        /// <summary>
        /// 需要检查参数
        /// </summary>
        /// <param name="target">目标参数</param>
        /// <param name="pName">参数名（用于验证未通过时，进行提示）</param>
        /// <returns><see cref="IParameterChecker"/></returns>
        IParameterChecker When(object target, string pName);
    }
}