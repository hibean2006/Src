/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-18 Created 
 * */
using System;

namespace Src
{
    /// <summary>
    /// 数据库连接字符串未能找到的异常
    /// </summary>
    public class DbConnectionNotFoundException : ApplicationException
    {
        /// <summary>
        /// 初始化<see cref="DbConnectionNotFoundException"/>
        /// </summary>
        public DbConnectionNotFoundException()
            : base("没有在 config（web.config/app.config/machine.config 文件中找到连接字符串信息")
        {
            
        }
    }
}