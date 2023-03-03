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
using Util.Extension;
using Util;

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

            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #region 属性
        /// <summary>
        /// 未选择项的文本
        /// </summary>
        [Category("_自定义"), Description("未选择项的文本")]
        public string NotSelecedString { get; set; } = "- 未选择 -";
        #endregion

        /// <summary>
        /// 选择 "未选择" 项, 如果没有未选择项, 则清除选择
        /// </summary>
        public void ClearSelected()
        {
            ItemData? temp = null;
            foreach (ItemData item in Items)
            {
                if (!item.IsData)
                {
                    temp = item;
                    break;
                }
            }
            if (temp != null)
            {
                SelectedItem = temp;
            }
            else
            {
                SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 尝试选择枚举对象与输入对象相等的可选项
        /// </summary>
        /// <param name="obj"></param>
        public void TrySelect(object obj)
        {
            if (obj == null) return;
            ItemData? same = null;
            foreach (ItemData item in Items)
            {
                if (item.EnumObj != null && item.EnumObj.Equals(obj))
                {
                    same = item;
                    break;
                }
            }
            if (same != null)
            {
                SelectedItem = same.Value;
            }
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
        /// <summary>
        /// 获取当前选中的枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selected">未选择时返回默认值</param>
        /// <returns></returns>
        public bool GetSelectedEnum<T>(out T selected) where T : Enum
        {
            bool result = GetSelectedEnum(typeof(T), out object obj);
            selected = result ? (T)obj : default;
            return result;
        }
        /// <summary>
        /// 获取当前选中的枚举
        /// </summary>
        /// <param name="type"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public bool GetSelectedEnum(Type type, out object selected)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException($"输入类型 {type} 不是枚举", nameof(type));
            }

            if (SelectedItem == null)
            {
                selected = null;
                return false;
            }

            ItemData seletecd = (ItemData)SelectedItem;
            if (seletecd.IsData && seletecd.EnumObj.GetType().Equals(type))
            {
                selected = seletecd.EnumObj;
                return true;
            }
            else
            {
                selected = null;
                return false;
            }

        }

        /// <summary>
        /// 将枚举类型各值的名字设置为可选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="noSelectedItem">是否加入“未选择”项</param>
        public void InitAsName<T>(bool noSelectedItem = true) where T : Enum
        {
            InitAsName(typeof(T), noSelectedItem);
        }
        public void InitAsName(Type type, bool noSelectedItem = true)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException($"输入类型 {type} 不是枚举", nameof(type));
            }

            List<ItemData> datas = new List<ItemData>();
            Array values = Enum.GetValues(type);
            foreach (object obj in values)
            {
                datas.Add(new ItemData()
                {
                    EnumObj = obj,
                    Str = Enum.GetName(type, obj),
                    IsData = true,
                });
            }
            ResetItems(datas, noSelectedItem);
        }
        /// <summary>
        /// 将枚举类型各值的描述 (使用 <see cref="System.ComponentModel.DescriptionAttribute"/> 标识的描述信息) 设置为可选项, 默认会跳过不含描述的项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="noSelectedItem">是否加入“未选择”项</param>
        /// <param name="ignoreWithoutDesc">是否跳过不含描述的项<, 不跳过时使用字段名字/param>
        public void InitAsDesc<T>(bool noSelectedItem = true, bool ignoreWithoutDesc = true) where T : Enum
        {
            InitAsDesc(typeof(T), noSelectedItem, ignoreWithoutDesc);
        }
        public void InitAsDesc(Type type, bool noSelectedItem = true, bool ignoreWithoutDesc = true)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException($"输入类型 {type} 不是枚举", nameof(type));
            }

            List<ItemData> datas = new List<ItemData>();
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                var attr = field.GetCustomAttribute<EnumHelper.DescAttribute>();
                if (attr != null)
                {
                    datas.Add(new ItemData()
                    {
                        EnumObj = field.GetValue(null),
                        Str = attr.Desc,
                        IsData = true,
                    });
                }
                else if (!ignoreWithoutDesc)
                {
                    datas.Add(new ItemData()
                    {
                        EnumObj = field.GetValue(null),
                        Str = field.Name,
                        IsData = true,
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
                items.Insert(0, ItemData.NoSelectedItem(NotSelecedString.WhenEmptyDefault("- 未选择 -")));
            }
            // 去重后添加进去
            Items.AddRange(
                items.Distinct()
                    .Select(i => (object)i)
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
            public static ItemData NoSelectedItem(string text) => new ItemData
            {
                IsData = false,
                EnumObj = null,
                Str = text
            };
        }
    }
}
