using ChaoticWinformControl;
using ChaoticWinformControl.FeatureGroup;
using HarmonyLib;
using MonoMod.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using Util;
using Util.Config;
using Util.String;

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
            SetTest<Tests.SocketTest001>();
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

        #region 对象详情
        private ObjDetailForm1? ObjDetailForm { get; set; }

        /// <summary>
        /// 显示对象详情
        /// </summary>
        /// <param name="getObjFunc"></param>
        /// <param name="objInfo">对象相关信息</param>
        public void ShowObjDetail(Func<object> getObjFunc, string? objInfo = null)
        {
            this.AutoBeginInvoke(()=>{
                if (ObjDetailForm == null)
                {
                    ObjDetailForm = new ObjDetailForm1();
                    ObjDetailForm.FormClosing += (sender, e) =>
                    {
                        e.Cancel = true;
                        ObjDetailForm.Hide();
                    };
                }
                ObjDetailForm.Text = "对象详情: " + objInfo;
                ObjDetailForm.GetObjFunc = getObjFunc;
                ObjDetailForm.TopMost = true;
                ObjDetailForm.Show();
            });
        }
        #endregion

        #region 单例
        public static MainForm? Instance { get; set; }
        /// <summary>
        /// 触发主窗口实例更新所有监听项
        /// </summary>
        public static void TriggerUpdateMoitorings()
        {
            Instance?.AutoBeginInvoke(Instance.UpdateAllMoitoring);
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
        /// <summary>
        /// 设置默认的Log颜色
        /// </summary>
        /// <param name="colors"></param>
        public void DefaultLogColor(Dictionary<string, Color> colors)
        {
            foreach (string title in colors.Keys)
            {
                Logger.DefaultColor(title, colors[title]);
            }
        }
        /// <summary>
        /// 设置默认的Log颜色
        /// </summary>
        /// <param name="title"></param>
        /// <param name="color"></param>
        public void DefaultLogColor(string title, Color color)
        {
            Logger.DefaultColor(title, color);
        }
        #endregion

        #region 监听


        #region 预览
        private void MoitoringsListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            MoitoringItem item = (MoitoringItem)e.Item.Tag;
            if (item != null)
            {
                MoitoringsListViewToolTip.Show(item.ValuePreview(true), MoitoringsListView,
                    item.ListViewItem!.Bounds.Left, item.ListViewItem.Bounds.Bottom);
            }
        }
        private void MoitoringsListView_MouseLeave(object sender, EventArgs e)
        {
            MoitoringsListViewToolTip.Hide(MoitoringsListView);
        }
        private void MoitoringsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = MoitoringsListView.GetItemAt(e.X, e.Y);
            MoitoringItem item = (MoitoringItem)listViewItem.Tag;
            if (item != null)
            {
                ShowObjDetail(() => item.GetValue(), $"监听项-{item.Prop?.Name ?? "null"}");
            }
        }
        #endregion
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
                    if (item.PropertyType == null) continue;
                    else if (!item.PropertyType!.IsValueType || newValue != item.LastValue)
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
            /// <summary>
            /// 获取监听属性当前值的预览
            /// </summary>
            /// <param name="full">是否返回更为完整的文本, 比如列表项</param>
            /// <returns></returns>
            public string? ValuePreview(bool full)
            {
                object value = GetValue();
                if (value == null) return null;
                else
                {
                    if (PropertyType.IsEnumerable())
                    {
                        IEnumerable temp =  (IEnumerable)value;
                        int count = 0;
                        List<string> strs = new List<string>();
                        foreach (object obj in temp)
                        {
                            count++;
                            strs.Add(obj.ToString());
                        }
                        if (full)
                        {
                            return $"{PropertyType?.Name}[{count}]{{\n{StringHelper.Concat(strs, "\n")}\n}}";
                        }
                        else
                        {
                            return $"{PropertyType?.Name}[{count}]";
                        }
                    }
                    else
                    {
                        return value.ToString();
                    }
                }
            }

            public override string ToString()
            {
                return $"[{Prop!.Name}] = {ValuePreview(false)}";
            }
        }
        #endregion

        #region 全局对象
        protected Dictionary<string, GlobalItem> GlobalItems { get; set; } = new Dictionary<string, GlobalItem>();

        protected void UpdateGlobalList()
        {
            GlobalListView.Items.Clear();
            foreach (string key in GlobalItems.Keys)
            {
                ListViewItem item = new ListViewItem()
                {
                    Text = GlobalItems[key]?.ToString(),
                    Tag = GlobalItems[key],
                };
                GlobalItems[key].RelateListViewItem = item;
                GlobalListView.Items.Add(item);
            }
        }
        #region 右键菜单
        /// <summary>
        /// 当前右键选择到的全局对象
        /// </summary>
        protected GlobalItem? CurrentRightButtonGlobalItem { get; set; } = null;

        private void GlobalListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            CurrentRightButtonGlobalItem = null;

            ListViewItem listItem = GlobalListView.GetItemAt(e.X, e.Y);
            if (listItem == null) return;

            if (listItem.Tag is GlobalItem globalItem)
            {
                CurrentRightButtonGlobalItem = globalItem;
                // 隐藏所有可选菜单项
                foreach (ToolStripItem item in GlobalItemRightMenuMenuStrip.Items)
                {
                    item.Visible = false;
                }

                if (globalItem.DataType == typeof(bool))
                {
                    Value_Bool_ToolStripMenuItem.Visible = true;
                    Value_Bool_ToolStripMenuItem.Text = $"切换为: {!(bool)globalItem.Data!}";
                }
                else if (globalItem.DataType == typeof(string))
                {
                    Value_String_ToolStripMenuItem.Visible = true;
                }
                else
                {
                    NoSupport_ToolStripMenuItem.Visible = true;
                }
                GlobalItemRightMenuMenuStrip.Show(GlobalListView, e.Location);
            }
        }

        #region 菜单项事件
        private void Value_Bool_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentRightButtonGlobalItem == null) return;

            CurrentRightButtonGlobalItem.SetValue(!(bool)CurrentRightButtonGlobalItem.Data!);
        }
        private void Value_String_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentRightButtonGlobalItem == null) return;

            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.OKCancel,
                Text = "修改字符串",
                HintText = $"修改全局对象 '{CurrentRightButtonGlobalItem.Name}' 的值:",
            };
            TextBox textBox = new TextBox()
            {
                Text = (string)CurrentRightButtonGlobalItem.Data!
            };
            frame.SetBody(textBox);
            DialogResult result = frame.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string newString = textBox.Text;
                CurrentRightButtonGlobalItem.SetValue(newString);
            }
        }
        #endregion


        #endregion

        public class GlobalItem
        {
            /// <summary>
            /// 全局对象的名称
            /// </summary>
            public string? Name { get; set; }
            /// <summary>
            /// 全局对象的数据
            /// </summary>
            public object? Data { get; private set; }
            /// <summary>
            /// 数据类型
            /// </summary>
            public Type? DataType { get; set; }

            /// <summary>
            /// 关联的列表项
            /// </summary>
            public ListViewItem? RelateListViewItem { get; set; }


            public virtual void SetValue(object? newValue) 
            {
                Data = newValue;
                if (RelateListViewItem != null)
                {
                    RelateListViewItem.Text = ToString();
                }
            }

            public override string ToString()
            {
                string? dataString = Data?.ToString();
                if (Data != null && DataType != null)
                {
                    if (typeof(IList).IsAssignableFrom(DataType)) 
                    { // 列表类型

                        IList list = (IList)Data;
                        List<string> arr = new();
                        foreach (object item in list)
                        {
                            arr.Add(item.ToString());
                        }
                        dataString = $"IList[{Util.String.StringHelper.Concat(arr, ", ")}]";
                    }
                }
                return $"{Name}={dataString}";
            }
        }
        #endregion

        #region 全局对象 绑定
        /// <summary>
        /// 绑定一个全局对象作为数据源, 可以在窗口左侧的全局对象区域修改数据, 修改数据后将同时应用到绑定的对象上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="name">该绑定的名称</param>
        /// <param name="target">将要绑定的对象</param>
        /// <param name="getProperty">取得字段或者属性的方法</param>
        /// <param name="initValue"></param>
        /// <param name="forceType">强制使用该类型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void BindData<T, TProperty>(
            string name,
            T target, 
            Expression<Func<T, TProperty>> getProperty,
            TProperty initValue,
            Type? forceType = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (GlobalItems.ContainsKey(name))
            {
                throw new ArgumentException($"已有同名全局对象 {name}", nameof(name));
            }
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (getProperty == null)
            {
                throw new ArgumentNullException(nameof(getProperty));
            }
            MemberExpression? member = getProperty.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"请为类型 \"{typeof(T).FullName}\" 指定一个字段或属性作为 Lambda 的主体", nameof(getProperty));
            }
            BindItem item = new BindItem()
            {
                Name = name,
                Target = target,
                TargetField = typeof(T).GetField(member.Member.Name),
                TargetProperty = typeof(T).GetProperty(member.Member.Name),
            };
            if (item.TargetField != null)
            {
                item.DataType = item.TargetField.FieldType;
            }
            else if (item.TargetProperty != null)
            {
                item.DataType = item.TargetProperty.PropertyType;
            }
            else
            {
                throw new ArgumentException($"未能在类型 \"{typeof(T).FullName}\" 中找到名为 \"{member.Member.Name}\" 的公共字段或属性", nameof(T));
            }
            if (forceType != null && item.DataType!.IsAssignableFrom(forceType))
            {
                throw new ArgumentException(
                    $"无法将类型 \"{forceType.FullName}\" 强制分配给需要绑定的字段或属性 \"{item.DataType.FullName}\"", 
                    nameof(forceType));
            }

            item.SetValue(initValue);

            GlobalItems.Add(name, item);
            UpdateGlobalList();
        }

        public class BindItem : GlobalItem
        {
            /// <summary>
            /// 绑定的目标
            /// </summary>
            public object? Target { get; set; }
            /// <summary>
            /// 绑定的目标字段 (绑定的字段与属性仅其中一者生效)
            /// </summary>
            public FieldInfo? TargetField { get; set; }
            /// <summary>
            /// 绑定的目标属性 (绑定的字段与属性仅其中一者生效)
            /// </summary>
            public PropertyInfo? TargetProperty { get; set; }


            public override void SetValue(object? newValue)
            {
                base.SetValue(newValue);
                if (TargetField != null)
                {
                    TargetField.SetValue(Target, newValue);
                }
                else if (TargetProperty != null)
                {
                    TargetProperty.SetValue(Target, newValue);
                }
            }

            public override string ToString()
            {
                return $"[绑定]{base.ToString()}";
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
