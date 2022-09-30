using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Data
{
    /// <summary>
    /// 三字段的值组
    /// </summary>
    public struct ThreeValueTuples<TValue1, TValue2, TValue3>
    {
        public ThreeValueTuples(TValue1 value1, TValue2 value2, TValue3 value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        #region 数据
        /// <summary>
        /// 值1
        /// </summary>
        public TValue1 Value1 { get; set; }
        /// <summary>
        /// 值2
        /// </summary>
        public TValue2 Value2 { get; set; }
        /// <summary>
        /// 值3
        /// </summary>
        public TValue3 Value3 { get; set; }
        #endregion

        #region 转换
        public KeyValuePair<TValue1, TValue2> GetValue1N2()
        {
            return new KeyValuePair<TValue1, TValue2>(Value1, Value2);
        }
        #endregion

        #region 相同比较
        public override bool Equals(object obj)
        {
            if (obj is ThreeValueTuples<TValue1, TValue2, TValue3> valueTuples)
            {
                return Value1.Equals(valueTuples.Value1)
                    && Value2.Equals(valueTuples.Value2)
                    && Value3.Equals(valueTuples.Value3);
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return $"{Value1.GetHashCode()}-{Value2.GetHashCode()}-{Value3.GetHashCode()}".GetHashCode();
        }
        #endregion
    }
}
