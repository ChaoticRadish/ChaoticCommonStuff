using ChaoticWinformControl.FeatureGroup;
using HarmonyLib;
using MonoMod.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest.Tests
{
    public partial class DateRangeInputerTest : TestFormBase
    {
        public DateRangeInputerTest()
        {
            InitializeComponent();
        }

        public override void TestContent()
        {
            base.TestContent();


        }

        protected override List<NeedMoitoringItem> GetNeedMoitorings()
        {
            var output = base.GetNeedMoitorings();
            output.AddRange(NeedMoitoringItem.From("日期控件", dateRangeSimpleInputBox1,
                nameof(dateRangeSimpleInputBox1.DayOption1),
                nameof(dateRangeSimpleInputBox1.DayOption2),
                nameof(dateRangeSimpleInputBox1.DayOption3),
                nameof(dateRangeSimpleInputBox1.DayOption4),
                nameof(dateRangeSimpleInputBox1.StartDate),
                nameof(dateRangeSimpleInputBox1.EndDate),
                nameof(dateRangeSimpleInputBox1.Working),
                nameof(dateRangeSimpleInputBox1.CurrentOption)
                ));
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm!.Log("按钮", $"开始时间: {dateRangeSimpleInputBox1.StartDate:yyyy-MM-dd}, 结束时间: {dateRangeSimpleInputBox1.EndDate:yyyy-MM-dd}");
        }

        private void dateRangeSimpleInputBox1_OnSelectedRangeChanged(object sender, DateTime startDate, DateTime endDate)
        {
            MainForm!.Log("事件", $"开始时间: {startDate:yyyy-MM-dd}, 结束时间: {endDate:yyyy-MM-dd}");
        }
    }
}
