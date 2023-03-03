using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;
using Util.String;

namespace ChaoticWinformControl.FeatureGroup
{
    public partial class ObjDetailForm1 : Form
    {
        public ObjDetailForm1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取准备显示的对象的方法, 将会循环调用
        /// </summary>
        public Func<object> GetObjFunc { get; set; }
        /// <summary>
        /// 展示指定对象的详情信息
        /// </summary>
        /// <param name="obj"></param>
        public void ShowObj(object obj)
        {
            ShowerBox.Text = GetDetail(obj);
        }
        public string GetDetail(object obj)
        {
            if (obj == null) return "null";

            Type type = obj.GetType();
            if (type.IsEnumerable())
            {
                IEnumerable temp = (IEnumerable)obj;
                int count = 0;
                List<string> strs = new List<string>();
                foreach (object o in temp)
                {
                    count++;
                    strs.Add(o.ToString());
                }
                return $"{type}[{count}]\n{{\n{StringHelper.Concat(strs, "\n")}\n}}";
            }
            else
            {
                return obj.ToString();
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (Visible && GetObjFunc != null)
            {
                ShowObj(GetObjFunc.Invoke());
            }
        }
    }
}
