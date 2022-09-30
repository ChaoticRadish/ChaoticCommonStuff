using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaoticWinformControl
{
    public partial class ControlList<T> : UserControl, IControlList
        where T : ControlListItem
    {
        #region 属性
        /// <summary>
        /// 控件方向
        /// </summary>
        [Browsable(true), Category("Layout"), Description("控件方向")]
        public OrientationEnum ControlOrientation { get; set; } = OrientationEnum.Horizontal;
        /// <summary>
        /// 显示滚动条
        /// </summary>
        [Browsable(true), Category("Layout"), Description("显示滚动条的方式")]
        public ShowScrollBarEnum ShowScrollBar 
        {
            get => showScrollBar;
            set
            {
                showScrollBar = value;
                UpdateTableLayoutCellSize();
                TableLayout.Invalidate();
            }
        }
        private ShowScrollBarEnum showScrollBar;
        /// <summary>
        /// 滚动条宽度
        /// </summary>
        [Browsable(true), Category("Layout"), Description("滚动条宽度")]
        public int ScrollBarWidth 
        {
            get
            {
                return scrollBarWidth;
            }
            set
            {
                scrollBarWidth = value >= 0 ? value : 0;
                UpdateTableLayoutCellSize();
                TableLayout.Invalidate();
            }
        }
        private int scrollBarWidth;

        /// <summary>
        /// 滚动条颜色
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("滚动条的颜色")]
        public Color ScrollColor
        {
            get
            {
                return scrollColor;
            }
            set
            {
                scrollColor = value;
                TableLayout.Invalidate();
            }
        }
        private Color scrollColor = Color.FromArgb(200, 200, 200);

        /// <summary>
        /// 滚动速度
        /// </summary>
        [Browsable(true)]
        [Category("Action")]
        [Description("滚动条滚动速度倍率")]
        public float ScrollSpeed { get; set; } = 0.5f;

        /// <summary>
        /// 间距
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("列表项的间距")]
        public int Gap
        {
            get
            {
                return gap;
            }
            set
            {
                if (gap == value)
                {
                    return;
                }
                gap = value;
                UpdateInnerAreaBound();
                InnerArea.Invalidate();
            }
        }
        private int gap = 3;

        /// <summary>
        /// 内部区域的偏移量
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("内部区域的偏移量")]
        public Point InnerAreaOffset
        {
            get
            {
                return innerAreaOffset;
            }
            set
            {
                innerAreaOffset = value;
                UpdateInnerAreaOffset();
                InnerArea.Location = innerAreaOffset;
            }
        }
        private Point innerAreaOffset;
        /// <summary>
        /// 清空偏移
        /// </summary>
        protected void ClearOffset()
        {
            InnerAreaOffset = new Point();
        }


        /// <summary>
        /// 是否自动设置提示
        /// </summary>
        [Browsable(true)]
        [Category("Action")]
        [Description("是否自动设置提示")]
        public bool AutoSetTooltip { get; set; }
        #endregion

        #region 尺寸计算
        public int GetInnerMaxWidth()
        {
            int output = ShowingArea.Width - Gap * 2;
            return output > 0 ? output : 1;
        }
        public int GetInnerMaxHeight()
        {
            int output = ShowingArea.Height - Gap * 2;
            return output > 0 ? output : 1;
        }
        #endregion

        #region 只读属性
        /// <summary>
        /// 显示区域的尺寸
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [Description("显示区域的尺寸")]
        public Size ShowingAreaSize
        {
            get => ShowingArea.Size;
        }
        /// <summary>
        /// 内部区域大小
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [Description("内部区域的尺寸")]
        public Size InnerAreaSize
        {
            get => InnerArea.Size;
        }
        #endregion

        #region 状态
        protected bool Initing { get; private set; } = true;
        #endregion

        public ControlList()
        {
            InitializeComponent();
            Initing = false;
            TableLayout.GetType()
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(TableLayout, true, null);
        }

        #region 挂起
        /// <summary>
        /// 挂起
        /// </summary>
        protected void Suspend()
        {
            SuspendLayout();
        }
        /// <summary>
        /// 取消挂起
        /// </summary>
        /// <param name="performLayout">立即执行</param>
        protected void Resume(bool performLayout = false)
        {
            ResumeLayout(performLayout);
        }
        #endregion

        #region 控制

        /// <summary>
        /// 只读模式
        /// </summary>
        public bool ReadOnly { get; protected set; }
        /// <summary>
        /// 设置只读模式
        /// </summary>
        /// <param name="b"></param>
        public virtual void SetReadOnlyMode(bool b)
        {
            ReadOnly = b;
            foreach (T item in Items)
            {
                item.SetReadOnlyMode(b);
            }
        }

        /// <summary>
        /// 实例化一个列表项对象, 默认实现返回null
        /// </summary>
        /// <returns></returns>
        protected virtual T InstanceItem()
        {
            return null;
        }

        /// <summary>
        /// 取得ID列表
        /// </summary>
        /// <typeparam name="TID"></typeparam>
        /// <returns></returns>
        public List<TID> GetIds<TID>()
        {
            List<TID> output = new List<TID>();
            foreach(T item in Items)
            {
                if (item.ID is TID id)
                {
                    output.Add(id);
                }
            }
            return output;
        }

        /// <summary>
        /// 根据ID获取控件
        /// </summary>
        /// <typeparam name="TID"></typeparam>
        /// <returns></returns>
        public T GetById<TID>(TID id)
        {
            foreach (T item in Items)
            {
                if (item.ID is TID _id && _id.Equals(id))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 列表项
        /// </summary>
        protected readonly List<T> Items = new List<T>();
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="addToHead">添加到头部</param>
        public void AddControl(T control, bool addToHead = false)
        {
            if (addToHead)
            {
                Items.Insert(0, control);
            }
            else
            {
                Items.Add(control);
            }
            AddControlFact(control);

            UpdateInnerAreaBound();
        }
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="controls"></param>
        public void AddControl(List<T> controls)
        {
            if (controls != null)
            {
                Items.AddRange(controls);
                List<Color> backColors = new List<Color>();
                foreach (T control in controls)
                {
                    backColors.Add(control.BackColor);
                }
                InnerArea.Controls.AddRange(controls.ToArray());
                for (int i = 0; i < controls.Count; i++)
                {
                    controls[i].BackColor = backColors[i];
                    controls[i].SetToolTip(toolTip);
                    controls[i].SetReadOnlyMode(ReadOnly);
                }
                UpdateInnerAreaBound();
            }
        }
        private void AddControlFact(T control)
        {
            Color backColor = control.BackColor;
            InnerArea.Controls.Add(control);
            control.SetToolTip(toolTip);
            control.BackColor = backColor;
            control.SetReadOnlyMode(ReadOnly);
        }


        /// <summary>
        /// 遍历所有控件列表项执行指定操作
        /// </summary>
        /// <param name="operation"></param>
        public void Traverse(TraverseDelegate operation)
        {
            if (operation == null) return;
            foreach (T control in Items)
            {
                operation.Invoke(control);
            }
        }
        /// <summary>
        /// 遍历操作的委托
        /// </summary>
        /// <param name="control"></param>
        public delegate void TraverseDelegate(T control);

        /// <summary>
        /// 移除列表项
        /// </summary>
        /// <param name="control"></param>
        public void RemoveControl(T control)
        {
            RemoveControlById(control.ID);
            _AutoInvokeUpdateInnerAreaBound();
        }
        /// <summary>
        /// 移除除了指定ID之外的其他列表项
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveControlExceptId(object ID)
        {
            int index = Items.FindIndex((item) => item.ID == ID);
            if (index >= 0 && Items.Count > 0)
            {
                for (int i = Items.Count - 1; i >= 0; i--)
                {
                    if (index != i)
                    {
                        T item = Items[i];
                        Items.RemoveAt(i);
                        InnerArea.Controls.Remove(item);
                    }
                }
            }
            else
            {
                Items.Clear();
                InnerArea.Controls.Clear();
            }
            _AutoInvokeUpdateInnerAreaBound();
        }
        /// <summary>
        /// 根据ID移除列表项
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveControlById(object ID)
        {
            T exist = Items.FirstOrDefault(item => item.ID.Equals(ID));
            if (exist != null)
            {
                Items.Remove(exist);
                InnerArea.Controls.Remove(exist);
                OnItemRemove?.Invoke(new T[] { exist });
                _AutoInvokeUpdateInnerAreaBound();
            }
        }
        /// <summary>
        /// 将第一个ID为输入值的列表项向后移动一个位置
        /// </summary>
        /// <param name="ID"></param>
        public void MoveToNext(object ID)
        {
            _MoveToNext(ID);
            _AutoInvokeUpdateInnerAreaBound();
        }
        private void _MoveToNext(object ID)
        {
            int index = Items.FindIndex((item) => item.ID == ID);
            if (index >= 0 && index != Items.Count - 1)
            {// 存在, 且不是最后一个, 交换与下一个的位置
                T temp = Items[index];
                Items[index] = Items[index + 1];
                Items[index + 1] = temp;
            }
        }
        /// <summary>
        /// 将第一个ID为输入值的列表项向后移动最后面
        /// </summary>
        /// <param name="ID"></param>
        public void MoveToLatest(object ID)
        {
            _MoveToLatest(ID);
            _AutoInvokeUpdateInnerAreaBound();
        }
        /// <summary>
        /// 仅将第一个ID为输入值的列表项向后移动最后面
        /// </summary>
        /// <param name="ID"></param>
        private void _MoveToLatest(object ID)
        {
            int index = Items.FindIndex((item) => item.ID == ID);
            if (index >= 0 && index != Items.Count - 1)
            {
                T temp = Items[index];
                Items.RemoveAt(index);
                Items.Add(temp);
            }
        }
        /// <summary>
        /// 将第一个ID为输入值的列表项向后移动, 直到输入的方法执行后返回了true
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="until"></param>
        public void MoveToNext(object ID, ListItemComparisonDelegate until)
        {
            if (until == null)
            {// 移动到下个位置
                MoveToNext(ID);
            }
            else
            {// 移动到下个位置, 直到判断结果是true
                int index = Items.FindIndex((item) => item.ID == ID);
                if (index >= 0)
                {// 存在
                    while (index != Items.Count - 1 && !until.Invoke(Items[index], Items[index + 1]))
                    {// 如果 索引还不是最后一个 而且 输入方法的执行结果是false
                        T temp = Items[index];
                        Items[index] = Items[index + 1];
                        Items[index + 1] = temp;
                        index++;
                    }
                }
            }
            _AutoInvokeUpdateInnerAreaBound();
        }
        private void _AutoInvokeUpdateInnerAreaBound()
        {
            this.AutoInvoke(() =>
            {
                UpdateInnerAreaBound();
            });
        }
        /// <summary>
        /// 移除列表项
        /// </summary>
        /// <param name="controls"></param>
        public void RemoveControl(params T[] controls)
        {
            if (controls != null)
            {
                foreach (T t in controls)
                {
                    T exist = Items.FirstOrDefault(item => item.ID.Equals(t.ID));
                    if (exist != null)
                    {
                        Items.Remove(exist);
                        InnerArea.Controls.Remove(exist);
                    }
                }
                OnItemRemove?.Invoke(controls);
                UpdateInnerAreaBound();
            }
        }
        /// <summary>
        /// 移除列表项
        /// </summary>
        /// <param name="controls"></param>
        public void RemoveControl(IEnumerable<T> controls)
        {
            if (controls != null)
            {
                T[] controlsArr = controls.ToArray();
                RemoveControl(controlsArr);
            }
        }
        /// <summary>
        /// 移除所有列表项
        /// </summary>
        public virtual void Clear()
        {
            InnerArea.Controls.Clear();
            Items.Clear();
            UpdateInnerAreaBound();
            InnerAreaOffset = new Point();
        }
        public void UpdateItemBound()
        {
            // 取得原有偏移
            Point offset = InnerAreaOffset;
            // 更新内部区域的大小
            UpdateInnerAreaBound();
            // 试图引用原有偏移为现有偏移
            InnerAreaOffset = offset;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 列表项比较的委托
        /// </summary>
        /// <param name="input"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public delegate bool ListItemComparisonDelegate(T input, T comparison);

        /// <summary>
        /// 列表项移除委托
        /// </summary>
        /// <param name="items"></param>
        public delegate void OnItemRemoveDelegate(T[] items);
        /// <summary>
        /// 列表项移除事件
        /// </summary>
        [Bindable(true)]
        public event OnItemRemoveDelegate OnItemRemove;

        /// <summary>
        /// 滚动到了底部委托
        /// </summary>
        public delegate void OnScrollBottomDelegate();
        /// <summary>
        /// 滚动到了底部事件
        /// </summary>
        public event OnScrollBottomDelegate OnScrollBottom;
        /// <summary>
        /// 限制滚动到底部的事件触发
        /// </summary>
        public bool LimitScrollButtomEvent { get; set; } = false;
        #endregion




        #region 属性更新

        /// <summary>
        /// 更新表格布局的单元格尺寸
        /// </summary>
        private void UpdateTableLayoutCellSize()
        {
            switch (showScrollBar)
            {
                case ShowScrollBarEnum.None:
                    TableLayout.ColumnStyles[1].Width = 0;
                    TableLayout.RowStyles[1].Height = 0;
                    break;
                case ShowScrollBarEnum.All:
                    TableLayout.ColumnStyles[1].Width = scrollBarWidth;
                    TableLayout.RowStyles[1].Height = scrollBarWidth;
                    break;
                case ShowScrollBarEnum.Horizontal:
                    TableLayout.ColumnStyles[1].Width = 0;
                    TableLayout.RowStyles[1].Height = scrollBarWidth;
                    break;
                case ShowScrollBarEnum.Vertical:
                    TableLayout.ColumnStyles[1].Width = scrollBarWidth;
                    TableLayout.RowStyles[1].Height = 0;
                    break;
            }
        }
        /// <summary>
        /// 更新内部区域及列表项的大小及位置
        /// </summary>
        private void UpdateInnerAreaBound()
        {
            if (Initing)
            {
                return;
            }

            int totalSize = 0;
            int count = Items.Count;
            int gapSize = Gap * (count + 1);
            int innerMaxWidth = GetInnerMaxWidth();
            int innerMaxHeight = GetInnerMaxHeight();
            int index = 0;
            int innerWidth = InnerArea.Width;
            int innerHeight = InnerArea.Height;
            Point[] finalLocations = new Point[Items.Count];
            Size[] finalSizes = new Size[Items.Count];
            T[] items = new T[Items.Count];
            switch (ControlOrientation)
            {
                case OrientationEnum.Vertical:
                    int maxHeight = 0;
                    foreach (T item in Items)
                    {
                        if (item.Visible)
                        {
                            items[index] = item;
                           
                            int finalWidth = item.Width;
                            int finalHeight = item.Height;
                            int finalX = item.Location.X;
                            int finalY = item.Location.Y;

                            if (item.AutoWidth)
                            {
                                if (item.KeepScale && finalHeight != 0)
                                {
                                    float scale = (float)finalWidth / finalHeight;
                                    finalHeight = innerMaxHeight;
                                    finalWidth = (int)(scale * finalHeight);
                                }
                                else
                                {
                                    finalHeight = innerMaxHeight;
                                }
                            }
                            if (finalHeight > maxHeight)
                            {
                                maxHeight = finalHeight;
                            }
                            finalX = totalSize + gap * (index + 1);
                            finalY = gap;
                            // 保存最终的尺寸及位置
                            finalSizes[index] = new Size(finalWidth, finalHeight);
                            finalLocations[index] = new Point(finalX, finalY);
                            // 更新总尺寸及索引
                            totalSize += finalWidth;
                            index++;
                        }
                    }
                    innerWidth = totalSize + gapSize;
                    innerHeight = maxHeight + gap * 2;
                    break;
                case OrientationEnum.Horizontal:
                    int maxWidth = 0;
                    foreach (T item in Items)
                    {
                        if (item.OccupySize)
                        {
                            items[index] = item;

                            int finalWidth = item.Width;
                            int finalHeight = item.Height;
                            int finalX = item.Location.X;
                            int finalY = item.Location.Y;

                            if (item.AutoWidth)
                            {
                                if (item.KeepScale && finalWidth != 0)
                                {
                                    float scale = (float)finalHeight / finalWidth;
                                    finalWidth = innerMaxWidth;
                                    finalHeight = (int)(scale * finalWidth);
                                }
                                else
                                {
                                    finalWidth = innerMaxWidth;
                                }
                            }
                            if (finalWidth > maxWidth)
                            {
                                maxWidth = finalWidth;
                            }
                            finalX = gap;
                            finalY = totalSize + gap * (index + 1);
                            // 保存最终的尺寸及位置
                            finalSizes[index] = new Size(finalWidth, finalHeight);
                            finalLocations[index] = new Point(finalX, finalY);
                            // 更新总尺寸及索引
                            totalSize += finalHeight;
                            index++;
                        }
                    }
                    innerWidth = maxWidth + gap * 2;
                    innerHeight = totalSize + gapSize;
                    break;
            }
            // Suspend();
            // 更新内部显示区域的尺寸
            if (innerWidth != InnerArea.Width || innerHeight != InnerArea.Height)
            {
                InnerArea.Size = new Size(innerWidth, innerHeight);
            }
            // 更新列表项的尺寸或位置
            for (int i = 0; i < index; i++)
            {
                // 更新列表项的尺寸
                if (items[i].Width != finalSizes[i].Width || items[i].Height != finalSizes[i].Height)
                {
                    items[i].Size = new Size(finalSizes[i].Width, finalSizes[i].Height);
                }
                // 更新列表项的坐标
                if (items[i].Location.X != finalLocations[i].X || items[i].Location.Y != finalLocations[i].Y)
                {
                    items[i].Location = new Point(finalLocations[i].X, finalLocations[i].Y);
                }
            }
            // Resume();
        }
        /// <summary>
        /// 更新内部区域的偏移
        /// </summary>
        private void UpdateInnerAreaOffset()
        {
            Size innerSize = InnerAreaSize;
            Size showingSize = ShowingAreaSize;
            Point offset = innerAreaOffset;
            if (innerSize.Width <= showingSize.Width)
            {
                // offset.X = (showingSize.Width - innerSize.Width) / 2;
                offset.X = 0;
            }
            else
            {
                if (offset.X > 0)
                {
                    offset.X = 0;
                }
                else if (offset.X + innerSize.Width < showingSize.Width)
                {
                    offset.X = showingSize.Width - innerSize.Width;
                }
            }
            if (innerSize.Height <= showingSize.Height)
            {
                // offset.Y = (showingSize.Height - innerSize.Height) / 2;
                offset.Y = 0;
            }
            else
            {
                if (offset.Y > 0)
                {
                    offset.Y = 0;
                }
                else if (offset.Y + innerSize.Height < showingSize.Height)
                {
                    offset.Y = showingSize.Height - innerSize.Height;
                }
            }
            // 更新偏移量
            innerAreaOffset = offset;
        }
        #endregion

        #region 重载
        #region 弃用的代码
        /*
        /// <summary>
        /// 重设尺寸的启动任务
        /// </summary>
        private Task ResizeTask;
        /// <summary>
        /// 重设尺寸的需要的冷却时间
        /// </summary>
        private float ResizeNeedTime;
        /// <summary>
        /// 重设尺寸时的冷却计时器
        /// </summary>
        private Util.TimeClock ResizeTimeClock;
        /// <summary>
        /// 冷却时间共0.5秒
        /// </summary>
        private float ResizeCoolingTime = 0.5f;
        */
        #endregion
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Visible)
            {
                UpdateInnerAreaBound();
                UpdateInnerAreaOffset();
            }
            #region 弃用的代码
            /*if (ResizeTask != null && !ResizeTask.IsCompleted)
            {// 如果任务未完成, 修改需要的冷却时间为线程运行时间 + ResizeCoolingTime;
                if (ResizeTimeClock != null)
                {
                    //Console.WriteLine("添加需要的时间");
                    ResizeNeedTime = (float)ResizeTimeClock.ElapseTime + ResizeCoolingTime;
                }
                else
                {
                    //Console.WriteLine("更新需要的时间");
                    ResizeNeedTime = ResizeCoolingTime;
                }
            }
            else
            {// 任务已完成
                ResizeTask = new Task(() =>
                {
                    //Console.WriteLine("启动重设尺寸的任务");

                    ResizeTimeClock = new Util.TimeClock(Util.TimeClock.TimerMode.Normal);
                    ResizeTimeClock.Start();
                    ResizeNeedTime = ResizeCoolingTime;  // 初始化需要的冷却时间
                    while (ResizeTimeClock.ElapseTime < ResizeNeedTime) 
                    {// 计时器运行的总时间比需要的冷却时间多, 则休眠一小段时间
                        // 休眠200毫秒
                        // Console.WriteLine($"休眠 {ResizeTimeClock.ElapseTime} : {ResizeNeedTime} ");
                        Task.Delay(200);
                    }
                    ResizeTimeClock.Stop();
                    ResizeTimeClock = null;
                    this.AutoInvoke(() =>
                    {
                        //Console.WriteLine("重设尺寸");
                        base.OnResize(e);
                        UpdateInnerAreaBound();
                        UpdateInnerAreaOffset();
                    });
                });
                ResizeTask.Start();
            }*/
            #endregion
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is T c)
            {
                Controls.Remove(e.Control);
                AddControl(c);
            }
        }
        
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Point offset = InnerAreaOffset;
            int delta = (int)(e.Delta * ScrollSpeed);
            switch (ControlOrientation)
            {
                case OrientationEnum.Horizontal:
                    InnerAreaOffset = new Point(offset.X, offset.Y + delta);
                    break;
                case OrientationEnum.Vertical:
                    InnerAreaOffset = new Point(offset.X + delta, offset.Y);
                    break;
            }
            if (!LimitScrollButtomEvent && delta < 0)
            {
                // 不限制滚动到底部的事件触发
                int innerSize;
                int showingSize;
                int offsetValue;
                if (ControlOrientation == OrientationEnum.Horizontal)
                {// 组件垂直排列
                    innerSize = InnerAreaSize.Height;
                    showingSize = ShowingAreaSize.Height;
                    offsetValue = innerAreaOffset.Y;
                }
                else
                {// 组件水平排列
                    innerSize = InnerAreaSize.Width;
                    showingSize = ShowingAreaSize.Width;
                    offsetValue = innerAreaOffset.X;
                }
                if (innerSize < showingSize)
                {// 内部区域比显示区域小
                    OnScrollBottom?.Invoke();
                }
                else
                {// 内部区域比显示区域大
                    if (innerSize + offsetValue <= showingSize)
                    {// 内部区域的底部位于显示区域的底部或底部之前
                        OnScrollBottom?.Invoke();
                    }
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                UpdateInnerAreaBound();
            }
        }

        #endregion
        #region 控件事件
        private void TableLayout_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            float regionWidth = TableLayout.ColumnStyles[e.Column].Width;
            float regionHeight = TableLayout.RowStyles[e.Row].Height;
            float pieSizeV = regionWidth > 4 ? regionWidth - 2 : regionWidth;   // 垂直滚动轴两端圆球的尺寸
            float pieSizeH = regionHeight > 4 ? regionHeight - 2 : regionHeight;// 水平滚动轴两端圆球的尺寸 
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            using (SolidBrush brush = new SolidBrush(ScrollColor))
            {
                switch (e.Column)
                {
                    case 0:
                        switch (e.Row)
                        {
                            case 0:
                                // (0, 0)
                                break;
                            case 1:
                                // (0, 1)
                                if (regionHeight > 0)
                                {
                                    int gap = (int)((regionHeight - pieSizeH) / 2);
                                    e.Graphics.FillPie(
                                        brush,  // 画刷
                                        e.CellBounds.X + gap, e.CellBounds.Y + gap, // 位置
                                        pieSizeH, pieSizeH, // 大小
                                        0, 360);    // 角度
                                    e.Graphics.FillPie(
                                        brush,  // 画刷
                                        e.CellBounds.X + e.CellBounds.Width - regionHeight + gap, e.CellBounds.Y + gap, // 位置
                                        pieSizeH, pieSizeH, // 大小
                                        0, 360);    // 角度
                                    float srcollBoxAreaWidth = e.CellBounds.Width - regionHeight * 2;
                                    float srcollBoxAreaHeight = regionHeight;
                                    e.Graphics.FillRectangle(
                                        brush,
                                        new RectangleF()
                                        {
                                            X = regionHeight + e.CellBounds.X,
                                            Y = (regionHeight - regionHeight / 6) / 2 + e.CellBounds.Y,
                                            Width = srcollBoxAreaWidth,
                                            Height = regionHeight / 6,
                                        });
                                    if (ShowingArea.Width > 0)
                                    {
                                        // 计算宽度占比
                                        float ratio = (float)InnerArea.Width / ShowingArea.Width;
                                        float scale = srcollBoxAreaWidth / InnerArea.Width;
                                        if (ratio > 1)
                                        {
                                            e.Graphics.FillRectangle(
                                                brush,
                                                new RectangleF()
                                                {
                                                    X = regionHeight + e.CellBounds.X
                                                        + (-InnerAreaOffset.X * scale),
                                                    Y = (srcollBoxAreaHeight - srcollBoxAreaHeight * 0.6f) / 2 + e.CellBounds.Y,
                                                    Width = srcollBoxAreaWidth / ratio,
                                                    Height = srcollBoxAreaHeight * 0.6f,
                                                });
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 1:
                        switch (e.Row)
                        {
                            case 0:
                                // (1, 0)
                                if (regionWidth > 0)
                                {
                                    int gap = (int)((regionWidth - pieSizeV) / 2);
                                    e.Graphics.FillPie(
                                        brush,  // 画刷
                                        e.CellBounds.X + gap, e.CellBounds.Y, // 位置
                                        pieSizeV, pieSizeV,   // 大小
                                        0, 360);    // 角度
                                    e.Graphics.FillPie(
                                        brush,  // 画刷
                                        e.CellBounds.X + gap, e.CellBounds.Y + e.CellBounds.Height - regionWidth + gap, // 位置
                                        pieSizeV, pieSizeV,   // 大小
                                        0, 360);    // 角度
                                    float srcollBoxAreaWidth = regionWidth;
                                    float srcollBoxAreaHeight = e.CellBounds.Height - regionWidth * 2;
                                    e.Graphics.FillRectangle(
                                        brush,
                                        new RectangleF()
                                        {
                                            X = (regionWidth - regionWidth / 8) / 2 + e.CellBounds.X,
                                            Y = regionWidth + e.CellBounds.Y,
                                            Width = regionWidth / 8,
                                            Height = srcollBoxAreaHeight,
                                        });
                                    if (ShowingArea.Height > 0)
                                    {
                                        // 计算高度占比
                                        float ratio = (float)InnerArea.Height / ShowingArea.Height;
                                        float scale = srcollBoxAreaHeight / InnerArea.Height;
                                        if (ratio > 1)
                                        {
                                            e.Graphics.FillRectangle(
                                                brush,
                                                new RectangleF()
                                                {
                                                    X = (srcollBoxAreaWidth - srcollBoxAreaWidth * 0.6f) / 2 + e.CellBounds.X,
                                                    Y = regionWidth + e.CellBounds.Y
                                                        + (-InnerAreaOffset.Y * scale),
                                                    Width = srcollBoxAreaWidth * 0.6f,
                                                    Height = srcollBoxAreaHeight / ratio,
                                                });
                                        }
                                    }
                                }
                                break;
                            case 1:
                                // (1, 1)
                                break;
                        }
                        break;
                }
            }
        }
        #endregion

    }
}
