using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class StringBuilderEx
    {
        public static StringBuilder AppendKeyValuePair(
            this StringBuilder sb,
            string key, string value, string split = ": ", bool ignoreEmpty = true, bool lf = true)
        {
            if (sb != null)
            {
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(key))
                {
                    sb.Append(key).Append(split).Append(value);
                    if (lf)
                    {
                        sb.AppendLine();
                    }
                }
            }
            return sb;
        }
        public static StringBuilder AppendKeyValuePair(
            this StringBuilder sb,
            string key, object value, string split = ": ", bool ignoreEmpty = true, bool lf = true)
        {
            return AppendKeyValuePair(sb, key, value.ToString(), split, ignoreEmpty, lf);
        }
    }
}
