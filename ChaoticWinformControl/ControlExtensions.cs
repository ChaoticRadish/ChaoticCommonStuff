using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl
{
    public static class ControlExtensions
    {
        /// <summary>
        /// 自动检查是否需要使用Invoke方法
        /// </summary>
        /// <param name="c"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public static void AutoInvoke(this Control c, Delegate method, params object[] args)
        {
            if (c.InvokeRequired)
            {
                c.Invoke(method, args);
            }
            else
            {
                method.DynamicInvoke(args);
            }
        }
        /// <summary>
        /// 自动检查是否需要使用Invoke方法
        /// </summary>
        /// <param name="c"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public static void AutoInvoke(this Control c, Action method, params object[] args)
        {
            if (c == null || c.IsDisposed)
            {
                return;
            }
            if (c.InvokeRequired)
            {
                c.Invoke(method, args);
            }
            else
            {
                method.DynamicInvoke(args);
            }
        }

        
        /// <summary>
        /// 自动设置是否可用
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        public static void AutoSetEnable(this Control c, bool b)
        {
            if (c.InvokeRequired)
            {
                try
                {
                    c.Invoke(new Action(() => { c.Enabled = b; }));
                }
                catch
                {

                }
            }
            else
            {
                c.Enabled = b;
            }
        }

        public static Point LocationOnClient(this Control c, Point p)
        {
            for (; c != null && !typeof(Form).IsAssignableFrom(c.GetType()); c = c.Parent)
            {
                if (c is IControlList controlList)
                {
                    p.X -= controlList.InnerAreaOffset.X;
                    p.Y -= controlList.InnerAreaOffset.Y;
                }
                p.Offset(c.Location);
            }
            return p;
        }
    }
}
