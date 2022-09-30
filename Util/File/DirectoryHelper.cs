using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.File
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void Empty(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                foreach (FileInfo file in info.GetFiles()) file.Delete();
                foreach (DirectoryInfo directory in info.GetDirectories()) directory.Delete(true);
            }
        }

        /// <summary>
        /// 清理比输入天数前的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="date"></param>
        public static void ClearOld(string path, double days)
        {
            ClearOld(path, DateTime.Now - new TimeSpan((long)(days * 24 * 60 * 60 * 1000 * 10000)));
        }
        /// <summary>
        /// 清理比输入日期更早的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="date"></param>
        public static void ClearOld(string path, DateTime date)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                foreach (FileInfo file in info.GetFiles())
                {
                    if (file.LastWriteTime <= date)
                    {
                        file.Delete();
                    }
                }
            }
        }
    }
}
