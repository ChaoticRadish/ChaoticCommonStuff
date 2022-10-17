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
    public partial class LogShowerBox : UserControl
    {
        public LogShowerBox()
        {
            InitializeComponent();

            AddTitleItem(" - 未选择 - ", false);
        }
        #region 数据集
        public List<Data> Datas { get; private set; } = new List<Data>();
        public List<string> TitleItems { get; private set; } = new List<string>();

        /// <summary>
        /// 颜色表
        /// </summary>
        public Dictionary<string, Color> ColorSet = new Dictionary<string, Color>(); 
        #endregion


        #region 控件事件
        private void TitleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContentShower.Clear();
            string selected = GetSelectedTitle();
            var needShow = selected == null ? Datas : Datas.Where(i => i.Title == selected);
            foreach (Data data in needShow)
            {
                Print(data);
            }
        }

        #endregion

        #region 控制方法
        /// <summary>
        /// 设置标题默认对应的颜色
        /// </summary>
        /// <param name="title"></param>
        /// <param name="color"></param>
        public void DefaultColor(string title, Color color)
        {
            if (ColorSet.ContainsKey(title))
            {
                ColorSet[title] = color;
            }
            else
            {
                ColorSet.Add(title, color);
            }
        }
        /// <summary>
        /// Log一个信息
        /// </summary>
        /// <param name="data"></param>
        public void Log(Data data)
        {
            // 颜色: 未传入颜色时, 使用颜色表中对应标题的颜色, 无对应颜色时使用当前前景色
            data.Color = data.Color ?? (ColorSet.ContainsKey(data.Title) ? ColorSet[data.Title] : ForeColor);
            data.Time = DateTime.Now;

            Datas.Add(data);

            AddTitleItem(data.Title);

            string selected = GetSelectedTitle();
            if (selected == null || selected == data.Title)
            {
                Print(data);
            }
        }
        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="data"></param>
        private void Print(Data data)
        {
            if (!string.IsNullOrEmpty(ContentShower.Text))
            {
                ContentShower.AppendText("\n");
            }
            ContentShower.SelectionStart = ContentShower.TextLength;
            ContentShower.SelectionLength = 0;
            ContentShower.SelectionColor = (Color)data.Color;
            ContentShower.AppendText(data.ToString());
            ContentShower.SelectionColor = ForeColor;

            MoveToEnd();
        }
        /// <summary>
        /// 移动Log显示的到最后一行
        /// </summary>
        private void MoveToEnd()
        {
            ContentShower.SelectionStart = ContentShower.Text.Length;
            ContentShower.SelectionLength = 0;
            ContentShower.ScrollToCaret();
        }
        /// <summary>
        /// 添加标题可选项到ComboBox
        /// </summary>
        /// <param name="title"></param>
        /// <param name="isSelectedItem"></param>
        private void AddTitleItem(string title, bool isSelectedItem = true)
        {
            if (isSelectedItem)
            {
                if (!TitleItems.Contains(title))
                {
                    TitleItems.Add(title);
                    TitleComboBox.Items.Add(new SelectedItem() { Text = title, IsSelectedItem = isSelectedItem });
                }
                // 已有的话不用做什么
            }
            else
            {
                TitleComboBox.Items.Add(new SelectedItem() { Text = title, IsSelectedItem = isSelectedItem });
            }
        }
        /// <summary>
        /// 获取选中的标题
        /// </summary>
        /// <returns>如果未选中, 就返回null</returns>
        private string GetSelectedTitle()
        {
            if (TitleComboBox.SelectedIndex >= 0 && TitleComboBox.SelectedItem is SelectedItem item)
            {
                return item.IsSelectedItem ? item.Text : null;
            }
            return null;
        }
        #endregion


        public struct SelectedItem
        {
            public bool IsSelectedItem { get; set; }
            public string Text { get; set; }
            public override string ToString()
            {
                return string.IsNullOrEmpty(Text) ? "空值" : Text;
            }
        }
        public struct Data
        {
            public string Content { get; set; }
            
            public string Title { get; set; }

            public DateTime Time { get; set; }

            public Color? Color { get; set; }

            public override string ToString()
            {
                return $"{(string.IsNullOrEmpty(Title) ? "" : $"[{ Title }]" )}({Time:yyyy-MM-dd hh:mm:ss:fff}): {Content}";
            }
        }

    }
}
