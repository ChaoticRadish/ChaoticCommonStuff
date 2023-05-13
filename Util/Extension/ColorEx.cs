using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class ColorEx
    {
        /// <summary>
        /// 将颜色的RGB值都乘以输入数字, 然后钳制到[0, 255]
        /// </summary>
        /// <param name="color"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Color Multi(this Color color, double d)
        {
            return Multi(color, d, d, d);
        }
        /// <summary>
        /// 将颜色的RGB值分别乘以输入数字, 然后钳制到[0, 255]
        /// </summary>
        /// <param name="color"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color Multi(this Color color, double r, double g, double b)
        {
            int R = (int)(color.R * r);
            if (R < 0) R = 0;
            if (R > 255) R = 255;
            int G = (int)(color.G * g);
            if (G < 0) G = 0;
            if (G > 255) G = 255;
            int B = (int)(color.B * b);
            if (B < 0) R = 0;
            if (B > 255) B = 255;

            return Color.FromArgb(color.A, R, G, B);
        }
    }
}
