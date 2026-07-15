using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
          //  using var udpClient = new UdpClient(portShits);
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
                  //  MessageBox.Show(ip.Text);
                    connectStatus.Content = "CN";
                   // listenClient = new UdpClient(listeningPort);
                   // Task.Run(() => ReceiveAsync(cts.Token) );
                      if (receivingUdpClient == null)
                    {
                        receivingUdpClient = new UdpClient(11005);
                        receivingUdpClient.Client.ReceiveTimeout = 5000;
                    }
                    SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("connect"));
                    receiveUdp();
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
                    receiveUdp();

                    if (controlStatus.Content.Equals("OP")) {
                        controlStatus.Content = "AC";   
                    }
                    else {
                        controlStatus.Content = "OP";
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


        void receiveUdp()
        {
            if (receivingUdpClient != null)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                try
                {

                    // Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.ASCII.GetString(receiveBytes);

                    MessageBox.Show(returnData);
                    //  Console.WriteLine("This is the message you received " +
                    //                          returnData.ToString());
                    //    receivingUdpClient.Close();
                    //receivingUdpClient.EndReceive();
                    //   receivingUdpClient.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    MessageBox.Show(e.ToString());
                }
            }
        }
         void SendUdp(int srcPort, string dstIp, int dstPort, byte[] data)
        {
            //  deviceClient.Send();
            using (UdpClient c = new UdpClient(srcPort))
            {
                // UdpClient deviceClient = new UdpClient();
                //  if (deviceClient!=null) {
                c.Send(data, data.Length, dstIp, dstPort);
                //deviceClient1
                // _ = _deviceClient.Send(data, data.Length, dstIp, dstPort);
                // }
                // while (true)
            }

            //Creates a UdpClient for reading incoming data.

            //Creates an IPEndPoint to record the IP Address and port number of the sender.
            // The IPEndPoint will allow you to read datagrams sent from any source.

          

            // using (UdpClient c = new UdpClient(11001))
            /*   {
                   UdpReceiveResult result =// await c.ReceiveAsync();
                       await listenClient.ReceiveAsync();
                       String a = Encoding.UTF8.GetString(result.Buffer);
                       if (a.Equals("motionAlarm")) {
                           MessageBox.Show("HELP!!!");
                       }
                     //  var remoteEP = new IPEndPoint(IPAddress.Any, portShits);
                     //  var data1 = c.Receive(ref remoteEP); // listen on port 11000
                   }
               }
           /* UdpClient udpServer = new UdpClient(portShits);

            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, portShits);
                var data1 = udpServer.Receive(ref remoteEP); // listen on port 11000
             //   Console.Write("receive data from " + remoteEP.ToString());
             //   udpServer.Send(new byte[] { 1 }, 1, remoteEP); // reply back
            }*/
        }
        UdpClient receivingUdpClient = null;// = new UdpClient();
        public UdpClient listenClient;// = new UdpClient();
        public CancellationTokenSource cts;// = new CancellationTokenSource();
        String deviceIP = null;
        int portShits = 11000;
        int listeningPort = 11001;

    }
}
