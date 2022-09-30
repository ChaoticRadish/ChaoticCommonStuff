using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Mission
{
    /// <summary>
    /// 任务进度类
    /// </summary>
    public class MissionProgress
    {
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; } = 0;

        /// <summary>
        /// 进度
        /// </summary>
        public float Progress 
        {
            get 
            {
                return progress;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 100)
                {
                    value = 100;
                }
                progress = value;
            }
        }
        private float progress = 0;



        /// <summary>
        /// 进度信息
        /// </summary>
        public string Info { get; set; }
    }
}
