using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random.Attributes
{
    /// <summary>
    /// 概率
    /// </summary>
    public class ProbabilityAttribute : Attribute
    {
        /// <summary>
        /// Null值的概率
        /// </summary>
        public double Null { get; set; }
        /// <summary>
        /// True值的概率
        /// </summary>
        public double True { get; set; }
    }
}
