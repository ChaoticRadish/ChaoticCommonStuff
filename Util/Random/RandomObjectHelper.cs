using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Util.Random.Attributes;

namespace Util.Random
{
    /// <summary>
    /// <para>本帮组类的制作情况: </para>
    /// <para>int属性: 完成</para>
    /// <para>bool属性: 完成</para>
    /// <para>array属性: 完成</para>
    /// <para>list属性: 完成</para>
    /// <para>double属性: 完成</para>
    /// <para>object属性: 完成</para>
    /// </summary>
    public static class RandomObjectHelper
    {
        #region 设置属性的值
        /// <summary>
        /// 为对象的指定属性赋随机值
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        public static void SetRandomValue(object obj, PropertyInfo propertyInfo, System.Random random = null)
        {
            random = random ?? new System.Random();

            if (obj == null || propertyInfo == null)
            {
                return;
            }
            if (propertyInfo.SetMethod == null)
            {
                // 属性未提供Setting
                return;
            }

            Type type = propertyInfo.PropertyType;
            bool isNullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isNullable)
            {
                // 可空类型
                type = Nullable.GetUnderlyingType(type);
            }

            // 前置的一些特性
            ProbabilityAttribute probability = propertyInfo.GetCustomAttribute<ProbabilityAttribute>();

            object propertyValue;
            if (isNullable && RandomValueTypeHelper.RandomTrue(random, probability == null ? 0.1 : probability.Null))
            {// 如果是可空类型, 按一定概率赋null值
                propertyValue = null;
            }
            else if (typeof(int) == type)
            {
                propertyValue = GetRandomIntValue(propertyInfo, random);
            }
            else if (typeof(bool) == type)
            {
                propertyValue = GetRandomBoolValue(propertyInfo, random);
            }
            else if (typeof(double) == type)
            {
                propertyValue = GetRandomDoubleValue(propertyInfo, random);
            }
            else if (typeof(float) == type)
            {
                propertyValue = GetRandomFloatValue(propertyInfo, random);
            }
            else if (typeof(long) == type)
            {
                propertyValue = GetRandomLongValue(propertyInfo, random);
            }
            else if (typeof(string) == type)
            {
                propertyValue = GetRandomStringValue(propertyInfo, random);
            }
            else if (typeof(DateTime) == type)
            {
                propertyValue = GetRandomDateTimeValue(propertyInfo, random);
            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                propertyValue = GetRandomListValue(propertyInfo, random);
            }
            else if (type.IsClass)
            {
                propertyValue = GetObject(type, random);
            }
            else
            {
                // 其他的未受支持的情况
                return;
            }

            propertyInfo.SetValue(obj, propertyValue, null);

        }

        #region 设置值的具体实现
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomIntValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            int min = 0;
            int max = 100;

            Attributes.IntRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomeInt(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomBoolValue(PropertyInfo propertyInfo, System.Random random)
        {
            ProbabilityAttribute probability = propertyInfo.GetCustomAttribute<ProbabilityAttribute>();
            return probability == null ? 
                RandomValueTypeHelper.RandomBool(random)
                :
                RandomValueTypeHelper.RandomTrue(random, probability.True);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomLongValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            long min = 0;
            long max = 100;

            Attributes.LongRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.LongRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomeLong(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomFloatValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            float min = 0;
            float max = 100;

            Attributes.FloatRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.FloatRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomeFloat(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomDoubleValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            double min = 0;
            double max = 100;

            Attributes.DoubleRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.DoubleRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomeDouble(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomStringValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            int min = 0;
            int max = 100;

            Attributes.IntRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            string output = RandomStringHelper.GetRandomEnglishString(
                RandomValueTypeHelper.RandomeInt(random, min, max),
                random);
            StringFormatAttribute format = propertyInfo.GetCustomAttribute<StringFormatAttribute>();
            if (format != null)
            {
                switch (format.Case)
                {
                    case StringFormatAttribute.CaseEnum.Upper:
                        output = output.ToUpper();
                        break;
                    case StringFormatAttribute.CaseEnum.Lower:
                        output = output.ToLower();
                        break;
                }
            }
            return output;
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomDateTimeValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            DateTime start = new DateTime(2000, 1, 1);
            int rangeDays = 3000;

            Attributes.DateTimeRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.DateTimeRangeAttribute>();
            if (range != null)
            {
                start = range.StartDate;
                rangeDays = range.RangeDays;
            }
            return start.AddDays(random.Next(rangeDays))
                        .AddMilliseconds(random.Next(24 * 60 * 60 * 1000));
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomListValue(PropertyInfo propertyInfo, System.Random random)
        {
            // 取值范围
            int min = 0;
            int max = 100;
            Attributes.IntRangeAttribute range = propertyInfo.GetCustomAttribute<Attributes.IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return GetList(propertyInfo.PropertyType, random, min, max);
        }

        #endregion

        #endregion*
        /// <summary>
        /// 取得随机的指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        public static object GetObject(Type type, System.Random random = null)
        {
            random = random ?? new System.Random();

            if (typeof(IList).IsAssignableFrom(type))
            {
                return GetList(type, random, 0, 100);
            }
            else
            {
                if (type.IsValueType)
                {
                    return Activator.CreateInstance(type);
                }
                else
                {
                    if (TypeHelper.ExistNonParamPublicConstructor(type))
                    {// 检查是否有无参构造函数
                        object output = Activator.CreateInstance(type);

                        foreach (PropertyInfo propertyInfo in type.GetProperties())
                        {// 遍历公共属性
                            SetRandomValue(output, propertyInfo, random);
                        }

                        return output;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 取得由随机指定类型的对象构成的列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="random"></param>
        /// <param name="minCount"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public static IList GetList(Type listType, System.Random random = null, int minCount = 0, int maxCount = 100)
        {
            if (!typeof(IList).IsAssignableFrom(listType))
            {
                return null;
            }

            random = random ?? new System.Random();

            // 生成数量
            int count = RandomValueTypeHelper.RandomNonnegativeInt(random, minCount, maxCount);

            IList list;
            if (listType.IsArray)
            {
                Array array = Array.CreateInstance(listType.GetElementType(), count);
                for (int i = 0; i < count; i++)
                {
                    array.SetValue(GetObject(listType.GetElementType(), random), i);
                }
                list = array;
            }
            else
            {
                list = (IList)Activator.CreateInstance(listType);
                Type[] genericArguments = listType.GetGenericArguments();
                if (genericArguments.Length > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(GetObject(genericArguments[0], random));
                    }
                }
            }

            return list;
        }


        #region 泛型方法
        /// <summary>
        /// 取得随机的T对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T GetObject<T>(System.Random random = null) where T : new()
        {
            return (T)GetObject(typeof(T), random);
        }
        public static List<T> GetList<T>(System.Random random = null, int minCount = 0, int maxCount = 100) where T : new()
        {
            return (List<T>)GetList(typeof(List<T>), random, minCount, maxCount);
        }
        #endregion


    }
}
