using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Coupang
{
   
    public partial class Login_frm : Form
    {
      


        public Login_frm()
        {
            InitializeComponent();

            username.Text = "01041224242";
            password.Text = "abcd4242@";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.password.UseSystemPasswordChar = ((CheckBox)(sender)).Checked;
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                if (Cookies_Class.Cookies.GetCookies(new Uri ("https://pos-api.coupang.com"))["device-id"] == null)
                {
                    Cookies_Class.Cookies.Add(new Cookie() { Domain = "coupang.com", Name = "device-id", Value = "df8ca0c4-7861-405e-af86-8bfc354f7dfeaaa" });
                    Cookies_Class.Cookies.Add(new Cookie() { Domain = "coupang.com", Name = "version", Value = "1.10.15" });
                    Cookies_Class.Cookies.Add(new Cookie() { Domain = "coupang.com", Name = "app-type", Value = "COUPANG_POS" });
                }

                //const string public_key = @"MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAJzE1obe1GiSE6rqaIWjreZ8NmXur3dYgJPths2FDnNtN3Mwgl0ZNDc7RUZwgE4LZf9E2Tf3JmLpRIKXHGXhCyUCAwEAAQ==";


                JObject request_payload = new JObject();
                request_payload.Add("username", this.username.Text);
                request_payload.Add("password", this.password.Text); //Signin_Helper.Password_Genrator(public_key,this.password.Text));
                request_payload.Add("encrypt", false);


                Task <IRestResponse> tx = Task.Run(() => Helper_Class.Send_Request("https://pos-api.coupang.com/api/v2/auth/sign-in/user", Method.POST, null, request_payload.ToString()));
                tx.Wait();

                if (!string.IsNullOrEmpty(tx.Result.Content))
                {
                    StringReader reader = new StringReader(tx.Result.Content.ToString());
                    using (JsonReader jsonReader = new JsonTextReader(reader))
                    {
                        JsonSerializer Deserializer = new JsonSerializer();
                        var o = (JToken)Deserializer.Deserialize<JToken>(jsonReader);

                        if (o.SelectToken("code").ToString () == "SUCCESS")
                        {
                            //MessageBox.Show(o["content"]["stores"].First["storeId"].ToString());
                            JObject request_payload2 = new JObject();
                            request_payload2.Add("storeId", o["content"]["stores"].First["storeId"].ToString());
                               

                            Task<IRestResponse> tx2 = Task.Run(() => Helper_Class.Send_Request("https://pos-api.coupang.com/api/v2/auth/sign-in/store", Method.POST, null, request_payload2.ToString()));
                               
                            tx2.Wait();

                            //this.Invoke(new Action(() => {
                            //    this.textBox1.Text = tx2.Result.Content.ToString();
                            //    //this.label3.Text = tx2.Result.StatusCode.ToString();

                            //}));

                            if (!string.IsNullOrEmpty(tx2.Result.Content))
                            {

                                StringReader reader2 = new StringReader(tx2.Result.Content.ToString());
                                using (JsonReader jsonReader2 = new JsonTextReader(reader2))
                                {
                                    JsonSerializer Deserializer2 = new JsonSerializer();
                                    var o2 = (JToken)Deserializer2.Deserialize<JToken>(jsonReader2);

                                    if (o2.SelectToken("code").ToString() == "SUCCESS")
                                    {
                                        Cookies_Class.Save_Cookies();
                                        this.Invoke(new Action(() =>
                                        {

                                            Task<IRestResponse> tx3 = Task.Run(() => Helper_Class.Send_Request("https://pos-api.coupang.com/api/v1/meta/cs-info", Method.GET, null, null));
                                            //Task<IRestResponse> tx4 = Task.Run(() => Helper_Class.Send_Request("https://pos-push.coupang.com/push/info?t=1653305147507", Method.GET, null, null));
                                            MessageBox.Show(this, "Authorization Success", "Authorized", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                            Form1 frm = (Form1)Application.OpenForms[0];
                                            frm.Get_stores();
                                            this.Dispose();

                                        }));
                                    }

                                }
                            }
                        }
                        else
                        {
                            this.Invoke(new Action(() => {

                                MessageBox.Show(this, o.SelectToken("message").ToString(), o.SelectToken("code").ToString().Replace("_" ," "), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));

                        }
                    }
                }

                if(!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {

                        this.button1.Enabled = true;

                    }));
                }
            }));
            th.Start();
        }


        private void Login_frm_Load(object sender, EventArgs e)
        {

        }
    }
}
