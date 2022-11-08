using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl
{
    public partial class FreedomFlowProgressPanel : Panel
    {
        public FreedomFlowProgressPanel()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }
        #region 提示信息
        public ToolTip ToolTip { get; set; }
        #endregion

        #region 颜色
        [Category("_自定义_颜色设定"), Description("完成颜色")]
        public Color ColorDone { get; set; } = Color.LightGreen;
        [Category("_自定义_颜色设定"), Description("异常颜色")]
        public Color ColorError { get; set; } = Color.FromArgb(255, 50, 50);
        [Category("_自定义_颜色设定"), Description("等待中颜色")]
        public Color ColorWaiting { get; set; } = Color.LightGray;
        [Category("_自定义_颜色设定"), Description("警告颜色")]
        public Color ColorWarning { get; set; } = Color.Orange;
        #endregion

        #region 箭头设置
        /// <summary>
        /// 箭头主体的宽度
        /// </summary>
        public int CrossWidth { get; set; } = 4;
        /// <summary>
        /// 箭头头部部分的宽度
        /// </summary>
        public int CrossHeadWidth { get; set; } = 8;
        /// <summary>
        /// 箭头头部部分的长度
        /// </summary>
        public int CrossHeadLength { get; set; } = 12;
        #endregion

        #region 重绘
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            List<FreedomFlowProgressPanelItem> items = GetItems();

            if (items.Count == 0) return;

            using (Brush brush = new SolidBrush(ForeColor))
            {
                for (int i = 0; i < items.Count - 1; i++)
                {
                    Point start = GetOrientationPoint(items[i], items[i + 1]);
                    Point end = GetMinDistance(start, items[i + 1]);

                    PaintCross(e.Graphics, brush, start, end);
                }
            }
        }

        /// <summary>
        /// 绘制一个箭头
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void PaintCross(Graphics graphics, Brush brush, Point start, Point end)
        {
            double distance = GetDistance(start, end);
            double sin = (end.Y - start.Y) / distance;
            double cos = (end.X - start.X) / distance;
            
            // 绘制主体
            GraphicsPath body = new GraphicsPath();
            body.AddLines(
                new Point[]
                {
                    end,
                    new Point((int)(start.X - sin * (CrossWidth / 2)), (int)(start.Y + cos * (CrossWidth / 2))),
                    new Point((int)(start.X + sin * (CrossWidth / 2)), (int)(start.Y - cos * (CrossWidth / 2))),
                    end,
                });
            // 绘制箭头的头部
            if (distance > CrossHeadLength * 2)
            {
                // 箭头坐标的起点
                Point headStart = new Point((int)(end.X - CrossHeadLength * cos), (int)(end.Y - CrossHeadLength * sin));
                // 箭头内侧的转折点
                Point headBreak = new Point(
                    headStart.X + (end.X - headStart.X) / 3, 
                    headStart.Y + (end.Y - headStart.Y) / 3);

                body.AddLines(
                    new Point[]
                    {
                    end,
                    new Point((int)(headStart.X - sin * (CrossHeadWidth / 2)), (int)(headStart.Y + cos * (CrossHeadWidth / 2))),
                    headBreak,
                    new Point((int)(headStart.X + sin * (CrossHeadWidth / 2)), (int)(headStart.Y - cos * (CrossHeadWidth / 2))),
                    end,
                    });
            }
            graphics.FillPath(brush, body);
        }
        #endregion

        #region 事件重载
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is FreedomFlowProgressPanelItem item)
            {
                item.MouseHover += Item_MouseHover;
                item.MouseLeave += Item_MouseLeave;
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (e.Control is FreedomFlowProgressPanelItem item)
            {
                item.MouseHover -= Item_MouseHover;
                item.MouseLeave -= Item_MouseLeave;
            }
        }
        private void Item_MouseLeave(object sender, EventArgs e)
        {
            if (ToolTip == null) return;
            ToolTip.Hide(this);
        }

        private void Item_MouseHover(object sender, EventArgs e)
        {
            if (ToolTip == null) return;
            FreedomFlowProgressPanelItem item = (FreedomFlowProgressPanelItem)sender;
            if (!string.IsNullOrEmpty(item.ToolTipInfo))
            {
                ToolTip.Show(item.ToolTipInfo, this, item.GetBottomLeft());
            }
        }

        #endregion

        #region 状态

        public enum ItemState
        {
            /// <summary>
            /// 完成
            /// </summary>
            Done,
            /// <summary>
            /// 异常
            /// </summary>
            Error,
            /// <summary>
            /// 等待中 (未开始)
            /// </summary>
            Waiting,
            /// <summary>
            /// 警告
            /// </summary>
            Warning,
        }
        #endregion

        /// <summary>
        /// 取得所有Item
        /// </summary>
        /// <returns></returns>
        private List<FreedomFlowProgressPanelItem> GetItems()
        {
            List<FreedomFlowProgressPanelItem> output = new List<FreedomFlowProgressPanelItem>();
            foreach (Control control in Controls)
            {
                if (control is FreedomFlowProgressPanelItem item)
                {
                    output.Add(item);
                }
            }
            output = output.OrderBy(i => i.TabIndex).ToList();
            return output;
        }
        /// <summary>
        /// 取得从start到end的最佳方向上的起始点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private Point GetOrientationPoint(FreedomFlowProgressPanelItem start, FreedomFlowProgressPanelItem end)
        {
            Point startCenter = start.GetCenter();
            Point endCenter = end.GetCenter();
            double angle = Math.Atan2(endCenter.X - startCenter.X, endCenter.Y - startCenter.Y) * 180 / Math.PI;
            // 旋转到比较容易理解的的方向, 正右是0度
            angle -= 90;
            // 值域移动到 [0 + k, 360 + k)
            int k = 360 / 8 / 2;
            if (angle < 0 - k)
            {
                angle += 360;
            }
            else if (angle >= 360 - k)
            {
                angle -= 360;
            }
            // 计算落在哪个区间
            int rangeIndex = (int)((angle + k) / (360 / 8));
            // 区间序号: 
            //
            //         3   ╲  2  ╱   1
            //        ╲   ┏━━━━━━━┓   ╱
            //       4    ┃ start ┃    0
            //        ╱   ┗━━━━━━━┛   ╲
            //         5   ╱  6  ╲   7
            //
            switch (rangeIndex)
            {
                case 0: return start.GetCenterRight();
                case 1: return start.GetTopRight();
                case 2: return start.GetTopCenter();
                case 3: return start.GetTopLeft();
                case 4: return start.GetCenterLeft();
                case 5: return start.GetBottomLeft();
                case 6: return start.GetBottomCenter();
                case 7: return start.GetBottomRight();
                default: return start.GetCenter();
            }
        }
        /// <summary>
        /// 取得点P与目标的最近点
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private Point GetMinDistance(Point p, FreedomFlowProgressPanelItem target)
        {
            Point[] targetPoints = new Point[]
            {
                target.GetCenterRight(),    // 0
                target.GetTopRight(),       // 1
                target.GetTopCenter(),      // 2
                target.GetTopLeft(),        // 3
                target.GetCenterLeft(),     // 4
                target.GetBottomLeft(),     // 5
                target.GetBottomCenter(),   // 6
                target.GetBottomRight(),    // 7
            };
            List<double> distances = targetPoints.Select(i => GetDistance(p, i)).ToList();
            int minIndex = distances.IndexOf(distances.Min());
            return targetPoints[minIndex];
        }
        /// <summary>
        /// 取得两点间的距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
