using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random
{
    /// <summary>
    /// short类型区间, 可用来表示长度范围
    /// </summary>
    public class ShortRangeAttribute : Attribute
    {
        public ShortRangeAttribute(short min, short max)
        {
            if (max < min)
            {
                short temp = min;
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
        public ShortRangeAttribute(short maxlength) : this(0, maxlength) { }

        public short Min { get; private set; }
        public short Max { get; private set; }
    }
}
