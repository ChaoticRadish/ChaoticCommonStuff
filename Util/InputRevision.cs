using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    /// <summary>
    /// 输入修正
    /// </summary>
    public static class InputRevision
    {
        #region 布尔值
        /// <summary>
        /// True值字符串
        /// </summary>
        public readonly static List<string> TrueStrings = new List<string>
        {
            "是", "是的", "正确", "没错", "1", "true", "ture", "y", "yes", "t",
        };
        /// <summary>
        /// False值字符串
        /// </summary>
        public readonly static List<string> FalseStrings = new List<string>
        {
            "不", "不是", "错误", "0", "false", "flase", "fales", "n", "f", "no", "nope", "",
        };
        /// <summary>
        /// 判断输入的值是否为True值的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsTrueString(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            if (TrueStrings.Contains(input.Trim().ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion



        #region 范围限制
        /// <summary>
        /// 将数值修正到指定区间内[start, end]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int ToRange(ref int input, int start = 0, int end = int.MaxValue)
        {
            if (input < start)
            {
                input = start;
            }
            else if (input > end)
            {
                input = end;
            }
            return input;
        }
        /// <summary>
        /// 取得将数值修正到指定区间内[start, end]后的值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int ToRange(int input, int start = 0, int end = int.MaxValue)
        {
            if (input < start)
            {
                input = start;
            }
            else if (input > end)
            {
                input = end;
            }
            return input;
        }

        /// <summary>
        /// 将数值修正到指定区间内[start, end]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static long ToRange(ref long input, long start = 0, long end = long.MaxValue)
        {
            if (input < start)
            {
                input = start;
            }
            else if (input > end)
            {
                input = end;
            }
            return input;
        }

        #endregion


        /// <summary>
        /// 将字符串截取到指定长度, 如果截取了, 则使用fill字符串补充(仅在截取长度大于填充长度时生效)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <param name="fill"></param>
        /// <returns></returns>
        public static string ToLength(ref string input, int length, string fill = "...")
        {
            if (input != null)
            {
                if (input.Length > length && length > 0)
                {
                    if (fill != null && length > fill.Length)
                    {
                        input = $"{input.Substring(0, length - fill.Length)}{fill}";
                    }
                    else
                    {
                        input = input.Substring(0, length);
                    }
                }
                else if (length == 0)
                {
                    input = "";
                }
            }
            return input;
        }

        /// <summary>
        /// 将文本转换为int值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(string input, int defaultValue = 0)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (int.TryParse(input, out int output))
                {
                    return output;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 取得输入值中数值最大的值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static float Max(float v1, float v2, float v3)
        {
            if (v1 > v2)
            {
                if (v2 > v3)
                {
                    return v1;
                }
                else
                {
                    if (v1 > v3)
                    {
                        return v1;
                    }
                    else
                    {
                        return v3;
                    }
                }
            }
            else
            {
                if (v1 > v3)
                {
                    return v2;
                }
                else
                {
                    if (v2 > v3)
                    {
                        return v2;
                    }
                    else
                    {
                        return v3;
                    }
                }
            }
        }
        /// <summary>
        /// 取得输入值中数值最小的值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static float Min(float v1, float v2, float v3)
        {
            if (v1 < v2)
            {
                if (v2 < v3)
                {
                    return v1;
                }
                else
                {
                    if (v1 < v3)
                    {
                        return v1;
                    }
                    else
                    {
                        return v3;
                    }
                }
            }
            else
            {
                if (v1 < v3)
                {
                    return v2;
                }
                else
                {
                    if (v2 < v3)
                    {
                        return v2;
                    }
                    else
                    {
                        return v3;
                    }
                }
            }
        }

        #region 全角 & 半角
        /// <summary>
        /// 全角空格
        /// </summary>
        public const int SPACEBAR_FULL_WIDTH = 12288;
        /// <summary>
        /// 全角标点的最小值
        /// </summary>
        public const int MIN_FULL_WIDTH = 65281;
        /// <summary>
        /// 全角标点的最大值
        /// </summary>
        public const int MAX_FULL_WIDTH = 65374;
        /// <summary>
        /// 半角空格
        /// </summary>
        public const int SPACEBAR_HALF_WIDTH = 32;
        /// <summary>
        /// 半角标点的最小值
        /// </summary>
        public const int MIN_HALF_WIDTH = 33;
        /// <summary>
        /// 半角标点的最大值
        /// </summary>
        public const int MAX_HALF_WIDTH = 126;
        /// <summary>
        /// 半角 to 全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns>输入字符串转换后的副本</returns>
        public static string HalfWidth2FullWidth(string input)
        {
            if (input == null) return null;
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
            {
                char temp;
                if (c == SPACEBAR_HALF_WIDTH)
                {// 半角空格
                    temp = (char)SPACEBAR_FULL_WIDTH;
                }
                else if (IsHalfWidthNotSpaceBar(c))
                {// 属于其他半角标点符号
                    temp = (char)(c + (MIN_FULL_WIDTH - MIN_HALF_WIDTH));
                }
                else
                {// 其他
                    temp = c;
                }
                builder.Append(temp);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 全角 to 半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns>输入字符串转换后的副本</returns>
        public static string FullWidth2HalfWidth(string input)
        {
            if (input == null) return null;
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
            {
                char temp;
                if (c == SPACEBAR_FULL_WIDTH)
                {// 全角空格
                    temp = (char)SPACEBAR_HALF_WIDTH;
                }
                else if (IsFullWidthNotSpaceBar(c))
                {// 属于其他全角标点符号
                    temp = (char)(c - (MIN_FULL_WIDTH - MIN_HALF_WIDTH));
                }
                else
                {// 其他
                    temp = c;
                }
                builder.Append(temp);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 检查输入字符是不是全角字符
        /// </summary>
        /// <returns></returns>
        public static bool IsFullWidth(char c)
        {
            return c == SPACEBAR_FULL_WIDTH || c >= MIN_FULL_WIDTH && c <= MAX_FULL_WIDTH;
        }
        /// <summary>
        /// 检查输入字符是不是全角空格外的全角字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsFullWidthNotSpaceBar(char c)
        {
            return c >= MIN_FULL_WIDTH && c <= MAX_FULL_WIDTH;
        }
        /// <summary>
        /// 检查输入字符是不是半角空格外的半角字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsHalfWidthNotSpaceBar(char c)
        {
            return c >= MIN_HALF_WIDTH && c <= MAX_HALF_WIDTH;
        }
        #endregion

        #region 字母大小写
        /// <summary>
        /// 大写字母 to 小写字母
        /// </summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public static char Upper2Lower(char C)
        {
            if (
                C >= 'A' && C <= 'Z'
                || C >= 'A' + (MAX_FULL_WIDTH - MAX_HALF_WIDTH) && C <= 'Z' + (MAX_FULL_WIDTH - MAX_HALF_WIDTH))
            {// 全角或者半角的大写字母
                return (char)(C - 'A' + 'a');    // C - c == A - a 
            }
            return C;
        }
        /// <summary>
        /// 小写字母 to 大写字母
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char Lower2Upper(char c)
        {
            if (
                c >= 'a' && c <= 'z'
                || c >= 'a' + (MAX_FULL_WIDTH - MAX_HALF_WIDTH) && c <= 'z' + (MAX_FULL_WIDTH - MAX_HALF_WIDTH))
            {// 全角或者半角的小写字母
                return (char)('A' - 'a' + c);    // C - c == A - a 
            }
            return c;
        }
        #endregion

        #region 型号
        /// <summary>
        /// 去空格, 转型号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToModel(string input)
        {
            if (input == null) return null;
            StringBuilder builder = new StringBuilder();
            string halfInput = FullWidth2HalfWidth(input);
            foreach (char c in halfInput)
            {
                if (c == SPACEBAR_HALF_WIDTH)
                {// 空格
                    continue;
                }
                else if (c >= 'a' && c <= 'z')
                {// 小写转大写
                    builder.Append(Lower2Upper(c));
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
        #endregion

        #region 取整
        /// <summary>
        /// 根据步长向上取整
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="step">步长</param>
        /// <returns></returns>
        public static double CeilByStep(double input, double step)
        {
            if (input == 0)
            {
                return 0;
            }
            if (input % step == 0)
            {
                return input;
            }
            else
            {
                int count = (int)(input / step);
                if (input > 0)
                {
                    return (count + 1) * step;
                }
                else
                {
                    return (count - 1) * step;
                }
            }
        }
        #endregion

        #region 文件名

        /// <summary>
        /// 将输入字符串中的window系统文件名不可用字符替换为输入的字符
        /// </summary>
        /// <param name="input"></param>
        /// <param name="toChar"></param>
        /// <returns></returns>
        public static string ReplaceWindowIllegalChar(string input, char toChar = ' ')
        {
            if (string.IsNullOrEmpty(input)) return input;
            StringBuilder builder = new StringBuilder(input);
            builder.Replace('/', toChar);
            builder.Replace('\\', toChar);
            builder.Replace(':', toChar);
            builder.Replace('*', toChar);
            builder.Replace('?', toChar);
            builder.Replace('"', toChar);
            builder.Replace('<', toChar);
            builder.Replace('>', toChar);
            builder.Replace('|', toChar);
            return builder.ToString();
        }
        /// <summary>
        /// 将输入字符串中的window系统文件名不可用字符替换为输入的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="toString"></param>
        /// <returns></returns>
        public static string ReplaceWindowIllegalChar(string input, string toString = " ")
        {
            if (string.IsNullOrEmpty(input)) return input;
            StringBuilder builder = new StringBuilder(input);
            builder.Replace("/", toString);
            builder.Replace("\\", toString);
            builder.Replace(":", toString);
            builder.Replace("*", toString);
            builder.Replace("?", toString);
            builder.Replace("\"", toString);
            builder.Replace("<", toString);
            builder.Replace(">", toString);
            builder.Replace("|", toString);
            return builder.ToString();
        }
        #endregion

        #region 简称
        /// <summary>
        /// 取得公司简称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetShortNameAsCompany(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            string output = input;
            foreach (string suffix in CompanyStringSuffix)
            {
                int subPoint;
                if ((subPoint = output.IndexOf(suffix)) > 0)
                {
                    output = output.Substring(0, subPoint);
                }
            }
            return output;
        }
        /// <summary>
        /// 公司字符串后缀
        /// </summary>
        private static readonly string[] CompanyStringSuffix =
        {
            "公司",
            "厂",
            "制品厂",
            "包装厂",
            "营业部",
            "电子商务商行",
            "有限责任公司",
            "有限",
            "责任",
            "包装制品",
            "包装制品有限公司",
            "制品有限公司",
            "包装有限公司",
            "包装制品厂",
            "生物科技有限公司",
            "生物科技",
            "科技有限公司",
            "有限公司",
            "官方旗舰店",
            "旗舰店",
            "工作室",
            "贸易",
            "五金",
            "日用品",
            "工艺品",
            "美容用品",
            "美发用品",
            "化妆品",
            "材料",
            "制品",
            "包材",
            "包装品",
            "包装",
            "玻璃",
            "自营",
            "京东",
        };
        #endregion

        #region 合并多个输入
        /// <summary>
        /// 将输入的多个列表合并为一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static List<T> Union<T>(params IList<T>[] lists)
        {
            int total = lists.Sum(i => i.Count);
            List<T> output = new List<T>(total);
            foreach (IList<T> list in lists)
            {
                foreach (T t in list)
                {
                    output.Add(t);
                }
            }
            return output;
        }
        #endregion
    }
}
