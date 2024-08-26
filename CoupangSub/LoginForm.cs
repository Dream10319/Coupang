using CoupangSub.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CoupangSub
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
                txtUserId.Text = "6122253748";
                txtPassword.Text = "abcd3186@";
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
                LoginUserApi(userId, password);
            }));
            th.Start();
        }

        private void LoginUserApi(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrEmpty(password) || (Global.setting == null))
            {
                Invoke(new Action(() => {
                    EnableControls(true);
                }));
                return;
            }
            var client = new RestClient("https://pos-api.coupang.com/api/v2/auth/sign-in/user")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            bool encrypt = Global.setting.encType == "A";
            var data = new
            {
                username = userName,
                password = encrypt ? Global.EncryptPassword(password) : password,
                encrypt = encrypt,
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Origin", "chrome-extension://opbdabemkeebgjjopefgcjjccafieono");
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v2/auth/sign-in/user", data, "POST", "application/json");

            request.AddJsonBody(data);

            var ts = Task.Run(() => client.ExecuteAsync(request));
            ts.Wait();
            IRestResponse _result = ts.Result;
            Invoke(new Action(() => {
                Global.SaveCookies(_result.Cookies);

                string text = _result.Content;
                string msg = "알수 없는 오류로 로그인 실패!";
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        CPSDataApiResult<CPSLoginResult> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSLoginResult>>(text);
                        if (res != null)
                        {
                            string code = res.code;
                            string err = res.message;

                            if (code == "SUCCESS")
                            {
                                msg = "";
                                Global.userInfo = res.content;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(err))
                                    msg = err;
                            }
                        }
                    }
                    catch { }
                }

                mAsyncActs--;
                EnableControls(true);
                if (!string.IsNullOrWhiteSpace(msg))
                    MessageBox.Show(msg);
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }));
        }
    }
}
