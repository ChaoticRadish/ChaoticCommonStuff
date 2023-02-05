using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaoticWinformControl.DrawingEx
{
    public static class RectangleEx
    {
        /// <summary>
        /// 获取矩形中点
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static PointF Center(this RectangleF rect)
        {
            return new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        }
    }
}
