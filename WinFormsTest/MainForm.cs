using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest
{
    public partial class MainForm : Form
    {
        #region 区域
        /// <summary>
        /// 日志区域
        /// </summary>
        public Panel LogArea { get => splitContainer1.Panel2; }
        /// <summary>
        /// 参数区域
        /// </summary>
        public Panel ParamArea { get => splitContainer2.Panel1; }
        /// <summary>
        /// 显示区域
        /// </summary>
        public Panel ShowerArea { get => splitContainer2.Panel2; }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }
        public static MainForm? Instance { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetTest<Tests.DateRangeInputerTest>();
        }

        private void SetTest<T>() where T : TestFormBase, new()
        {
            T form = new()
            {
                TopLevel = false,
                MainForm = this,
                Location = new(),
                Parent = ShowerArea,
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None,
            };
            form.BringToFront();
            form.Show();
        }
    }
}
