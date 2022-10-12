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
using Util;
using Util.String;

namespace ChaoticWinformControl
{
    public partial class TypeComboBox : ComboBox
    {
        #region 属性
        /// <summary>
        /// 默认加入 "未选择" 选项
        /// </summary>
        [Category("自定义属性"), Description("过滤的类型范围")]
        public bool DefaultUseNone { get; set; } = true;
        /// <summary>
        /// 过滤的类型范围
        /// </summary>
        [Category("自定义属性"), Description("过滤的类型范围")]
        public TypeClassifyEnum TypeClassify
        {
            get
            {
                return typeClassify;
            }
            set
            {
                typeClassify = value;
                if (DefaultUseNone && !typeClassify.HasFlag(TypeClassifyEnum.None))
                {
                    typeClassify |= TypeClassifyEnum.None;
                }
                RefreshShowing();
            }
        }
        private TypeClassifyEnum typeClassify = TypeClassifyEnum.All | TypeClassifyEnum.None;

        /// <summary>
        /// 类型的分类
        /// </summary>
        [Flags]
        public enum TypeClassifyEnum : int
        {
            None = 0x1,
            Enum = 0x2,
            Abstract = 0x4,
            Array = 0x08,
            ValueType = 0x10,
            /// <summary>
            /// 泛型类型
            /// </summary>
            GenericType = 0x20,
            /// <summary>
            /// 可以用来构造其他泛型类型的泛型定义
            /// </summary>
            GenericTypeDefinition = 0x40,
            Interface = 0x80,
            Class = 0x100,
            All = 0x200,

            /// <summary>
            /// 过滤条件, 非嵌套类
            /// </summary>
            NoNested = 0x2000,
            /// <summary>
            /// 过滤条件, 提供无参构造函数
            /// </summary>
            HasEmptyArgConstructor = 0x4000,
        }
        #endregion
        #region 数据
        /// <summary>
        /// 类型项
        /// </summary>
        public List<Item> TypeItems { get; set; } = new List<Item>();
        /// <summary>
        /// 类型源
        /// </summary>
        public Assembly TypeSource { get; set; }

        /// <summary>
        /// 当前选中的类型
        /// </summary>
        public Type SelectedType
        {
            get
            {
                if (SelectedItem != null && SelectedItem is Item item)
                {
                    return item.Type;
                }
                return null;
            }
        }
        #endregion
        public TypeComboBox()
        {
            InitializeComponent();
            DropDownStyle = ComboBoxStyle.DropDownList;//下拉框样式设置为不能编辑
            
        }

        #region 暴露给外面的控制方法
        /// <summary>
        /// 设置程序集
        /// </summary>
        /// <param name="assembly"></param>
        public void SetAssembly(Assembly assembly)
        {
            TypeSource = assembly;
        }
        #endregion

        #region 显示
        /// <summary>
        /// 刷新正在显示的
        /// </summary>
        public void RefreshShowing()
        {
            Items.Clear();

            if (typeClassify.HasFlag(TypeClassifyEnum.None))
            {
                Items.Add(new Item() { Type = null });
            }

            EnumHelper.ForEach<TypeClassifyEnum>((e) =>
            {
                if (typeClassify.HasFlag(e))
                {
                    Console.WriteLine($"HasFlag {e} ");
                }
            });

            if (TypeSource != null)
            {
                Type[] types = TypeSource.GetTypes();
                foreach (Type t in types)
                {
                    if (typeClassify.HasFlag(TypeClassifyEnum.NoNested) && t.IsNested)
                    {// 类型是嵌套类, 但是没被允许
                        continue;
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.HasEmptyArgConstructor) && t.GetConstructor(Type.EmptyTypes) == null)
                    {// 需要有无参构造函数, 但是实际没有
                        continue;
                    }
                    
                    if (typeClassify.HasFlag(TypeClassifyEnum.Enum) && t.IsEnum)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if(typeClassify.HasFlag(TypeClassifyEnum.Abstract) && t.IsAbstract)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.Array) && t.IsArray)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.ValueType) && t.IsValueType)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.GenericType) && t.IsGenericType)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.GenericTypeDefinition) && t.IsGenericTypeDefinition)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.Interface) && t.IsInterface)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.Class) && t.IsClass)
                    {
                        Items.Add(new Item() { Type = t });
                    }
                    else if (typeClassify.HasFlag(TypeClassifyEnum.All))
                    {// 所有类型都可以
                        Items.Add(new Item() { Type = t });
                    }
                }
            }
        }

        public struct Item
        {
            public Type Type { get; set; }
            public override string ToString()
            {
                return Type == null ? " - 未选择 - " : StringHelper.GetTypeString(Type); 
            }
        }
        #endregion
    }
}
