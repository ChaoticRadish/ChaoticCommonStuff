using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Base
{
    public static class StringHelper
    {
        #region 相识度比较


        /// <summary>
        /// 一致时的相似度
        /// </summary>
        public const float SAME_SIMILARITY_VALUE = 7;
        /// <summary>
        /// 是首部的相似度
        /// </summary>
        public const float START_SIMILARITY_VALUE = 4;
        /// <summary>
        /// 是尾部的相似度
        /// </summary>
        public const float END_SIMILARITY_VALUE = 3;
        /// <summary>
        /// 包含的相似度
        /// </summary>
        public const float CONATIN_SIMILARITY_VALUE = 1;
        /// <summary>
        /// 单位长度的相似度差值
        /// </summary>
        public const float DETAIL_LENGTH_SIMILARITY_VALUE = 0.1f;

        /// <summary>
        /// 默认的高相似度的值
        /// </summary>
        public const float DEFAULT_BIG_SIMILARITY_VALUE = 100;
        /// <summary>
        /// 默认的无相似度的值
        /// </summary>
        public const float DEFAULT_NOT_SIMILARITY_VALUE = 0;


        /// <summary>
        /// 比较输入值与参照值的相似度
        /// </summary>
        /// <param name="input"></param>
        /// <param name="reference">参照值</param>
        /// <param name="sameValueScale">相等值的补正系数</param>
        /// <param name="lengthCheck">参照值</param>
        /// <returns>越大越相似</returns>
        public static float Similarity(string input, string reference, float sameValueScale = 1f, bool lengthCheck = true)
        {
            if ((string.IsNullOrEmpty(reference) && !string.IsNullOrEmpty(input))
                || (!string.IsNullOrEmpty(reference) && string.IsNullOrEmpty(input)))
            {// 一个是空的, 另一个不是
                return DEFAULT_NOT_SIMILARITY_VALUE;
            }
            float output = DEFAULT_NOT_SIMILARITY_VALUE;
            if (string.IsNullOrEmpty(reference) && string.IsNullOrEmpty(input))
            {// 两者均为空
                return SAME_SIMILARITY_VALUE;
            }
            else if (reference.Equals(input))
            {
                output = SAME_SIMILARITY_VALUE * sameValueScale;
            }
            else if (reference.StartsWith(input) || input.StartsWith(reference))
            {
                output = START_SIMILARITY_VALUE;
            }
            else if (reference.EndsWith(input) || input.EndsWith(reference))
            {
                output = END_SIMILARITY_VALUE;
            }
            else if(reference.Contains(input) || input.Contains(reference))
            {
                output = CONATIN_SIMILARITY_VALUE;
            }
            // 长度偏差
            if (lengthCheck && output > CONATIN_SIMILARITY_VALUE)
            {
                output += (float)Math.Pow(1 - DETAIL_LENGTH_SIMILARITY_VALUE, Math.Abs(input.Length - reference.Length));
            }

            return output;
        }
        /// <summary>
        /// 比较输入值与参照值的相似度
        /// </summary>
        /// <param name="input"></param>
        /// <param name="references">参照值列表</param>
        /// <param name="sameValueScale">相等值的补正系数</param>
        /// <param name="lengthCheck">参照值</param>
        /// <returns>越大越相似</returns>
        public static float Similarity(string input, IEnumerable<string> references, float sameValueScale = 1f, bool lengthCheck = true)
        {
            float output = 0;
            references = references.Where(s => !string.IsNullOrEmpty(s)).Distinct();
            foreach (string reference in references) 
            {
                output += Similarity(input, reference, sameValueScale, lengthCheck);
            }
            return output;
        }


        #endregion


        #region 长度控制
        /// <summary>
        /// 将文本转换为预览文本
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length">长度显示</param>
        /// <param name="endString">超出长度之后将多余的部分替换为此字符串</param>
        /// <returns></returns>
        public static string Preview(string input, int length = 50, string endString = "...")
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            // 输入长度值检查
            length = length < 0 ? 0 : length;
            endString = endString ?? "";

            if (input.Length > length)
            {
                if (endString.Length > length)
                {
                    return endString.Substring(0, length);
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(input, 0, length - endString.Length).Append(endString);
                    return builder.ToString();
                }
            }
            else
            {
                return input;
            }
        }
        #endregion


        #region 拼接
        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="split">分隔符</param>
        /// <param name="lastSplit">是否保留最后一个分隔符</param>
        /// <returns></returns>
        public static string Concat(IList<string> inputs, string split = "\n", bool lastSplit = false)
        {
            if (inputs == null) return null;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputs.Count; i++)
            {
                sb.Append(inputs[i]);
                if (i < inputs.Count - 1)
                {
                    sb.Append(split);
                }
                else if (lastSplit)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="split">分隔符</param>
        /// <param name="lastSplit">是否保留最后一个分隔符</param>
        /// <returns></returns>
        public static string Concat2(
            List<string> inputs, 
            string split = ", ", string left = "[", string right = "]", 
            bool lastSplit = false)
        {
            if (inputs == null) return null;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputs.Count; i++)
            {
                sb.Append(left).Append(inputs[i]).Append(right);
                if (i < inputs.Count - 1)
                {
                    sb.Append(split);
                }
                else if (lastSplit)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 分段
        /// <summary>
        /// 使用指定字符将输入字符串等距离得分段
        /// </summary>
        /// <param name="input"></param>
        /// <param name="distance"></param>
        /// <param name="split"></param>
        /// <returns>还是一个字符串, 总字符长度会更长一点</returns>
        public static string SectionSameDistance(string input, int distance = 5, char split = ' ')
        {
            if (string.IsNullOrEmpty(input)) return input;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                builder.Append(input[i]);
                if (i % distance == distance - 1)
                {
                    builder.Append(split);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 使用指定字符将输入字符串按检查字符出现次数分段
        /// </summary>
        /// <param name="input"></param>
        /// <param name="per">字符出现几次后分段</param>
        /// <param name="check">需要检查出现次数的字符</param>
        /// <param name="split">用于分段的字符</param>
        /// <returns></returns>
        public static string SectionSameChar(string input, int per = 2, char check = ' ', char split = '\n')
        {
            if (string.IsNullOrEmpty(input)) return input;
            StringBuilder builder = new StringBuilder();
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                builder.Append(input[i]);
                if (input[i] == check)
                {
                    count++;
                    if (count == per)
                    {
                        builder.Append(split);
                        count = 0;
                    }
                }
            }
            return builder.ToString();
        }
        #endregion

        #region 切分
        /// <summary>
        /// 分割字符串 分段
        /// </summary>
        /// <param name="input"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<string> Split(string input, char split = ' ')
        {
            return input.Split(split).Where(s => !string.IsNullOrEmpty(s)).ToList();
        }
        /// <summary>
        /// 使用输入的字符串切分
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splits">标志分割位的字符, 将会视为多个字符来使用, 而不是作为字符串使用</param>
        /// <param name="dosomething"></param>
        /// <returns></returns>
        public static List<string> SplitMultiChar(
            string input, 
            string splits = " ,;，。\n\t:",
            Func<string, string> dosomething = null)
        {
            IEnumerable<string> strArr = input.Split(splits.ToArray());
            if (dosomething != null)
            {
                strArr = strArr.Select(s => dosomething.Invoke(s));
            }
            return strArr.Where(s => !string.IsNullOrEmpty(s)).ToList();
        }
        #endregion
        #region 换行

        /// <summary>
        /// 每隔一定间距, 插入一个换行符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lineWidth"></param>
        /// <returns></returns>
        public static string SplitLine(string str, int lineWidth = 60)
        {
            if (str.Length > lineWidth)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < str.Length; i++)
                {
                    sb.Append(str[i]);
                    if ((i + 1) % 50 == 0 && i != str.Length - 1)
                    {
                        sb.AppendLine();
                    }
                }
                str = sb.ToString();
            }
            return str;
        }

        #endregion
        #region 类型字符串
        /// <summary>
        /// 取得类型字符串, 会对一些特殊的类型做一点处理, 比如Nullable<>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeString(Type type)
        {
            string output = type.FullName;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                output = $"Nullable<{type.GetGenericArguments()[0].FullName}>";
            }
            return output;
        }
        #endregion

        #region 数值范围
        /// <summary>
        /// 获取输入的最小值最大值构成的数值范围字符串, 包含两端. 值为null时代表不限
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static string RangeString(object min, object max)
        {
            return (min == null ? "( -∞ " : $"[ {min} ")
                + ", " +
                (max == null ? " +∞ )" : $"{max} ]");
        }
        #endregion


        /// <summary>
        /// 默认的结束字符数组
        /// </summary>
         public static char[] DefaultEndChars = new char[]
         {
             (char)0x00,
             (char)0x0D,
             (char)0x0A,
         };
    }
}
