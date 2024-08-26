using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Coupang
{
    class Connection_Helper
    {
        public static ClientWebSocket2 Connection_Socket;


        public static async void Web_Socket_Client()
        {
            

            Connection_Socket = new ClientWebSocket2();



#if Test_Proxy
            WebProxy proxy = new WebProxy("192.168.231.134", Int32.Parse("8888"));
            Connection_Socket.Options.Proxy = proxy;
#endif

            Connection_Socket.Options.Cookies = Cookies_Class.Cookies;
            Connection_Socket.Options.RequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36");
            Connection_Socket.Options.RequestHeaders.Add("Accept-Encoding", "gzip, deflate, sdch");
            Connection_Socket.Options.RequestHeaders.Add("Accept-Language", "en-US,en;q=0.8");
            Connection_Socket.Options.RequestHeaders.Add("Cache-Control", "no-cache");
            Connection_Socket.Options.RequestHeaders.Add("Origin", "chrome-extension://opbdabemkeebgjjopefgcjjccafieono");
            Connection_Socket.Options.RequestHeaders.Add("Pragma", "no-cache");
            Connection_Socket.Options.RequestHeaders.Add("Sec-WebSocket-Extensions", "permessage-deflate; client_max_window_bits");
            //Connection_Socket.Options.KeepAliveInterval = TimeSpan.FromMinutes(10);


            Random r = new Random();
            string ID1 = r.Next(111, 999).ToString();
          

            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string ID2 = new string(Enumerable.Repeat(chars, 8).Select(s => s[r.Next(s.Length)]).ToArray());

            Uri uri = new Uri($"wss://pos-push.coupang.com/push/{ID1}/{ID2}/websocket");
            var cts = new CancellationTokenSource();
            await Connection_Socket.ConnectAsync(uri, cts.Token);

            //Console.WriteLine(Connection_Socket.State);
           
            await Task.Factory.StartNew(
                async () =>
                {
                    var rcvBytes = new byte[1024];
                    var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                    while (true)
                    {
                        WebSocketReceiveResult rcvResult = await Connection_Socket.ReceiveAsync(rcvBuffer, cts.Token);
                        byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                      

                        if (rcvResult.CloseStatus.HasValue)
                        {
                            //Console.WriteLine("Closed; Status: " + rcvResult.CloseStatus + ", " + rcvResult.CloseStatusDescription);
                            //Form1 frm = (Form1)Application.OpenForms["Form1"];
                            //frm.Invoke(new Action(() =>
                            //{
                            //    frm.textBox1.Text += "Closed; Status: " + rcvResult.CloseStatus + ", " + rcvResult.CloseStatusDescription + "\r\n";
                            //    frm.Socket_Timer.Enabled = false;
                            //}));


                        }
                        else
                        {

                        
                            string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                            //Console.WriteLine(rcvMsg);

                            if ( rcvMsg == "o")
                            {

                                
                                byte[] sendBytes = Encoding.UTF8.GetBytes("[\"CONNECT\\naccept-version:1.0,1.1,1.2\\nheart-beat:0,0\\n\\n\\u0000\"]");
                                var sendBuffer = new ArraySegment<byte>(sendBytes);
                                await
                                    Connection_Socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true,
                                                     cancellationToken: cts.Token);

                                byte[] sendBytes2 = Encoding.UTF8.GetBytes($"[\"SUBSCRIBE\\nid:sub-0\\ndestination:/subscribe/message/{ID2}-orders\\n\\n\\u0000\"]");

                                var sendBuffer2 = new ArraySegment<byte>(sendBytes2);
                                await
                                    Connection_Socket.SendAsync(sendBuffer2, WebSocketMessageType.Text, endOfMessage: true,
                                                     cancellationToken: cts.Token);

                                byte[] sendBytes3 = Encoding.UTF8.GetBytes($"[\"SUBSCRIBE\\nid:sub-1\\ndestination:/subscribe/message/{ID2}-stores\\n\\n\\u0000\"]");
                                var sendBuffer3 = new ArraySegment<byte>(sendBytes3);
                                await
                                    Connection_Socket.SendAsync(sendBuffer3, WebSocketMessageType.Text, endOfMessage: true,
                                                     cancellationToken: cts.Token);
                                //ping.Enabled = true;
                            }
                            
                            if(rcvMsg.Length > 1 && rcvMsg.StartsWith("a"))
                            {

                                String msg_spletter_01 = rcvMsg.Substring(rcvMsg.IndexOf("[\"") + 2, rcvMsg.IndexOf("\"]") - 3);


                                String[] msg_spletter_02 = Regex.Split(msg_spletter_01, @"\\n");

                                Form1 frm = (Form1)Application.OpenForms["Form1"];
                                if (msg_spletter_02[0]== "CONNECTED")
                                {


                                    frm.Invoke(new Action(() =>
                                    {
                                        frm.Socket_Timer.Enabled = true;

                                    }));
                                }
                                else if(msg_spletter_02[0] == "MESSAGE") {
                                    foreach (string str in msg_spletter_02)
                                    {
                                        //frm.Invoke(new Action(() =>
                                        //{
                                        if (str.Contains("{"))
                                        {
                                            JToken json = Helper_Class.Json_Responce (str.Replace(@"\", null) );

                                            //Console.WriteLine("ORDER soket Response: " + json["type"].ToString());
                                            if (json["type"].ToString () == "STORE_AVAILABILITY_UPDATE_NOTIFICATION")
                                            {
                                                frm.Stores_Availabilities_Status();
                                            }

                                         

                                            if (json["type"].ToString() == "ORDER_REQUEST" || json["type"].ToString() == "TUTORIAL_ORDER_REQUEST")
                                            {
                                                frm.Invoke(new Action(() =>
                                                {
                                                    //MessageBox.Show("ddd");
                                                    Order_Notification n = (Order_Notification)Application.OpenForms["Order_Notification"];
                                                    if (n == null)
                                                    {
                                                        n = new Order_Notification();
                                                        n.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - 0, Screen.PrimaryScreen.WorkingArea.Height - n.MinimumSize.Height);
                                                        n.Size = new System.Drawing.Size(0, n.MinimumSize.Height);

                                                        n.orderId = json["payLoad"]["orderId"] != null ? json["payLoad"]["orderId"].ToString() : "...";
                                                        n.orderServiceType.Text = json["payLoad"]["orderServiceType"] != null ? json["payLoad"]["orderServiceType"].ToString() : "...";
                                                        n.orderStoreId.Text = json["payLoad"]["orderStoreId"] != null ? json["payLoad"]["orderStoreId"].ToString() : "...";

                                                        if (json["type"].ToString() == "TUTORIAL_ORDER_REQUEST")
                                                        {
                                                            n.abbrOrderId.Text = "TEST";
                                                        }
                                                        else
                                                        {
                                                            n.abbrOrderId.Text = json["payLoad"]["abbrOrderId"] != null ? json["payLoad"]["abbrOrderId"].ToString() : "...";
                                                            if (n.orderId != "..." && n.orderStoreId.Text != "...")
                                                            {
                                                                n.Click += (ss, ee) => { frm.Get_Order_Details(ss, ee, n.orderStoreId.Text, n.orderId); };


                                                                if (frm.tabControl1.SelectedTab.Text == "접수대기")
                                                                {
                                                                    frm.GetOrder(n.orderStoreId.Text, Form1.Order_Type.PENDING);
                                                                }
                                                                  
                                                            }
                                                          
                                                         
                                                        }

                                                        

                                                        n.Show();
                                                    }
                                                    else
                                                    {
                                                        n.Focus();
                                                    }
                                                 
                                                }));

                                               
                                            }
                                            
                                        }
                                            //MessageBox.Show(str);

                                   
                                    }
                                }

                                
                             
                            
                            }

                            //Form1 frm2 = (Form1)Application.OpenForms["Form1"];
                            //frm2.Invoke(new Action(() =>
                            //{
                            //    frm2.textBox1.Text += rcvMsg + "\r\n";

                            //}));

                            //if (rcvMsg == "h")
                            //{

                            //    byte[] sendBytes3 = Encoding.UTF8.GetBytes("[\"SEND\\ndestination:/app/ping/stores\\ncontent-length:14\\n\\nsend HeartBeat\\u0000\"]");
                            //    var sendBuffer3 = new ArraySegment<byte>(sendBytes3);

                            //        Connection_Socket.SendAsync(sendBuffer3, WebSocketMessageType.Text, endOfMessage: false,
                            //                         cancellationToken: cts.Token);

                            //}



                            //Console.WriteLine("Received: {0}", rcvMsg);
                            //Console.WriteLine("Received message: " + Encoding.UTF8.GetString(incomingData, 0, result.Count));
                        }

                    }
                }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            //while (true)
            //{
            //    var message = Console.ReadLine();
            //    if (message == "Bye")
            //    {
            //        cts.Cancel();
            //        return;
            //    }
            //    byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            //    var sendBuffer = new ArraySegment<byte>(sendBytes);
            //    await
            //        Connection_Socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true,
            //                         cancellationToken: cts.Token);
            //}

        }


  
    }
}
