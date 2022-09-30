using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl
{
    public partial class ControlListItem : UserControl
    {
        #region 标识
        /// <summary>
        /// 标识
        /// </summary>
        public object ID { get; set; }
        #endregion

        #region 属性
        /// <summary>
        /// 自动调整宽度到列表宽度(水平时)/高度(垂直时)
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [Description("自动调整宽度到列表宽度(水平时)/高度(垂直时)")]
        public bool AutoWidth { get; set; }
        /// <summary>
        /// 自动调整大小时保持比例
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [Description("自动调整大小时保持比例")]
        public bool KeepScale { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("鼠标悬停时的提示文本")]
        public string ToolTip { get; set; }

        /// <summary>
        /// 在列表中占用尺寸
        /// </summary>
        public bool OccupySize { get; set; } = true;
        #endregion

        #region 从属
        /// <summary>
        /// 所属列表
        /// </summary>
        public IControlList Father { get; set; }
        #endregion

        public ControlListItem()
        {
            InitializeComponent();
        }

        #region 控制
        /// <summary>
        /// 设置提示框
        /// </summary>
        /// <param name="toolTip"></param>
        public void SetToolTip(ToolTip toolTip)
        {
            if (toolTip != null)
            {
                toolTip.SetToolTip(this, ToolTip);
                foreach (Control control in Controls)
                {
                    toolTip.SetToolTip(control, ToolTip);
                }
            }
        }

        /// <summary>
        /// 只读模式
        /// </summary>
        public bool ReadOnly { get; protected set; }
        /// <summary>
        /// 设置只读模式
        /// </summary>
        /// <param name="b"></param>
        public virtual void SetReadOnlyMode(bool b)
        {
            ReadOnly = b;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }
        #endregion
    }
}
