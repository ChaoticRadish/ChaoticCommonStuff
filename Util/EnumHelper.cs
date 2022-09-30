using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class EnumHelper
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class DescAttribute : System.Attribute
        {
            public const string NULL_VALUE_DEFAULT_TEXT = "无描述枚举";

            public DescAttribute(string desc)
            {
                Desc = string.IsNullOrEmpty(desc) ? NULL_VALUE_DEFAULT_TEXT : desc.Trim();
            }

            public string Desc { get; }
        }

        /// <summary>
        /// 取得枚举值的描述, 为空时返回枚举值的名字
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetDesc(this Enum enumObj)
        {
            Type type = enumObj.GetType();
            string enumName = Enum.GetName(type, enumObj);
            FieldInfo field = type.GetField(enumName);
            DescAttribute enumDesc
                = field.GetCustomAttribute<DescAttribute>();
            return enumDesc == null ? enumName : enumDesc.Desc;
        }

        /// <summary>
        /// 遍历输入的枚举类型中的每一个枚举值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="action"></param>
        public static void ForEach<TEnum>(Action<TEnum> action) where TEnum : Enum
        {
            if (action == null) return;
            foreach (var enumObj in Enum.GetValues(typeof(TEnum)))
            {
                action?.Invoke((TEnum)enumObj);
            }
        }
        /// <summary>
        /// 遍历输入的枚举类型中的每一个枚举值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="action"></param>
        public static void ForEach(Type type, Action<object> action)
        {
            if (action == null) return;
            if (!type.IsEnum) return;
            foreach (var enumObj in Enum.GetValues(type))
            {
                action?.Invoke(enumObj);
            }
        }
    }
}
