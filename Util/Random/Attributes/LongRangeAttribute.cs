using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random
{
    /// <summary>
    /// 区间
    /// </summary>
    public class LongRangeAttribute : Attribute
    {
        public LongRangeAttribute(long min, long max)
        {
            if (max < min)
            {
                long temp = min;
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
        public LongRangeAttribute(long maxlength) : this(0, maxlength) { }

        public long Min { get; private set; }
        public long Max { get; private set; }
    }
}
