using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.File
{
    /// <summary>
    /// 图片文件信息
    /// </summary>
    public class ImageFileInfo
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 是否存在
        /// </summary>
        public bool Exist { get; set; } = true;
        /// <summary>
        /// 图片窄边限制, 图片宽度和长度中较短的一边, 尺寸不应该超过这个数值, 如果为零, 不做限制
        /// </summary>
        public int ImageSizeLimit { get; set; }
    }
}
