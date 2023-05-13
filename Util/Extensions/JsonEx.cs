using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class JsonEx
    {
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isIndented">是否显示格式化</param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool isIndented = false)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                obj, 
                isIndented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
        }
        public static T ToObject<T>(this string jsonStr)
        {
            return jsonStr == null ? default(T) : JsonConvert.DeserializeObject<T>(jsonStr); 
        }
   
    }
}
