using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Excel
{
    public static class Helper
    {
        /// <summary>
        /// 取得默认的过滤器
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultFilter()
        {
            return "xlsx(2007或其后版本)|*.xlsx|xls(2003版本)|*.xls";
        }
    }
}
