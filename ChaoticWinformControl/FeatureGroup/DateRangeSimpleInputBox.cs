using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl.FeatureGroup
{
    /// <summary>
    /// 时间范围的简单输入, 选择一个开始时间之后，再选择一个时间长度
    /// </summary>
    [DefaultEvent(nameof(OnSelectedRangeChanged))]
    public partial class DateRangeSimpleInputBox : UserControl
    {
        #region 可配置属性
        [Browsable(true), Category("自订属性"), Description("显示在日期选择器下方 CheckBox 的文本")]
        public string InnerText { get => WorkingCheckBox.Text; set => WorkingCheckBox.Text = value; }

        [Browsable(true), Category("自订属性"), Description("天数选项1")]
        public uint DayOption1 { get => dayOption1; set { dayOption1 = value; UpdateOptionText(); } }
        private uint dayOption1;
        [Browsable(true), Category("自订属性"), Description("天数选项2")]
        public uint DayOption2 { get => dayOption2; set { dayOption2 = value; UpdateOptionText(); } }
        private uint dayOption2;
        [Browsable(true), Category("自订属性"), Description("天数选项3")]
        public uint DayOption3 { get => dayOption3; set { dayOption3 = value; UpdateOptionText(); } }
        private uint dayOption3;
        [Browsable(true), Category("自订属性"), Description("天数选项4")]
        public uint DayOption4 { get => dayOption4; set { dayOption4 = value; UpdateOptionText(); } }
        private uint dayOption4;

        [Browsable(true), Category("自订属性"), Description("默认选中的选项编号")]
        public OptionEnum CurrentOption 
        { 
            get
            {
                if (Option1Box.Checked) return OptionEnum.Option1;
                if (Option2Box.Checked) return OptionEnum.Option2;
                if (Option3Box.Checked) return OptionEnum.Option3;
                if (Option4Box.Checked) return OptionEnum.Option4;
                return default;
            }
            set
            {
                switch (value)
                {
                    case OptionEnum.Option1:
                        Option1Box.Checked = true;
                        break;
                    case OptionEnum.Option2:
                        Option2Box.Checked = true;
                        break;
                    case OptionEnum.Option3:
                        Option3Box.Checked = true;
                        break;
                    case OptionEnum.Option4:
                        Option4Box.Checked = true;
                        break;
                }
            }
        }
        public enum OptionEnum
        {
            Option1, Option2, Option3, Option4,
        }

        [Browsable(true), Category("自订属性"), Description("控件目前的工作状态, 是否启用日期范围")]
        public bool Working { get => WorkingCheckBox.Checked; set { WorkingCheckBox.Checked = value; UpdateWorking(); } }


        [Browsable(true), Category("自订属性"), Description("默认的时间偏移量, 比如 0 值, 相当于今天当天, -3值, 相当于三天前")]
        public int DefaultDate
        {
            get => defaultDate;
            set
            {
                defaultDate = value;
                UpdateDateInput();
            }
        }
        private int defaultDate = 0;

        #endregion

        #region 只读属性 
        /// <summary>
        /// 当前选择的开始时间
        /// </summary>
        [Browsable(false)]
        public DateTime StartDate
        {
            get
            {
                return new DateTime(DateInput.Value.Year, DateInput.Value.Month, DateInput.Value.Day);
            }
        }
        /// <summary>
        /// 当前选择的结束时间
        /// </summary>
        [Browsable(false)]
        public DateTime EndDate
        {
            get
            {
                uint timeLength = 0;
                if (Option1Box.Checked) timeLength = dayOption1;
                if (Option2Box.Checked) timeLength = dayOption2;
                if (Option3Box.Checked) timeLength = dayOption3;
                if (Option4Box.Checked) timeLength = dayOption4;
                return new DateTime(DateInput.Value.Year, DateInput.Value.Month, DateInput.Value.Day).AddDays(timeLength);
            }
        }

        #endregion

        #region 对外暴露的事件
        public delegate void OnSelectedRangeChangedDelegate(object sender, DateTime startDate, DateTime endDate);
        public event OnSelectedRangeChangedDelegate OnSelectedRangeChanged;
        #endregion

        public DateRangeSimpleInputBox()
        {
            InitializeComponent();
            // 初始化控件状态
            dayOption1 = 3;
            dayOption2 = 7;
            dayOption3 = 14;
            dayOption4 = 30;

            WorkingCheckBox.Checked = true;

            UpdateOptionText();
            UpdateWorking();
            UpdateDateInput();
        }

        #region 显示刷新
        /// <summary>
        /// 刷新选项的文本
        /// </summary>
        private void UpdateOptionText()
        {
            Option1Box.Text = GetDayOptionText(dayOption1);
            Option2Box.Text = GetDayOptionText(dayOption2);
            Option3Box.Text = GetDayOptionText(dayOption3);
            Option4Box.Text = GetDayOptionText(dayOption4);
        }
        /// <summary>
        /// 刷新各子控件的工作方式
        /// </summary>
        private void UpdateWorking()
        {
            Option1Box.Enabled = Working;
            Option2Box.Enabled = Working;
            Option3Box.Enabled = Working;
            Option4Box.Enabled = Working;

            DateInput.Enabled = Working;
        }
        private void UpdateDateInput()
        {
            LastDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateInput.Value =
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                + new TimeSpan(defaultDate, 0, 0, 0);
            DateInput.Checked = true;
        }
        #endregion

        #region 临时数据
        private DateTime LastDate { get; set; }
        #endregion

        #region 控制
        /// <summary>
        /// 设置为当前月份
        /// </summary>
        public void SetToCurrentMonth()
        {
            DateTime startTime = StartDate;
            int days = DateTime.DaysInMonth(startTime.Year, startTime.Month);
            Option4Box.Checked = true;
            DayOption4 = (uint)days;
            DateInput.Value = new DateTime(startTime.Year, startTime.Month, 1);

        }
        #endregion

        #region 计算方法
        private string GetDayOptionText(uint day)
        {
            if (day == 0)
            {
                return " 当天 ";
            }
            if (day % 7 == 0)
            {
                return $" {day / 7} 周 ";
            }
            else
            {
                return $" {day} 天";
            }
        }
        private bool IsSameDate(DateTime d1, DateTime d2)
        {
            return d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day;
        }

        #endregion

        #region 事件
        protected override void OnMouseClick(MouseEventArgs e)
        {
            ChildControl_MouseUp(this, e);
        }
        #endregion
        #region 控件事件
        private void WorkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateWorking();
        }
        private void DateInput_ValueChanged(object sender, EventArgs e)
        {
            if (!IsSameDate(LastDate, StartDate))
            {
                OnSelectedRangeChanged?.Invoke(this, StartDate, EndDate);
            }
        }

        private void Option1Box_CheckedChanged(object sender, EventArgs e)
        {
            if (Option1Box.Checked)
                OnSelectedRangeChanged?.Invoke(this, StartDate, EndDate);
        }

        private void Option2Box_CheckedChanged(object sender, EventArgs e)
        {
            if (Option2Box.Checked)
                OnSelectedRangeChanged?.Invoke(this, StartDate, EndDate);
        }

        private void Option3Box_CheckedChanged(object sender, EventArgs e)
        {
            if (Option3Box.Checked)
                OnSelectedRangeChanged?.Invoke(this, StartDate, EndDate);
        }

        private void Option4Box_CheckedChanged(object sender, EventArgs e)
        {
            if (Option4Box.Checked)
                OnSelectedRangeChanged?.Invoke(this, StartDate, EndDate);
        }
        private void SetThisMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetToCurrentMonth();
        }
        private void ChildControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Enabled)
            {
                RightMenuContextMenuStrip.Show(this, PointToClient(MousePosition));
            }
        }
        #endregion

    }
}
