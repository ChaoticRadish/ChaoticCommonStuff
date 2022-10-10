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
    public partial class EnumComboBoxTest001 : TestFormBase
    {
        public EnumComboBoxTest001()
        {
            InitializeComponent();

            enumComboBox1.InitAsName<TestEnum>();
        }

        public override void TestContent()
        {
            base.TestContent();
        }
        protected override List<NeedMoitoringItem> GetNeedMoitorings()
        {
            var output = base.GetNeedMoitorings();
            output.AddRange(NeedMoitoringItem.From("ComboBox", enumComboBox1,
                nameof(enumComboBox1.SelectedString)
                ));
            return output;
        }

        public enum TestEnum
        {
            增,
            删,
            改,
            查
        }
    }
}
