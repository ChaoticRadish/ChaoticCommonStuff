using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class StringEx
    {
        /// <summary>
        /// 当当前字符串为null或空值时, 返回指定的默认值, 否则返回原值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string WhenEmptyDefault(this string str, string defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 当当前字符串为null或空值或仅由空白字符组成时, 返回指定的默认值, 否则返回原值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string WhenWhiteSpaceDefault(this string str, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 将以16进制格式表示的字符串转换为byte[] (会移除字符串中的空格)
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToHexByte(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
            {
                hexString += " ";
            }
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        #region IP地址判断
        /// <summary>
        /// IPv4
        /// </summary>
        public const string IPV4_REGEX_STR = @"(?:(?:1[0-9][0-9]\\.)|(?:2[0-4][0-9]\\.)|(?:25[0-5]\\.)|(?:[1-9][0-9]\\.)|(?:[0-9]\\.)){3}(?:(?:1[0-9][0-9])|(?:2[0-4][0-9])|(?:25[0-5])|(?:[1-9][0-9])|(?:[0-9]))";
        public static Regex IPv4Regex { get; private set; } = new System.Text.RegularExpressions.Regex(IPV4_REGEX_STR);

        /// <summary>
        /// IPv6 
        /// </summary>
        public const string IPV6_REGEX_STR = @"(?:^|(?<=\s))(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))(?=\s|$)";
        public static Regex IPv6Regex { get; private set; } = new System.Text.RegularExpressions.Regex(IPV6_REGEX_STR);

        public static bool IsIPv4(this string ip)
        {
            return IPv4Regex.Match(ip).Success;
        }
        public static bool IsIPv6(this string ip)
        {
            return IPv6Regex.Match(ip).Success;
        }
        #endregion
    }
}
