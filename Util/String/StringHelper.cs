﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.String
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


        /// <summary>
        /// 比较输入值中, 哪一项与输入值的相识度最高
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputs"></param>
        /// <param name="reference"></param>
        /// <returns>Value1: 文本值, Value2: 相似度, Value3: 绑定对象</returns>
        public static Data.ThreeValueTuples<string, float, T>? MostSimilarity<T>(
            List<KeyValuePair<string, T>> inputs, string reference)
        {
            if (inputs == null || inputs.Count == 0) return null;

            Dictionary<string, float> similarityValue
                = new Dictionary<string, float>();
            foreach (KeyValuePair<string, T> input in inputs)
            {
                similarityValue.Add(input.Key, Similarity(input.Key, reference));
            }
            var most = similarityValue.ToList()
                .OrderByDescending(item => item.Value).Take(1).First();
            string s = most.Key;
            float v = most.Value;
            T obj = inputs.First(i => i.Key == s).Value;
            return new Data.ThreeValueTuples<string, float, T>(s, v, obj);
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
        public static string Concat(List<string> inputs, string split = "\n", bool lastSplit = false)
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

        #region 切分
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<string> Split(string input, char split = ' ')
        {
            return input.Split(split).Where(s => !string.IsNullOrEmpty(s)).ToList();
        }
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

    }
}
