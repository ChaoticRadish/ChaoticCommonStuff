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
    /// 单选文件选择输入框
    /// </summary>
    [DefaultEvent(nameof(OnFileSelected))]
    public partial class FileSelectTextBox : TextBox
    {
        #region 属性
        /// <summary>
        /// 过滤字符串
        /// </summary>
        [Category("自定义属性"), Description("文件过滤字符串")]
        public string Filter { get; set; }
        /// <summary>
        /// 文件选择窗口的标题
        /// </summary>
        [Category("自定义属性"), Description("文件过滤字符串")]
        public string SelectFormTitle { get; set; }
        /// <summary>
        /// 文件是否必须存在
        /// </summary>
        [Category("自定义属性"), Description("文件是否必须存在")]
        public bool FileMustExist { get; set; } = false;

        /// <summary>
        /// 当前选择的文件名就是 <see cref="TextBox.Text"/>
        /// </summary>
        public string FileName { get => Text; }
        #endregion

        public FileSelectTextBox()
        {
            InitializeComponent();

            ReadOnly = true;
        }


        /// <summary>
        /// 设置过滤器
        /// </summary>
        /// <param name="filters"></param>
        public void SetFilters(params string[] filters)
        {
            if (filters == null || filters.Length == 0)
            {
                Filter = "*.*";
            }
            else
            {
                Filter = Util.String.StringHelper.Concat(filters, ";");
            }
        }

        /// <summary>
        /// 清空选中的文件名
        /// </summary>
        public new void Clear()
        {
            base.Clear();
        }

        #region 事件
        public delegate void OnFileSelectedHandle(string filename);
        /// <summary>
        /// 文件选中事件
        /// </summary>
        public event OnFileSelectedHandle OnFileSelected;
        #endregion

        protected override void OnClick(EventArgs e)
        {
            FileDialog form;
            if (FileMustExist)
            {
                form = new OpenFileDialog()
                {
                    Filter = Filter,
                    Title = SelectFormTitle,
                    Multiselect = false,
                    ValidateNames = true,
                    RestoreDirectory = true,
                };
            }
            else
            {
                form = new SaveFileDialog()
                {
                    Filter = Filter,
                    Title = SelectFormTitle,
                    ValidateNames = true,
                    RestoreDirectory = true,
                };
            }
            if (form.ShowDialog(FindForm()) == DialogResult.OK)
            {
                string fileName = form.FileName;
                Text = fileName;

                OnFileSelected?.Invoke(fileName);
            }
        }
    }
}
