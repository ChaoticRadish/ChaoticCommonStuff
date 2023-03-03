using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Util.Random
{
    /// <summary>
    /// <para>本帮助类的制作情况: </para>
    /// <para>int属性: 完成</para>
    /// <para>char属性: 完成</para>
    /// <para>short属性: 完成</para>
    /// <para>long属性: 完成</para>
    /// <para>float属性: 完成</para>
    /// <para>bool属性: 完成</para>
    /// <para>array属性: 完成</para>
    /// <para>list属性: 完成</para>
    /// <para>double属性: 完成</para>
    /// <para>object属性: 完成</para>
    /// </summary>
    public static class RandomObjectHelper
    {
        #region 配置类
        /// <summary>
        /// 随机生成的配置
        /// </summary>
        public struct RandomConfig
        {
            public short MinShort { get; set; }
            public short MaxShort { get; set; }
            public int MinInt { get; set; }
            public int MaxInt { get; set; }
            public long MinLong { get; set; }
            public long MaxLong { get; set; }
            public float MinFloat { get; set; }
            public float MaxFloat { get; set; }
            public double MinDouble { get; set; }
            public double MaxDouble { get; set; }
            public int MinStringLength { get; set; }
            public int MaxStringLength { get; set; }
            public int MinCount { get; set; }
            public int MaxCount { get; set; }
            public DateTime MinDateTime { get; set; }
            public int DateTimeRange { get; set; }
            public double ProbabilityTrue { get; set; }
            /// <summary>
            /// 属性是可空类型 <see cref="Nullable"/> 时, 赋null值的概率
            /// </summary>
            public double ProbabilityNull { get; set; }
            /// <summary>
            /// <para>列表深度, 在生成列表时使用, 在方法 <see cref="GetList(Type, System.Random, int, int, RandomConfig?)"/> 中若为0, 将返</para>
            /// <para>回null, 在该方法内传入生成列表项的方法是, 会先减1</para>
            /// <para>但是如果是负数, 则说明不做深度限制, 调用生成列表项, 数值将不变</para>
            /// <para>!!! 如果要设置成负数, 需要谨慎使用, 避免无限递归 !!!</para>
            /// </summary>
            public int ListDepth { get; set; }
            /// <summary>
            /// 大小写(仅用于字符串)
            /// </summary>
            public CaseEnum Case { get; set; }

            /// <summary>
            /// 字符范围
            /// </summary>
            public IList<char> CharRange { get; set; }

            /// <summary>
            /// 字符串大小写
            /// </summary>
            public enum CaseEnum
            {
                /// <summary>
                /// 大写
                /// </summary>
                Upper,
                /// <summary>
                /// 小写
                /// </summary>
                Lower,
                /// <summary>
                /// 未设置 (即维持原状)
                /// </summary>
                None,
            }
        }
        /// <summary>
        /// 默认配置
        /// </summary>
        public static RandomConfig DefaultConfig { get; private set; } = new RandomConfig()
        {
            MinShort = 0,       MaxShort = 50,
            MinInt = 0,         MaxInt = 100,
            MinLong = 0,        MaxLong = 100,
            MinFloat = 0,       MaxFloat = 100,
            MinDouble = 0,      MaxDouble = 100,
            MinCount = 0,       MaxCount = 20,      ListDepth = 1,
            MinStringLength = 0,    MaxStringLength = 50, Case = RandomConfig.CaseEnum.None,
            MinDateTime = new DateTime(2008, 08, 07),   DateTimeRange = 10 * 365,
            ProbabilityTrue = 0.5,
            ProbabilityNull = 0.1,
            CharRange = InputRevision.Union(RandomValueTypeHelper.CharCommonRangeLetter, RandomValueTypeHelper.CharCommonRangeNumber),
        };
        #endregion

        #region 设置属性的值
        /// <summary>
        /// 为对象的指定属性赋随机值
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        public static void SetRandomValue(object obj, PropertyInfo propertyInfo, System.Random random = null, RandomConfig? config = null)
        {
            random = random ?? new System.Random();
            RandomConfig useConfig = config ?? DefaultConfig;

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

            object propertyValue = null;
            if (isNullable && RandomValueTypeHelper.RandomTrue(random, probability == null ? useConfig.ProbabilityNull : probability.Null))
            {// 如果是可空类型, 按一定概率赋null值
                propertyValue = null;
            }
            else
            {
                TypeHelper.SwitchType(type)
                    .Case<short>(() =>
                    {
                        propertyValue = GetRandomShortValue(propertyInfo, random, useConfig);
                    })
                    .Case<char>(() =>
                    {
                        propertyValue = GetRandomCharValue(propertyInfo, random, useConfig);
                    })
                    .Case<int>(() =>
                    {
                        propertyValue = GetRandomIntValue(propertyInfo, random, useConfig);
                    })
                    .Case<bool>(() =>
                    {
                        propertyValue = GetRandomBoolValue(propertyInfo, random, useConfig);
                    })
                    .Case<double>(() =>
                    {
                        propertyValue = GetRandomDoubleValue(propertyInfo, random, useConfig);
                    })
                    .Case<float>(() =>
                    {
                        propertyValue = GetRandomFloatValue(propertyInfo, random, useConfig);
                    })
                    .Case<long>(() =>
                    {
                        propertyValue = GetRandomLongValue(propertyInfo, random, useConfig);
                    })
                    .Case<string>(() =>
                    {
                        propertyValue = GetRandomStringValue(propertyInfo, random, useConfig);
                    })
                    .Case<DateTime>(() =>
                    {
                        propertyValue = GetRandomDateTimeValue(propertyInfo, random, useConfig);
                    })
                    .CaseBase<IList>(() =>
                    {
                        propertyValue = GetRandomListValue(propertyInfo, random, useConfig);
                    })
                    .Default(() =>
                    {
                        if (type.IsEnum)
                        {
                            propertyValue = GetRandomEnumValue(propertyInfo, random, useConfig);
                        }
                        else if (type.IsClass)
                        {
                            propertyValue = GetObject(type, random);
                        }
                        else
                        {
                            // 其他的未受支持的情况
                        }
                    })
                    .Run();
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
        private static object GetRandomShortValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            short min = useConfig.MinShort;
            short max = useConfig.MaxShort;

            ShortRangeAttribute range = propertyInfo.GetCustomAttribute<ShortRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomShort(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomCharValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            IList<char> range = useConfig.CharRange;

            return RandomValueTypeHelper.RandomChar(random, range);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomIntValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            int min = useConfig.MinInt;
            int max = useConfig.MaxInt;

            IntRangeAttribute range = propertyInfo.GetCustomAttribute<IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomInt(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomBoolValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            ProbabilityAttribute probability = propertyInfo.GetCustomAttribute<ProbabilityAttribute>();
            return probability == null ? 
                (config == null ? RandomValueTypeHelper.RandomBool(random) : RandomValueTypeHelper.RandomTrue(config.Value.ProbabilityTrue))
                :
                RandomValueTypeHelper.RandomTrue(random, probability.True);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomEnumValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            return RandomValueTypeHelper.RandomEnum(random, propertyInfo.PropertyType);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomLongValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            long min = useConfig.MinLong;
            long max = useConfig.MaxLong;

            LongRangeAttribute range = propertyInfo.GetCustomAttribute<LongRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomLong(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomFloatValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            float min = useConfig.MinFloat;
            float max = useConfig.MaxFloat;

            FloatRangeAttribute range = propertyInfo.GetCustomAttribute<FloatRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomFloat(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomDoubleValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            double min = useConfig.MinDouble;
            double max = useConfig.MaxDouble;

            DoubleRangeAttribute range = propertyInfo.GetCustomAttribute<DoubleRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return RandomValueTypeHelper.RandomDouble(random, min, max);
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomStringValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            int min = useConfig.MinStringLength;
            int max = useConfig.MaxStringLength;
            RandomConfig.CaseEnum textCase = useConfig.Case;

            IntRangeAttribute range = propertyInfo.GetCustomAttribute<IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            string output = RandomStringHelper.GetRandomEnglishString(
                RandomValueTypeHelper.RandomInt(random, min, max),
                random);
            StringFormatAttribute format = propertyInfo.GetCustomAttribute<StringFormatAttribute>();
            if (format != null)
            {
                textCase = format.Case;
            }
            switch (textCase)
            {
                case RandomConfig.CaseEnum.Upper:
                    output = output.ToUpper();
                    break;
                case RandomConfig.CaseEnum.Lower:
                    output = output.ToLower();
                    break;
            }
            return output;
        }
        /// <summary>
        /// 为对象的指定属性获取随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static object GetRandomDateTimeValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            DateTime start = useConfig.MinDateTime;
            int rangeDays = useConfig.DateTimeRange;

            DateTimeRangeAttribute range = propertyInfo.GetCustomAttribute<DateTimeRangeAttribute>();
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
        private static object GetRandomListValue(PropertyInfo propertyInfo, System.Random random, RandomConfig? config = null)
        {
            RandomConfig useConfig = config ?? DefaultConfig;
            // 取值范围
            int min = useConfig.MinCount;
            int max = useConfig.MaxCount;
            IntRangeAttribute range = propertyInfo.GetCustomAttribute<IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
            }
            return GetList(propertyInfo.PropertyType, random, min, max, config);
        }

        #endregion

        #endregion*
        /// <summary>
        /// 取得随机的指定类型的对象
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="random"></param>
        /// <param name="config">本次调用的通用随机参数, 优先级低于属性上的随机参数注解</param>
        /// <returns></returns>
        public static object GetObject(Type type, System.Random random = null, RandomConfig? config = null)
        {
            random = random ?? new System.Random();
            RandomConfig useConfig = config ?? DefaultConfig;

            if (typeof(IList).IsAssignableFrom(type))
            {// 是列表的话
                return GetList(type, random, useConfig.MinCount, useConfig.MaxCount);
            }
            else
            {// 如果不是
                object output = null;
                TypeHelper.SwitchType(type)
                    .Case<short>(() =>
                    {
                        output = RandomValueTypeHelper.RandomInt(random, useConfig.MinShort, useConfig.MaxShort);
                    })
                    .Case<char>(() =>
                    {
                        output = RandomValueTypeHelper.RandomChar(random, useConfig.CharRange);
                    })
                    .Case<int>(() =>
                    {
                        output = RandomValueTypeHelper.RandomInt(random, useConfig.MinInt, useConfig.MaxInt);
                    })
                    .Case<long>(() =>
                    {
                        output = RandomValueTypeHelper.RandomLong(random, useConfig.MinLong, useConfig.MaxLong);
                    })
                    .Case<double>(() =>
                    {
                        output = RandomValueTypeHelper.RandomDouble(random, useConfig.MinDouble, useConfig.MaxDouble);
                    })
                    .Case<float>(() =>
                    {
                        output = RandomValueTypeHelper.RandomFloat(random, useConfig.MinFloat, useConfig.MaxFloat);
                    })
                    .Case<bool>(() =>
                    {
                        output = RandomValueTypeHelper.RandomTrue(random, useConfig.ProbabilityTrue);
                    })
                    .Case<string>(() =>
                    {
                        string s = RandomStringHelper.GetRandomEnglishString(
                                        RandomValueTypeHelper.RandomInt(random, useConfig.MinStringLength, useConfig.MaxStringLength),
                                        random);
                        switch (useConfig.Case)
                        {
                            case RandomConfig.CaseEnum.Upper:
                                s = s.ToUpper();
                                break;
                            case RandomConfig.CaseEnum.Lower:
                                s = s.ToLower();
                                break;
                        }
                        output = s;
                    })
                    .Default(() =>
                    {
                        if (TypeHelper.ExistNonParamPublicConstructor(type))
                        {// 检查是否有无参构造函数
                            output = Activator.CreateInstance(type);

                            foreach (PropertyInfo propertyInfo in type.GetProperties())
                            {// 遍历公共属性
                                SetRandomValue(output, propertyInfo, random, useConfig);
                            }
                        }
                        else
                        {
                            output = null;
                        }
                    })
                    .Run();
                return output;
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
        public static IList GetList(Type listType, System.Random random = null, int minCount = 0, int maxCount = 100, RandomConfig? config = null)
        {
            if (!typeof(IList).IsAssignableFrom(listType))
            {
                return null;
            }

            random = random ?? new System.Random();
            RandomConfig useConfig = config ?? DefaultConfig;
            if (useConfig.ListDepth == 0)
            {
                return null;
            }
            else if(useConfig.ListDepth > 0)
            {
                useConfig.ListDepth--;
            }

            // 生成数量
            int count = RandomValueTypeHelper.RandomNonnegativeInt(random, minCount, maxCount);

            IList list;
            if (listType.IsArray)
            {
                Array array = Array.CreateInstance(listType.GetElementType(), count);
                for (int i = 0; i < count; i++)
                {
                    array.SetValue(GetObject(listType.GetElementType(), random, useConfig), i);
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
                        list.Add(GetObject(genericArguments[0], random, useConfig));
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
        public static T GetObject<T>(System.Random random = null, RandomConfig? config = null) where T : new()
        {
            return (T)GetObject(typeof(T), random, config);
        }
        public static List<T> GetList<T>(System.Random random = null, int minCount = 0, int maxCount = 100, RandomConfig? config = null) where T : new()
        {
            return (List<T>)GetList(typeof(List<T>), random, minCount, maxCount, config);
        }
        #endregion


    }
}
