using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class TypeHelper
    {
        /// <summary>
        /// 判断是否内置类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBulitInType(Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        /// <summary>
        /// 检查是否包含无参公共构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ExistNonParamPublicConstructor(Type type)
        {
            bool result = false;
            ConstructorInfo[] infoArray = type.GetConstructors();
            foreach (ConstructorInfo info in infoArray)
            {
                if (info.IsPublic && info.GetParameters().Length == 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
