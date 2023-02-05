using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.CSV
{
    public static class Helper
    {
        /// <summary>
        /// 取得默认的过滤器
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultFilter()
        {
            return "CSV 文件|*.csv";
        }
    }
}
