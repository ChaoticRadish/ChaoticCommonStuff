using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Util.File
{
    public class QuickSave
    {
        /// <summary>
        /// 保存文本
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content">正文</param>
        public static void SaveText(string filePath, string content)
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.Write(content);
                }
            }
        }
    }
}
