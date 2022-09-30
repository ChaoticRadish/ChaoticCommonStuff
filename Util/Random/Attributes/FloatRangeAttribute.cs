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
        public FloatRangeAttribute(float length) : this(0, length) { }

        public float Min { get; private set; }
        public float Max { get; private set; }
    }
}
