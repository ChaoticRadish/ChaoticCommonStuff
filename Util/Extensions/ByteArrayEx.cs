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
        public static Comparison<byte[]> DefaultComparison = new Comparison<byte[]>((b1, b2) =>
                    {
                        int min = Math.Min(b1.Length, b2.Length);
                        int i = 0;
                        for (; i < min; i++)
                        {
                            if (b1[i] > b2[i])
                            {
                                return 1;
                            }
                            else if (b1[i] < b2[i])
                            {
                                return -1;
                            }
                        }
                        if (b1.Length > b2.Length)
                        {
                            return 1;
                        }
                        else if (b1.Length < b2.Length)
                        {
                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    });

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
        /// 截取从索引0开始的指定数量的元素
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static byte[] Sub(this byte[] bs, int count)
        {
            return Sub(bs, 0, count);
        }
        /// <summary>
        /// 截取从指定索引开始的指定数量的元素
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static byte[] Sub(this byte[] bs, int startIndex, int count)
        {
            int min = Math.Min(startIndex + count, bs.Length);
            byte[] output = new byte[count];
            for (int i = startIndex; i < min; i++)
            {
                output[i - startIndex] = bs[i];
            }
            return output;
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
        /// 将输入的所有字节追加到列表末端
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="inputs"></param>
        public static void Add(this IList<byte> bs, params byte[] inputs)
        {
            if (inputs != null)
            {
                foreach (byte b in inputs)
                {
                    bs.Add(b);
                }
            }    
        }
        /// <summary>
        /// 追加指定数量的指定字节到列表末端
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="b"></param>
        /// <param name="count"></param>
        public static void Add(this IList<byte> bs, byte b, int count)
        {
            for (int i = 0; i < count; i++)
            {
                bs.Add(b);
            }
        }

        /// <summary>
        /// 比较两个字节数组内的数据是否一致
        /// </summary>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        public static bool IsSame(this byte[] bs1, byte[] bs2)
        {
            if ((bs1 == null) && (bs2 == null)) return true;
            if (((bs1 == null) && (bs2 != null)) || ((bs1 != null) && (bs2 == null))) return false;
            if (bs1.Length != bs2.Length) return false;
            for (int i = 0; i < bs1.Length; i++)
            {
                if (bs1[i] != bs2[i]) return false;
            }
            return true;
        }
        /// <summary>
        /// 获取最后的不为0x00的字符为起点处的第n个字符 (往前数, 0表示该不为0x00的字符)
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="index">获取最后第几个</param>
        /// <returns>超过数组范围时返回null</returns>
        public static byte? LastNo0x00(this byte[] bs, int index = 0)
        {
            if (bs == null || bs.Length == 0) return null;
            int lastNo0x00Index = -1;
            for (int i = 0; i < bs.Length; i++)
            {
                int tempIndex = bs.Length - 1 - i;
                byte b = bs[tempIndex];
                if (b != 0x00)
                {
                    lastNo0x00Index = tempIndex;
                    break;
                }
            }
            //if (lastNo0x00Index < 0) return null;
            if (lastNo0x00Index - index >= 0)
            {
                return bs[lastNo0x00Index - index];
            }
            return null;
        }
    }
}
