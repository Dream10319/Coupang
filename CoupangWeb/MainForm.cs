using CoupangWeb.Auth;
using CoupangWeb.Menus;
using CoupangWeb.Orders;
using CoupangWeb.Reviews;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;



namespace CoupangWeb
{
    public partial class MainForm : Form
    {
        private int mAsyncActs = 0;
        private Timer mTimer = new Timer();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*mTimer.Interval = 2000;
            mTimer.Enabled = true;
            mTimer.Tick += OnLoadTimer;*/
            EnableControls(false);
            //btnLogin.Enabled = false;

            txtGreetings.Text = "";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mTimer.Stop();
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }
            //유저 로그인 한 경우 로그아웃
            if (Global.userInfo != null)
            {
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    CallLogoutApi(true);
                }));
                th.Start();
            }
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Stop();
            CallInitApi();
        }

        private void EnableControls(bool enable)
        {
            btnMenus.Enabled = enable;
            btnOrders.Enabled = enable;
            btnReviews.Enabled = enable;
        }

        private void CallInitApi()
        {
            if (mAsyncActs > 0)
                return;
            mAsyncActs = 0;
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "MainForm")
                    {
                        Invoke(new Action(() => {
                            EnableControls(false);
                            btnLogin.Enabled = true;
                            UseWaitCursor = false;
                        }));
                        break;
                    }
                }
            }));
            th.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Global.userInfo == null)
            {
                LoginForm frm = new LoginForm();
                Hide();
                frm.ShowDialog(this);
                if (Global.userInfo == null)
                    Close();
                else
                {
                    AfterLogin();
                    Show();
                }
            }
            else
            {
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    CallLogoutApi(false);
                }));
                th.Start();
            }
        }

        private void AfterLogin()
        {
            btnLogin.Text = "로그아웃 (&L)";
            string name = Global.userInfo.name ?? "";
            if (!string.IsNullOrWhiteSpace(name))
                txtGreetings.Text = string.Format("안녕하세요 {0} 사장님!", name);
            else
                txtGreetings.Text = "";
            mAsyncActs = 0;
            EnableControls(true);
        }

        private void AfterLogout()
        {
            EnableControls(false);
            btnLogin.Text = "로그인 (&L)";
            txtGreetings.Text = "";
            Global.ClearValues();
            Global.cookies = new CookieContainer();
            CallInitApi();
        }

        private void btnMenus_Click(object sender, EventArgs e)
        {
            MenuListForm frm = new MenuListForm();
            Hide();
            frm.ShowDialog();
            Show();
        }

        private void btnReviews_Click(object sender, EventArgs e)
        {
            ReviewListForm frm = new ReviewListForm();
            Hide();
            frm.ShowDialog();
            Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrderStatsForm frm = new OrderStatsForm();
            Hide();
            frm.ShowDialog();
            Show();
        }

        public void CallLogoutApi(bool bExit)
        {
            if (Global.userInfo == null)
                return;
            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/logout")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Origin", "https://store.coupangeats.com");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;
            string text = _result.Content;
            string msg = "로그아웃 실패!";
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<CPWLogoutResultData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWLogoutResultData>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                        msg = "";
                }
                catch { }
            }
            mAsyncActs--;
            if (!bExit)
            {
                Invoke(new Action(() => {
                    if (!string.IsNullOrWhiteSpace(msg))
                        MessageBox.Show(msg);
                    else
                        AfterLogout();
                }));
            }
        }
    }
}
