using DSOFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.File
{
    public static class FilePropertyHelper
    {

        public static string GetCategory(string fileName)
        {
            string output = null;
            OleDocumentProperties file = new OleDocumentProperties();
            file.Open(fileName);//打开本地文件
            output = file.SummaryProperties.Category;
            file.Save();//保存更改，注意，千万不能忘了这行代码
            file.Close();
            return output;
        }
        public static void SetCategory(string fileName, string value)
        {
            OleDocumentProperties file = new OleDocumentProperties();
            file.Open(fileName);//打开本地文件
            file.SummaryProperties.Category = value;
            file.Save();//保存更改，注意，千万不能忘了这行代码
            file.Close();
        }
        public static void Set(string fileName, string property, string value)
        {
            OleDocumentProperties file = new OleDocumentProperties();
            file.Open(fileName);//打开本地文件
            bool exist = false;
            for (int i = 0; i < file.CustomProperties.Count; i++)
            {
                if (file.CustomProperties[i].Name == property)
                {
                    file.CustomProperties[i].set_Value(value);
                    exist = true;
                    break;
                }
            }
            if (!exist)
            {
                file.CustomProperties.Add(property, value);
            }
            file.Save();//保存更改，注意，千万不能忘了这行代码
            file.Close();
        }
    }
}
