using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Excel.Attributes
{
    /// <summary>
    /// 列名
    /// </summary>
    public class ColumnNameAttribute : Common.Attributes.ColumnNameAttribute
    {
        public ColumnNameAttribute(string name) : base(name) { }

    }
}
