using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public static IConfigReadWriteImpl DefaultImpl { get; } = new ConfigurationManagerReadWriteImpl();

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
                    t.ExistCustomAttribute<ConfigReadWriteImplAttribute>((attr) =>
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
            Type t = typeof(T);
            // 移除 "最后保存配置" 字典中暂存的配置信息
            if (Configs.ContainsKey(t))
            {
                Configs.Remove(t);
            }

            // 需要使用的配置读写实现
            IConfigReadWriteImpl impl = DefaultImpl;
            t.ExistCustomAttribute<ConfigReadWriteImplAttribute>((attr) =>
            {
                if (Impls.ContainsKey(attr.Name))
                {
                    impl = Impls[attr.Name];
                }
            });
            // 保存
            impl.SaveConfig(config);
        }





    }
    public class DefaultAttribute : System.Attribute
    {
        public DefaultAttribute(string defaultValue)
        {
            DefaultValue = defaultValue;
        }

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
