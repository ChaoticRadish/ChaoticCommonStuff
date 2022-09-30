﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Excel.Attributes
{
    /// <summary>
    /// 索引列
    /// </summary>
    public class IndexColumnAttribute : Attribute
    {
        /// <summary>
        /// 索引起始值
        /// </summary>
        public int IndexStartValue { get; set; } = 1;
    }
}
