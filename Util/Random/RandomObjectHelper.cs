using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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

            if (typeof(int) == propertyInfo.PropertyType)
            {
                SetRandomIntValue(obj, propertyInfo, random);
            }
            else if (typeof(bool) == propertyInfo.PropertyType)
            {
                propertyInfo.SetValue(obj, RandomValueTypeHelper.RandomBool(random), null);
            }
            else if (typeof(double) == propertyInfo.PropertyType)
            {
                SetRandomDoubleValue(obj, propertyInfo, random);
            }
            else if (typeof(float) == propertyInfo.PropertyType)
            {
                SetRandomFloatValue(obj, propertyInfo, random);
            }
            else if (typeof(long) == propertyInfo.PropertyType)
            {
                SetRandomLongValue(obj, propertyInfo, random);
            }
            else if (typeof(string) == propertyInfo.PropertyType)
            {
                SetRandomStringValue(obj, propertyInfo, random);
            }
            else if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
            {
                SetRandomListValue(obj, propertyInfo, random);
            }
            else if (propertyInfo.PropertyType.IsClass)
            {
                propertyInfo.SetValue(obj, GetObject(propertyInfo.PropertyType, random), null);
            }

        }

        #region 设置值的具体实现
        /// <summary>
        /// 为对象的指定属性赋随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static void SetRandomIntValue(object obj, PropertyInfo propertyInfo, System.Random random)
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
            propertyInfo.SetValue(obj, RandomValueTypeHelper.RandomeInt(random, min, max), null);
        }
        /// <summary>
        /// 为对象的指定属性赋随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static void SetRandomLongValue(object obj, PropertyInfo propertyInfo, System.Random random)
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
            propertyInfo.SetValue(obj, RandomValueTypeHelper.RandomeLong(random, min, max), null);
        }
        /// <summary>
        /// 为对象的指定属性赋随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static void SetRandomFloatValue(object obj, PropertyInfo propertyInfo, System.Random random)
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
            propertyInfo.SetValue(obj, RandomValueTypeHelper.RandomeFloat(random, min, max), null);
        }
        /// <summary>
        /// 为对象的指定属性赋随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static void SetRandomDoubleValue(object obj, PropertyInfo propertyInfo, System.Random random)
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
            propertyInfo.SetValue(obj, RandomValueTypeHelper.RandomeDouble(random, min, max), null);
        }
        /// <summary>
        /// 为对象的指定属性赋随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static void SetRandomStringValue(object obj, PropertyInfo propertyInfo, System.Random random)
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
            propertyInfo.SetValue(
            obj,
            RandomStringHelper.GetRandomEnglishString(
            RandomValueTypeHelper.RandomeInt(random, min, max),
            random),
            null);
        }
        /// <summary>
        /// 为对象的指定属性赋随机值, 不会判断, 需要确保输入的参数不为空
        /// </summary>
        /// <param name="obj">被赋值对象</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="random"></param>
        private static void SetRandomListValue(object obj, PropertyInfo propertyInfo, System.Random random)
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
            propertyInfo.SetValue(obj, GetList(propertyInfo.PropertyType, random, min, max), null);
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
