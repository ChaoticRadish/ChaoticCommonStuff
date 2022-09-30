using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl
{ 
    /// <summary>
    /// 带提示文本的输入框
    /// </summary>
    public partial class HintTextBox : TextBox
    {
        public HintTextBox()
        {
            InitializeComponent();

            /* SetStyle(ControlStyles.UserPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw, true);*/
        }

        #region 属性
        [Browsable(true)]
        [Description("提示信息字符串")]
        [Category("外观")]
        public string HintText { get; set; }

        [Browsable(true)]
        [Description("提示信息字符串显示的颜色")]
        [Category("外观")]
        public Color HintTextColor { get; set; } = Color.LightGray;

        [Browsable(true)]
        [Description("边框的内外侧间距")]
        [Category("外观")]
        public float BorderGap { get; set; } = 1.5f;

        /*[Browsable(true)]
        [Description("边框默认情况下的颜色")]
        [Category("外观")]
        public Color BorderColor { get; set; } = Color.DarkGray;*/

        #endregion

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case (int)Util.WMMsgCodeEnum.WM_PAINT:
                    RePaint();
                    break;
            }
        }

        protected void RePaint()
        {
            Graphics g = CreateGraphics();
            /*g.Clear(Color.FromArgb(0, 255, 255, 255));
            // 绘制内部背景色
            using (Brush brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, new RectangleF()
                {
                    X = BorderGap * 1,
                    Y = BorderGap * 1,
                    Width = Width - BorderGap * 2,
                    Height = Height - BorderGap * 2
                });
            }
            // 绘制边框
            using (Pen pen = new Pen(BorderColor))
            {
                g.DrawRectangle(pen, new Rectangle()
                {
                    X = (int)(BorderGap * 1),
                    Y = (int)(BorderGap * 1),
                    Width = (int)(Width - BorderGap * 2),
                    Height = (int)(Height - BorderGap * 2)
                });
            }
            // 绘制文字
            if (!string.IsNullOrEmpty(Text))
            {
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    g.DrawString(Text, Font, brush, new RectangleF()
                    {
                        X = BorderGap * 2,
                        Y = BorderGap * 2,
                        Width = Width - BorderGap * 4,
                        Height = Height - BorderGap * 4
                    });
                }
            }*/
            // 绘制提示文字
            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(HintText) && !Focused)
            {
                using (Brush brush = new SolidBrush(HintTextColor))
                {
                    g.DrawString(HintText, Font, brush, new RectangleF()
                    {
                        X = BorderGap * 2,
                        Y = BorderGap * 2,
                        Width = Width - BorderGap * 4,
                        Height = Height - BorderGap * 4
                    });
                }
            }
        }
    }
}
