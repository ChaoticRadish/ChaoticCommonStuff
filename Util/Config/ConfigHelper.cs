using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util.Extension;
using Util.String;

namespace Util.Config
{
    
    public static class ConfigHelper
    {
        #region 实现方式
        /// <summary>
        /// 配置读写实现列表
        /// </summary>
        private static Dictionary<string, IConfigReadWriteImpl> Impls = new Dictionary<string, IConfigReadWriteImpl>();

        /// <summary>
        /// 默认的配置读写实现
        /// </summary>
        public static IConfigReadWriteImpl DefaultImpl { get; private set; } = new ConfigurationManagerReadWriteImpl();

        /// <summary>
        /// 设置默认的配置读写实现
        /// </summary>
        /// <param name="impl"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetDefaultImpl(IConfigReadWriteImpl impl)
        {
            if (impl == null) throw new ArgumentNullException(nameof(impl));
            DefaultImpl = impl;
        }

        /// <summary>
        /// 配置配置读写的实现
        /// </summary>
        /// <param name="name"></param>
        /// <param name="impl"></param>
        public static void SetImpl(string name, IConfigReadWriteImpl impl)
        {
            Impls.Set(name, impl);
        }
        /// <summary>
        /// 配置配置读写的实现, 使用枚举值作为名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumObj"></param>
        /// <param name="impl"></param>
        public static void SetImpl(Enum enumObj, IConfigReadWriteImpl impl)
        {
            Impls.Set(enumObj.ToString(), impl);
        }


        #endregion

        #region 值转换
        /// <summary>
        /// 对象转换为配置值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Obj2ConfigValue(object obj)
        {
            if (obj == null) return null;

            if (typeof(IEnumerable<string>).IsAssignableFrom(obj.GetType()))
            {
                return StringHelper.Concat(((IEnumerable<string>)obj).ToList(), "; ", false);
            }
            else if (obj.GetType().IsEnumerable()
                && obj.GetType().GenericTypeArguments.Length == 1
                && obj.GetType().GenericTypeArguments[0].IsEnum)
            {
                IEnumerable list = obj as IEnumerable;
                List<string> valueStrings = new List<string>();
                Type type = obj.GetType().GenericTypeArguments[0];
                foreach (object item in list)
                {
                    valueStrings.Add(EnumHelper.GetDesc(item, type));
                }
                return StringHelper.Concat(valueStrings, "; ", false);
            }
            else
            {
                return obj.ToString();
            }
        }
        #endregion


        /// <summary>
        /// 配置数据对象字典, 存放最后一次保存的配置
        /// </summary>
        private static Dictionary<Type, object> Configs = new Dictionary<Type, object>();

        private static readonly object GetConfigLocker = new object();
        /// <summary>
        /// 读取一个配置信息对象, 使用类所设置的配置读写实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetConfig<T>()
            where T : new()
        {
            lock (GetConfigLocker)
            {
                Type t = typeof(T);
                if (Configs.ContainsKey(t))
                {
                    return (T)Configs[t];
                }
                else
                {
                    // 需要使用的配置读写实现
                    IConfigReadWriteImpl impl = DefaultImpl;
                    t.ExistCustomAttribute<ConfigReadWriteImplAttribute>((attr)=>
                    {
                        if (Impls.ContainsKey(attr.Name))
                        {
                            impl = Impls[attr.Name];
                        }
                    });
                    // 读取, 写入对象
                    T output = impl.GetConfig<T>();

                    Configs.Add(t, output);
                    return output;
                }
            }
        }
        /// <summary>
        /// 保存一个配置信息, 使用类所设置的配置读写实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void SaveConfig<T>(T config)
            where T : new()
        {
            SaveConfig(config, (string)null);
            }
        /// <summary>
        /// 使用指定实现保存配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="implName"></param>
        public static void SaveConfig<T>(T config, string implName)
            where T : new()
        {
            Type t = typeof(T);

            IConfigReadWriteImpl impl = null;
            // 需要使用的配置读写实现
            if (string.IsNullOrEmpty(implName))
            {
                impl = DefaultImpl;
            t.ExistCustomAttribute<ConfigReadWriteImplAttribute>((attr) =>
            {
                if (Impls.ContainsKey(attr.Name))
                {
                    impl = Impls[attr.Name];
                }
            });
            }
            else
            {
                if (Impls.ContainsKey(implName))
                {
                    impl = Impls[implName];
                }
                if (impl == null)
                {
                    throw new ArgumentException("未找到指定的配置读写实现", nameof(implName));
                }
            }

            SaveConfig(config, impl);
        }
        /// <summary>
        /// 使用指定实现保存配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="implName"></param>
        public static void SaveConfig<T>(T config, Enum implName)
            where T : new()
        {
            SaveConfig(config, implName.ToString());
        }
        /// <summary>
        /// 使用指定实现保存配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="impl"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SaveConfig<T>(T config, IConfigReadWriteImpl impl)
            where T : new()
        {
            // 检查
            if (impl == null)
            {
                throw new ArgumentNullException(nameof(impl));
            }
            Type t = typeof(T);
            // 移除 "最后保存配置" 字典中暂存的配置信息
            if (Configs.ContainsKey(t))
            {
                Configs.Remove(t);
            }
            // 保存
            impl.SaveConfig<T>(config);
        }





    }
    public class DefaultAttribute : System.Attribute
    {
        public DefaultAttribute(string defaultValue, string debugValue = null)
        {
#if DEBUG
            if (debugValue == null)
            {
                DefaultValue = defaultValue;
            }
            else
        {
                DefaultValue = debugValue;
            }
#else
            DefaultValue = defaultValue;
#endif
        }
        public DefaultAttribute(bool v, string debugValue = null) : this(v.ToString(), debugValue) { }
        public DefaultAttribute(int v, string debugValue = null) : this(v.ToString(), debugValue) { }
        public DefaultAttribute(double v, string debugValue = null) : this(v.ToString(), debugValue) { }
        public DefaultAttribute(decimal v, string debugValue = null) : this(v.ToString(), debugValue) { }

        public string DefaultValue { get; }
    }

    /// <summary>
    /// 配置信息
    /// </summary>
    public class ConfigInfoAttribute : System.Attribute
    {
        public ConfigInfoAttribute(string name, string desc = null)
        {
            Name = name;
            Desc = desc;
            if (string.IsNullOrEmpty(desc))
            {
                Desc = name;
            }
        }

        public string Name { get; }
        public string Desc { get; }
    }
}
