using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl.FeatureGroup
{
    /// <summary>
    /// 分页控件
    /// </summary>
    [DefaultEvent(nameof(OnPageIndexChanged))]
    public partial class PagingBox : UserControl
    {
        public PagingBox()
        {
            InitializeComponent();
        }



        #region 属性

        public bool ReadOnly
        {
            get => readOnly;
            set
            {
                if (readOnly != value)
                {
                    readOnly = value;
                    // 变更按钮状态

                    FirstButton.Enabled = !readOnly;
                    PrevButton.Enabled = !readOnly;
                    NextButton.Enabled = !readOnly;
                    LastButton.Enabled = !readOnly;
                    JumpButton.Enabled = !readOnly;
                    PageInput.ReadOnly = readOnly;

                    foreach (PageButton button in pageButtons)
                    {
                        button.Button.Enabled = !readOnly;
                    }
                }
            }
        }
        private bool readOnly = false;

        /// <summary>
        /// 页面容量
        /// </summary>
        public int PageSize 
        {
            get => pageSize;
            set
            {
                pageSize = value;
            }
        }
        private int pageSize = 20;

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount 
        {
            get => totalCount;
            set
            {
                totalCount = value;
            }
        }
        private int totalCount = 100;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentIndex
        {
            get => currentIndex;
            set
            {
                currentIndex = value;
                if (currentIndex <= 0)
                {
                    currentIndex = 1;
                }
                if (currentIndex > TotalPage) 
                {
                    currentIndex = TotalPage;
                }


                UpdatePageButtons();

                OnPageIndexChanged?.Invoke(currentIndex, pageSize);
            }
        }
        private int currentIndex;

        public int TotalPage
        {
            get => totalPage;
            set
            {
                totalPage = value;
                TotalPageShower.Text = totalPage.ToString();
                PageInput.MaxValue = totalPage;
            }
        }
        private int totalPage;
        #endregion

        #region 事件
        public delegate void OnPageIndexChangedDelegate(int pageIndex, int pageSize);
        /// <summary>
        /// 页码更新事件
        /// </summary>
        public event OnPageIndexChangedDelegate OnPageIndexChanged;
        #endregion

        #region 按钮

        private void FirstButton_Click(object sender, EventArgs e)
        {
            CurrentIndex = 1;
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            CurrentIndex--;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            CurrentIndex++;
        }

        private void LastButton_Click(object sender, EventArgs e)
        {
            CurrentIndex = TotalPage;
        }

        private void JumpButton_Click(object sender, EventArgs e)
        {
            CurrentIndex = PageInput.Value;
        }

        #endregion

        #region 页码按钮
        class PageButton
        {
            public Button Button { get; set; }

            public int PageIndex { get; set; }

            public bool IsCurrentPageIndex
            {
                get => isCurrentPageIndex;
                set 
                {
                    isCurrentPageIndex = value;
                    if (isCurrentPageIndex)
                    {
                        Button.BackColor = Color.White;
                    }
                    else
                    {
                        Button.BackColor = Color.FromArgb(225, 225, 225);
                    }
                }
            }
            private bool isCurrentPageIndex;

            public bool Visable { get => Button.Visible; set => Button.Visible = value; }
        }
        private int PageButtonWidth = 40;
        private int PageButtonGap = 3;

        private List<PageButton> pageButtons = new List<PageButton>();


        private void UpdatePageButtons()
        {
            TotalPage = (TotalCount - 1) / PageSize + 1;

            SuspendLayout();

            int canShowButtonCount = (PageButtonArea.Width - PageButtonGap) / (PageButtonGap + PageButtonWidth);
            if (canShowButtonCount % 2 == 0)
            {
                canShowButtonCount -= 1;
            }
            if (canShowButtonCount < 3)
            {
                canShowButtonCount = 3;
            }
            int needShowStart = CurrentIndex - canShowButtonCount / 2;
            int startOffset = needShowStart <= 0 ? (1 - needShowStart) : 0;
            int needShowEnd = CurrentIndex + canShowButtonCount / 2 + startOffset;
            if (needShowEnd > TotalPage)
            {
                needShowEnd = TotalPage;
            }
            int needShowCount = needShowEnd - (needShowStart + startOffset) + 1;

            while (pageButtons.Count < needShowCount) 
            {
                PageButton button = new PageButton()
                {
                    Button = new Button()
                    {
                        Visible = false,
                        Width = PageButtonWidth,
                        Height = PageButtonInnerArea.Height,
                    },
                };
                button.Button.Click += (sender, arg) =>
                {
                    CurrentIndex = button.PageIndex;
                };
                button.IsCurrentPageIndex = false;
                pageButtons.Add(button);
                PageButtonInnerArea.Controls.Add(button.Button);
            }
            int totalWidth = 0;
            for (int i = 0; i < pageButtons.Count; i++)
            {
                PageButton button = pageButtons[i];
                button.PageIndex = i + needShowStart + startOffset;
                button.Visable = i < needShowCount;
                if (!button.Visable) continue;

                button.Button.Text = (i + needShowStart + startOffset).ToString();
                button.IsCurrentPageIndex = i + needShowStart + startOffset == CurrentIndex;

                button.Button.Size = new Size(PageButtonWidth, PageButtonInnerArea.Height);
                button.Button.Location = new Point(totalWidth, 0);

                totalWidth += button.Button.Width + PageButtonGap;
            }
            PageButtonInnerArea.Width = totalWidth;
            PageButtonInnerArea.Location = new Point((PageButtonArea.Width - totalWidth) / 2, PageButtonInnerArea.Location.Y);

            ResumeLayout();
        }


        #endregion

        #region 重载
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!IsHandleCreated) return;
            UpdatePageButtons();
        }
        #endregion
    }
}
