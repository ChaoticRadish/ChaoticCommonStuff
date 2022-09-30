using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl
{
    /// <summary>
    /// 可初始化选项为枚举类型的ComboBox
    /// </summary>
    public partial class EnumComboBox : ComboBox
    {
        public EnumComboBox()
        {
            InitializeComponent();
            DropDownStyle = ComboBoxStyle.DropDownList;//下拉框样式设置为不能编辑
        }

        public string SelectedString
        {
            get
            {
                if (SelectedItem == null) return null;
                ItemData seletecd = (ItemData)SelectedItem;
                return seletecd.IsData ? seletecd.Str : null;
            }
        }
        public T SelectedEnum<T>(T defaultValue = default) where T : Enum
        {
            if (SelectedItem == null) return defaultValue;

            ItemData seletecd = (ItemData)SelectedItem;
            if (seletecd.IsData && seletecd.EnumObj.GetType().Equals(typeof(T)))
            {
                return (T)seletecd.EnumObj;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将枚举类型各值的名字设置为可选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="noSelectedItem">是否加入“未选择”项</param>
        public void InitAsName<T>(bool noSelectedItem = true) where T : Enum
        {
            List<ItemData> datas = new List<ItemData>();
            Type type = typeof(T);
            Array values = Enum.GetValues(type);
            foreach (object obj in values)
            {
                datas.Add(new ItemData()
                {
                    EnumObj = obj,
                    Str = Enum.GetName(type, obj)
                });
            }
            ResetItems(datas, noSelectedItem);
        }
        /// <summary>
        /// 将枚举类型各值的描述 (使用 <see cref="DescriptionAttribute"/> 标识的描述信息) 设置为可选项, 会跳过不含描述的项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="noSelectedItem">是否加入“未选择”项</param>
        public void InitAsDesc<T>(bool noSelectedItem = true) where T : Enum
        {
            List<ItemData> datas = new List<ItemData>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                var attr = field.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null)
                {
                    datas.Add(new ItemData()
                    {
                        EnumObj = field.GetValue(null),
                        Str = attr.Description
                    });
                }
            }
            ResetItems(datas, noSelectedItem);
        }

        /// <summary>
        /// 重设可选项
        /// </summary>
        /// <param name="items">可选项的枚举对象及其对应的字符串，注意不能重复</param>
        /// <param name="noSelectedItem"></param>
        private void ResetItems(List<ItemData> items, bool noSelectedItem = true)
        {
            Items.Clear();
            if (noSelectedItem)
            {
                Items.Add(ItemData.NoSelectedItem);
            }
            // 去重后添加进去
            Items.AddRange(
                items.Distinct()
                    .Select(i => (object)ItemData.NewData(i.EnumObj, i.Str))
                    .ToArray());
            if (Items.Count > 0)
                SelectedIndex = 0;
        }

        private struct ItemData
        {
            public bool IsData { get; set; }
            public string Str { get; set; }
            public object EnumObj { get; set; }

            public override string ToString()
            {
                return Str;
            }

            public override bool Equals(object obj)
            {
                if (obj is ItemData iObj)
                {
                    return Str.Equals(iObj.Str);
                }
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return Str.GetHashCode();
            }

            public static ItemData NewData(object obj, string str)
            {
                return new ItemData()
                {
                    IsData = true,
                    EnumObj = obj,
                    Str = str
                };
            }
            public static ItemData NoSelectedItem = new ItemData()
            {
                IsData = false,
                Str = "- 未选择 -"
            };
        }
    }
}
