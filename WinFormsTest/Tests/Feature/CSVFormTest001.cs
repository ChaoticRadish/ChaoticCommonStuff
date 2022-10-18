using ChaoticWinformControl.FeatureGroup;
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
    public partial class CSVFormTest001 : TestFormBase
    {
        public CSVFormTest001()
        {
            InitializeComponent();
        }
        List<Model.TestModel001>? Data;

        public override void TestContent()
        {
            base.TestContent();

            Data = Model.TestModel001.Random();
            DataShower.DataSource = Model.TestModel001.Random();
        }

        private void ExportForm_Click(object sender, EventArgs e)
        {
            CSVExportForm form = new CSVExportForm();
            form.SetData(Data);
            form.Show(this);
        }
    }
}
