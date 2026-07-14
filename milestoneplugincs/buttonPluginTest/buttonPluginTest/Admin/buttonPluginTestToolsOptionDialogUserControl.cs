using VideoOS.Platform.Admin;

namespace buttonPluginTest.Admin
{
    public partial class buttonPluginTestToolsOptionDialogUserControl : ToolsOptionsDialogUserControl
    {
        public buttonPluginTestToolsOptionDialogUserControl()
        {
            InitializeComponent();
        }

        public override void Init()
        {
        }

        public override void Close()
        {
        }

        public string MyPropValue
        {
            set { textBoxPropValue.Text = value ?? ""; }
            get { return textBoxPropValue.Text; }
        }

        private void label1_Click(object sender, System.EventArgs e)
        {
           // e.Equals(textBoxPropValue.Text);
        }
    }
}
