using System.ComponentModel;

namespace WinFormsTest
{
    public partial class TestFormBase : Form
    {
        [Browsable(false)]
        public MainForm? MainForm { get; set; } = null;

        public TestFormBase()
        {
            InitializeComponent();
        }
    }
}
