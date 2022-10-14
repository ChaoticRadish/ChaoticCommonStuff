using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Random
{
    public static class RandomValueTypeHelper
    {
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
