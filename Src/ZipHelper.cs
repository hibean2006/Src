/**
 * file depends: ICSharpCode.SharpZipLib.dll
 *                      FileHelper.cs
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-24 Created
 * */
using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace Src
{
    /// <summary>
    /// ѹ����ѹ�������ࡣ
    /// </summary>
    public class ZipHelper
    {
        private const int SIZE = 1024 * 1024;
        private readonly static Crc32 crc = new Crc32();
        /// <summary>
        /// ִ��ѹ����
        /// <param name="targetFileName">Ŀ��ѹ���ļ���</param>
        /// <param name="sourceFilesName">Դ�ļ���</param>
        /// </summary>
        public static void Compress(string targetFileName, params string[] sourceFilesName)
        {
            string physicalFile = FileHelper.GetPhysicalPath(targetFileName);
            FileHelper.EnsureDirectory(physicalFile);
            using (Stream targetFileStream = File.Create(targetFileName))
            {
                using (ZipOutputStream outStream = new ZipOutputStream(targetFileStream))
                {
                    foreach (string compressFile in sourceFilesName)
                    {
                        Compress(compressFile, outStream);
                    }
                }
            }
        }

        ///<summary>
        /// ѹ��Ŀ¼��
        ///</summary>
        ///<param name="targetFileName">Ŀ���ļ���</param>
        ///<param name="directory">��Ҫѹ����Ŀ¼��</param>
        public static void Compress(string targetFileName, string directory)
        {
            Compress(targetFileName, Directory.GetFiles(directory));
        }

        private static void Compress(string compressFile, ZipOutputStream outStream)
        {
            //long offset = 0;
            using (FileStream sourceFileStream = File.OpenRead(compressFile))
            {
                ZipEntry entry = new ZipEntry(Path.GetFileName(compressFile));
                entry.Size = sourceFileStream.Length;
                entry.DateTime = DateTime.Now;
                outStream.PutNextEntry(entry);
                int read;
                byte[] buffer = new byte[SIZE];
                while ((read = sourceFileStream.Read(buffer, 0, SIZE)) > 0)
                {
                    crc.Reset();
                    crc.Update(buffer, 0, read);
                    outStream.Write(buffer, 0, read);
                }
            }
        }

        ///<summary>
        /// ��ѹ����
        ///</summary>
        ///<param name="sourceZipFile">ѹ���ļ���</param>
        ///<param name="targetDirectoryPath">��ѹ���ļ��С�</param>
        public static void UnCompress(string sourceZipFile, string targetDirectoryPath)
        {
            using (ZipInputStream stream = new ZipInputStream(File.OpenRead(sourceZipFile)))
            {
                ZipEntry entry;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    UpCompress(entry, stream, targetDirectoryPath);
                }
            }
        }

        private static void UpCompress(ZipEntry entry, Stream stream, string targetDirectoryPath)
        {
            if (entry.IsDirectory)
            {
                Directory.CreateDirectory(targetDirectoryPath + "\\" + entry.Name);
                return;
            }
            string filename = targetDirectoryPath + "/" + entry.Name;
            FileInfo f = new FileInfo(filename);
            if (f.Exists)
            {
                f.IsReadOnly = false;
                f.Delete();
            }
            using (Stream file = File.Create(filename))
            {
                byte[] data = new byte[SIZE];
                int read;
                while ((read = stream.Read(data, 0, data.Length)) > 0)
                {
                    file.Write(data, 0, read);
                }
            }
        }
    }
}