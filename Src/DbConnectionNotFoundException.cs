/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-18 Created 
 * */
using System;

namespace Src
{
    /// <summary>
    /// ���ݿ������ַ���δ���ҵ����쳣
    /// </summary>
    public class DbConnectionNotFoundException : ApplicationException
    {
        /// <summary>
        /// ��ʼ��<see cref="DbConnectionNotFoundException"/>
        /// </summary>
        public DbConnectionNotFoundException()
            : base("û���� config��web.config/app.config/machine.config �ļ����ҵ������ַ�����Ϣ")
        {
            
        }
    }
}