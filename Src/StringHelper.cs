/**
 * Haibin Zou
 * hibean2006@126.com
 * 2011-11-16 Created
 * 
 * */

using System;

namespace Src
{
    /// <summary>
    /// <see cref="string"/> 辅助类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 获取介于 <see cref="left"/> 与 <see cref="right"/> 之间的字符串
        /// </summary>
        /// <param name="source"><see cref="string"/>源</param>
        /// <param name="left">目标字符串左边的部分</param>
        /// <param name="right">目标字符串右边的部分</param>
        /// <returns>介于 <see cref="left"/> 与 <see cref="right"/> 之间的字符串</returns>
        /// <example>
        /// <code>
        /// string str = StringHelper.Between("abc", "a", "c");
        /// //str 的结果为 "b"
        /// </code>
        /// </example>
        public static string Between(string source, string left, string right)
        {
            if (left == null)
            {
                throw new ArgumentNullException("left");
            }
            if (right == null)
            {
                throw new ArgumentNullException("right");
            }
            if (source == null)
            {
                return null;
            }

            int leftIndex = source.IndexOf(left);
            int rightIndex = source.IndexOf(right);

            //left 或 right不存在，或者 right 的位置在 left 左边
            if (leftIndex < 0 || rightIndex < 0 || rightIndex < leftIndex + left.Length)
            {
                return string.Empty;
            }

            return source.Substring(leftIndex + left.Length, rightIndex - leftIndex - left.Length);
        }
    }
}