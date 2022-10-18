using ChaoticWinformControl.DataContainer;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl.FeatureGroup
{
    public partial class CSVExportForm : Form
    {
        public CSVExportForm()
        {
            InitializeComponent();

            RelateTable.AutoGenerateColumns = false;
            Items = new SortableBindingList<Item>();
            RelateTable.DataSource = Items;
            
        }
        #region 属性
        /// <summary>
        /// 输出字符集
        /// </summary>
        public Encoding ExportEncoding { get; set; }
        #endregion

        #region 数据
        /// <summary>
        /// 数据
        /// </summary>
        public object[] Data { get; private set; }

        /// <summary>
        /// 配置项列表
        /// </summary>
        public SortableBindingList<Item> Items { get; private set; }
        #endregion

        #region 控制
        /// <summary>
        /// 设置准备导出的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void SetData<T>(IList<T> list)
        {
            // 转换为 objec[]
            if (list == null) Data = new object[0];
            else Data = list.Select(i => (object)i).ToArray();

            // 读取字段信息
            List<Item> items = new List<Item>();
            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                items.Add(new Item()
                {
                    PropertyInfo = property,
                    IsExport = true,    // 默认要输出
                    TargetColumnName = property.Name,   // 默认使用属性名
                });
            }
            Items.ResetAs(items);
        }
        #endregion

        #region 表格
        public class Item
        {
            /// <summary>
            /// 属性名
            /// </summary>
            public string PropertyName { get => PropertyInfo.Name; }
            /// <summary>
            /// 属性类型
            /// </summary>
            public string PropertyType { get => Util.String.StringHelper.GetTypeString(PropertyInfo.PropertyType); }
            /// <summary>
            /// 属性
            /// </summary>
            public PropertyInfo PropertyInfo { get; set; }
            /// <summary>
            /// 目标列名
            /// </summary>
            public string TargetColumnName { get; set; }
            /// <summary>
            /// 是否输出列
            /// </summary>
            public bool IsExport { get; set; }
        }
        #endregion

        #region 事件
        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileInput.FileName))
            {
                MessageBox.Show("未选择输出文件");
                return;
            }
            using (var file = new FileStream(FileInput.FileName, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(file, ExportEncoding ?? Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // 写列名
                foreach (Item item in Items)
                {
                    if (item.IsExport)
                    {
                        csv.WriteField(item.TargetColumnName);
                    }
                }
                csv.NextRecord();
                // 写数据
                foreach (object obj in Data)
                {
                    foreach (Item item in Items)
                    {
                        if (item.IsExport)
                        {
                            object value = item.PropertyInfo.GetValue(obj);
                            csv.WriteField(value == null ? "" : value.ToString());
                        }
                    }
                    csv.NextRecord();
                }
                MessageBox.Show("导出完成");
            }
        }
        #endregion
    }
}
