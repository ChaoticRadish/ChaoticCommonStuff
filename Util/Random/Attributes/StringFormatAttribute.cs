using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random.Attributes
{
    public class StringFormatAttribute : Attribute
    {
        /// <summary>
        /// 大小写设定
        /// </summary>
        public CaseEnum Case { get; set; }
        /// <summary>
        /// 字符串大小写
        /// </summary>
        public enum CaseEnum
        {
            /// <summary>
            /// 大写
            /// </summary>
            Upper,
            /// <summary>
            /// 小写
            /// </summary>
            Lower,
            /// <summary>
            /// 未设置 (即维持原状)
            /// </summary>
            None,
        }
    }
}
