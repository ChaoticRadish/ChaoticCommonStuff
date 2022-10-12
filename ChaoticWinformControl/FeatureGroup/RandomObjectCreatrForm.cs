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
using Util.Random;
using Util.String;

namespace ChaoticWinformControl.FeatureGroup
{
    public partial class RandomObjectCreatrForm : Form
    {

        /// <summary>
        /// 准备生成的类型
        /// </summary>
        public Type TargetType { get; private set; }
        /// <summary>
        /// 被生成的数据
        /// </summary>
        public object Obj { get; private set; }
        /// <summary>
        /// 准备生成的类型属性列表
        /// </summary>
        private BindingList<Data> PropertyDatas { get; set; } = new BindingList<Data>();

        #region 事件
        public delegate void OnCreateExceptionHandler(Exception ex);
        /// <summary>
        /// 随机生成对象时发生异常的事件
        /// </summary>
        public event OnCreateExceptionHandler OnCreateException;
        #endregion


        public RandomObjectCreatrForm()
        {
            InitializeComponent();
            ObjShower.AutoGenerateColumns = false;
            ObjShower.DataSource = PropertyDatas;
        }
        /// <summary>
        /// 设置准备生成的目标类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetTargetType<T>()
            where T : new()
        {
            TargetType = typeof(T);

            ShowPropertys();
        }
        /// <summary>
        /// 设置准备生成的目标类型
        /// </summary>
        /// <param name="t"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetTargetType(Type t)
        {
            if (t.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException("输入类型未提供无参构造函数", nameof(t));
            }
            TargetType = t;

            ShowPropertys();

        }


        /// <summary>
        /// 将对象显示到表格
        /// </summary>
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
                    TypeString = property.PropertyType.FullName.SplitLine(),
                    Name = property.Name,
                    Value = property.GetValue(Obj)?.ToString()
                });
            }
        }
        private void ShowPropertys()
        {
            TypeShower.Text = string.Empty;
            PropertyDatas.Clear();

            if (TargetType == null) return;

            TypeShower.Text = TargetType.FullName;


            foreach (PropertyInfo property in TargetType.GetProperties())
            {
                PropertyDatas.Add(new Data()
                {
                    // 类型 = StringHelper.GetTypeString(property.PropertyType).SplitLine(),
                    TypeString = property.PropertyType.FullName.SplitLine(),
                    Name = property.Name,
                    Value = null,
                });
            }
        }

        class Data
        {
            public string TypeString { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }

        }

        #region 控件事件
        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (TargetType == null) return;
            try
            {
                Obj = RandomObjectHelper.GetObject(TargetType);
                ShowObj();
            }
            catch (Exception ex)
            {
                OnCreateException?.Invoke(ex);
            }
        }

        #endregion

    }
}
