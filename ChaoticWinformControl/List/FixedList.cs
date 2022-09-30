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
    /// <summary>
    /// 单页显示固定数量的列表控件
    /// </summary>
    /// <typeparam name="Item"></typeparam>
    /// <typeparam name="Data"></typeparam>
    public partial class FixedList<Item, Data> : UserControl
        where Item : FixedListItem<Data>, new()
    {
        public FixedList()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 自动管理数量
        /// </summary>
        [Browsable(true), Category("Layout"), Description("是否自动管理数量")]
        public bool AutoManagerCount { get; set; } = false;
        /// <summary>
        /// 尺寸限制
        /// </summary>
        [Browsable(true), Category("Layout"), Description("列表项的尺寸限制")]
        public int ItemMaxSize
        {
            get => itemMaxSize;
            set
            {
                itemMaxSize = value;
                AutoUpdateItemCount();
            }
        }
        private int itemMaxSize = 100;

        /// <summary>
        /// 列表项数量
        /// </summary>
        [Browsable(true), Category("Layout"), Description("列表项数量")]
        public int ItemCount 
        {
            get
            {
                return itemCount;
            }
            set
            {
                itemCount = value;
                UpdateItemCount();
            }
        }
        private int itemCount = 10;
        /// <summary>
        /// 列表项的间距
        /// </summary>
        [Browsable(true), Category("Layout"), Description("列表项的间距")]
        public int Gap
        {
            get
            {
                return gap;
            }
            set
            {
                if (gap != value)
                {
                    gap = value;
                    UpdateInnerSize();
                }
            }
        }
        private int gap = 3;
        /// <summary>
        /// 控件方向
        /// </summary>
        [Browsable(true), Category("Layout"), Description("控件方向")]
        public OrientationEnum Orientation { get; set; } = OrientationEnum.Horizontal;



        #endregion

        #region 数据
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="datas"></param>
        public void SetData(IEnumerable<Data> datas)
        {
            this.AutoInvoke(() =>
            {
                if (datas == null) return;
                int index = 0;
                foreach (Data data in datas)
                {
                    SetData(index, data);
                    index++;
                    if (index == ItemCount)
                    {
                        return;
                    }
                }
                while (index < ItemCount)
                {
                    SetData(index, default);
                    index++;
                }
            });
        }
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void SetData(int index, Data data)
        {
            this.AutoInvoke(() =>
            {
                if (index >= 0 && index < itemCount)
                {
                    Item item = ControlItems[index];
                    if (item != null)
                    {
                        item.ShowingData = data;
                    }
                }
            });
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        public void ClearData()
        {
            this.AutoInvoke(() =>
            {
                foreach (Item item in ControlItems)
                {
                    if (item != null)
                    {
                        item.ShowingData = default;
                    }
                }
            });
        }
        #endregion
        #region 显示
        /// <summary>
        /// 实例化列表控件
        /// </summary>
        /// <returns></returns>
        protected virtual Item InstanceListItem()
        {
            return null;
        }

        /// <summary>
        /// 控件项列表
        /// </summary>
        private List<Item> ControlItems { get; set; }

        /// <summary>
        /// 自动更新列表项数量
        /// </summary>
        private void AutoUpdateItemCount()
        {
            int oriSize = Orientation == OrientationEnum.Horizontal ? Height : Width;
            if (oriSize - gap > ItemMaxSize + gap)
            {
                int count
                    = (oriSize - gap) / (ItemMaxSize + gap)
                    + ((oriSize - gap) % (ItemMaxSize + gap) > 0 ? 1 : 0);
                ItemCount = count;
            }
            else
            {
                ItemCount = 1;
            }
        }
        /// <summary>
        /// 更新列表项数量
        /// </summary>
        private void UpdateItemCount()
        {
            if (ControlItems == null)
            {
                ControlItems = new List<Item>();
            }
            while (ControlItems.Count > ItemCount)
            {
                Item item = ControlItems[ControlItems.Count - 1];
                ControlItems.Remove(item);
                Controls.Remove(item);
            }
            while (ControlItems.Count < ItemCount)
            {
                Item item = InstanceListItem();
                ControlItems.Add(item);
                Controls.Add(item);
            }
            UpdateInnerSize();
        }
        /// <summary>
        /// 更新内容尺寸
        /// </summary>
        public void UpdateInnerSize()
        {
            if (ItemCount <= 0 || ControlItems == null) return;
            // 控件尺寸
            int width = 1;
            int height = 1;

            if (Orientation == OrientationEnum.Horizontal)
            {
                width = Width - gap * 2;
                height = (Height - (ItemCount + 1) * gap) / ItemCount;
            }
            else if (Orientation == OrientationEnum.Vertical)
            {
                height = Height - gap * 2;
                width = (Width - (ItemCount + 1) * gap) / ItemCount;
            }
            // 最小尺寸
            if (width <= 0) width = 1;
            if (height <= 0) height = 1;
            // 应有的尺寸及位置
            Rectangle[] rectangles = new Rectangle[ItemCount];
            int index = 0;
            foreach(Item item in ControlItems)
            {
                if (Orientation == OrientationEnum.Horizontal)
                {
                    rectangles[index] = new Rectangle(gap, gap * (index + 1) + height * index, width, height);
                }
                else if (Orientation == OrientationEnum.Vertical)
                {
                    rectangles[index] = new Rectangle(gap * (index + 1) + width * index, gap, width, height);
                }
                index++;
            }
            // 更新尺寸
            index = 0;
            foreach (Item item in ControlItems)
            {
                if (item == null)
                {
                    continue;
                }
                if (item.Location.X != rectangles[index].X || item.Location.Y != rectangles[index].Y)
                {
                    item.Location = rectangles[index].Location;
                }
                if (item.Width != rectangles[index].Width || item.Height != rectangles[index].Height)
                {
                    item.Size = new Size(rectangles[index].Width, rectangles[index].Height);
                }
                index++;
            }
        }
        #endregion

        #region 事件
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (AutoManagerCount)
            {
                AutoUpdateItemCount();
            }
            else
            {
                UpdateInnerSize();
            }
        }
        #endregion

    }
}
