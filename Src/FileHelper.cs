/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-17 Created 
 * */

using System;
using System.IO;
using System.Security;

namespace Src
{
    /// <summary>
    /// 文件操作辅助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取目录或文件的物理路径
        /// </summary>
        /// <param name="path">目录或文件路径（物理路径或使用~表示应用程序根目录的路径）</param>
        /// <remarks>并不确保路径存在。</remarks>
        /// <returns>目录或文件的物理路径</returns>
        /// <exception cref="ArgumentException"><see cref="Path.GetFullPath"/></exception>
        /// <exception cref="NotSupportedException"><see cref="Path.GetFullPath"/></exception>
        /// <exception cref="PathTooLongException"><see cref="Path.GetFullPath"/></exception>
        /// <exception cref="SecurityException"><see cref="Path.GetFullPath"/></exception>
        public static string GetPhysicalPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            if(path.StartsWith("~"))
            {
                return string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, path.Remove(0, 1));
            }
            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 确保目录存在（如果目录不存在，会创建一个目录）
        /// </summary>
        /// <param name="path">目录或文件路径
        /// <remarks>可使用～表示应用根目录</remarks>
        /// </param>
        /// <example>
        /// <code>
        /// //如果根目录下不存在 Upload 文件夹，则创建
        /// FileHelper.EnsureDirectory("~/Upload/File1.txt");
        /// </code>
        /// </example>
        /// <exception cref="ArgumentException"><see cref="Path.GetFullPath"/></exception>
        /// <exception cref="NotSupportedException"><see cref="Path.GetFullPath"/></exception>
        /// <exception cref="PathTooLongException"><see cref="Path.GetFullPath"/></exception>
        /// <exception cref="SecurityException"><see cref="Path.GetFullPath"/></exception>
        public static void EnsureDirectory(string path)
        {
            string physcialPath = GetPhysicalPath(path);
            string dirPath = Path.GetDirectoryName(physcialPath);
            if (dirPath == null)
            {
                throw new ApplicationException("查找文件夹出错，物理路径为：" + physcialPath);
            }

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }
}