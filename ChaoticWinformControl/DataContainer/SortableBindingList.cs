using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChaoticWinformControl.DataContainer
{
    /// <summary>
    /// 通用的可排序BindingList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSortedValue;
        ListSortDirection sortDirectionValue;
        PropertyDescriptor sortPropertyValue;
        public SortableBindingList() : base() { }
        public SortableBindingList(IList<T> list) : base(list) { }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            // 准备排序的属性
            Type sortType = prop.PropertyType;

            // 尝试获取关联的属性
            PropertyInfo relatedProperty = null;
            foreach (Attribute attribute in prop.Attributes)
            {
                if (attribute is RelatePropertyAttribute propertyAttribute)
                {
                    relatedProperty = typeof(T).GetProperty(propertyAttribute.PropertyName);
                }
            }
            // 尝试从属性的类型中取得 IComparable 接口
            Type interfaceType = sortType.GetInterface(nameof(IComparable));
            if (interfaceType == null && sortType.IsValueType)
            {
                Type underlyingType = Nullable.GetUnderlyingType(sortType);
                if (underlyingType != null)
                {
                    interfaceType = underlyingType.GetInterface(nameof(IComparable));
                }
            }
            if (interfaceType != null)
            {
                // 设置当前在排序的属性信息
                sortPropertyValue = prop;  
                sortDirectionValue = direction;
                // 准备查询的数据源
                IEnumerable<T> query = base.Items;
                // 查询
                if (direction == ListSortDirection.Ascending)   // 升序
                {
                    query = query.OrderBy(i => relatedProperty == null ? prop.GetValue(i) : relatedProperty.GetValue(i));
                }
                else    // 降序
                {
                    query = query.OrderByDescending(i => relatedProperty == null ? prop.GetValue(i) : relatedProperty.GetValue(i));
                }
                // 按照查询结果的顺序放入Items
                int newIndex = 0;
                foreach (object item in query)
                {
                    this.Items[newIndex] = (T)item;
                    newIndex++;
                }
                isSortedValue = true;
                sorting = true;
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                sorting = false;
            }
            else
            {
                OnNotSupportedException?.Invoke(
                    $"不支持属性: {prop.Name}{(relatedProperty == null ? "" : $"[关联属性: {relatedProperty.Name}]")} (排序类型: {sortType} ), 无法实例化为 IComparable 接口",
                    sortType);
                /*throw new NotSupportedException("Cannot sort by " + prop.Name +
                    ". This" + prop.PropertyType.ToString() +
                    " does not implement IComparable");*/
            }
        }
        /// <summary>
        /// 正在排序中
        /// </summary>
        bool sorting = false;

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyValue; }
        }
        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirectionValue; }
        }
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }
        protected override bool IsSortedCore
        {
            get { return isSortedValue; }
        }
        protected override void RemoveSortCore()
        {
            isSortedValue = false;
            sortPropertyValue = null;
        }
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (!sorting && sortPropertyValue != null)
                ApplySortCore(sortPropertyValue, sortDirectionValue);
            else
                base.OnListChanged(e);
        }

        /// <summary>
        /// 发生未支持类型的异常事件, 所使用的委托
        /// </summary>
        /// <param name="message"></param>
        /// <param name="notSupportedType"></param>
        public delegate void OnNotSupportedExceptionHandle(string message, Type notSupportedType);
        /// <summary>
        /// 发生未支持类型的异常事件
        /// </summary>
        public event OnNotSupportedExceptionHandle OnNotSupportedException;

        /// <summary>
        /// 将当前的数据内容清空并重设为输入的列表
        /// </summary>
        /// <param name="datas"></param>
        public void ResetAs(IEnumerable<T> datas)
        {
            Items.Clear();
            foreach (var data in datas)
            {
                Items.Add(data);
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }

    /// <summary>
    /// 关联的属性名, 用于指定使用另一个属性来排序
    /// </summary>
    public class RelatePropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }

        public RelatePropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
