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
    public class DoubleRangeAttribute : Attribute
    {
        public DoubleRangeAttribute(double min, double max)
        {
            if (max < min)
            {
                double temp = min;
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
        public DoubleRangeAttribute(double maxlength) : this(0, maxlength) { }

        public double Min { get; private set; }
        public double Max { get; private set; }
    }
}
