/**
 * file depends: 
 *                      
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-23 Created 
 * */
using System;
using System.Runtime.Serialization;

namespace Src
{
    /// <summary>
    /// 处理<see cref="XmlParser"/>的异常信息
    /// </summary>
    [Serializable]
    public class XmlParserException : Exception
    {
        /// <summary>
        /// 初始化<see cref="XmlParserException"/>的实例
        /// </summary>
        public XmlParserException()
        {
        }

        /// <summary>
        /// 初始化<see cref="XmlParserException"/>的实例
        /// </summary>
        /// <param name="message">消息内容</param>
        public XmlParserException(string message)
            : base(message)
        {
        }

        protected XmlParserException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}