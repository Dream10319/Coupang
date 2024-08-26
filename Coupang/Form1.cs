using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coupang.Controls;
using Coupang.Restaurants_Controls;

namespace Coupang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //public string Store_Id;
        public string Account_Id;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get_stores();
            //Order_Notification test = new Order_Notification() {  orderId = "test" };
            //test.Show();
        }

        #region Store Control 
        public void Get_stores()
        {
            this.tabControl1.Enabled = false;

            Thread th = new Thread(new ThreadStart(() =>
            {
                Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request("https://pos-api.coupang.com/api/v2/auth/verify", Method.GET));

                tx.Wait();
                if (this.IsDisposed == false)
                {
                    this.Invoke(new Action(() => {
                        this.Restaurants.Controls.Clear();

                        if (!string.IsNullOrEmpty(tx.Result.Content))
                        {
                            JToken o = Helper_Class.Json_Responce(tx.Result.Content.ToString());

                            if (tx.Result.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                if (o.SelectToken("code").ToString() == "SUCCESS")
                                {
                                    Account_Id = o["content"]["accountId"].ToString();

                                    foreach (JToken x in o["content"]["verifiedStoreList"])
                                    {

                                        Restaurants.Controls.Add(new Stores_Item()
                                        {
                                            R_ID = x.SelectToken("storeId").ToString(),
                                            R_name = x.SelectToken("storeName").ToString(),
                                            Size = new Size(Restaurants.Width - 27, 0)

                                        });

                                    }
                                    if (Restaurants.Controls.Count > 0)
                                    {
                                        Stores_Availabilities_Status();
                                        this.tabControl1.Enabled = true;
                                    }


                                    Connection_Helper.Web_Socket_Client();

                                }

                                Login_btn.Text = "로그아웃";
                            }
                            else
                            {
                                MessageBox.Show(this, o.SelectToken("message").ToString(), o.SelectToken("code").ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                                Login_frm frm = new Login_frm();
                                frm.ShowDialog(this);
                                Login_btn.Text = "로그인";
                            }

                            this.Status.Text = tx.Result.StatusDescription.ToString();
                        }


                        Status.Text = tx.Result.ResponseStatus.ToString();
                    }));
                }

            }));
            th.Start();

        }

        public void Stores_Availabilities_Status()
        {

            Thread th = new Thread(new ThreadStart(() =>
            {

                foreach (Stores_Item r in this.Restaurants.Controls)
                {
                    var QueryParameters = new List<Tuple<string, string>>();



                    QueryParameters.Add(new Tuple<string, string>("storeIds", r.R_ID));
                    QueryParameters.Add(new Tuple<string, string>("accountId", this.Account_Id));

                    Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request($"https://pos-api.coupang.com/api/v1/stores/availability?", Method.GET, QueryParameters));

                    tx.Wait();

                    if (!string.IsNullOrEmpty(tx.Result.Content))
                    {
                        JToken o = Helper_Class.Json_Responce(tx.Result.Content.ToString());
                        if (tx.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (o["code"].ToString() == "SUCCESS")
                            {
                                if (o["content"].First()["storeId"].ToString() == r.R_ID)
                                {

                                    if (this.IsDisposed == false)
                                    {
                                        r.Invoke(new Action(() =>
                                        {
                                            r.R_status = o["content"].First()["openStatus"].ToString();
                                            r.openStatusText = o["content"].First()["openStatusText"].ToString();
                                            r._displayItemDTO = o["content"].First()["displayItemDTO"]["text"].ToString();
                                        }));

                                    }

                                }
                            }
                        }

                    }
                }



            }));
            th.Start();


        }


        #endregion

        #region Order Control
        public enum Order_Type { PENDING, PROCESSING, COMPLETED }


        public void GetOrder(string Store_ID, Order_Type _order_type, string start_date = null, string end_date = null)
        {







            var QueryParameters = new List<Tuple<string, string>>();

            QueryParameters.Add(new Tuple<string, string>("storeIds", Store_ID));
            QueryParameters.Add(new Tuple<string, string>("status", _order_type.ToString()));





            if (start_date != null && end_date != null)
            {
                QueryParameters.Add(new Tuple<string, string>("startDate", start_date));
                QueryParameters.Add(new Tuple<string, string>("endDate", end_date));
            }

            //MessageBox.Show(QueryParameters.ToString());

            Thread th = new Thread(new ThreadStart(() =>
            {
                Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request("https://pos-api.coupang.com/api/v2/stores/orders?", Method.GET, QueryParameters));
                //Task<IRestResponse> tx = Getrestaurants().RunSynchronously();
                tx.Wait();
                this.Invoke(new Action(() => {
                    //this.dataGridView1.Rows.Clear();
                    //MessageBox.Show(tx.Result.Content.ToString());

                    if (!string.IsNullOrEmpty(tx.Result.Content))
                    {


                        JToken o = Helper_Class.Json_Responce(tx.Result.Content.ToString());

                        //MessageBox.Show (tx.Result.Content.ToString());


                        if (tx.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (o.SelectToken("code").ToString() == "SUCCESS")
                            {
                                if (_order_type == Order_Type.COMPLETED)
                                {
                                    Order_Details_pan.Controls.Clear();
                                }
                                else if (_order_type == Order_Type.PROCESSING)
                                {
                                    In_Progress_pan.Controls.Clear();
                                }
                                else if (_order_type == Order_Type.PENDING)
                                {
                                    New_Orders_pan.Controls.Clear();
                                }



                                foreach (JToken x in o["content"]["content"])
                                {



                                    Order_Item o_item = new Order_Item();
                                    o_item.StoreID = Store_ID;
                                    o_item.orderId = x["orderId"].ToString();

                                    o_item.abbrOrderId.Text = x["abbrOrderId"] != null ? x["abbrOrderId"].ToString() : "...";
                                    o_item.Order_Time.Text = x["orderedAt"]["dateTime"] != null ? Helper_Class.From_Unix_Timestamp(double.Parse(x["orderedAt"]["dateTime"].ToString())).ToString("hh:mm tt") : "...";
                                    o_item.note.Text = x["note"] != null ? x["note"].ToString() : "...";
                                    o_item.Size = new Size(Order_Details_pan.Width - 27, 151);

                                    o_item.O_status.Text = x["status"] != null ? x["status"].ToString() : "...";
                                    o_item.O_orderServiceType.Text = x["orderServiceType"] != null ? x["orderServiceType"].ToString() : "...";
                                    //o_item.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                                    o_item.Click += (ss, ee) => { Get_Order_Details(ss, ee, Store_ID, o_item.orderId); };
                                    switch (o_item.O_status.Text)
                                    {
                                        case "COMPLETED":
                                            o_item.O_status.ForeColor = Color.MediumSpringGreen;
                                            break;

                                        case "CANCELLED":
                                            o_item.O_status.ForeColor = Color.DarkOrange;
                                            break;
                                    }
                                    if (_order_type == Order_Type.COMPLETED)
                                    {
                                        Order_Details_pan.Controls.Add(o_item);
                                    }
                                    else if (_order_type == Order_Type.PROCESSING)
                                    {
                                        In_Progress_pan.Controls.Add(o_item);
                                    }
                                    else if (_order_type == Order_Type.PENDING)
                                    {
                                        New_Orders_pan.Controls.Add(o_item);
                                    }




                                }
                            }

                        }




                        this.Status.Text = tx.Result.StatusDescription.ToString();
                    }


                    //this.button3.Enabled = true;

                }));

            }));
            th.Start();


            //string test = string.Empty;
            //Thread thx = new Thread(new ThreadStart(() =>
            //{
            //    Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request($"https://api-gateway.coupang.com/v2/providers/openapi/apis/api/v4/vendors/218497/ordersheets/17JP00/history", Method.GET, null));
            //    //Task<IRestResponse> tx = Getrestaurants().RunSynchronously();
            //    tx.Wait();
            //    this.Invoke(new Action(() =>
            //    {
            //        test = tx.Result.Content;
            //    }));

                   

            //}));
            //thx.Start();


        }




        public void Get_Order_Details(object sender, EventArgs e, string StoreID, string orderId)
        {

            Thread th = new Thread(new ThreadStart(() =>
            {
                Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request($"https://pos-api.coupang.com/api/v1/stores/{StoreID}/orders/{orderId}", Method.GET));
                //Task<IRestResponse> tx = Getrestaurants().RunSynchronously();
                tx.Wait();
                this.Invoke(new Action(() => {

                    if (!string.IsNullOrEmpty(tx.Result.Content))
                    {


                        JToken o = Helper_Class.Json_Responce(tx.Result.Content.ToString());

                        //MessageBox.Show (tx.Result.Content.ToString());


                        if (tx.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Order_Viewer viewer = new Order_Viewer();

                            viewer.Total.Text = o["content"]["totalAmount"].ToString();
                            viewer.order_ID = o["content"]["orderId"].ToString();
                            viewer.orderServiceType = o["content"]["orderServiceType"].ToString();
                            viewer.store_ID = o["content"]["storeId"].ToString();
                            viewer.Order_No.Text = o["content"]["abbrOrderId"] != null ? o["content"]["abbrOrderId"].ToString() : "...";

                            viewer.status = o["content"]["status"] != null ? o["content"]["status"].ToString() : "..."; ;
                            viewer.O_status.Text = viewer.status;

                            viewer.customerOrderCount.Text = o["content"]["customerOrderCount"] != null ? o["content"]["customerOrderCount"].ToString() : "...";

                            viewer.Order_Time.Text = o["content"]["orderedAt"]["dateTime"] != null ? Helper_Class.From_Unix_Timestamp(double.Parse(o["content"]["orderedAt"]["dateTime"].ToString())).ToString("hh:mm tt") : "...";

                            foreach (JToken I in o["content"]["items"])
                            {
                                FlowLayoutPanel Items = new FlowLayoutPanel();
                                Items.FlowDirection = FlowDirection.TopDown;
                                Items.MinimumSize = new Size(viewer.Order_Items.Width - 27, 25);


                                Panel pn = new Panel() { Size = new Size(Items.Width, 25) };
                                Label I_name = new Label() { Text = I["name"].ToString(), Location = new Point(16, 0) };
                                Label I_quantity = new Label() { Text = I["quantity"].ToString(), Location = new Point(350, 0) };
                                Label I_subTotalPrice = new Label() { TextAlign = ContentAlignment.MiddleRight, Text = I["unitSalePrice"].ToString(), Location = new Point(466, 0) };

                                pn.Controls.Add(I_name);
                                pn.Controls.Add(I_quantity);
                                pn.Controls.Add(I_subTotalPrice);

                                Items.Controls.Add(pn);

                                foreach (JToken I_options in I["itemOptions"])
                                {
                                    //MessageBox.Show("op");
                                    Panel pn1 = new Panel() { Size = new Size(Items.Width, 25) };
                                    Label I_optionName = new Label() { Text = I_options["optionName"].ToString(), Location = new Point(30, 0) };
                                    //Label I_optionQuantity = new Label() { Text = I_options["optionQuantity"].ToString(), Location = new Point(350, 0) };
                                    Label I_optionPrice = new Label() { TextAlign = ContentAlignment.MiddleRight, Text = "+" + I_options["optionPrice"].ToString(), Location = new Point(466, 0) };


                                    pn1.Controls.Add(I_optionName);
                                    //pn1.Controls.Add(I_optionQuantity);
                                    pn1.Controls.Add(I_optionPrice);

                                    Items.Controls.Add(pn1);
                                }

                                Items.AutoSize = true;
                                Items.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                                viewer.Order_Items.Controls.Add(Items);
                            }



                            viewer.Show((Form1)Application.OpenForms["Form1"]);
                        }
                    }

                }));

            }));
            th.Start();

        }

        #endregion





        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.button3.Enabled = false;
            //FromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);
            //ToDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);


            //FromDate.Value = DateTime.Now.AddDays(-1);
            //ToDate.Value = DateTime.Now.AddDays(1);
            //MessageBox.Show(Connection_Helper.Connection_Socket.State.ToString());
            if (Connection_Helper.Connection_Socket.State == System.Net.WebSockets.WebSocketState.Open)
            {
                //MessageBox.Show(Connection_Helper.Connection_Socket.State.ToString());
                //Thread th = new Thread(new ThreadStart( () =>
                //{


                string message = "[\"SEND\\ndestination:/app/ping/stores\\ncontent-length:14\\n\\nsend HeartBeat\\u0000\"]";
                //MessageBox.Show(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                Connection_Helper.Connection_Socket.SendAsync(new ArraySegment<byte>(messageBytes), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);


                if (this.tabControl1.SelectedTab.Text == "접수대기")
                {
                    GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.PENDING);
                }


                //}));
                //th.Start();
            }
            else
            {
                Socket_Timer.Enabled = false;
            }

            //GetOrder((string)f);
            //GetOrder();


        }







        private void Login_Button_Click(object sender, EventArgs e)
        {


            if (Login_btn.Text == "로그인")
            {
                Login_frm frm = new Login_frm();
                frm.ShowDialog(this);

            }
            else
            {
                Thread th = new Thread(new ThreadStart(() => {
                    Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request("https://pos-api.coupang.com/api/v1/sign-out", Method.POST));
                    tx.Wait();

                    JToken o = Helper_Class.Json_Responce(tx.Result.Content.ToString());
                    if (!string.IsNullOrEmpty(tx.Result.Content) && o.SelectToken("code").ToString() == "SUCCESS")
                    {

                        if (File.Exists(Application.StartupPath + "\\Cookies.json") == true)
                        {
                            File.Delete(Application.StartupPath + "\\Cookies.json");
                        }
                        Cookies_Class.Cookies = null;

                        this.Invoke(new Action(async () =>
                        {
                            Login_btn.Text = "로그인";
                            Restaurants.Controls.Clear();
                            Order_Details_pan.Controls.Clear();
                            Socket_Timer.Enabled = false;

                            if (Connection_Helper.Connection_Socket.State == System.Net.WebSockets.WebSocketState.Open)
                            {


                                await Connection_Helper.Connection_Socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(
                                     "[\"DISCONNECT\\nreceipt: close - 2\\n\\n\\u0000\"]"
                                    )), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);

                                await Connection_Helper.Connection_Socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(
                                             "(3000) Go away!"
                                            )), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);

                            }

                            Application.Restart();

                        }));
                    }


                }));

                if (MessageBox.Show("정말 로그아웃 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    th.Start();
                }

            }
        }





        private void button7_Click(object sender, EventArgs e)
        {
            GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.COMPLETED, FromDate.Value.ToString("yyyy-MM-dd") + "T00:00:00.000+02:00", ToDate.Value.ToString("yyyy-MM-dd") + "T23:59:59.999+02:00");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Restaurants.Controls.Count > 0)
            {
                switch (this.tabControl1.SelectedTab.Text)
                {
                    case "주문내역":
                        GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.COMPLETED, "2000-05-27" + "T00:00:00.000+02:00", "2025-05-27" + "T23:59:59.999+02:00");
                        //GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.COMPLETED, "2022-05-27" + "T00:00:00.000+02:00", "2022-05-27" + "T23:59:59.999+02:00");
                        break;

                    case "진행중":
                        GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.PROCESSING, "2000-05-27" + "T00:00:00.000+02:00", "2025-05-27" + "T23:59:59.999+02:00");
                        break;

                    case "접수대기":
                        GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.PENDING,"2000-05-27" + "T00:00:00.000+02:00", "2025-05-27" + "T23:59:59.999+02:00");
                        break;
                }
            }


        }

        private void FromDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.Restaurants.Controls.Count > 0)
            {
                GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.COMPLETED, FromDate.Value.ToString("yyyy-MM-dd") + "T00:00:00.000+02:00", ToDate.Value.ToString("yyyy-MM-dd") + "T23:59:59.999+02:00");
            }

        }

        private void ToDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.Restaurants.Controls.Count > 0)
            {
                GetOrder(((Stores_Item)this.Restaurants.Controls[0]).R_ID, Order_Type.COMPLETED, FromDate.Value.ToString("yyyy-MM-dd") + "T00:00:00.000+02:00", ToDate.Value.ToString("yyyy-MM-dd") + "T23:59:59.999+02:00");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.Restaurants.Controls.Count > 0)
            {
                FromDate.Value = FromDate.Value.AddDays(+1);


            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Restaurants.Controls.Count > 0)
            {
                FromDate.Value = FromDate.Value.AddDays(-1);

            }
        }

    }
}
