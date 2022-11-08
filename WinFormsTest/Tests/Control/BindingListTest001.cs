using ChaoticWinformControl.DataContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util.Random;

namespace WinFormsTest.Tests
{
    public partial class BindingListTest001 : TestFormBase
    {
        public BindingListTest001()
        {
            InitializeComponent();
        }
        static SortableBindingList<Model> List = new SortableBindingList<Model>();

        public override void TestContent()
        {
            base.TestContent();

            dataGridView1.DataSource = List;
            RecreateList();
        }
        private void RecreateList()
        {
            List<Model> temp = RandomObjectHelper.GetList<Model>();
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].ID = i;
            }

            List.ResetAs(temp);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RecreateList();
        }


        class Model
        {
            

            public int ID { get; set; }
            public string? Value { get; set; }


            public int Index 
            {
                get
                {
                    for (int i = 0; i < List.Count; i++)
                    {
                        Model model = List[i];
                        if (ID == model.ID && Value == model.Value)
                        {
                            return i;
                        }
                    }
                    return -1;
                } 
            }
            public int  IndexOfItem
            {
                get
                {
                    Model item = List.SingleOrDefault(i => i.ID == ID);
                    return List.IndexOf(item);
                }
            }
        }

    }
}
