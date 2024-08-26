using CoupangWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangWeb.Auth
{
    public partial class LoginForm : Form
    {
        private int mAsyncActs = 0; //API호출 동작 회수

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (Global.TEST_MODE != 0)
            {
                txtUserId.Text   = "01086078688";
                txtPassword.Text = "@coupang1";
            }
        }

        private void EnableControls(bool enable)
        {
            txtUserId.Enabled = enable;
            txtPassword.Enabled = enable;
            btnLogin.Enabled = enable;
            UseWaitCursor = !enable;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text;
            if (string.IsNullOrWhiteSpace(userId))
            {
                MessageBox.Show("사장님 아이디를 입력해 주세요.");
                return;
            }
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("비밀번호를 입력해 주세요.");
                return;
            }

            if (mAsyncActs > 0)
                return;
            mAsyncActs = 0;

            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                GetHomepageApi();
                LoginUserApi(userId, password);
            }));
            th.Start();
        }

        public void GetHomepageApi()
        {
            var client = new RestClient("https://store.coupangeats.com/")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Upgrade-Insecure-Requests", "1");

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            mAsyncActs--;
            try
            {
                var rnd = new Random();
                string pcId = string.Format("{0}{1}{2}", Global.TimeInMillis(), rnd.Next(12345, 100000), rnd.Next(12345, 100000));
                Global.cookies.Add(new System.Net.Cookie("device-id", Guid.NewGuid().ToString(), "/", "coupangeats.com"));
                Global.cookies.Add(new System.Net.Cookie("PCID", pcId, "/", "coupangeats.com"));
                Global.cookies.Add(new System.Net.Cookie("X-EATS-LOCALE", "ko", "/", "coupangeats.com"));
                Task.Delay(rnd.Next(2000)).Wait();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoginUserApi(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrEmpty(password))
            {
                Invoke(new Action(() => {
                    EnableControls(true);
                }));
                return;
            }
            mAsyncActs++;
            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/login")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Origin", "https://store.coupangeats.com");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/login");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddJsonBody(new
            {
                loginId = userName,
                password = password
            });

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Invoke(new Action(() => {
                Global.SaveCookies(_result.Cookies);

                string text = _result.Content;
                string msg = "알수 없는 오류로 로그인 실패!";
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        CPWCommonApiResult<CPWLoginSessionData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWLoginSessionData>>(text);
                        if (res != null)
                        {
                            string code = (res.code ?? "").ToUpper();
                            if (code == "SUCCESS")
                            { //success
                                if (res.data != null)
                                {
                                    msg = "";
                                    Global.userInfo = res.data;
                                }
                            }
                            else
                            {
                                if (res.error != null)
                                {
                                    string err = res.error.message;
                                    if (!string.IsNullOrWhiteSpace(err))
                                        msg = err;
                                }
                            }
                        }
                    }
                    catch { }
                }

                mAsyncActs--;
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    EnableControls(true);
                    MessageBox.Show(msg);
                }   
                else
                    PostProcessApi();
            }));
        }

        private void PostProcessApi()
        {
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                int nInd = 0;
                bool bInit = false;
                while (Global.userInfo != null)
                {
                    //GetManagementApi();
                    if (nInd == 0)
                    {
                        GetStoreListApi();
                    }

                    if (bInit == false)
                    {
                        Invoke(new Action(() =>
                        {
                            if ((Global.userInfo != null) && Global.HasStores())
                            {
                                DialogResult = DialogResult.OK;
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("프로필정보를 불러오는데 실패했습니다.");
                                Close();
                            }
                               
                        }));

                        bInit = true;
                    }

                    nInd = (nInd + 1) % 30;
                    Thread.Sleep(1000);
                }
            }));
            th.Start();
        }

        /*public void GetManagementApi()
        {
            var client = new RestClient("https://store.coupangeats.com/merchant/management")
            {
                CookieContainer = Global.cookies,
                Timeout = 30000
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "* / *");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/login");
            request.AddHeader("Upgrade-Insecure-Requests", "1");

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();            
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            mAsyncActs--;
        }*/

        /*public void GetUserProfileApi()
        {
            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/whoami")
            {
                CookieContainer = Global.cookies,
                Timeout = 30000
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<CPWUserProfileData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWUserProfileData>>(text);
                    if ((res != null) && (res.data != null))
                        Global.userInfo = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }*/

        public void GetStoreListApi()
        {
            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/web/stores")
            {
                CookieContainer = Global.cookies,
                Timeout = 30000
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<List<CPWShopData>> res = JsonConvert.DeserializeObject<CPWCommonApiResult<List<CPWShopData>>>(text);
                    if ((res != null) && (res.data != null))
                        Global.shopList = res.data;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);           
                }
            }
            mAsyncActs--;
        }

        /*public void GetAccountInfoApi()
        {
            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/detail/form")
            {
                CookieContainer = Global.cookies,
                Timeout = 30000
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "* / *");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management");

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<CPWLoginUserData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWLoginUserData>>(text);
                    if ((res != null) && (res.data != null))
                        Global.accountInfo = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }*/


    }
}
