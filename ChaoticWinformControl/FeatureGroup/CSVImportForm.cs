using ChaoticWinformControl.DataContainer;
using CsvHelper;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl.FeatureGroup
{
    public partial class CSVImportForm : Form
    {
        public CSVImportForm()
        {
            InitializeComponent();

            SetLogColor();

            LoggerBox.TimeFormat = null;

            Logger = new LogController();
            Logger.LogBody += (title, content) =>
            {
                LoggerBox.SimpleLog(title, content);
            };

        }
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type TargetType { get; private set; }
        /// <summary>
        /// 目标类型含有Setter的属性
        /// </summary>
        protected Dictionary<string, PropertyInfo> SetterOwnerProperty { get; private set; } = new Dictionary<string, PropertyInfo>();
        /// <summary>
        /// 目标类型含有Setter的属性的名字列表
        /// </summary>
        protected List<string> SetterOwnerPropertyNames{ get; private set; } = new List<string>();

        /// <summary>
        /// Log的控制器
        /// </summary>
        public LogController Logger { get; private set; }

        /// <summary>
        /// 读取到的列
        /// </summary>
        protected SortableBindingList<Item> Columns { get; private set; } = new SortableBindingList<Item>();

        #region 设置
        /// <summary>
        /// 导入字符集
        /// </summary>
        public Encoding ExportEncoding { get; set; }

        #endregion

        #region 事件
        /// <summary>
        /// 导入对象
        /// </summary>
        /// <param name="importObj">读取到的一个对象</param>
        /// <param name="logController"></param>
        /// <returns>方法运行是否正常</returns>
        public delegate bool ImportingHandle(object importObj, LogController logController);
        /// <summary>
        /// 导入过程中没读取到一项, 就调用一次
        /// </summary>
        public event ImportingHandle Importing;

        /// <summary>
        /// 导入对象列表
        /// </summary>
        /// <param name="importObj">读取到的所有对象</param>
        /// <param name="logController"></param>
        public delegate void ImportDoneHandle(List<object> importObj, LogController logController);
        /// <summary>
        /// 全部读取完后调用
        /// </summary>
        public event ImportDoneHandle ImportDone;
        #endregion

        #region 控制
        /// <summary>
        /// 设置目标类型
        /// </summary>
        /// <param name="type">需要有公共无参构造函数</param>
        public void SetTargetType(Type type)
        {
            TargetType = type;
            if (TargetType == null) return;

            // 检查是否有无参构造函数
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException("输入类型未提供无参构造函数", nameof(type));
            }

            // 目标类型显示框
            TargetTypeShower.Text = TargetType.FullName;
            // 遍历目标类型的属性, 取得有Set方法的属性
            PropertyInfo[] properties = type.GetProperties();
            SetterOwnerProperty.Clear();
            foreach (PropertyInfo property in properties)
            {
                if (property.SetMethod != null)
                {
                    SetterOwnerProperty.Add(property.Name, property);
                }
            }
            SetterOwnerPropertyNames = SetterOwnerProperty.Keys.ToList();

            // 绑定数据源到属性名的下拉框中
            PropertyNameColumn.DataSource = SetterOwnerPropertyNames;
            // DataGridView的数据源需要比列的晚设置
            RelateTable.DataSource = Columns;
        }
        #endregion

        #region 读取
        /// <summary>
        /// 读取列名
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadColumnHead(string fileName)
        {
            if (string.IsNullOrEmpty(FileInput.FileName))
            {
                MessageBox.Show("未选择输出文件");
                return;
            }
            using (var file = new FileStream(FileInput.FileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file, ExportEncoding ?? Encoding.UTF8))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                Logger.Log("进度", $"开始读取列头");
                if (!csv.Read())
                {
                    Logger.Log("失败", $"未能读取到任何行");
                    return;
                }
                if (!csv.ReadHeader())
                {
                    Logger.Log("失败", $"未能读取到列头");
                    return;
                }
                string[] heads = csv.HeaderRecord;
                if (heads == null || heads.Length == 0)
                {
                    Logger.Log("失败", $"读取到的列头是空的");
                    return;
                }
                List<Item> columns = new List<Item>();
                int index = 0;
                foreach (string head in heads)
                {
                    Logger.Log("进度", $"读取到列头[{index}] {head}");

                    Item item = new Item()
                    {
                        ImportColumnName = head,
                        ColumnIndex = index,
                    };
                    if (SetterOwnerPropertyNames.Contains(item.ImportColumnName))
                    {
                        item.PropertyName = item.ImportColumnName;
                        item.PropertyType = Util.String.StringHelper.GetTypeString(SetterOwnerProperty[item.PropertyName].PropertyType);
                    }
                    columns.Add(item);

                    index++;
                }
                Logger.Log("进度", $"共读取到 {index} 列, 请配置实体与列的对应关系");
                Columns.ResetAs(columns);
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadData(string fileName)
        {
            if (string.IsNullOrEmpty(FileInput.FileName))
            {
                MessageBox.Show("未选择输出文件");
                return;
            }
            using (var file = new FileStream(FileInput.FileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file, ExportEncoding ?? Encoding.UTF8))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // 先读取一行, 以跳过列头
                csv.Read();

                // 检查配置信息
                if (Columns.Count() == 0)
                {
                    Logger.Log("失败", "没有列配置信息");
                    return;
                }
                if (Columns.Count(c => !c.Ignore) == 0)
                {
                    Logger.Log("失败", "忽略了所有行");
                    return;
                }
                if (Columns.Count(c => !c.Ignore && string.IsNullOrEmpty(c.PropertyName)) > 0)
                {
                    Logger.Log("失败", "存在未被忽略, 但是无关联属性的列");
                    return;
                }
                // 将属性信息存入列项中, 方便调用
                foreach (Item item in Columns)
                {
                    if (!string.IsNullOrEmpty(item.PropertyName) && !item.Ignore)
                        item.PropertyInfo = SetterOwnerProperty[item.PropertyName];
                }

                List<object> list = new List<object>();
                int failedCount = 0;
                int index = 0;
                while (csv.Read())
                {
                    object obj = Activator.CreateInstance(TargetType);
                    foreach (Item item in Columns)
                    {
                        if (item.Ignore) continue;
                        try
                        {
                            item.PropertyInfo.SetValue(obj, csv.GetField(item.PropertyInfo.PropertyType, item.ColumnIndex));
                        }
                        catch (Exception ex)
                        {
                            Logger.Log("异常", $"尝试读取值以写入时发生异常: {ex.Message}");
                        }
                    }
                    Logger.Log("进度", $"读取到对象[{index}]: {obj}");
                    if (Importing != null)
                    {
                        if (!Importing.Invoke(obj, Logger))
                        {
                            failedCount++;
                        }
                    }
                    

                    list.Add(list);

                    index++;
                }
                Logger.Log("完成", $"总共读取到 {list.Count} 项{(Importing != null ? $"其中 {failedCount} 项执行失败" : "")}");
                ImportDone?.Invoke(list, Logger);
            }
        }



        #region 控件事件
        private void FileInput_OnFileSelected(string filename)
        {
            Logger.Log("设置", $"设置导入文件为: {filename}");
            ReadColumnHead(filename);
        }
        private void ImportButton_Click(object sender, EventArgs e)
        {
            ReadData(FileInput.FileName);
        }
        #endregion

        #endregion

        #region 关联表
        private void RelateTable_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var currentCell = RelateTable.CurrentCell;
            if (currentCell.RowIndex >= 0 && currentCell.RowIndex < RelateTable.RowCount
                && currentCell.OwningColumn.Name == PropertyNameColumn.Name)
            {
                // 设置当前单元格为Tag
                e.Control.Tag = currentCell;
                (e.Control as ComboBox).SelectedIndexChanged += new EventHandler(PropertyNameColumn_SelectedIndexChanged);
            }
        }

        private void PropertyNameColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            // 离开时移除选择事件
            comboBox.Leave += (s, args) =>
            {
                comboBox.SelectedIndexChanged -= new EventHandler(PropertyNameColumn_SelectedIndexChanged);
            };
            DataGridViewCell cell = (DataGridViewCell)comboBox.Tag;
            if (SetterOwnerProperty.ContainsKey(comboBox.Text)
                && RelateTable.Rows[cell.RowIndex].DataBoundItem is Item item)
            {
                item.PropertyType = Util.String.StringHelper.GetTypeString(SetterOwnerProperty[comboBox.Text].PropertyType);
                item.Ignore = false;
                // 重绘本行
                RelateTable.InvalidateRow(cell.RowIndex);
            }

        }

        public class Item
        {
            public int ColumnIndex { get; set; }
            /// <summary>
            /// 导入的列名
            /// </summary>
            public string ImportColumnName { get; set; }
            /// <summary>
            /// 属性名
            /// </summary>
            public string PropertyName { get; set; }
            /// <summary>
            /// 属性类型
            /// </summary>
            public string PropertyType { get; set; }
            /// <summary>
            /// 属性
            /// </summary>
            public PropertyInfo PropertyInfo { get; set; }
            /// <summary>
            /// 忽略本列
            /// </summary>
            public bool Ignore { get; set; }
        }
        #endregion

        #region 日志信息
        /// <summary>
        /// 设置一些默认的Log颜色
        /// </summary>
        protected virtual void SetLogColor()
        {
            LoggerBox.ColorSet.Add("完成", Color.DarkGreen);
            LoggerBox.ColorSet.Add("成功", Color.DarkGreen);
            LoggerBox.ColorSet.Add("进度", ForeColor);
            LoggerBox.ColorSet.Add("失败", Color.Red);
            LoggerBox.ColorSet.Add("异常", Color.Red);
            LoggerBox.ColorSet.Add("测试", Color.Blue);
        }


        public class LogController
        {
            public delegate void LogHandle(string title, string content);
            public event LogHandle LogBody;

            public void Log(string title, string content)
            {
                LogBody?.Invoke(title, content);
            }
        }
        #endregion

    }
}
