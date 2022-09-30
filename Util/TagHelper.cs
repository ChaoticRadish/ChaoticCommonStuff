using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    /// <summary>
    /// 标签帮助类
    /// </summary>
    public static class TagHelper
    {
        #region 比较
        #region 常数

        /// <summary>
        /// 一致时的匹配度
        /// </summary>
        public const int SAME_MATCH_VALUE = 8;
        /// <summary>
        /// 是首部的匹配度
        /// </summary>
        public const int START_MATCH_VALUE = 5;
        /// <summary>
        /// 是尾部的匹配度
        /// </summary>
        public const int END_MATCH_VALUE = 4;
        /// <summary>
        /// 包含的匹配度
        /// </summary>
        public const int CONATIN_MATCH_VALUE = 2;
        /// <summary>
        /// 包含的匹配度
        /// </summary>
        public const int BE_CONATIN_MATCH_VALUE = 1;

        /// <summary>
        /// 默认的高匹配度的值
        /// </summary>
        public const int DEFAULT_BIG_MATCH_VALUE = 100;
        /// <summary>
        /// 默认的无匹配度的值
        /// </summary>
        public const int DEFAULT_NOT_MATCH_VALUE = 0;
        #endregion

        #region 并集比较, 拥有的标签只要有一项与输入的标签列表中的一项匹配即可
        /// <summary>
        /// 取得拥有的标签列表与输入标签列表的总匹配度
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="inputTags"></param>
        /// <returns></returns>
        public static int GetTotalFuzzyUnionMatchValue(IEnumerable<string> tags, IEnumerable<string> inputTags)
        {
            return tags.Sum(tag => GetFuzzyTagMatchValue(tag, inputTags));
        }

        /// <summary>
        /// 取得拥有的标签列表与输入标签列表的总匹配度
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="inputTags"></param>
        /// <returns></returns>
        public static int GetTotalUnionMatchValue(IEnumerable<string> tags, IEnumerable<string> inputTags)
        {
            return tags.Sum(tag => GetTagMatchValue(tag, inputTags));
        }


        #endregion

        #region 交集比较, 输入的每一个标签必须与拥有的标签的其中至少一项匹配
        /// <summary>
        /// 取得输入标签列表与拥有标签列表的交集模糊匹配度, 如果输入的其中一个标签匹配度为零, 则输入列表与拥有列表不相交
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="inputTags"></param>
        /// <returns></returns>
        public static int GetTotalFuzzyIntersectMatchValue(IEnumerable<string> tags, IEnumerable<string> inputTags)
        {
            if (inputTags.Count() == 0)
            {
                return DEFAULT_BIG_MATCH_VALUE;
            }
            int output = DEFAULT_NOT_MATCH_VALUE;
            foreach (string inputTag in inputTags)
            {
                // 匹配度
                int matchValue = GetFuzzyTagMatchValue(inputTag, tags);
                if (matchValue == DEFAULT_NOT_MATCH_VALUE)
                {// 存在不匹配项
                    return DEFAULT_NOT_MATCH_VALUE;
                }
                else
                {
                    output += matchValue;
                }
            }
            return output;
        }
        /// <summary>
        /// 取得输入标签列表与拥有标签列表的交集匹配度, 如果输入的其中一个标签匹配度为零, 则输入列表与拥有列表不相交
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="inputTags"></param>
        /// <returns></returns>
        public static int GetTotalIntersectMatchValue(IEnumerable<string> tags, IEnumerable<string> inputTags)
        {
            if (inputTags.Count() == 0)
            {
                return DEFAULT_BIG_MATCH_VALUE;
            }
            int output = DEFAULT_NOT_MATCH_VALUE;
            foreach (string inputTag in inputTags)
            {
                if (!tags.Contains(inputTag))
                {// 输入的标签有一项不存在于已有标签
                    return DEFAULT_NOT_MATCH_VALUE;
                }
                else
                {
                    output += SAME_MATCH_VALUE;
                }
            }
            return output;
        }

        #endregion

        #region 原子比较

        /// <summary>
        /// 获取标签与输入标签列表的模糊匹配度
        /// </summary>
        /// <param name="tag">需要比较的源</param>
        /// <param name="inputTags">用于与源比较的列表</param>
        /// <returns></returns>
        public static int GetFuzzyTagMatchValue(string tag, IEnumerable<string> inputTags)
        {
            int matchValue = 0;
            var lTag = tag.ToLower();
            var lInputs = inputTags.Select(x => x.ToLower());
            foreach (string inputTag in lInputs)
            {
                if (lTag.Equals(inputTag))
                {
                    matchValue += SAME_MATCH_VALUE;
                }
                else if (inputTag.StartsWith(lTag))
                {
                    matchValue += START_MATCH_VALUE;
                }
                else if (inputTag.EndsWith(lTag))
                {
                    matchValue += END_MATCH_VALUE;
                }
                else if (inputTag.Contains(lTag))
                {
                    matchValue += CONATIN_MATCH_VALUE;
                }
                else if (lTag.Contains(inputTag))
                {
                    matchValue += BE_CONATIN_MATCH_VALUE;
                }
            }
            return matchValue;
        }

        /// <summary>
        /// 获取拥有与输入标签列表的精准匹配度
        /// </summary>
        /// <param name="tag">需要比较的源</param>
        /// <param name="inputTags">用于与源比较的列表</param>
        /// <returns></returns>
        public static int GetTagMatchValue(string tag, IEnumerable<string> inputTags)
        {
            int matchValue = 0;
            var lTag = tag.ToLower();
            var lInputs = inputTags.Select(x => x.ToLower());
            foreach (string inputTag in lInputs)
            {
                if (lTag.Equals(inputTag))
                {
                    matchValue += SAME_MATCH_VALUE;
                }
            }
            return matchValue;
        }
        #endregion

        #endregion
    }
}
