/**
 * file depends: IDbHelper.cs
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-12-02 Created 
 * */

using System.Collections.Generic;

namespace Src
{
    /// <summary>
    /// 扩展<see cref="IDbHelper"/>的功能
    /// </summary>
    public interface IDbHelperEx : IDbHelper
    {
        /// <summary>
        /// 提取第一行的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>对象</returns>
        T FirstObject<T>(string sql, params object[] values) where T : class ,new();

        /// <summary>
        /// 提取第一行第一个值，并转换为基础数据类型
        /// </summary>
        /// <typeparam name="T">基础数据类型</typeparam>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>基础数据类型对象</returns>
        T FirstValue<T>(string sql, params object[] values);

        /// <summary>
        /// 提取并转换为对象迭代器
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>对象迭代器</returns>
        IEnumerable<T> GetObjects<T>(string sql, params object[] values) where T : class ,new();

        /// <summary>
        /// 提取并转换为基础数据类型的迭代器
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>基础数据类型对象迭代器</returns>
        IEnumerable<T> GetValues<T>(string sql, params object[] values);
    }
}