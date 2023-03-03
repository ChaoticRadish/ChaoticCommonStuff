using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Util.Extension;
using Point = System.Drawing.Point;

namespace WinFormsTest.Tests
{
    public partial class GraphicsTest001 : TestFormBase
    {
        public GraphicsTest001()
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
            bool deepOutsideLine = true;
            Color color = Color.Blue;

            int min = rect.Width < rect.Height ? rect.Width : rect.Height;
            // 三角形占据空间
            int size = min / 4 * 3 - 2;
            if (size < 0)
            {
                return;
            }

            // 画三角形
            int a = size / 6;   // 圆角的半径
            double halfBottom = Math.Sqrt(3) * size / 3;    // 半个底边
            double dist = Math.Sqrt(3) * a;    // 圆形与边的交点到最近的顶点的距离
            PointF p1 = new PointF((float)(rect.X + rect.Width / 2 - halfBottom), rect.Y + rect.Height - (rect.Height - size) / 2); // 左下角
            PointF p2 = new PointF((float)(rect.X + rect.Width / 2 + halfBottom), rect.Y + rect.Height - (rect.Height - size) / 2); // 右下角
            PointF p3 = new PointF(rect.X + rect.Width / 2, rect.Y + (rect.Height - size) / 2); // 中上角

            PointF o1 = new PointF((float)(p1.X + a * 2 * Math.Cos(Math.PI / 6)), (float)(p1.Y - a * 2 * Math.Sin(Math.PI / 6))); // 左下圆心
            PointF o2 = new PointF((float)(p2.X - a * 2 * Math.Cos(Math.PI / 6)), (float)(p2.Y - a * 2 * Math.Sin(Math.PI / 6))); // 右下圆心
            PointF o3 = new PointF(p3.X, p3.Y + a * 2); // 中上圆心
            RectangleF o1Rect = new RectangleF(o1.X - a, o1.Y - a, a * 2, a * 2);
            RectangleF o2Rect = new RectangleF(o2.X - a, o2.Y - a, a * 2, a * 2);
            RectangleF o3Rect = new RectangleF(o3.X - a, o3.Y - a, a * 2, a * 2);

            PointF p1_1 = new PointF((float)(p1.X + dist), p1.Y);  // 左下圆形与底边的交点
            PointF p1_2 = new PointF((float)(p1.X + dist * Math.Cos(Math.PI / 3)), (float)(p1.Y - dist * Math.Sin(Math.PI / 3)));  // 左下圆形与斜边的交点
            PointF p2_1 = new PointF((float)(p2.X - dist), p2.Y);  // 右下圆形与底边的交点
            PointF p2_2 = new PointF((float)(p2.X - dist * Math.Cos(Math.PI / 3)), (float)(p2.Y - dist * Math.Sin(Math.PI / 3)));  // 右下圆形与斜边的交点
            PointF p3_1 = new PointF((float)(p3.X - dist * Math.Sin(Math.PI / 6)), (float)(p3.Y + dist * Math.Cos(Math.PI / 6)));  // 中上圆形与左边的交点
            PointF p3_2 = new PointF((float)(p3.X + dist * Math.Sin(Math.PI / 6)), (float)(p3.Y + dist * Math.Cos(Math.PI / 6)));  // 中上圆形与右边的交点

            if (deepOutsideLine)
            {
                using (Brush b = new SolidBrush(color.Multi(0.5)))
                {
                    int change = 8;
                    float cos30 = (float)(Math.Cos(Math.PI / 6) * change);
                    float sin30 = (float)(Math.Sin(Math.PI / 6) * change);

                    RectangleF o1Rect2 = new RectangleF(o1.X - a - change, o1.Y - a - change, a * 2 + change * 2, a * 2 + change * 2);
                    RectangleF o2Rect2 = new RectangleF(o2.X - a - change, o2.Y - a - change, a * 2 + change * 2, a * 2 + change * 2);
                    RectangleF o3Rect2 = new RectangleF(o3.X - a - change, o3.Y - a - change, a * 2 + change * 2, a * 2 + change * 2);
                    PointF p1_1_2 = new PointF(p1_1.X, p1_1.Y + change);
                    PointF p1_2_2 = new PointF(p1_2.X - cos30, p1_2.Y - sin30);
                    PointF p2_1_2 = new PointF(p2_1.X, p2_1.Y + change);
                    PointF p2_2_2 = new PointF(p2_2.X + cos30, p2_2.Y - sin30);
                    PointF p3_1_2 = new PointF(p3_1.X - cos30, p3_1.Y - sin30);
                    PointF p3_2_2 = new PointF(p3_2.X + cos30, p3_2.Y - sin30);

                    GraphicsPath path = new GraphicsPath();

                    path.AddArc(o1Rect2, 90, 120);
                    path.AddLine(p1_1_2, p2_1_2);
                    path.AddArc(o2Rect2, -30, 120);
                    path.AddLine(p2_2_2, p3_2_2);
                    path.AddArc(o3Rect2, 210, 120);
                    path.AddLine(p3_1_2, p1_2_2);


                    e.Graphics.FillPath(b, path);
                }
            }
            using (Brush b = new SolidBrush(color))
            {
                GraphicsPath path = new GraphicsPath();
                
                path.AddArc(o1Rect, 90, 120);
                path.AddLine(p1_1, p2_1);
                path.AddArc(o2Rect, -30, 120);
                path.AddLine(p2_2, p3_2);
                path.AddArc(o3Rect, 210, 120);
                path.AddLine(p3_1, p1_2);

                path.CloseFigure();
                e.Graphics.FillPath(b, path);
            }

        }
    }
}
