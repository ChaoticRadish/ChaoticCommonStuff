using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random.Attributes
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
        public DoubleRangeAttribute(double length) : this(0, length) { }

        public double Min { get; private set; }
        public double Max { get; private set; }
    }
}
