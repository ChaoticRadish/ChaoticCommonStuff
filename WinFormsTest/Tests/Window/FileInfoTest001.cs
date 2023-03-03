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

namespace WinFormsTest.Tests
{
    public partial class FileInfoTest001 : TestFormBase
    {
        public FileInfoTest001()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Items;
        }

        SortableBindingList<Item> Items = new SortableBindingList<Item>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileDialog.FileNames.Select(i => new Item()
                {
                    FileName = i,
                }).ToList().ForEach(i =>
                {
                    try
                    {
                        i.Group = Util.File.FilePropertyHelper.GetCategory(i.FileName);
                        Items.Add(i);
                    }
                    catch
                    {

                    }
                });
                dataGridView1.AutoResizeColumn(0);
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {// 更改
            foreach (Item item in Items)
            {
                Util.File.FilePropertyHelper.SetCategory(item.FileName, item.Group);
            }
        }
        private void FileInfoTest001_Load(object sender, EventArgs e)
        {

        }

        public class Item
        {
            public string FileName { get; set; }
            public string Group { get; set; }
        }

    }
}
