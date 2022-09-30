using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 取得当天的开始时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime GetDayStartTime(long ticks)
        {
            DateTime temp = new DateTime(ticks);
            return new DateTime(temp.Year, temp.Month, temp.Day, 9, 30, 0);
        }
        /// <summary>
        /// 取得当天的结束时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetDayStartTime(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, 18, 30, 0).Ticks;
        }

        /// <summary>
        /// 取得当天的开始时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime GetDayEndTime(long ticks)
        {
            DateTime temp = new DateTime(ticks);
            return new DateTime(temp.Year, temp.Month, temp.Day, 9, 30, 0);
        }
        /// <summary>
        /// 取得当天的结束时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetDayEndTime(DateTime time)
        {
            return GetDayEndTimeAsDateTime(time).Ticks;
        }
        /// <summary>
        /// 取得当天的结束时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetDayEndTimeAsDateTime(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, 18, 30, 0);
        }

        #region 取得正午时间
        /// <summary>
        /// 取得当天的正午十二点
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetDayNoonTime(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, 12, 0, 0).Ticks;
        }
        /// <summary>
        /// 取得当天的正午十二点
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetDayNoonTime(long time)
        {
            DateTime dateTime = new DateTime(time);
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0);
        }
        /// <summary>
        /// 取得当天的正午十二点
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetDayNoonTimeAsDateTime(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, 12, 0, 0);
        }
        /// <summary>
        /// 取得单天的正午十二点
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static long GetDayNoonTimeAsTick(long tick)
        {
            return GetDayNoonTime(tick).Ticks;
        }
        #endregion

        /// <summary>
        /// 取得工作日结束时间 (如果输入的时间是下午5点之后, 则返回下一个工作日的结束时间)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetWorkDayEndTime(long ticks)
        {
            DateTime time = new DateTime(ticks);
            if (time.Hour >= 17)
            {// 下午五点之后, 取下一天
                time += new TimeSpan(1, 0, 0, 0);
            }
            while (!IsWorkDay(time))
            {// 如果输入的时间不是工作日, 取下一天
                time += new TimeSpan(1, 0, 0, 0);
            }
            return GetDayEndTimeAsDateTime(time);
        }
        /// <summary>
        /// 取得工作日结束时间 (如果输入的时间是下午5点之后, 则返回下一个工作日的结束时间)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>ggcv
        public static long GetWorkDayEndTime(DateTime time)
        {
            if (time.Hour >= 17)
            {// 下午五点之后, 取下一天
                time += new TimeSpan(1, 0, 0, 0);
            }
            while (!IsWorkDay(time))
            {// 如果输入的时间不是工作日, 取下一天
                time += new TimeSpan(1, 0, 0, 0);
            }
            return GetDayEndTime(time);
        }

        /// <summary>
        /// 将Tick转化为时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime TickToTime(long ticks)
        {
            if (ticks > 0)
            {
                return new DateTime(ticks);
            }
            else
            {
                return new DateTime(1, 1, 1);
            }
        }
        /// <summary>
        /// 时间转化为Tick
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long TimeToTick(DateTime time)
        {
            if (time.Year < 1600)
            {
                return -1;
            }
            else
            {
                return time.Ticks;
            }
        }


        /// <summary>
        /// 结束时间 - 开始时间 所得的工作日
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static float GetDeltaWorkDay(DateTime startTime, DateTime endTime)
        {
            float output = 0;
            int dist = 1;
            if (startTime > endTime)
            {// 开始时间在结束时间之后, 交换时间
                DateTime temp = startTime;
                startTime = endTime;
                endTime = temp;
                dist = -1;
            }
            // 计算工作日差值
            if (IsSameDay(startTime, endTime))
            {// 同一天开始结束
                if (IsWorkDay(startTime))
                {// 如果是工作日
                    output = GetDeltaDay(startTime, endTime);
                }
                else 
                {// 不是
                    output = 0;
                }
            }
            else
            {// 不同天
                // 添加头尾两天的天数
                if (IsWorkDay(startTime))
                {// 如果是工作日
                    output += GetSurplusDay(startTime);
                }
                if (IsWorkDay(endTime))
                {// 如果是工作日
                    output += GetUsedDay(endTime);
                }
                // 临时的日期, 初始化为开始的日期
                DateTime temp = startTime;
                while(IsDateLessThen(temp += new TimeSpan(1, 0, 0, 0), endTime))
                {// 将临时日期移动到第二天, 如果移动之后的这一天的日期比结束时间的日期要早, 则执行循环
                    if (IsWorkDay(temp))
                    {// 如果临时日期是工作日
                        output++;
                    }
                }
            }
            return output * dist;
        }

        /// <summary>
        /// 判断输入的日期是不是工作日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWorkDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {// 如果是周日, 则是休息日
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取输入的时间在当天已经过去的时间 [0, 1)天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static float GetUsedDay(DateTime date)
        {
            return (float)((date - new DateTime(date.Year, date.Month, date.Day)).TotalDays);
        }
        /// <summary>
        /// 获取输入的时间还剩下多久 (0, 1]天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static float GetSurplusDay(DateTime date)
        {
            return (float)(1 - (date - new DateTime(date.Year, date.Month, date.Day)).TotalDays);
        }


        /// <summary>
        /// 判断输入的两个时间是否同一天
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsSameDay(DateTime a, DateTime b)
        {
            return a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;
        }
        /// <summary>
        /// 判断输入的日期 a 是否比日期 b 小
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsDateLessThen(DateTime a, DateTime b)
        {
            if (a.Year < b.Year)
            {
                return true;
            }
            else if (a.Year == b.Year && a.Month < b.Month)
            {
                return true;
            }
            else if (a.Year == b.Year && a.Month == b.Month && a.Day < b.Day)
            {
                return true;
            }
            return false;
        }
        #region 日期的简单计算
        /// <summary>
        /// 取得输入日期的天数差值 (b - a)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float GetDeltaDay(DateTime a, DateTime b)
        {
            return (float)((b - a).TotalDays);
        }
        #endregion

        #region 字符串
        private const string DISABLED_TIME_STRING = " -- -- -- -- -- -- ";
        /// <summary>
        /// 取得时间的字符串
        /// </summary>
        /// <param name="time">输入的时间</param>
        /// <param name="minTime">判定的最小时间</param>
        /// <returns></returns>
        public static string GetTimeString(DateTime time, DateTime minTime)
        {
            if (time >= minTime)
            {
                return time.ToString();
            }
            else
            {
                return DISABLED_TIME_STRING;
            }
        }
        /// <summary>
        /// 取得时间的字符串
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minYear">判定的最小年份</param>
        /// <returns></returns>
        public static string GetTimeString(DateTime time, int minYear = 0)
        {
            if (time.Year >= minYear)
            {
                return time.ToString();
            }
            else
            {
                return DISABLED_TIME_STRING;
            }
        }
        /// <summary>
        /// 取得日期的字符串
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minYear"></param>
        /// <returns></returns>
        public static string GetDateString(DateTime time, int minYear = 0)
        {
            if (time.Year >= minYear)
            {
                return time.ToShortDateString();
            }
            else
            {
                return DISABLED_TIME_STRING;
            }
        }
        /// <summary>
        /// 取得时间的字符串
        /// </summary>
        /// <param name="tick"></param>
        /// <param name="minTick">判定的最小tick</param>
        /// <returns></returns>
        public static string GetTimeString(long tick, long minTick = 0)
        {
            if (tick >= minTick)
            {
                return new DateTime(tick).ToString();
            }
            else
            {
                return DISABLED_TIME_STRING;
            }
        }
        #endregion

        #region 日期
        /// <summary>
        /// 一天内的Tick数量
        /// </summary>
        public static readonly long TICKS_ON_ONE_DAY
            = 1L
                * 10000 // => 毫秒
                * 1000  // => 秒
                * 60    // => 分
                * 60    // => 时
                * 24;   // => 天
        public static readonly DateTime MIN_DATE_TIME
            = new DateTime(1, 1, 1);
        /// <summary>
        /// 计算日期代表的总天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetTotalDay(long date)
        {
            return GetTotalDay(new DateTime(date));
        }
        /// <summary>
        /// 计算日期代表的总天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetTotalDay(DateTime date)
        {
            return (int)(date - MIN_DATE_TIME).TotalDays;
        }
        /// <summary>
        /// 计算输入日期所在月份的第一天代表的总天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetTotalDayAsMonthFirstDay(DateTime date)
        {
            return GetTotalDay(new DateTime(date.Year, date.Month, 1));
        }
        /// <summary>
        /// 总天数转化为日期
        /// </summary>
        /// <param name="totalDay"></param>
        /// <returns></returns>
        public static DateTime TotalDayToDateTime(int totalDay)
        {
            return MIN_DATE_TIME + new TimeSpan(totalDay, 0, 0, 0);
        }
        /// <summary>
        /// 减去输入日期的时间及之后的时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ClipHour(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
        /// <summary>
        /// 获得今天 (仅到天)
        /// </summary>
        /// <returns></returns>
        public static DateTime GetToday()
        {
            return ClipHour(DateTime.Now);
        }
        /// <summary>
        /// 获得明天 (仅到天)
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTomorrow()
        {
            return GetNext(DateTime.Now);
        }
        /// <summary>
        /// 获得昨天 (仅到天)
        /// </summary>
        /// <returns></returns>
        public static DateTime GetYesterday()
        {
            return GetPrev(DateTime.Now);
        }
        /// <summary>
        /// 获得输入日期的下一天 (仅到天)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetNext(DateTime date)
        {
            return ClipHour(date + new TimeSpan(1, 0, 0, 0));
        }
        /// <summary>
        /// 获得输入日期的上一天 (仅到天)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetPrev(DateTime date)
        {
            if (date.Year == 1 && date.Month == 1 && date.Day == 1)
            {
                return MIN_DATE_TIME;
            }
            else
            {
                return ClipHour(date - new TimeSpan(1, 0, 0, 0));
            }
        }
        #endregion
    }
}
