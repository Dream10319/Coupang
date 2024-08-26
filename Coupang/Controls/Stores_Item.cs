using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Coupang.Restaurants_Controls
{
    public partial class Stores_Item : UserControl
    {
        public bool collapsed = true;

        public Stores_Item()
        {
            InitializeComponent();
        }
     

        private void Restaurant_Item_Load(object sender, EventArgs e)
        {
            foreach (Control cn in this.Controls)
            {
                cn.Click += Restaurant_Item_Click;
            }
        }
        public bool _changeable = false;


        public bool changeable
        {
            get { return _changeable; }

            set {
                _changeable = value;
             
            }
        }
        public string R_name
        {
            get { return this.R_Name.Text; }

            set { this.R_Name.Text = value; }
        }

        public string _displayItemDTO
        {
            get { return this.displayItemDTO.Text; }

            set { this.displayItemDTO.Text = value; }
        }

        private string _R_ID;
        public string R_ID
        {
            get { return _R_ID; }

            set { _R_ID = value; }
        }


        public string openStatusText { 
            get { return this.R_Status.Text; }
            set { this.R_Status.Text = value; }
        }

        private string _State;


        public string R_status
        {
            get { return _State; }

            set { 
                this._State = value;
                //SolidBrush sb = null;

                switch (value)
                {
                    case "OPEN":
                        changeable = true;
                        R_Status.Visible = true;
                        this.Status_Buttons.Controls.Clear();

                       
                      
                        for (int b = 15; b <= 60; b *= 2)
                        {
                
                            Button b1 = new Button
                            {
                                Text = $"{b.ToString()} 분 영업중지",
                                Size = new Size(this.Width - 20, 40),
                                Tag = b,
                                Image = Properties.Resources.Busy,
                                ImageAlign = ContentAlignment.MiddleLeft,
                                TextImageRelation = TextImageRelation.Overlay,
                                TextAlign = ContentAlignment.MiddleCenter,
                            };

                            b1.Click += (sender, EventArgs) => { Restaurants_Availability_Change(sender, EventArgs,  statues_set.Close); };
                            this.Status_Buttons.Controls.Add(b1);
                        }
                        
                        this.Status.Image = Properties.Resources.Open;
                        break;


                    case "TEMP_CLOSED":
                        changeable = true;
                        R_Status.Visible = true;
                        this.Status_Buttons.Controls.Clear();

                        Button b2 = new Button
                        {
                            Text = "영업시작",
                            Size = new Size(this.Width - 20, 40),
                            Image = Properties.Resources.Open,
                            ImageAlign = ContentAlignment.MiddleLeft,
                            TextImageRelation = TextImageRelation.Overlay,
                            TextAlign = ContentAlignment.MiddleCenter,
                        };


                        b2.Click += (sender, EventArgs) => { Restaurants_Availability_Change(sender, EventArgs, statues_set.Open); };
                        this.Status_Buttons.Controls.Add(b2);
                        this.Status.Image = Properties.Resources.Busy;
                        break;




                    case "EATS_SERVICE_CLOSED":
                        changeable = false;
                        R_Status.Visible = false;
                        this.Status.Image = Properties.Resources.Disabled;
                   
             
                        break;

                 
                }

                if(collapsed== false)
                {
                    this.Height = this.panel1.Height + 100;

                }

            
            }
        }

        enum statues_set { Open,Close }
        private void Restaurants_Availability_Change(object sender, EventArgs e,  statues_set s)
        {
       

            this.panel1.Enabled = false;
            Thread th = new Thread(new ThreadStart(() =>
            {


                Form1 frm = (Form1)Application.OpenForms["Form1"];


                string request_url;
                JObject request_payload = new JObject();
                request_payload.Add("accountId", frm.Account_Id);

                if(s == statues_set.Close)
                {
                    request_url = $"https://pos-api.coupang.com/api/v1/stores/{this.R_ID}/availability/temp-close";
                    request_payload.Add("closeInterval", ((Button)sender).Tag.ToString());

                }
                else
                {
                    request_url = $"https://pos-api.coupang.com/api/v1/stores/{this.R_ID}/availability/cancel-temp-close";
                }


           

                Task<IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request(request_url, Method.POST, null, request_payload.ToString()));
     
                tx.Wait();
                this.Invoke(new Action(() => {


                

                    this.panel1.Enabled = true;
                    Status.Text = tx.Result.ResponseStatus.ToString();




                }));


            }));
            th.Start();

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
          
            switch (collapsed)
            {

                case true:

                    if(this.Height >= this.panel1.Height + 100)
                    {
                        this.timer1.Enabled = false;
                        this.Height = this.panel1.Height + 100;
                        collapsed = false;
                    }
                    else
                    {
                        this.Height += 10;
                    }
                    break;

                case false:
                    if (this.Height == this.MinimumSize.Height)
                    {
                        this.timer1.Enabled = false;
                        collapsed = true ;
                    }
                    else
                    {
                        this.Height -= 10;
                    }
                    break;
            }
        }

        private void Restaurant_Item_Click(object sender, EventArgs e)
        {
            if (changeable == true)
            {
                this.timer1.Enabled = true;
            }

        }



    
    }
}
