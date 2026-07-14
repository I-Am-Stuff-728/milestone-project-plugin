using System;
using System.Net.Sockets;
using System.Text;
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
            String command = split[split.Length - 1].Trim().ToLower();
          
            switch (command) {
                case "connect":
                  //  deviceClient = new UdpClient(portShits);
                    deviceIP = ip.Text;
                    MessageBox.Show(ip.Text);
                    break;
                case "left":
                    if (//deviceClient != null &&
                        !deviceIP.Equals(null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("left"));
                    }
                    break;
                case "right":
                    if (//deviceClient != null && 
                        !deviceIP.Equals(null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("right"));
                    }
                    break;
                case "toggle":
                    if (//deviceClient != null &&
                        !deviceIP.Equals(null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("toggle"));
                    }
                    break;
                case "activate":
                    if (//deviceClient != null &&
                        !deviceIP.Equals(null))
                    {
                        // MessageBox.Show("trigger!!!");
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("trigger"));
                    }
                    break;

            }
            //HERE FOR BUTTOn
            //SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("cmd1"));
        }
        static void SendUdp(int srcPort, string dstIp, int dstPort, byte[] data)
        {
          //  deviceClient.Send();
             using (UdpClient c = new UdpClient(srcPort))
           // UdpClient deviceClient = new UdpClient();
            //  if (deviceClient!=null) {
              c.Send(data, data.Length, dstIp, dstPort);
            //deviceClient1
           // _ = _deviceClient.Send(data, data.Length, dstIp, dstPort);
           // }
        }
        UdpClient deviceClient = new UdpClient();
        String deviceIP = null;
        int portShits = 11000;
    }
}
