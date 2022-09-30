﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Excel.Attributes
{
    /// <summary>
    /// 工作表名字
    /// </summary>
    public class SheetNameAttribute : Attribute
    {
        public SheetNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
