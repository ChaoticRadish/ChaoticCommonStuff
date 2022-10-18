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
using WinFormsTest.Model;

namespace WinFormsTest.Tests
{
    public partial class CSVFormTest001 : TestFormBase
    {
        public CSVFormTest001()
        {
            InitializeComponent();
        }
        BindingList<Model.MiniModel001>? Data;

        public override void TestContent()
        {
            base.TestContent();

            Data = new BindingList<Model.MiniModel001>(Model.MiniModel001.Random());
            DataShower.DataSource = Data;
        }

        private void ExportForm_Click(object sender, EventArgs e)
        {
            CSVExportForm form = new CSVExportForm();
            form.SetData(Data);
            form.Show(this);
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            CSVImportForm form = new CSVImportForm();
            form.SetTargetType(typeof(Model.MiniModel001));
            form.Importing += Form_Importing;
            form.Show(this);
        }

        private bool Form_Importing(object importObj, CSVImportForm.LogController logController)
        {
            logController.Log("写入", "写入对象到dgv");
            Data!.Add((MiniModel001)importObj);
            Thread.Sleep(500);
            return true;
        }

        private void AddTestButton_Click(object sender, EventArgs e)
        {
            Data!.Add(Util.Random.RandomObjectHelper.GetObject<MiniModel001>());
            DataShower.Invalidate();
        }
    }
}
