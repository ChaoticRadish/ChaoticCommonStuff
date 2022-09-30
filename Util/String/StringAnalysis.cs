using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.String
{
    /// <summary>
    /// 字符串解析工具
    /// </summary>
    public static class StringAnalysis
    {
        /// <summary>
        /// 根据字符对解析字符串
        /// </summary>
        /// <param name="str">要解析的字符串</param>
        /// <param name="normalCharAction">一般字符的操作</param>
        /// <param name="inPairNormalAction">在字符对内的字符串的操作</param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void AnalysisForPair(
            IEnumerable<char> str, 
            Action<char> normalCharAction, 
            Action<string> inPairNormalAction,
            char left = '{', char right = '}')
        {
            if (str == null || str.Count() == 0 || left == right)
            {
                return;
            }
            // 容器
            StringBuilder builder = new StringBuilder();
            // 是否正在字符对中
            bool innerPair = false;
            foreach(char temp in str)
            {
                if (innerPair)
                {// 在字符对之内
                    if (temp == right)
                    {// 是字符对之右
                        inPairNormalAction?.Invoke(builder.ToString());
                        innerPair = false;
                    }
                    else
                    {
                        builder.Append(temp);
                    }
                }
                else
                {// 在字符对之外
                    if (temp == left)
                    {// 是字符对之左
                        builder.Clear();
                        innerPair = true;
                    }
                    else
                    {
                        normalCharAction?.Invoke(temp);
                    }
                }
            }
            // 读取结束
            if (innerPair)
            {
                inPairNormalAction?.Invoke(builder.ToString());
                innerPair = false;
            }
        }

        /// <summary>
        /// 根据字符对解析字符串并输出为字符串
        /// </summary>
        /// <param name="str">要解析的字符串</param>
        /// <param name="pairConvert">字符对内的字符串的解析方法</param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string AnalysisToStringForPair(
            IEnumerable<char> str, Func<string, string> pairConvert,
            char left = '{', char right = '}')
        {
            StringBuilder builder = new StringBuilder();
            AnalysisForPair(
                str,
                (c) =>
                {
                    builder.Append(c);
                },
                (s) =>
                {
                    builder.Append(pairConvert?.Invoke(s));
                }, left, right);
            return builder.ToString();
        }


        #region 纸箱字符串
        /// <summary>
        /// 纸箱标识词语
        /// </summary>
        private static List<string> PaperboxFlagWords = new List<string>()
        {
            "打钉", "黏胶", "粘胶", "特殊印刷", "内箱", "外箱", "纸箱", "供应商纸箱", "特殊印刷", "纸板"
        };

        /// <summary>
        /// 纸箱字符串信息解析
        /// </summary>
        /// <returns></returns>
        public static (float length, float width, float height, string specialInfo, string otherInfo)? PaperboxStringAnalysis(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            float length = 0;
            float width = 0;
            float height = 0;
            string sInfo;
            string oInfo;

            // 取得前三个数字
            List<float> floats = SubFloats(str, "*xX✖× ", out _);
            length = floats.Count >= 1 ? floats[0] : length;
            width = floats.Count >= 2 ? floats[1] : width;
            height = floats.Count >= 3 ? floats[2] : height;

            // 切割所有词语
            string[] words = str.Split(' ', ',');
            
            // 切割特殊印刷信息
            bool specialFlag = false;
            List<string> specialWords = new List<string>();
            foreach (string word in words)
            {
                if (word.Equals("特殊印刷"))
                {
                    specialFlag = true;
                }
                else if (specialFlag)
                {
                    string temp = word.Trim();
                    if (PaperboxFlagWords.Contains(temp))
                    {// 读取到这标识词时读取结束
                        break;
                    }
                    if (!string.IsNullOrEmpty(temp))
                    {
                        specialWords.Add(temp);
                    }
                }
            }
            StringBuilder specialStringBuilder = new StringBuilder();
            foreach (string word in specialWords)
            {
                specialStringBuilder.Append(word).Append(' ');
            }
            sInfo = specialStringBuilder.ToString().Trim();

            // 读取所有标识词
            List<string> flagWords = new List<string>();
            foreach (string word in words)
            {
                if (PaperboxFlagWords.Contains(word))
                {
                    flagWords.Add(word);
                }
            }
            StringBuilder otherStringBuilder = new StringBuilder();
            foreach (string word in flagWords)
            {
                otherStringBuilder.Append(word).Append(' ');
            }
            oInfo = otherStringBuilder.ToString().Trim();


            return (length, width, height, sInfo, oInfo);
        }

        #endregion

        #region 数量字符串
        /// <summary>
        /// 常用量词
        /// </summary>
        private static List<char> MeasureWords = new List<char>()
        {
            '个', '块', '只', '本', 
            '瓶', '杯', '双', '件',
            '张', '碗', '种', '匹',
            '头', '条', '位', '项',
            '名', '些', '家', '点',
            '场', '句', '段', '分',
            '处', '片', '套', '座',
            '部', '则', '层', '样',
            '群', '届', '支', '批',
            '篇', '番', '股', '首',
            '声', '颗', '组', '盏',
            '口', '把', '间', /*'笔',*/
            '所', '对', '根', '幅',
            '出', '道', '拨', '队',
            '堆', '阵', /*'面',*/ '台',
            '局', '棵', /*'盒',*/ '户',
            '栋', '节', '封', '班',
            '盘', '副', '趟', /*'款',*/
            '团', '束', '门', '架',
            '顿', '堵', '壶', '朵',
            '排', '份', '具', '包',
            '罐', '粒', '卷', '堂',
            '起', '枝', '株', '轮',
            '桌', '桶', '滴', '串',
            '列', '箱', '扇', '辆',
        };
        /// <summary>
        /// 取得字符中可能的数量信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float? GetPossibleCountValue(string str, string splitChars = "\n")
        {
            SubFloats(str, splitChars, out List<SubOriginal> originals);
            if (originals.Count == 0) return null;

            // 计算数字与量词的距离
            Dictionary<SubOriginal, int> map = new Dictionary<SubOriginal, int>();
            foreach (var original in originals)
            {
                int dist = 0;
                int index = original.LastCharIndex;
                bool existMeasure = false;
                while (index < str.Length)
                {
                    char c = str[index];
                    if (MeasureWords.Contains(c))
                    {
                        existMeasure = true;
                        map.Add(original, dist);
                        break;
                    }
                    switch (c)
                    {
                        case ' ':
                            break;
                        case '\t':
                        case '\n':
                            dist += 2;
                            break;
                        default:
                            dist++;
                            break;
                    }
                    index++;
                }
                if (!existMeasure)
                {
                    map.Add(original, str.Length);
                }
            }
            return map.OrderBy(i => i.Value).First().Key.Value;
        }
        #endregion


        #region 字符串中的数字获取
        /// <summary>
        /// 截取输入字符串中的第一个数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static float SubFirstFloat(string input, float defaultValue = 0, string filter = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return defaultValue;
            }
            input = InputRevision.ToModel(input);
            bool readEnd = false;
            bool existNumber = false;
            bool existMinus = false;
            bool existPoint = false;
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
            {
                if (!string.IsNullOrEmpty(filter) && filter.Contains(c))
                {
                    continue;
                }
                switch (c)
                {
                    case '-':
                        if (!existMinus)
                        {
                            existMinus = true;
                            builder.Append(c);
                        }
                        else
                        {
                            if (builder[builder.Length - 1] != c)
                            {
                                readEnd = true;
                            }
                        }
                        break;
                    case '.':
                        if (!existNumber)
                        {
                            existNumber = true;
                            builder.Append("0");
                        }
                        if (!existPoint)
                        {
                            existPoint = true;
                            builder.Append(c);
                        }
                        else
                        {
                            if (builder[builder.Length - 1] != c)
                            {
                                readEnd = true;
                            }
                        }
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        existNumber = true;
                        builder.Append(c);
                        break;
                    default:
                        if (existNumber || existMinus || existPoint)
                        {
                            readEnd = true;
                        }
                        break;
                }
                if (readEnd)
                {
                    break;
                }
            }
            if (float.TryParse(builder.ToString(), out float output))
            {
                return output;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 截取输入字符串中的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filter"></param>
        /// <param name="originals">float值与其对应的原文详情</param>
        /// <returns></returns>
        public static List<float> SubFloats(
            string input,
            string filter,
            out List<SubOriginal> originals)
        {
            List<float> output = new List<float>();
            originals = new List<SubOriginal>();
            if (string.IsNullOrEmpty(input))
            {
                return output;
            }
            input = InputRevision.ToModel(input);

            bool readEnd = false;
            bool existNumber = false;
            bool existMinus = false;
            bool existPoint = false;
            StringBuilder builder = new StringBuilder();
            StringBuilder originalBuilder = new StringBuilder();
            int index = 0;
            foreach (char c in input)
            {
                if (!string.IsNullOrEmpty(filter) && filter.Contains(c))
                {
                    readEnd = true;
                }
                else
                {
                    switch (c)
                    {
                        case '-':
                            if (!existMinus && !existNumber)
                            {
                                existMinus = true;
                                builder.Append(c);
                            }
                            else
                            {
                                if (builder[builder.Length - 1] != c)
                                {
                                    readEnd = true;
                                }
                            }
                            break;
                        case '.':
                            if (!existNumber)
                            {
                                existNumber = true;
                                builder.Append("0");
                            }
                            if (!existPoint)
                            {
                                existPoint = true;
                                builder.Append(c);
                            }
                            else
                            {
                                if (builder[builder.Length - 1] != c)
                                {
                                    readEnd = true;
                                }
                            }
                            break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            existNumber = true;
                            builder.Append(c);
                            break;
                        default:
                            if (existNumber || existMinus || existPoint)
                            {
                                readEnd = true;
                            }
                            break;
                    }
                }
                originalBuilder.Append(c);  // 所有字符均会被读取
                if (readEnd || index == input.Length - 1)
                {
                    if (float.TryParse(builder.ToString(), out float v))
                    {
                        // 当前字符串转换成功
                        output.Add(v);
                        originals.Add(new SubOriginal()
                        {
                            Value = v,
                            LastCharIndex = index,
                            Original = originalBuilder.ToString(),
                        });
                    }
                    readEnd = false;
                    existNumber = false;
                    existMinus = false;
                    existPoint = false;
                    builder.Clear();
                    originalBuilder.Clear();
                }
                index++;
            }
            return output;
        }
        public struct SubOriginal
        {
            public float Value { get; set; }
            public string Original { get; set; }
            public int LastCharIndex { get; set; }
        }
        #endregion

        #region 键值对
        public static KeyValuePair<string, string>? ReadAsKeyValuePair(string str, string splitChars = ":：-")
        {
            int index = -1;
            foreach (char c in splitChars)
            {
                index = str.IndexOf(c);
                if (index >= 0) break; 
            }

            if (index < 0) return null;
            string key = str.Substring(0, index);
            string value = str.Substring(index + 1);
            return new KeyValuePair<string, string>(key.Trim(), value.Trim());
        }
        #endregion
    }
}


