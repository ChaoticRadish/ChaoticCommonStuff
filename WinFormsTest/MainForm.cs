using ChaoticWinformControl;
using HarmonyLib;
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


        private void Test()
        {
            // 在这里放执行测试的内容
            SetTest<Tests.DateRangeInputerTest>();
            Testing?.InitMoitorings();
            Testing?.TestContent();

            MoitoringTask = Task.Run(() =>
            {
                IsMoitoring = true;
                while (IsMoitoring)
                {
                    this.AutoInvoke(new Action(UpdateAllMoitoring));
                    Thread.Sleep(MoitoringTime);
                }
            });
        }
        


        public MainForm()
        {
            InitializeComponent();
            Instance = this;

            MoitoringTime = 100;

            Harmony = new Harmony(nameof(MainForm));
            CommonFixMethod = new HarmonyMethod(typeof(MainForm).GetMethod(nameof(TriggerUpdateMoitorings)));
        }

        #region 监听
        private Harmony Harmony { get; set; }
        /// <summary>
        /// 通用的用于监听的方法 (Setter控制器之后)
        /// </summary>
        private HarmonyMethod CommonFixMethod { get; set; }

        /// <summary>
        /// 监听轮询的时间间隔 (单位: ms)
        /// </summary>
        [Category("行为"), Description("监听轮询的时间间隔 (单位: ms)")]
        public int MoitoringTime 
        { 
            get => moitoringTime; 
            set
            {
                moitoringTime = value;
                MoitoringTimeLabel.Text = $"轮询间隔: {moitoringTime} ms";
            }
        }
        private int moitoringTime;

        private Task? MoitoringTask { get; set; }
        private bool IsMoitoring { get; set; } = false;
        #endregion

        #region 单例
        public static MainForm? Instance { get; set; }
        /// <summary>
        /// 触发主窗口实例更新所有监听项
        /// </summary>
        public static void TriggerUpdateMoitorings()
        {
            Instance?.UpdateAllMoitoring();
        }

        #endregion
        private TestFormBase? Testing { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Test();
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

            Testing = form;
        }

        #region Log
        public void Log(string title, string content, Color? color = null)
        {
            Logger.Log(new ChaoticWinformControl.LogShowerBox.Data()
            {
                Content = content,
                Title = title,
                Color = color
            });
        }
        #endregion

        #region 监听
        private Dictionary<string, List<MoitoringItem>> Moitorings { get; set; } = new Dictionary<string, List<MoitoringItem>>();

        /// <summary>
        /// 刷新所有监听项
        /// </summary>
        public void UpdateAllMoitoring()
        {
            foreach (string key in Moitorings.Keys)
            {
                ListViewGroup group = MoitoringsListView.Groups.Add(key, key);
                foreach (MoitoringItem item in Moitorings[key])
                {
                    object? newValue = item.GetValue();
                    if (newValue != item.LastValue)
                    {
                        item.LastValue = newValue;
                        if (item.ListViewItem != null)
                        {
                            item.ListViewItem.Text = item.ToString();
                        }
                    }
                }
            }
        }
        public void InitMoitoringListView()
        {
            MoitoringsListView.Clear();
            foreach (string key in Moitorings.Keys)
            {
                ListViewGroup group = MoitoringsListView.Groups.Add(key, key);
                foreach (MoitoringItem item in Moitorings[key])
                {
                    item.ListViewItem = new ListViewItem(item.ToString(), group);
                    item.ListViewItem.Tag = item;
                    MoitoringsListView.Items.Add(item.ListViewItem);
                }
            }
        }
        /// <summary>
        /// 添加需要监听的事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        public void AddNeedMoitoring(string? name, object obj, PropertyInfo property)
        {
            if (obj == null || property.GetMethod == null) return;
            if (string.IsNullOrEmpty(name))
            {
                name = "未命名分组";
            }
            MoitoringItem item = new MoitoringItem()
            {
                Obj = obj,
                Prop = property,
            };

            if (property.SetMethod != null)
            {
                Harmony.Patch(property.SetMethod, null, CommonFixMethod);
            }

            if (Moitorings.ContainsKey(name!))
            {
                Moitorings[name!].Add(item);
            }
            else
            {
                Moitorings.Add(name!, new List<MoitoringItem>() { item });
            }
        }

        class MoitoringItem
        {
            /// <summary>
            /// 监听的对象
            /// </summary>
            public object? Obj { get; set; }
            /// <summary>
            /// 监听的属性
            /// </summary>
            public PropertyInfo? Prop { get; set; }
            /// <summary>
            /// 监听的属性的类型
            /// </summary>
            public Type? PropertyType { get => Prop?.PropertyType; }

            /// <summary>
            /// 所在的控件
            /// </summary>
            public ListViewItem? ListViewItem { get; set; }

            /// <summary>
            /// 最后一个获取到的值
            /// </summary>
            public object? LastValue { get; set; }

            /// <summary>
            /// 获取监听属性的当前值
            /// </summary>
            /// <returns></returns>
            public object GetValue()
            {
                return Prop!.GetMethod.Invoke(Obj, null);
            }

            public override string ToString()
            {
                return $"[{Prop!.Name}] = {GetValue()}";
            }
        }
        #endregion

        #region 窗口事件
        protected override void OnClosing(CancelEventArgs e)
        {
            if (MoitoringTask != null)
            {
                IsMoitoring = false;
                if (!MoitoringTask.IsCompleted)
                {
                    Task.Run(() =>
                    {
                        while (!MoitoringTask.IsCompleted)
                        {
                            Thread.Sleep(MoitoringTime);
                        }
                        this.AutoInvoke(new Action(Close));
                    });
                    e.Cancel = true;
                }
            }

            base.OnClosing(e);
        }
        #endregion
    }
}
