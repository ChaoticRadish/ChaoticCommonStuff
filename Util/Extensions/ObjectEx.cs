using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extensions
{
    public static class ObjectEx
    {
        /// <summary>
        /// 浅拷贝对象的公共属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CloneProperty<T>(this T obj)
        {
            Type type = typeof(T);
            T output = (T)Activator.CreateInstance(type);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.SetMethod == null) continue;
                object value = property.GetValue(obj, null);
                if (value == null) continue;
                property.SetValue(output, value, null);
            }
            return output;
        }
    }
}
