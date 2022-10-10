using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ChaoticWinformControl.FeatureGroup
{
    public partial class InputFormFrame : Form
    {
        #region 属性
        /// <summary>
        /// 提示文本
        /// </summary>
        public string HintText
        {
            get => HintLabel.Text;
            set => HintLabel.Text = value;
        }

        /// <summary>
        /// 使用的按钮组
        /// </summary>
        public MessageBoxButtons Buttons 
        {
            get => buttons;
            set
            {
                buttons = value;
                RefreshButtons();
            }
        }
        private MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        #endregion

        public InputFormFrame()
        {
            InitializeComponent();
        }
        #region 控制方法
        public Control Body { get; private set; }

        /// <summary>
        /// 使用指定的控件作为主体区域, 会将控件Dock设置为Fill, 如果控件不是窗口, 会将其Anchor设置为 Left | Right | Top | Bottom, 同时设置Padding为0
        /// </summary>
        /// <param name="control"></param>
        public void SetBody(Control control)
        {
            Body = control;

            BodyPanel.Controls.Clear();

            Body.Parent = BodyPanel;
            Body.Location = new Point();
            if (Body is Form form)
            {
                form.Dock = DockStyle.Fill;
                form.TopLevel = false;
            }
            else
            {
                Body.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                Body.Size = new Size(BodyPanel.Width, BodyPanel.Height);
                if (Body.Height != BodyPanel.Height)
                {
                    // 如果是不能自由设定高度的控件, 窗口高度适配内容高度
                    Height -= BodyPanel.Height - Body.Height;
                }
            }

            Body.BringToFront();
            Body.Show();
        }
        /// <summary>
        /// 使用指定类型的控件实例作为主体区域 (会将其Dock设置为Fill)
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        public void SetBody<TControl>()
            where TControl : Control, new()
        {
            SetBody(new TControl());
        }


        #endregion

        #region 控件增减
        /// <summary>
        /// 根据所设置的按钮类型 <see cref="MessageBoxButtons"/> 更新可选按钮列表
        /// </summary>
        protected virtual void RefreshButtons()
        {
            ButtonArea.Controls.Clear();
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    AddButton("确定", DialogResult.OK);
                    break;
                case MessageBoxButtons.OKCancel:
                    AddButton("取消", DialogResult.Cancel);
                    AddButton("确定", DialogResult.OK);
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    AddButton("忽略", DialogResult.Ignore);
                    AddButton("重试", DialogResult.Retry);
                    AddButton("中止", DialogResult.Abort);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    AddButton("取消", DialogResult.Cancel);
                    AddButton("否", DialogResult.No);
                    AddButton("是", DialogResult.Yes);
                    break;
                case MessageBoxButtons.YesNo:
                    AddButton("否", DialogResult.No);
                    AddButton("是", DialogResult.Yes);
                    break;
                case MessageBoxButtons.RetryCancel:
                    AddButton("忽略", DialogResult.Cancel);
                    AddButton("重试", DialogResult.Retry);
                    break;
            }
        }
        /// <summary>
        /// 添加按钮到按钮区域
        /// </summary>
        /// <param name="text"></param>
        /// <param name="dialogResult"></param>
        /// <returns>被添加的按钮</returns>
        protected virtual Button AddButton(string text, DialogResult dialogResult)
        {
            Button button = new Button()
            {
                Text = text,
                DialogResult = dialogResult,
            };
            ButtonArea.Controls.Add(button);
            return button;
        }
        #endregion
        
    }
}
