using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class DictionaryEx
    {
        /// <summary>
        /// 尝试从字典中获取值, 如果字典不含输入键, 则返回默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default)
        {
            return dic.ContainsKey(key) ? dic[key] : defaultValue;
        }

        /// <summary>
        /// 设置输入的键值对到字典中, 如果已存在同键项, 将覆盖, 不存在, 将添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
        /// <summary>
        /// 批量设置输入的键值对到字典中
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="other"></param>
        public static void BatchSet<TKey, TValue>(this IDictionary<TKey, TValue> dic, IDictionary<TKey, TValue> other)
        {
            foreach (TKey key in other.Keys)
            {
                Set(dic, key, other[key]);
            }
        }

        /// <summary>
        /// 批量设置输入的键值对到字典中
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="other"></param>
        /// <param name="getKeyFunc"></param>
        public static void BatchSet<TKey, TValue>(this IDictionary<TKey, TValue> dic, IEnumerable<TValue> other, Func<TValue, TKey> getKeyFunc)
        {
            foreach (TValue value in other)
            {
                Set(dic, getKeyFunc.Invoke(value), value);
            }
        }


        /// <summary>
        /// 尝试移除
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
        {
            if (dic.ContainsKey(key))
            {
                TValue value = dic[key];
                if (dic.Remove(key))
                {
                    return value;
                }
                else
                {
                    return default;
                }
            }
            else
            {
                return default;
            }
        }
    }
}
