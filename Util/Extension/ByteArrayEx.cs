
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    /// <summary>
    /// <see cref="byte[]"/> 类型的扩展方法
    /// </summary>
    public static class ByteArrayEx
    {
        /// <summary>
        /// 将byte[]转换为16进制的字符串, 大写, 域宽2位, 不足左侧填0
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bs)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bs)
            {
                builder.AppendFormat("{0:X2} ", b);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 将byte[]强制转换为字符数组
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static char[] ToChars(this byte[] bs)
        {
            char[] chars = new char[bs.Length];
            for (int i = 0; i < bs.Length; i++)
            {
                chars[i] = (char)bs[i];
            }
            return chars;
        }
        /// <summary>
        /// 将byte[]强制转换为字符数组, 再转换为字符串
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static string ToCharsToString(this byte[] bs)
        {
            return new string(ToChars(bs));
        }
        /// <summary>
        /// 使用指定的值清空数组
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="clearValue"></param>
        public static void Clear(this byte[] bs, byte clearValue = 0)
        {
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = clearValue;
            }
        }
    }
}
