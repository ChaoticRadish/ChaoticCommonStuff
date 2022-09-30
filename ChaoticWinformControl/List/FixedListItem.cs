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
    public partial class FixedListItem<Data> : UserControl
    {
        public FixedListItem()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 正在显示的数据
        /// </summary>
        public Data ShowingData 
        {
            get
            {
                return showingData;
            }
            set
            {
                showingData = value;
                this.AutoInvoke(() =>
                {
                    SetShowing(showingData);
                });
                Visible = showingData != null;
            }
        }
        private Data showingData;


        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="data"></param>
        protected virtual void SetShowing(Data data)
        {
        }


    }
}
