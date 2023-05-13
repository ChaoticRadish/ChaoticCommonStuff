using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class ListEx
    {
        /// <summary>
        /// 列表是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IList<T> list)
        {
            return list != null && list.Count > 0;
        }
    }
}
