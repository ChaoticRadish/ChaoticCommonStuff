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
    public class FloatRangeAttribute : Attribute
    {
        public FloatRangeAttribute(float min, float max)
        {
            if (max < min)
            {
                float temp = min;
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
        public FloatRangeAttribute(float maxlength) : this(0, maxlength) { }

        public float Min { get; private set; }
        public float Max { get; private set; }
    }
}
