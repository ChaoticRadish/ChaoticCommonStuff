using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.String
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 每隔一定间距, 插入一个换行符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lineWidth"></param>
        /// <returns></returns>
        public static string SplitLine(this string str, int lineWidth = 60)
        {
            return StringHelper.SplitLine(str, lineWidth);
        }
    }
}
