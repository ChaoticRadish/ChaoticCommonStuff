using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random
{
    /// <summary>
    /// int类型区间, 可用来表示长度范围
    /// </summary>
    public class IntRangeAttribute : Attribute
    {
        public IntRangeAttribute(int min, int max)
        {
            if (max < min)
            {
                int temp = min;
                min = max;
                max = temp;
            }
            Min = min;
            Max = max;
        }
        /// <summary>
        /// 范围设置为 (0, maxlength)
        /// </summary>
        /// <param name="maxlength"></param>
        public IntRangeAttribute(int maxlength) : this(0, maxlength) { }

        public int Min { get; private set; }
        public int Max { get; private set; }
    }
}
