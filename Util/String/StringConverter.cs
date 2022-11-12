using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.String
{
    /// <summary>
    /// 字符串转换的帮助类
    /// </summary>
    public static class StringConverter
    {
        /// <summary>
        /// 尝试将输入的字符串转换为指定类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object Convert(string str, Type targetType)
        {
            return Convert(str, targetType, out _);
        }
        /// <summary>
        /// 尝试将输入的字符串转换为指定类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="targetType"></param>
        /// <param name="isSuccess">转换是否成功</param>
        /// <returns></returns>
        public static object Convert(string str, Type targetType, out bool isSuccess)
        {
            if (targetType == typeof(string))
            {
                isSuccess = true;
                return str;
            }
            else if (targetType.IsEnum)
            {
                try
                {
                    object enumObj = Enum.Parse(targetType, str);

                    isSuccess = true;
                    return enumObj;
                }
                catch { }
            }
            else if (targetType == typeof(bool))
            {
                if (bool.TryParse(str, out bool v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(char))
            {
                if (char.TryParse(str, out char v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(byte))
            {
                if (byte.TryParse(str, out byte v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(short))
            {
                if (short.TryParse(str, out short v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(ushort))
            {
                if (ushort.TryParse(str, out ushort v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(sbyte))
            {
                if (sbyte.TryParse(str, out sbyte v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(int))
            {
                if (int.TryParse(str, out int v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(uint))
            {
                if (uint.TryParse(str, out uint v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(long))
            {
                if (long.TryParse(str, out long v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(ulong))
            {
                if (ulong.TryParse(str, out ulong v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(float))
            {
                if (float.TryParse(str, out float v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(double))
            {
                if (double.TryParse(str, out double v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(decimal))
            {
                if (decimal.TryParse(str, out decimal v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            else if (targetType == typeof(DateTime))
            {
                if (DateTime.TryParse(str, out DateTime v))
                {
                    isSuccess = true;
                    return v;
                }
            }
            isSuccess = false;
            return null;
        }
    }
}
