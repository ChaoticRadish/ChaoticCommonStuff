using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.File
{
    /// <summary>
    /// 文件尺寸帮助类
    /// </summary>
    public static class FileSizeHelper
    {
        /// <summary>
        /// 取得图片的数据大小
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static long GetSize(Image image)
        {
            return BitmapHelper.GetImageLength(image);
        }
        /// <summary>
        /// 取得图片的数据大小的字符串
        /// </summary>
        /// <param name="image"></param>
        /// <param name="reserved">保留小数位数</param>
        /// <returns></returns>
        public static string GetSizeString(Image image, int reserved = 2)
        {
            return GetSizeString(GetSize(image), reserved);
        }
        /// <summary>
        /// 取得图片的数据大小的字符串
        /// </summary>
        /// <param name="image"></param>
        /// <param name="reserved">保留小数位数</param>
        /// <returns></returns>
        public static string GetSizeString(BitmapHelper.BitmapDataInfo bitmap, int reserved = 2)
        {
            return GetSizeString(bitmap.IsNonData ? 0 : bitmap.Datas.Length, reserved);
        }
        /// <summary>
        /// 取得尺寸字符串
        /// </summary>
        /// <param name="size"></param>
        /// <param name="reserved">保留小数位数</param>
        /// <returns></returns>
        public static string GetSizeString(long size, int reserved = 2)
        {
            int unitIndex = 0;  // 单位索引
            double valueThis = size;    // 当前单位下的数值
            while (valueThis > 1024)
            {// 满1024
                valueThis /= 1024;
                unitIndex++;    // 单位索引增加
            }
            string output = $"{valueThis.ToString($"f{(reserved >= 0 ? reserved : 0)}")} {UnitsOfMeasure[unitIndex]}";
            return output;
        }
        /// <summary>
        /// 存储单位, 从小到大
        /// </summary>
        public static string[] UnitsOfMeasure = new string[]
        {
            "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB", "BB", "NB", "DB"
        };
    }
}
