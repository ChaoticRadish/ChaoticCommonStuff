using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Util;
using Util.Extension;
using Point = System.Drawing.Point;

namespace WinFormsTest.Tests
{
    public partial class GraphicsTest002 : TestFormBase
    {
        public GraphicsTest002()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            Rectangle rect = ClientRectangle;
            Color color = Color.Blue;

            int min = rect.Width < rect.Height ? rect.Width : rect.Height;
            // 菱形占据空间
            int size = min / 4 * 3 - 2;
            if (size < 0)
            {
                return;
            }
            // 画菱形
            int a = size / 6;   // 圆角的半径
            float halfDist = (float)(a * Math.Cos(Math.PI / 4));   // 圆心到最近顶点的距离的一半
            PointF c = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            PointF p1 = new PointF(c.X, c.Y - size / 2);   // 上
            PointF p2 = new PointF(c.X, c.Y + size / 2);   // 下
            PointF p3 = new PointF(c.X - size / 2, c.Y);   // 左
            PointF p4 = new PointF(c.X + size / 2, c.Y);   // 右
            PointF o1 = new PointF(p1.X, p1.Y + halfDist * 2);   // 上圆心
            PointF o2 = new PointF(p2.X, p2.Y - halfDist * 2);   // 下圆心
            PointF o3 = new PointF(p3.X + halfDist * 2, p3.Y);   // 左圆心
            PointF o4 = new PointF(p4.X - halfDist * 2, p4.Y);   // 右圆心
            RectangleF o1Rect = new RectangleF(o1.X - a, o1.Y - a, a * 2, a * 2);
            RectangleF o2Rect = new RectangleF(o2.X - a, o2.Y - a, a * 2, a * 2);
            RectangleF o3Rect = new RectangleF(o3.X - a, o3.Y - a, a * 2, a * 2);
            RectangleF o4Rect = new RectangleF(o4.X - a, o4.Y - a, a * 2, a * 2);
            PointF p1_1 = new PointF(p1.X + halfDist, p1.Y + halfDist);
            PointF p1_2 = new PointF(p1.X - halfDist, p1.Y + halfDist);
            PointF p3_1 = new PointF(p3.X + halfDist, p3.Y - halfDist);
            PointF p3_2 = new PointF(p3.X + halfDist, p3.Y + halfDist);
            PointF p2_1 = new PointF(p2.X - halfDist, p2.Y - halfDist);
            PointF p2_2 = new PointF(p2.X + halfDist, p2.Y - halfDist);
            PointF p4_1 = new PointF(p4.X - halfDist, p4.Y + halfDist);
            PointF p4_2 = new PointF(p4.X - halfDist, p4.Y - halfDist);


            using (Brush b = new SolidBrush(color))
            {
                GraphicsPath path = new GraphicsPath();

                path.AddArc(o1Rect, 225, 90);
                path.AddLine(p1_2, p3_1);
                path.AddArc(o3Rect, 135, 90);
                path.AddLine(p3_2, p2_1);
                path.AddArc(o2Rect, 45, 90);
                path.AddLine(p2_2, p4_1);
                path.AddArc(o4Rect, -45, 90);
                path.AddLine(p4_2, p1_1);

                e.Graphics.FillPath(b, path);

                return;
            }


            // 画文字
            string text = "i";
            using (Brush bTextB = new SolidBrush(Color.White))
            {
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                };
                Font font = new Font(SystemFonts.DefaultFont.FontFamily, FontHelper.GetEmSize(size, e.Graphics.DpiY), FontStyle.Bold);
                e.Graphics.DrawString(text, font, bTextB, rect, format);
            }
        }
    }
}
