using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coupang
{
    public partial class Order_Viewer : Form
    {
        private  JToken cancel_reasons;

        public string store_ID;
        public string order_ID;
        public string orderServiceType;
        public string status;

        public Order_Viewer()
        {
            InitializeComponent();
            
        }

      

        private void Order_Viewer_Load(object sender, EventArgs e)
        {
           
            switch (status)
            {
                case "PAYMENT_APPROVED":
                    Button Cancel_btn = new Button() { Text = "Cancel Order", Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat, Size = new Size((Order_Control.Width - 20) / 2, Order_Control.Height - 10) };
                    Cancel_btn.Click += (ss, ee) => { Reject_Order(ss, ee, "reject"); };

                    Button Accept_btn = new Button() { Text = "Accept Order", Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat, Size = new Size((Order_Control.Width - 20) / 2, Order_Control.Height - 10) };
                    //Accept_btn.Click += Accept_Button_Click;
                    Accept_btn.Click += (ss,ee)=> { Accept_or_delay_Order(ss, ee, request_type.accept); };

                    Order_Control.Controls.Add(Cancel_btn);
                    Order_Control.Controls.Add(Accept_btn);
                    Order_Control.AutoSize = true;
                    break;

                case "COMPLETED":  case "CANCELLED":
                    Button Ok_btn = new Button() { Text = "OK", Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat, Size = new Size((Order_Control.Width - 20) , Order_Control.Height - 10) };
                    Ok_btn.Click += (ss,ee)=>{ this.Dispose(); };
                    Order_Control.Controls.Add(Ok_btn);
                    Order_Control.AutoSize = true;
                    break;

                case "ACCEPTED":
                    Button Cancellation_btn = new Button() { Text = "Order Cancellation", Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat, Size = new Size((Order_Control.Width - 20) / 2, Order_Control.Height - 10) };
                    //Cancel_btn.Click += button1_Click_1;
                    Cancellation_btn.Click += (ss, ee) => { Reject_Order(ss, ee, "cancel"); };

                    Button Delay_btn = new Button() { Text = "Delay in Preparation", Cursor = Cursors.Hand, FlatStyle = FlatStyle.Flat, Size = new Size((Order_Control.Width - 20) / 2, Order_Control.Height - 10) };
                    Delay_btn.Click += (ss, ee) => { Accept_or_delay_Order(ss, ee, request_type.delay); };

                    Order_Control.Controls.Add(Cancellation_btn);
                    Order_Control.Controls.Add(Delay_btn);
                    Order_Control.AutoSize = true;

                    break;
            }


        }

    
        enum request_type { accept , delay }
        private void Accept_or_delay_Order(object sender, EventArgs e, request_type _Request_type)
        {

            Accept_Order frm = new Accept_Order();
            if(frm.ShowDialog(this) == DialogResult.OK)
            {
       
                Thread th = new Thread(new ThreadStart(() =>
                {

                    JObject x = new JObject();
                    x.Add("duration", int.Parse (frm.Accept_Button.Tag.ToString ()));
                    //MessageBox.Show(x.ToString());
                    Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request($"https://pos-api.coupang.com/api/v1/stores/{store_ID}/orders/{order_ID}/{_Request_type.ToString ()}", Method.POST, null, x.ToString()));

                    tx.Wait();

                    if (!string.IsNullOrEmpty(tx.Result.Content))
                    {
                        JToken j = Helper_Class.Json_Responce(tx.Result.Content);

                        if (j.SelectToken("code").ToString() == "SUCCESS")
                        {
                            this.Invoke(new Action(() =>
                            {

                                if (_Request_type == request_type.accept)
                                {
                                    foreach (Form f in Application.OpenForms)
                                    {
                                        if (f.GetType() == typeof(Order_Notification))
                                        {

                                            Order_Notification ff = (Order_Notification)f;
                                            if (ff.orderId == order_ID)
                                            {
                                                ff.Dispose();
                                                break;
                                            }

                                        }
                                    }
                                }


                                //Form1 f = (Form1)this.Owner;

                                Form1 main_frm = (Form1)Application.OpenForms["Form1"];
                                if (main_frm.tabControl1.SelectedTab.Text == "접수대기")
                                {
                                    main_frm.GetOrder(store_ID, Form1.Order_Type.PENDING);
                                }

                                this.Dispose();
                            }));
                        }
                    }

                }));
                th.Start();
            }

    
        }


     
        private void Reject_Order(object sender, EventArgs e , string cancel_type)
        {
            Thread th = new Thread(new ThreadStart(() =>
            {
                Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request($"https://pos-api.coupang.com/api/v1/codes/cancel-reasons?orderServiceType={orderServiceType}", Method.GET));

                tx.Wait();
                this.Invoke(new Action(() =>
                {

                    if (!string.IsNullOrEmpty(tx.Result.Content))
                    {

                        cancel_reasons = Helper_Class.Json_Responce(tx.Result.Content.ToString());

                        REJECT_REASON_frm frm = new REJECT_REASON_frm();
                        Load_Reasons(frm, (JArray)cancel_reasons["content"]);
                       
                        frm.Text = "주문 취소 사유를 선택해 주세요.";
                     

                        if(frm.ShowDialog(this) == DialogResult.OK)
                        {
                         
                            JObject x = new JObject();
                            x.Add("cancelReasonId", frm.Tag.ToString());

                            Task<IRestResponse> tx2 = Task.Run(() => Helper_Class.Send_Request($"https://pos-api.coupang.com/api/v1/stores/{store_ID}/orders/{order_ID}/{cancel_type}", Method.POST, null, x.ToString()));

                            tx2.Wait();

                            if (!string.IsNullOrEmpty(tx2.Result.Content))
                            {
                                JToken j = Helper_Class.Json_Responce(tx2.Result.Content);

                                if (j.SelectToken("code").ToString() == "SUCCESS")
                                {
                                    this.Invoke(new Action(() =>
                                    {


                                        foreach (Form f in Application.OpenForms)
                                        {
                                            if (f.GetType() == typeof(Order_Notification))
                                            {

                                                Order_Notification ff = (Order_Notification)f;
                                                if (ff.orderId == order_ID)
                                                {
                                                    ff.Dispose();
                                                    break;
                                                }

                                            }
                                        }


                                        Form1 main_frm = (Form1)Application.OpenForms["Form1"];
                                        if (main_frm.tabControl1.SelectedTab.Text == "접수대기")
                                        {
                                            main_frm.GetOrder(store_ID, Form1.Order_Type.PENDING);
                                        }


                                        this.Dispose();
                                    }));
                                }
                            }



                         
                        }

                    }
                }));

            }));
            th.Start();
        }

        private void Load_Reasons(REJECT_REASON_frm frm, JArray root_reson)
        {
            frm.flowLayoutPanel1.Controls.Clear();
            foreach (JToken r in root_reson)
            {
                Button reson = new Button()
                {
                    Text = r["message"].ToString(), //r["title"].ToString(),
                    Size = new Size(250, 30),
                    Cursor = Cursors.Hand
                };

                if (string.IsNullOrEmpty(r["childCancellationReasonList"].ToString()))
                {
                    reson.Tag = r["cancellationReasonId"].ToString();
                    reson.Click += (ss, ee) => {
                        frm.Tag = reson.Tag;
                        frm.DialogResult = DialogResult.OK;
                    };
                }
                else
                {
                    reson.Tag = r["childCancellationReasonList"];
                    reson.Click += (ss, ee) => {
                        Load_Reasons(frm, (JArray)reson.Tag);
                    };

                }


                frm.flowLayoutPanel1.Controls.Add(reson);
            }

        }

    }
}
