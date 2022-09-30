using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl.FeatureGroup
{
    /// <summary>
    /// 时间范围的简单输入, 选择一个开始时间之后，再选择一个时间长度
    /// </summary>
    public partial class DateRangeSimpleInputBox : UserControl
    {
        #region 可配置属性
        [Category("自订属性"), Description("显示文本")]
        public new string Text { get => WorkingCheckBox.Text; set => WorkingCheckBox.Text = value; }
        #endregion

        public DateRangeSimpleInputBox()
        {
            InitializeComponent();
        }

    }
}
