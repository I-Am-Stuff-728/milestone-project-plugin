using System;
using System.Windows;
using VideoOS.Platform.Client;

namespace buttonPluginTest.Client
{
    /// <summary>
    /// Interaction logic for buttonPluginTestWorkSpaceViewItemWpfUserControl.xaml
    /// </summary>
    public partial class buttonPluginTestWorkSpaceViewItemWpfUserControl : ViewItemWpfUserControl
    {
        public buttonPluginTestWorkSpaceViewItemWpfUserControl()
        {
            InitializeComponent();
        }

        public override void Init()
        {
        }

        public override void Close()
        {
        }

        /// <summary>
        /// Do not show the sliding toolbar!
        /// </summary>
        public override bool ShowToolbar
        {
            get { return false; }
        }

        private void ViewItemWpfUserControl_ClickEvent(object sender, EventArgs e)
        {
          //  FireClickEvent();
         //   MessageBox.Show("ASDASD");
        }

        private void ViewItemWpfUserControl_DoubleClickEvent(object sender, EventArgs e)
        {
         //   FireDoubleClickEvent();
         //   MessageBox.Show("hello");
        }

        private void textBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void buttonMin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            String[] split = sender.ToString().Split(':');
            //  MessageBox.Show(sender.GetType()+ "");
            String command = split[split.Length - 1].Trim().ToLower();
            //MessageBox.Show("n"+test+ "c");
          //  MessageBox.Show(e.ToString());
          //  MessageBox.Show(e.GetType()+"");

            switch (command) {
                case "connect":
                    MessageBox.Show("TEST!!!");
                    MessageBox.Show(ip.Text);
                    MessageBox.Show(pass.Text);

                    break;
                case "left":
                    MessageBox.Show("LEFT!!!");
                    break;

            }
            //HERE FOR BUTTOn
        }
    }
}
