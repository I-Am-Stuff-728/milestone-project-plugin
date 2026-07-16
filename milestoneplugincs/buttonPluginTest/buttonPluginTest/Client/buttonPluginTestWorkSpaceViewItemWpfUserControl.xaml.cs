using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NAudio.CoreAudioApi;
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
        //
        //ip.Text = loggingEvent;
            String[] split = sender.ToString().Split(':');
            String command = split[split.Length - 1].Trim().ToLower();

           // MessageBox.Show(command);

            switch (command) {
                case "connect":
                    deviceIP = ip.Text.Trim();

                    if (receivingUdpClient == null)
                    {
                        //   cts = new CancellationTokenSource();
                        receivingUdpClient = new UdpClient(listeningPort);
                        receivingUdpClient.Client.ReceiveTimeout = 5000;
                        //    receivingUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                        //    receivingUdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, listeningPort));

                        _ = listenUdp();

                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("connect"));

                        /*
                         * Exception type:System.Net.Sockets.SocketException
Exception message:An invalid argument was supplied
Exception source:System
Exception Target Site: DoBind
   at System.Net.Sockets.Socket.DoBind(EndPoint endPointSnapshot, SocketAddress socketAddress)
   at System.Net.Sockets.Socket.Bind(EndPoint localEP)

                        Exception type:System.Net.Sockets.SocketException
Exception message:No such host is known
Exception source:System

                        */
                    }
                    else {
                        connect1.Content = "Disonnect";
                    }


                        // receiveUdp();
                        break;

                case "disconnect":
                    cts.Cancel();
                    cts.Dispose();
                    cts = null;

                    receivingUdpClient.Close();
                    receivingUdpClient.Dispose();
                    receivingUdpClient = null;

                    connect1.Content = "Connect";
                    deviceIP = null;

                    break;

                case "left":
                    if (//deviceClient != null &&
                        deviceIP != (null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("left"));
                    }
                    break;
                case "right":
                    if (//deviceClient != null && 
                        deviceIP != (null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("right"));
                    }
                    break;
                case "toggle":
                    if (//deviceClient != null &&
                        deviceIP != (null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("toggle"));
                    }
                 //   receiveUdp();
                 /*
                    if (controlStatus1.Content.Equals("OP")) {
                        controlStatus1.Content = "AC";   
                    }
                    else {
                        controlStatus1.Content = "OP";
                    }*/
                    break;
                case "activate":
                    if (//deviceClient != null &&
                        deviceIP != (null))
                    {
                        SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("trigger"));
                    }
                    break;

            }
            //HERE FOR BUTTOn
            //SendUdp(portShits, deviceIP, portShits, Encoding.ASCII.GetBytes("cmd1"));
        }

        /*
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
        }*/

                        void SendUdp(int srcPort, string dstIp, int dstPort, byte[] data)
        {
            using (UdpClient c = new UdpClient(srcPort))
            {
                c.Send(data, data.Length, dstIp, dstPort);
            }

        
        }

        // static string loggingEvent = "";

        // public static void UDPListener()
        //  {
        private async Task listenUdp()
       // {

         //   Task.Run(async () =>
            {
          //  if (receivingUdpClient == null) return;
            cts = new CancellationTokenSource();

            // using (var udpClient = new UdpClient(11000))
            //try
            {
                String loggingEvent = "";
               // while (!cts.Token.IsCancellationRequested)
               while (true)
                {
                    //IPEndPoint object will allow us to read datagrams sent from any source.
                    var receivedResults = await receivingUdpClient.ReceiveAsync();
                    loggingEvent = Encoding.ASCII.GetString(receivedResults.Buffer);
                    //frontStatus1.Content = "Front: N";
                    //motionStatus1.Content = "Motion: N";

                    switch (loggingEvent)
                    {
                        case "connect success":
                            connect1.Content = "Disconnect";
                            MessageBox.Show("Connected!");
                            break;
                        case "frontAlarm":
                            frontStatus1.Content = "Front: Y";
                            break;
                        case "moveAlarm":
                            motionStatus1.Content = "Motion: Y";
                            break;
                        case "frontAlarmOff":
                            frontStatus1.Content = "Front: X";
                            break;
                        case "moveAlarmOff":
                            motionStatus1.Content = "Motion: X";
                            break;
                        case "autocontrol":
                            controlStatus1.Content = "AutoContr: Y";
                            break;
                        case "operator":
                            controlStatus1.Content = "AutoContr: X";
                            break;

                    }
                    //ip.Text = ""+loggingEvent;
                    // MessageBox.Show("AIUFHADUID");
                    //MessageBox.Show(loggingEvent);
                   // connectStatus.Content = loggingEvent;
                    // setText(loggingEvent);
                    //  connectStatus.Content = loggingEvent;

                }
              //  cts = null;
               // receivingUdpClient = null;

                /*   cts.Cancel();
                   cts.Dispose();
                   cts = null;
                */
            }


            /* finally {

              *  receivingUdpClient.Close();
         receivingUdpClient.Dispose();
         receivingUdpClient = null;
               }
             */
            //   });
        }


        static UdpClient receivingUdpClient = null;// = new UdpClient();
        public UdpClient listenClient;// = new UdpClient();
        public CancellationTokenSource cts;// = new CancellationTokenSource();
        String deviceIP = null;
        int portShits = 11000;
        int listeningPort = 11005;

    }
}
