using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsTest.Tests
{
    public partial class ChartTest001 : TestFormBase
    {
        public ChartTest001()
        {
            InitializeComponent();
        }

        public List<TempClass> data 
        { 
            get => _data;
            set
            {
                _data = value;
                if (_data == null)
                {
                    _data = new List<TempClass>();
                }
                chart1.Series[0].Points.DataBind(_data, nameof(TempClass.x), nameof(TempClass.y), string.Empty);
            }
        }
        private List<TempClass> _data = new List<TempClass>();

        public override void TestContent()
        {
            MainForm!.BindData("可见?", chart1, c => c.Visible, chart1.Visible);
            MainForm!.BindData(
                "数据",   // 名称
                this, f => f.data, // 绑定对象 
                Util.Random.RandomObjectHelper.GetList<TempClass>(minCount: 10, maxCount: 20)   // 初始数据
                );

        }
        protected override List<NeedMoitoringItem> GetNeedMoitorings()
        {
            var output = base.GetNeedMoitorings();
            output.AddRange(NeedMoitoringItem.From("数据", data, 
                nameof(data.Count)));
            return output;
        }

        public class TempClass
        {
            [Util.Random.Attributes.IntRange(0, 50)]
            public int x { get; set; }
            [Util.Random.Attributes.IntRange(0, 50)]
            public int y { get; set; }

            public override string ToString()
            {
                return $"({x}, {y})";
            }
        }
    }
}
