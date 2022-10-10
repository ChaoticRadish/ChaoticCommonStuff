using ChaoticWinformControl;
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
    public partial class InputFormFrameTest001 : TestFormBase
    {
        public InputFormFrameTest001()
        {
            InitializeComponent();

            textBox = new TextBox();
        }

        public override void TestContent()
        {
            base.TestContent();

        }
        protected override List<NeedMoitoringItem> GetNeedMoitorings()
        {
            var output = base.GetNeedMoitorings();
            output.AddRange(NeedMoitoringItem.From("输入", textBox!,
                nameof(textBox.Text)
                ));
            return output;
        }

        public TextBox? textBox { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.OK,
            };
            frame.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.OKCancel,
            };
            frame.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.YesNoCancel,
            };
            frame.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.YesNoCancel,
            };
            frame.SetBody<TextBox>();
            frame.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.YesNoCancel,
            };
            frame.SetBody(textBox);
            frame.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InputFormFrame frame = new InputFormFrame()
            {
                Buttons = MessageBoxButtons.OK,
                Text = "富文本输入!",
                HintText = "在 这 输 入 !",
            };
            frame.SetBody<RichTextBox>();
            frame.ShowDialog(this);
        }
    }
}
