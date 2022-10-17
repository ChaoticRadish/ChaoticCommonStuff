using System.ComponentModel;
using System.Reflection;
using Util;

namespace WinFormsTest
{
    public partial class TestFormBase : Form
    {
        [Browsable(false)]
        public MainForm? MainForm { get; set; } = null;

        public TestFormBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化需要监听的项目
        /// </summary>
        public void InitMoitorings()
        {
            List<NeedMoitoringItem> moitorings = GetNeedMoitorings();
            foreach (NeedMoitoringItem moitoring in moitorings)
            {
                PropertyInfo property = moitoring.Obj!.GetType().GetProperty(moitoring.PropertyName);
                if (property != null)
                {
                    MainForm?.AddNeedMoitoring(moitoring.GroupName, moitoring.Obj!, property);
                }
            }
            MainForm?.InitMoitoringListView();
        }
        /// <summary>
        /// 测试内容
        /// </summary>
        public virtual void TestContent()
        {
            
        }


        protected void Log(string title, object obj, Color? color = null)
        {
            MainForm?.Log(title, obj == null ? "null" : obj.ToString(), color);
        }
        protected void Log(string title, string content, Color? color = null)
        {
            MainForm?.Log(title, content, color);
        }
        protected void Log<TEnum>(TEnum e, object obj, Color? color = null) where TEnum : Enum
        {
            MainForm?.Log(e.GetDesc(), obj == null ? "null" : obj.ToString(), color);
        }
        protected void Log<TEnum>(TEnum e, string content, Color? color = null) where TEnum : Enum
        {
            MainForm?.Log(e.GetDesc(), content, color);
        }
        protected void LogColor(string title, Color color)
        {
            MainForm?.DefaultLogColor(title, color);
        }
        protected void LogColor<TEnum>(TEnum e, Color color) where TEnum : Enum
        {
            MainForm?.DefaultLogColor(e.GetDesc(), color);
        }


        /// <summary>
        /// 取得需要监听的项
        /// </summary>
        /// <returns></returns>
        protected virtual List<NeedMoitoringItem> GetNeedMoitorings()
        {
            List<NeedMoitoringItem> output = new List<NeedMoitoringItem>();
            return output;
        }
        protected struct NeedMoitoringItem
        {
            public string? GroupName { get; set; }
            public object? Obj { get; set; }
            public string? PropertyName { get; set; }

            public static List<NeedMoitoringItem> From(string groupName, object obj, params string[] propertyNames)
            {
                List<NeedMoitoringItem> output = new List<NeedMoitoringItem>();
                foreach (string name in propertyNames)
                {
                    output.Add(new NeedMoitoringItem()
                    {
                        GroupName = groupName,
                        Obj = obj,
                        PropertyName = name
                    });
                }
                return output;
            }
        }
    }
}
