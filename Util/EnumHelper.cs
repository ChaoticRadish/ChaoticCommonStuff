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
        /// 获取指定类型的所有枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetAllValues<T>() where T : Enum
        {
            Type enumType = typeof(T);
            string[] enumNames = Enum.GetNames(enumType);
            T[] output = new T[enumNames.Length];
            for (int i = 0; i < enumNames.Length; i++)
            {
                FieldInfo field = enumType.GetField(enumNames[i]);
                output[i] = (T)field.GetValue(null);
            }
            return output;
        }
        /// <summary>
        /// 取得枚举所有值的描述, 描述为空时返回枚举值的名字
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string[] GetAllDesc(this Enum enumObj)
        {
            return GetAllDesc(enumObj.GetType());
        }
        /// <summary>
        /// 取得枚举所有值的描述, 描述为空时返回枚举值的名字
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string[] GetAllDesc(Type enumType)
        {
            string[] enumNames = Enum.GetNames(enumType);
            string[] output = new string[enumNames.Length];
            for (int i = 0; i < enumNames.Length; i++)
            {
                FieldInfo field = enumType.GetField(enumNames[i]);
                DescAttribute enumDesc
                    = field.GetCustomAttribute<DescAttribute>();
                output[i] = enumDesc == null ? enumNames[i] : enumDesc.Desc;
            }
            return output;
        }
        /// <summary>
        /// 取得枚举值的描述, 为空时返回枚举值的名字
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetDesc(this Enum enumObj)
        {
            return GetDesc(enumObj, enumObj.GetType());
        }
        /// <summary>
        /// 取得枚举值的描述, 为空时返回枚举值的名字
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string GetDesc(object enumObj, Type type)
        {
            if (!type.IsEnum) return null;
            string enumName = Enum.GetName(type, enumObj);
            FieldInfo field = type.GetField(enumName);
            DescAttribute enumDesc
                = field.GetCustomAttribute<DescAttribute>();
            return enumDesc == null ? enumName : enumDesc.Desc;
        }
        /// <summary>
        /// 将字符串转换为枚举 (优先通过枚举名, 不匹配再通过描述)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T Convert<T>(string str)
            where T : Enum
        {
            Type type = typeof(T);
            object result = Convert(type, str);
            return result == null ? default : (T)result;
        }
        /// <summary>
        /// 将字符串转换为枚举 (优先通过枚举名, 不匹配再通过描述)
        /// </summary>
        /// <param name="str"></param>
        /// <returns>null时表示转换失败</returns>
        public static object Convert(Type type, string str)
        {
            string[] names = Enum.GetNames(type);

            List<FieldInfo> fields = new List<FieldInfo>();
            // 优先通过名字来判断
            foreach (string name in names)
            {
                FieldInfo field = type.GetField(name);
                fields.Add(field);
                if (name.Equals(str))
                {
                    return field.GetValue(null);
                }
            }
            // 没有匹配名字的再通过枚举来判断
            foreach (FieldInfo field in fields)
            {
                DescAttribute enumDesc = field.GetCustomAttribute<DescAttribute>();
                if (enumDesc != null && enumDesc.Desc.Equals(str))
                {
                    return field.GetValue(null);
                }

            }
            return null;
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
