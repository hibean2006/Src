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
    /// ����<see cref="XmlParser"/>���쳣��Ϣ
    /// </summary>
    [Serializable]
    public class XmlParserException : Exception
    {
        /// <summary>
        /// ��ʼ��<see cref="XmlParserException"/>��ʵ��
        /// </summary>
        public XmlParserException()
        {
        }

        /// <summary>
        /// ��ʼ��<see cref="XmlParserException"/>��ʵ��
        /// </summary>
        /// <param name="message">��Ϣ����</param>
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