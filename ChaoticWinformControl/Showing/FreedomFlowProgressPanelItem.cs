using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChaoticWinformControl.FreedomFlowProgressPanel;

namespace ChaoticWinformControl
{
    public partial class FreedomFlowProgressPanelItem : UserControl
    {
        public FreedomFlowProgressPanelItem()
        {
            InitializeComponent();

            Padding = new Padding(5);
            Font = new Font(Font.FontFamily, 16);

            BackColor = GetColor(state);

            MainLabel.ForeColor = Color.Black;
            MinorLabel.ForeColor = Color.DarkGray;

            UpdateChildBounds();
        }


        #region 状态
        /// <summary>
        /// 当前状态
        /// </summary>
        [Category("_自定义"), Description("当前状态")]
        public ItemState State
        {
            get => state;
            set
            {
                state = value;
                BackColor = GetColor(value);
            }
        }
        private ItemState state = ItemState.Waiting;

        #endregion

        #region 文本
        [Category("_自定义"), Description("主要文本")]
        public new string Text
        {
            get => MainLabel.Text;
            set => MainLabel.Text = value;
        }
        [Category("_自定义"), Description("主要文本")]
        public string MainText
        {
            get => MainLabel.Text;
            set => MainLabel.Text = value;
        }
        [Category("_自定义"), Description("次要文本")]
        public string MinorText
        {
            get => MinorLabel.Text;
            set
            {
                MinorLabel.Text = value;
                UpdateChildBounds();
            }
        }

        [Category("_自定义"), Description("主要文本的字体")]
        public new Font Font
        {
            get => MainLabel.Font;
            set => MainLabel.Font = value;
        }
        [Category("_自定义"), Description("主要文本的字体")]
        public Font MainFont
        {
            get => MainLabel.Font;
            set => MainLabel.Font = value;
        }
        [Category("_自定义"), Description("次要文本的字体")]
        public Font MinorFont
        {
            get => MinorLabel.Font;
            set => MinorLabel.Font = value;
        }

        [Category("_自定义"), Description("主要文本的颜色")]
        public new Color ForeColor
        {
            get => MainLabel.ForeColor;
            set => MainLabel.ForeColor = value;
        }
        [Category("_自定义"), Description("次要文本的颜色")]
        public Color MinorForeColor
        {
            get => MinorLabel.ForeColor;
            set => MinorLabel.ForeColor = value;
        }
        #endregion

        #region 颜色设置
        /// <summary>
        /// 使用当前项的自定义颜色, 而不是所在<see cref="FreedomFlowProgressPanel"/>的颜色
        /// </summary>
        [Category("_自定义_颜色设定"), Description("使用当前项的自定义颜色")]
        public bool CustomColor { get; set; } = false;


        [Category("_自定义_颜色设定"), Description("完成颜色")]
        public Color ColorDone { get; set; } = Color.LightGreen;
        [Category("_自定义_颜色设定"), Description("异常颜色")]
        public Color ColorError { get; set; } = Color.FromArgb(255, 50, 50);
        [Category("_自定义_颜色设定"), Description("等待中颜色")]
        public Color ColorWaiting { get; set; } = Color.LightGray;
        [Category("_自定义_颜色设定"), Description("警告颜色")]
        public Color ColorWarning { get; set; } = Color.Orange;

        /// <summary>
        /// 取得状态对应的颜色
        /// </summary>
        /// <param name="state"></param>
        public Color GetColor(ItemState state)
        {
            if (Parent != null && Parent is FreedomFlowProgressPanel panel)
            {
                switch (state)
                {
                    case ItemState.Done:
                        return panel.ColorDone;
                    case ItemState.Error:
                        return panel.ColorError;
                    case ItemState.Waiting:
                        return panel.ColorWaiting;
                    case ItemState.Warning:
                        return panel.ColorWarning;
                }
            }
            else
            {
                switch (state)
                {
                    case ItemState.Done:
                        return ColorDone;
                    case ItemState.Error:
                        return ColorError;
                    case ItemState.Waiting:
                        return ColorWaiting;
                    case ItemState.Warning:
                        return ColorWarning;
                }
            }
            return Parent?.BackColor ?? Color.White;
        }
        #endregion

        #region 取得位置
        public Point GetTopLeft()
        {
            return new Point(Location.X, Location.Y);
        }
        public Point GetCenterLeft()
        {
            return new Point(Location.X, Location.Y + Height / 2);
        }
        public Point GetBottomLeft()
        {
            return new Point(Location.X, Location.Y + Height);
        }
        public Point GetTopCenter()
        {
            return new Point(Location.X + Width / 2, Location.Y);
        }
        /// <summary>
        /// 取得控件的中心点
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public Point GetCenter()
        {
            return new Point(Location.X + Width / 2, Location.Y + Height / 2);
        }

        public Point GetBottomCenter()
        {
            return new Point(Location.X + Width / 2, Location.Y + Height);
        }
        public Point GetTopRight()
        {
            return new Point(Location.X + Width, Location.Y);
        }
        public Point GetCenterRight()
        {
            return new Point(Location.X + Width, Location.Y + Height / 2);
        }
        public Point GetBottomRight()
        {
            return new Point(Location.X + Width, Location.Y + Height);
        }
        #endregion

        #region 数据
        /// <summary>
        /// 设置需要显示的文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tooltipInfo"></param>
        public void SetText(string text, string minorText, string tooltipInfo)
        {
            Text = text;
            MinorText = minorText;
            ToolTipInfo = text;
        }

        /// <summary>
        /// 需要文本提示框显示的文本信息, 如异常信息, 警告信息等
        /// </summary>
        [Category("_自定义"), Description("需要文本提示框显示的文本信息, 如异常信息, 警告信息等")]
        public string ToolTipInfo { get; set; }
        #endregion

        #region 子控件尺寸更新
        private void UpdateChildBounds()
        {
            if (string.IsNullOrWhiteSpace(MinorLabel.Text))
            {
                MinorLabel.Visible = false;
                MainLabel.Size = Size;
                MainLabel.Location = new Point();
            }
            else
            {
                int temp = Height / 3;
                MinorLabel.Visible = true;
                MainLabel.Size = new Size(Width, temp * 2);
                MinorLabel.Size = new Size(Width, temp);
                MainLabel.Location = new Point();
                MinorLabel.Location = new Point(0, temp * 2);
            }
        }
        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateChildBounds();
        }
    }
}
