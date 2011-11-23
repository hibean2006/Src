/**
 * file depends:  
 *                      
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-23 Created 
 * */
using System;
using System.IO;
using System.Xml;

namespace Src
{
    /// <summary>
    /// 扩展<see cref="XmlReader"/>的方法
    /// </summary>
    public class ExtXmlReader : XmlReader
    {
        private readonly XmlReader innerReader;

        /// <summary>
        /// 初始化<see cref="ExtXmlReader"/>的实例
        /// </summary>
        /// <param name="innerReader">内部用于处理的<see cref="XmlReader"/>实例</param>
        private ExtXmlReader(XmlReader innerReader)
        {
            this.innerReader = innerReader;
        }

        #region 静态 Create 方法
        /// <summary>
        /// <see cref="XmlReader.Create(XmlReader, XmlReaderSettings)"/>
        /// </summary>
        public static new ExtXmlReader Create(XmlReader reader, XmlReaderSettings settings)
        {
            return new ExtXmlReader(XmlReader.Create(reader, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader, XmlReaderSettings, XmlParserContext)"/>
        /// </summary>
        public static new ExtXmlReader Create(TextReader input, XmlReaderSettings settings, XmlParserContext inputContext)
        {
            return new ExtXmlReader(XmlReader.Create(input, settings, inputContext));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader, XmlReaderSettings, string)"/>
        /// </summary>
        public static new ExtXmlReader Create(TextReader input, XmlReaderSettings settings, string baseUri)
        {
            return new ExtXmlReader(XmlReader.Create(input, settings, baseUri));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader)"/>
        /// </summary>
        public static new ExtXmlReader Create(TextReader input, XmlReaderSettings settings)
        {
            return new ExtXmlReader(XmlReader.Create(input, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader)"/>
        /// </summary>
        public static new ExtXmlReader Create(TextReader input)
        {
            return new ExtXmlReader(XmlReader.Create(input));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream, XmlReaderSettings, XmlParserContext)"/>
        /// </summary>
        public static new ExtXmlReader Create(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
        {
            return new ExtXmlReader(XmlReader.Create(input));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream, XmlReaderSettings, string)"/>
        /// </summary>
        public static new ExtXmlReader Create(Stream input, XmlReaderSettings settings, string baseUri)
        {
            return new ExtXmlReader(XmlReader.Create(input, settings, baseUri));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream, XmlReaderSettings)"/>
        /// </summary>
        public static new ExtXmlReader Create(Stream input, XmlReaderSettings settings)
        {
            return new ExtXmlReader(XmlReader.Create(input, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream)"/>
        /// </summary>
        public static new ExtXmlReader Create(Stream input)
        {
            return new ExtXmlReader(XmlReader.Create(input));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(string, XmlReaderSettings, XmlParserContext)"/>
        /// </summary>
        public static new ExtXmlReader Create(string inputUri, XmlReaderSettings settings, XmlParserContext inputContext)
        {
            return new ExtXmlReader(XmlReader.Create(inputUri, settings, inputContext));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(string, XmlReaderSettings)"/>
        /// </summary>
        public static new ExtXmlReader Create(string inputUri, XmlReaderSettings settings)
        {
            return new ExtXmlReader(XmlReader.Create(inputUri, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(string)"/>
        /// </summary>
        public static new ExtXmlReader Create(string inputUri)
        {
            return new ExtXmlReader(XmlReader.Create(inputUri));
        }

        #endregion

        #region 扩展的方法

        /// <summary>
        /// 读取属性并转换为Int32，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public int ReadAttrToInt32(string name)
        {
            return Convert.ToInt32(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Boolean，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public bool ReadAttrToBoolean(string name)
        {
            return Convert.ToBoolean(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Char，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public char ReadAttrToChar(string name)
        {
            return Convert.ToChar(GetAttribute(name) ?? "\0");
        }

        /// <summary>
        /// 读取属性并转换为DateTime，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public DateTime ReadAttrToDateTime(string name)
        {
            return Convert.ToDateTime(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Decimal，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public decimal ReadAttrToDecimal(string name)
        {
            return Convert.ToInt32(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Double，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public double ReadAttrToDouble(string name)
        {
            return Convert.ToDouble(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Int16，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public short ReadAttrToInt16(string name)
        {
            return Convert.ToInt16(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Int64，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public long ReadAttrToInt64(string name)
        {
            return Convert.ToInt64(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为SByte，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public sbyte ReadAttrToSByte(string name)
        {
            return Convert.ToSByte(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为Single，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public float ReadAttrToSingle(string name)
        {
            return Convert.ToSingle(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为UInt16，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public ushort ReadAttrToUInt16(string name)
        {
            return Convert.ToUInt16(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为UInt32，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public uint ReadAttrToUInt32(string name)
        {
            return Convert.ToUInt32(GetAttribute(name));
        }

        /// <summary>
        /// 读取属性并转换为UInt64，<see cref="XmlReader.GetAttribute(string)"/>
        /// </summary>
        /// <param name="name">属性的限定名</param>
        public ulong ReadAttrToUInt64(string name)
        {
            return Convert.ToUInt64(GetAttribute(name));
        }

        #endregion

        #region Overrides of XmlReader

        public override string GetAttribute(string name)
        {
            return innerReader.GetAttribute(name);
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return innerReader.GetAttribute(name, namespaceURI);
        }

        public override string GetAttribute(int i)
        {
            return innerReader.GetAttribute(i);
        }

        public override bool MoveToAttribute(string name)
        {
            return innerReader.MoveToAttribute(name);
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            return innerReader.MoveToAttribute(name, ns);
        }

        public override bool MoveToFirstAttribute()
        {
            return innerReader.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            return innerReader.MoveToNextAttribute();
        }

        public override bool MoveToElement()
        {
            return innerReader.MoveToElement();
        }

        public override bool ReadAttributeValue()
        {
            return innerReader.ReadAttributeValue();
        }

        public override bool Read()
        {
            return innerReader.Read();
        }

        public override void Close()
        {
            innerReader.Close();
        }

        public override string LookupNamespace(string prefix)
        {
            return innerReader.LookupNamespace(prefix);
        }

        public override void ResolveEntity()
        {
            innerReader.ResolveEntity();
        }

        public override XmlNodeType NodeType
        {
            get
            {
                return innerReader.NodeType;
            }
        }

        public override string LocalName
        {
            get
            {
                return innerReader.LocalName;
            }
        }

        public override string NamespaceURI
        {
            get
            {
                return innerReader.NamespaceURI;
            }
        }

        public override string Prefix
        {
            get
            {
                return innerReader.Prefix;
            }
        }

        public override bool HasValue
        {
            get
            {
                return innerReader.HasValue;
            }
        }

        public override string Value
        {
            get
            {
                return innerReader.Value;
            }
        }

        public override int Depth
        {
            get
            {
                return innerReader.Depth;
            }
        }

        public override string BaseURI
        {
            get
            {
                return innerReader.BaseURI;
            }
        }

        public override bool IsEmptyElement
        {
            get
            {
                return innerReader.IsEmptyElement;
            }
        }

        public override int AttributeCount
        {
            get
            {
                return innerReader.AttributeCount;
            }
        }

        public override bool EOF
        {
            get
            {
                return innerReader.EOF;
            }
        }

        public override ReadState ReadState
        {
            get
            {
                return innerReader.ReadState;
            }
        }

        public override XmlNameTable NameTable
        {
            get
            {
                return innerReader.NameTable;

            }
        }

        #endregion
    }
}