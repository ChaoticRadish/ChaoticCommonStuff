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
    public class DateTimeRangeAttribute : Attribute
    {
        public DateTimeRangeAttribute(string startDate, int rangeDays)
        {
            if (DateTime.TryParse(startDate, out DateTime result))
            {
                StartDate = result;
            }
            else
            {
                StartDate = DateTime.Now.AddDays(-30);
            }
            RangeDays = rangeDays;
        }

        /// <summary>
        /// 区间开始日期
        /// </summary>
        public DateTime StartDate { get; }
        /// <summary>
        /// 区间的总日期长度
        /// </summary>
        public int RangeDays { get; }
    }
}
