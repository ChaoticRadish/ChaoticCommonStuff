using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Util.Random.RandomObjectHelper.RandomConfig;

namespace Util.Random
{
    public class StringFormatAttribute : Attribute
    {
        /// <summary>
        /// 大小写设定
        /// </summary>
        public CaseEnum Case { get; set; }
        
    }
}
