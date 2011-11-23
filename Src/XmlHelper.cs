/**
 * file depends: FileHelper.cs
 *                      ExtXmlReader.cs
 *                      
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-23 Created 
 * */

using System;
using System.Collections.Generic;
using System.Xml;

namespace Src
{
    /// <summary>
    /// Xml 处理辅助类
    /// </summary>
    public class XmlParser
    {
        private readonly string file;

        private readonly Dictionary<string, Action<ExtXmlReader>> actions = new Dictionary<string, Action<ExtXmlReader>>();
        private string currentPath;

        /// <summary>
        /// 初始化<see cref="XmlParser"/>的实例
        /// </summary>
        /// <param name="file">xml 文件路径</param>
        public XmlParser(string file)
        {
            this.file = FileHelper.GetPhysicalPath(file);
        }

        /// <summary>
        /// 添加解析内容
        /// </summary>
        /// <param name="xpath">xpath 路径</param>
        /// <param name="readAction">读取动作</param>
        /// <returns>返回<code>this</code></returns>
        public XmlParser AddParse(string xpath, Action<ExtXmlReader> readAction)
        {
            if (actions.ContainsKey(xpath))
            {
                throw new XmlParserException(string.Format("xpath:{0} 已注册", xpath));
            }
            actions[xpath] = readAction;
            return this;
        }

        /// <summary>
        /// 解析xml内容
        /// </summary>
        public void Parse()
        {
            using (var reader = ExtXmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Whitespace || reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        continue;
                    }
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        RemoveLastName(reader);
                        continue;
                    }
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        currentPath = string.Format("{0}/{1}", currentPath, reader.Name);
                        if (actions.ContainsKey(currentPath) && actions[currentPath] != null)
                        {
                            actions[currentPath](reader);
                        }
                        if (reader.IsEmptyElement)
                        {
                            RemoveLastName(reader);
                        }
                    }
                }
            }
        }

        private void RemoveLastName(ExtXmlReader reader)
        {
            currentPath = currentPath.Remove(currentPath.Length - reader.Name.Length - 1, reader.Name.Length + 1);
        }
    }
}