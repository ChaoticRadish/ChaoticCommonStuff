using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest.Tests
{
    public partial class ProgessTest001 : TestFormBase
    {
        public ProgessTest001()
        {
            InitializeComponent();

            freedomFlowProgressPanel1.ToolTip = toolTip1;

            Item1.SetText("测试1", null, "测试1");
            Item2.SetText("测试2", null, "测试2");
        }

        private void freedomFlowProgressPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            // Item2.Location = new Point(e.X, e.Y);

            Point startCenter = Item1.GetCenter();
            Point endCenter = Item2.GetCenter();
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
            int temp = (int)((angle + k) / (360 / 8));


            Log("测试", $"angle: {angle}, temp: {temp}");

            freedomFlowProgressPanel1.Invalidate();
        }
    }
}
