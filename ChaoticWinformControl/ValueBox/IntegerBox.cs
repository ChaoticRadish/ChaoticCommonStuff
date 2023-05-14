using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl.ValueBox
{
    public partial class IntegerBox : TextBox
    {
        #region 属性
        [Browsable(true)]
        [Category("Action")]
        public int DefaultValue
        {
            get => defaultValue;
            set
            {
                defaultValue = IntoRange(value);
                Text = defaultValue.ToString();
                Value = defaultValue;
            }
        }
        private int defaultValue = 0;

        /// <summary>
        /// 数值范围,设置改值将会重新设置默认值
        /// </summary>
        [Browsable(true)]
        [Category("Action")]
        public NumberRangeEnum Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
                DefaultValue = defaultValue;
            }
        }
        private NumberRangeEnum range = NumberRangeEnum.Arbitrarily;


        [Browsable(true)]
        [Category("Action"), Description("最小值")]
        public int? MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                TrySetValue(Value);
            }
        }
        private int? minValue = null;

        [Browsable(true)]
        [Category("Action"), Description("最大值")]
        public int? MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                TrySetValue(Value);
            }
        }
        private int? maxValue = null;
        #endregion

        #region 数据
        public int Value { get; private set; }
        #endregion

        public IntegerBox()
        {
            Text = defaultValue.ToString();
            Value = defaultValue;
            Leave += IntegerBox_Leave;
        }
        #region 控制
        /// <summary>
        /// 清空输入
        /// </summary>
        public void ClearInput()
        {
            TrySetValue(defaultValue);
        }
        /// <summary>
        /// 尝试设置值
        /// </summary>
        /// <param name="newValue"></param>
        public void TrySetValue(int newValue)
        {
            newValue = IntoRange(newValue);
            int oldValue = Value;
            Text = newValue.ToString();
            Value = newValue;
            if (oldValue != newValue)
            {
                ValueChanged?.Invoke(oldValue, newValue);
            }
        }
        #endregion

        private void IntegerBox_Leave(object sender, EventArgs e)
        {
            Leave -= IntegerBox_Leave;
            int newValue = ToValue(Text);
            TrySetValue(newValue);
            Leave += IntegerBox_Leave;
        }

        #region 事件
        /// <summary>
        /// 值变化委托
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public delegate void ValueChangedDelegate(int oldValue, int newValue);
        /// <summary>
        /// 值变化事件
        /// </summary>
        public event ValueChangedDelegate ValueChanged;
        #endregion

        /// <summary>
        /// 将值转换为int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int ToValue(string str)
        {
            if (int.TryParse(str, out int output))
            {
                return output;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 将值转换到可用区间内
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int IntoRange(int value)
        {
            int output = value;
            if (MaxValue != null && output > MaxValue)
            {
                output = MaxValue.Value;
            }
            if (MinValue != null && output < MinValue)
            {
                output = MinValue.Value;
            }
            switch (range)
            {
                case NumberRangeEnum.Negative:
                    if (output >= 0)
                    {
                        output = -1;
                    }
                    break;
                case NumberRangeEnum.Nonnegative:
                    if (output < 0)
                    {
                        output = 0;
                    }
                    break;
                case NumberRangeEnum.Positive:
                    if (output <= 0)
                    {
                        output = 1;
                    }
                    break;
                case NumberRangeEnum.Arbitrarily:
                    break;
            }
            return output;
        }
    }
}
