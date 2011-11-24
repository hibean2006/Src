/**
 * file depends:  FileHelper.cs
 *                      Action.cs
 *                      
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-23 Created 
 * 2011-11-24 Renamed
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Src
{
    /// <summary>
    /// 扩展<see cref="XmlReader"/>的方法
    /// </summary>
    public class XmlReaderEx : XmlReader
    {
        private readonly XmlReader innerReader;//内部用来读取数据的XmlReader
        private readonly Stack stack = new Stack();//关联对象的栈
        private readonly Dictionary<string, Action> actions = new Dictionary<string, Action>();//解析动作
        private string currentPath;//当前读取的路径
        private readonly object Empty = new object();//当没有关联对象时，压入该对象
        private bool pushed;//是否已关联了对象

        /// <summary>
        /// 初始化<see cref="XmlReaderEx"/>的实例
        /// </summary>
        /// <param name="innerReader">内部用于处理的<see cref="XmlReader"/>实例</param>
        private XmlReaderEx(XmlReader innerReader)
        {
            this.innerReader = innerReader;
        }

        #region 静态 Create 方法
        /// <summary>
        /// <see cref="XmlReader.Create(XmlReader, XmlReaderSettings)"/>
        /// </summary>
        public static new XmlReaderEx Create(XmlReader reader, XmlReaderSettings settings)
        {
            return new XmlReaderEx(XmlReader.Create(reader, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader, XmlReaderSettings, XmlParserContext)"/>
        /// </summary>
        public static new XmlReaderEx Create(TextReader input, XmlReaderSettings settings, XmlParserContext inputContext)
        {
            return new XmlReaderEx(XmlReader.Create(input, settings, inputContext));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader, XmlReaderSettings, string)"/>
        /// </summary>
        public static new XmlReaderEx Create(TextReader input, XmlReaderSettings settings, string baseUri)
        {
            return new XmlReaderEx(XmlReader.Create(input, settings, baseUri));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader)"/>
        /// </summary>
        public static new XmlReaderEx Create(TextReader input, XmlReaderSettings settings)
        {
            return new XmlReaderEx(XmlReader.Create(input, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(TextReader)"/>
        /// </summary>
        public static new XmlReaderEx Create(TextReader input)
        {
            return new XmlReaderEx(XmlReader.Create(input));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream, XmlReaderSettings, XmlParserContext)"/>
        /// </summary>
        public static new XmlReaderEx Create(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
        {
            return new XmlReaderEx(XmlReader.Create(input));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream, XmlReaderSettings, string)"/>
        /// </summary>
        public static new XmlReaderEx Create(Stream input, XmlReaderSettings settings, string baseUri)
        {
            return new XmlReaderEx(XmlReader.Create(input, settings, baseUri));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream, XmlReaderSettings)"/>
        /// </summary>
        public static new XmlReaderEx Create(Stream input, XmlReaderSettings settings)
        {
            return new XmlReaderEx(XmlReader.Create(input, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(Stream)"/>
        /// </summary>
        public static new XmlReaderEx Create(Stream input)
        {
            return new XmlReaderEx(XmlReader.Create(input));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(string, XmlReaderSettings, XmlParserContext)"/>
        /// </summary>
        public static new XmlReaderEx Create(string inputUri, XmlReaderSettings settings, XmlParserContext inputContext)
        {
            return new XmlReaderEx(XmlReader.Create(inputUri, settings, inputContext));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(string, XmlReaderSettings)"/>
        /// </summary>
        public static new XmlReaderEx Create(string inputUri, XmlReaderSettings settings)
        {
            return new XmlReaderEx(XmlReader.Create(inputUri, settings));
        }

        /// <summary>
        /// <see cref="XmlReader.Create(string)"/>
        /// </summary>
        public static new XmlReaderEx Create(string inputUri)
        {
            return new XmlReaderEx(XmlReader.Create(inputUri));
        }

        /// <summary>
        /// 从文件创建<see cref="XmlReaderEx"/>对象
        /// </summary>
        /// <param name="file">文件路径（可用~表示应用根目录）</param>
        public static XmlReaderEx CreateFromFile(string file)
        {
            return new XmlReaderEx(XmlReader.Create(FileHelper.GetPhysicalPath(file)));
        }

        #endregion

        #region 扩展转化类型的方法

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

        #region 扩展解析方法
        /// <summary>
        /// 在当前位置关联到某个对象
        /// </summary>
        /// <param name="obj"></param>
        public void Attach(object obj)
        {
            if (pushed)
            {
                throw new XmlParserException("当前节点已 Attach 对象");
            }
            stack.Push(obj);
            pushed = true;
        }

        /// <summary>
        /// 获取前一Attach上的对象
        /// </summary>
        /// <returns>如果在父节点调用了<see cref="Attach"/>方法，则返回该对象，否则返回 <c>null</c></returns>
        public object GetAttach()
        {
            return stack.Peek();
        }

        /// <summary>
        /// 弹出前一节点的对象
        /// </summary>
        private void Pop()
        {
            stack.Pop();
        }

        /// <summary>
        /// 添加解析内容
        /// </summary>
        /// <param name="xpath">xpath 路径</param>
        /// <param name="readAction">读取动作</param>
        /// <returns>返回<code>this</code></returns>
        public XmlReaderEx AddParse(string xpath, Action readAction)
        {
            if (actions.ContainsKey(xpath))
            {
                throw new XmlParserException(string.Format("xpath:{0} 已注册", xpath));
            }
            actions[xpath] = readAction;
            return this;
        }

        /// <summary>
        /// 读取内容并解析
        /// </summary>
        public void Go()
        {
            while (Read())
            {
                if (NodeType == XmlNodeType.Whitespace || NodeType == XmlNodeType.XmlDeclaration)
                {
                    continue;
                }
                if (NodeType == XmlNodeType.EndElement)
                {
                    MoveUp();
                    continue;
                }
                if (NodeType != XmlNodeType.Element) continue;
                currentPath = string.Format("{0}/{1}", currentPath, Name);
                pushed = false;
                if (actions.ContainsKey(currentPath) && actions[currentPath] != null)
                {
                    actions[currentPath]();
                }
                if (!pushed)
                {
                    Attach(Empty);
                }
                if (IsEmptyElement)
                {
                    MoveUp();
                }
            }
        }

        /// <summary>
        /// 移动到上一级了
        /// </summary>
        private void MoveUp()
        {
            currentPath = currentPath.Remove(currentPath.Length - Name.Length - 1, Name.Length + 1);
            Pop();
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