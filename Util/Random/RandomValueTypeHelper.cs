using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random
{
    public static class RandomValueTypeHelper
    {
        static RandomValueTypeHelper()
        {
            // 初始化常用范围
            CharCommonRangeUpperLetter = new char[26];
            CharCommonRangeLowerLetter = new char[26];
            CharCommonRangeLetter = new char[26 * 2];
            for (int i = 0; i < 26; i++)
            {
                CharCommonRangeUpperLetter[i] = (char)(i + 'A');
                CharCommonRangeLowerLetter[i] = (char)(i + 'a');
                CharCommonRangeLetter[2 * i] = (char)(i + 'A');
                CharCommonRangeLetter[2 * i + 1] = (char)(i + 'a');
            }
            CharCommonRangeNumber = new char[10];
            for (int i = 0; i < 10; i++)
            {
                CharCommonRangeNumber[i] = (char)(i + '0');
            }
        }
        #region 常用范围
        /// <summary>
        /// 大写英文字母
        /// </summary>
        public static char[] CharCommonRangeUpperLetter;
        /// <summary>
        /// 小写英文字母
        /// </summary>
        public static char[] CharCommonRangeLowerLetter;
        /// <summary>
        /// 大小写英文字母
        /// </summary>
        public static char[] CharCommonRangeLetter;
        /// <summary>
        /// 数字
        /// </summary>
        public static char[] CharCommonRangeNumber;

        #endregion
        /// <summary>
        /// 随机字符
        /// </summary>
        /// <param name="random"></param>
        /// <param name="charRanges"></param>
        /// <returns></returns>
        public static char RandomChar(System.Random random, params IList<char>[] charRanges)
        {
            char[] range;
            int total;
            switch (charRanges.Length)
            {
                case 0:
                    throw new ArgumentException("没有输入char的取值范围", nameof(charRanges));
                case 1:
                    total = charRanges[0].Count;
                    range = charRanges[0].ToArray();
                    break;
                default:
                    total = charRanges.Sum(i => i.Count);
                    range = new char[total];
                    int index = 0;
                    foreach (IList<char> charRange in charRanges)
                    {
                        foreach (char c in charRange)
                        {
                            range[index++] = c;
                        }
                    }
                    break;

            }
            if (total == 0)
                throw new ArgumentException("输入的char取值范围是空的", nameof(charRanges));
            return range[random.Next(total)];
        }

        /// <summary>
        /// 随机布尔值
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static bool RandomBool(System.Random random)
        {
            return random.Next(0, 2) == 0;
        }
        /// <summary>
        /// 按指定的概率返回true
        /// </summary>
        /// <param name="random"></param>
        /// <param name="probability"></param>
        /// <returns></returns>
        public static bool RandomTrue(System.Random random, double probability = 0.25)
        {
            if (random == null) return RandomTrue(probability);
            else return random.NextDouble() < probability;
        }
        /// <summary>
        /// 按指定的概率返回true
        /// </summary>
        /// <param name="probability"></param>
        /// <returns></returns>
        public static bool RandomTrue(double probability = 0.25)
        {
            return new System.Random().NextDouble() < probability;
        }

        /// <summary>
        /// 指定区间内的整数
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static short RandomShort(System.Random random, short min, short max)
        {
            if (min > max)
            {
                short temp = max;
                max = min;
                min = temp;
            }

            return (short)(min == max ? min : random.Next(min, max));
        }
        /// <summary>
        /// 指定区间内的整数
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomInt(System.Random random, int min, int max)
        {
            if (min > max)
            {
                int temp = max;
                max = min;
                min = temp;
            }

            return min == max ? min : random.Next(min, max);
        }
        /// <summary>
        /// 指定区间内的非负整数
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomNonnegativeInt(System.Random random, int min, int max)
        {
            if (min < 0)
            {
                min = 0;
            }
            if (max < 0)
            {
                max = 0;
            }
            if (min > max)
            {
                int temp = max;
                max = min;
                min = temp;
            }

            return min == max ? min : random.Next(min, max);
        }

        /// <summary>
        /// 指定区间内的long
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long RandomLong(System.Random random, long min, long max)
        {
            return (long)RandomDouble(random, min, max);
        }
        /// <summary>
        /// 指定区间内的double
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double RandomDouble(System.Random random, double min, double max)
        {
            if (min > max)
            {
                double temp = max;
                max = min;
                min = temp;
            }

            return min == max ? min : random.NextDouble() * (max - min) + min;
        }
        /// <summary>
        /// 指定区间内的float
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float RandomFloat(System.Random random, float min, float max)
        {
            if (min > max)
            {
                float temp = max;
                max = min;
                min = temp;
            }

            return min == max ? min : (float)random.NextDouble() * (max - min) + min;
        }
    }
}
