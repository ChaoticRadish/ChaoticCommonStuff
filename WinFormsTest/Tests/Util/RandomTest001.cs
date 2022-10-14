using ChaoticWinformControl;
using ChaoticWinformControl.FeatureGroup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util.Random;
using Util.String;

namespace WinFormsTest.Tests
{
    public partial class RandomTest001 : TestFormBase
    {
        /// <summary>
        /// 准备生成的类型
        /// </summary>
        public Type? TargetType { get; private set; }
        /// <summary>
        /// 被生成的数据
        /// </summary>
        public object? Obj { get; private set; }
        private BindingList<Data> PropertyDatas { get; set; } = new BindingList<Data>();

        public RandomTest001()
        {
            InitializeComponent();
            ObjShower.DataSource = PropertyDatas;
        }

        public void SetTargetType<T>()
            where T : new()
        {
            TargetType = typeof(T);
            TypeShower.Text = TargetType?.FullName;
        }
        private void ShowObj()
        {
            PropertyDatas.Clear();

            if (Obj == null) return;

            Type type = Obj.GetType();

            foreach (PropertyInfo property in type.GetProperties())
            {
                PropertyDatas.Add(new Data()
                {
                    // 类型 = StringHelper.GetTypeString(property.PropertyType).SplitLine(),
                    类型 = property.PropertyType.FullName.SplitLine(),
                    名称 = property.Name,
                    值 = property.GetValue(Obj)?.ToString()
                });
            }
        }

        class Data
        {
            public string? 类型 { get; set; }
            public string? 名称 { get; set; }
            public string? 值 { get; set; }

        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (TargetType == null) return;
            CreateObj();
        }
        private void CreateObj()
        {
            try
            {
                RandomObjectHelper.RandomConfig config = RandomObjectHelper.DefaultConfig;
                config.MinStringLength = 5;
                config.MaxStringLength = 8;
                config.ProbabilityNull = 0.5;
                config.MinCount = 1;
                config.MaxCount = 4;
                config.ListDepth = 3;
                Obj = RandomObjectHelper.GetObject(type: TargetType, config: config);
                ShowObj();
            }
            catch (Exception ex)
            {
                Log("随机生成", ex.Message);
                Log("随机生成", ex.StackTrace);
            }
        }


        public override void TestContent()
        {
            base.TestContent();
            TargetType = typeof(TestClass);
        }


        class TestClass
        {
            [IntRange(10)]
            public string? 字符串1 { get; set; }
            [IntRange(10)]
            public string? 字符串2 { get; set; }
            public DateTime 日期1 { get; set; }
            [DateTimeRange("2020/1/30", 999)]
            [Probability(Null = 0.7)]
            public DateTime? 日期2 { get; set; }

            [Probability(True = 0.1)]
            public bool BOOL_1 { get; set; }

            [Probability(True = 0.1)]
            public bool BOOL_2 { get; set; }

            [Probability(True = 0.1)]
            public bool BOOL_3 { get; set; }

            [Probability(True = 0.1)]
            public bool BOOL_4 { get; set; }

            [Probability(True = 0.1)]
            public bool BOOL_5 { get; set; }

            [Probability(True = 0.1)]
            public bool BOOL_6 { get; set; }
            public bool BOOL_7 { get; set; }
            public bool BOOL_8 { get; set; }
            public List<string>? List { get; set; }
            public string ListString 
            {
                get
                {
                    if (List != null)
                    {
                        return Util.String.StringHelper.Concat2(List);
                    }
                    return "null";
                }
            }

            public int[]? Ints { get; set; }
            public string IntsString
            {
                get
                {
                    if (Ints != null)
                    {
                        return Util.String.StringHelper.Concat2(Ints.Select(i => i.ToString()).ToList());
                    }
                    return "null";
                }
            }

            public List<TestClass>? TestList { get; set; }
            public string TestString
            {
                get
                {
                    if (TestList != null)
                    {
                        return $"({TestList.Count})[ {Util.String.StringHelper.Concat(TestList.Select(i => i.TestString).ToList(), ", ")} ]";
                    }
                    return "null";
                }
            }

        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            InputFormFrame typeSelect = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.OKCancel,
            };
            TypeComboBox typeComboBox = new TypeComboBox();
            typeComboBox.SetAssembly(Assembly.GetExecutingAssembly());
            typeComboBox.TypeClassify = 
                TypeComboBox.TypeClassifyEnum.All 
                | TypeComboBox.TypeClassifyEnum.HasEmptyArgConstructor;
            typeSelect.SetBody(typeComboBox);
            if (typeSelect.ShowDialog() == DialogResult.OK && typeComboBox.SelectedType != null)
            {
                RandomObjectCreatrForm form = new RandomObjectCreatrForm()
                {
                    FormBorderStyle = FormBorderStyle.None,
                };
                form.OnCreateException += (ex) =>
                {
                    Log("生成失败", ex.Message);
                    Log("生成失败", ex.StackTrace);
                };
                form.SetTargetType(typeComboBox.SelectedType);
                InputFormFrame frame = new InputFormFrame()
                {
                    Buttons = MessageBoxButtons.YesNo,
                };
                frame.SetBody(form);
                frame.ShowDialog();
            }


        }
    }
}
