using CoupangSub.Models;
using CoupangSub.Orders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace CoupangSub
{
    public partial class MainForm : Form, OrderItemCtrl.IEventCallback
    {
        private int mAsyncActs = 0;
        private Timer mTimer = new Timer();

        private string mErrorMsg = null;
        private string mSelProgress = null; //주문진행상황  "PENDING" - 접수 대기, "PROCESSING" - 진행 중, "COMPLETED" - 완료
        private List<CPSOrderEntity> mShowOrders = null; //표시용 주문 리스트
        private CPSOrderEntity mOrder = null;

        //WEBSOCKET connection with STOMP https://pos-push.coupang.com/push

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Global.setting == null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            EnableControls(false);
            UseWaitCursor = false;  //Remove waiting cursor to try again
            btnLogin.Enabled = false; //login is disabled before initialization
            txtGreetings.Text = "";

            mTimer.Enabled = true;
            mTimer.Interval = 2000;
            mTimer.Tick += OnLoadTimer;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }

            //유저 로그인 한 경우 로그아웃
            if (Global.userInfo != null)
            {
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    CallLogoutApi();
                    SwitchEncryptApi();
                }));
                th.Start();
            }
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Tick -= OnLoadTimer;
            mTimer.Stop();
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                PingWebSocketApi();
                GetMetaCsInfoApi();
                VerifyAccountApi();
                SwitchEncryptApi();
                Invoke(new Action(() =>
                {
                    btnLogin.Enabled = true;
                    EnableControls(false);
                    UseWaitCursor = false;
                }));
            }));
            th.Start();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Global.userInfo == null)
            {
                LoginForm frm = new LoginForm();
                Hide();
                frm.ShowDialog();
                AfterLogin();
                Show();
            }
            else
            {
                EnableControls(false);
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    CallLogoutApi();
                    SwitchEncryptApi();
                    Invoke(new Action(() =>
                    {
                        AfterLogout();
                    }));
                }));
                th.Start();
            }
        }

        private void AfterLogin()
        {
            var user = Global.userInfo;
            if ((user == null) || (user.stores == null))
            {
                EnableControls(false); //로그아웃처럼 처리
                UseWaitCursor = false;
                return;
            }

            btnLogin.Text = "로그아웃 (&L)";
            txtGreetings.Text = string.Format("안녕하세요, {0} 사장님!", user.GetFirstStoreName());
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                List<long> storeIds = new List<long>();
                foreach (var store in user.stores)
                {
                    if ((store != null) && (store.storeId > 0))
                        storeIds.Add(store.storeId);
                }
                int count = storeIds.Count;
                if (count > 1)
                    SinginMultiStoreApi(storeIds);
                else if (count == 1)
                    SinginStoreApi(storeIds[0]);
                
                GetConfigInfoApi();
                GetMetaCsInfoApi();
                VerifyAccountApi();

                GetOrderListApi(storeIds, "PENDING");
                FetchReadyRemindersApi(storeIds, "PROCESSING");

                GetOrderListApi(storeIds, mSelProgress);
                if ((ActiveForm == this) || !IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        EnableControls(true);
                        mTimer.Interval = 60000;
                        mTimer.Enabled = true;
                        mTimer.Tick += OnAlertTimer;
                        ShowOrderList();
                    }));
                }
            }));
            th.Start();
        }

        private void AfterLogout()
        {
            EnableControls(false);
            Global.ClearValues();
            btnLogin.Text = "로그인 (&L)";
        }

        private void EnableControls(bool enable)
        {
            chkPending.Enabled = enable;
            chkProgress.Enabled  = enable;
            chkCompleted.Enabled = enable;

            btnRefresh.Enabled  = enable;
            btnSettings.Enabled = enable;

            pnlOrders.Enabled = enable;

            UseWaitCursor = !enable;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm frm = new SettingsForm();
            //mCallback = frm;
            Hide();
            frm.ShowDialog();
            Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshOrderList();
        }

        private void RefreshOrderList()
        {
            var user = Global.userInfo;
            if ((user == null) || (user.stores == null))
                return;
            if (mAsyncActs > 0)
                return;
            mAsyncActs = 0;
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                List<long> storeIds = new List<long>();
                foreach (var store in user.stores)
                {
                    if ((store != null) && (store.storeId > 0))
                        storeIds.Add(store.storeId);
                }
                GetOrderListApi(storeIds, mSelProgress);
                Invoke(new Action(() => {
                    ShowOrderList();
                    EnableControls(true);
                }));
            }));
        }

        private void ShowOrderList()
        {
            pnlOrders.Controls.Clear();
            if (mShowOrders != null)
            {
                foreach (CPSOrderEntity order in mShowOrders)
                {
                    if ((order != null) && order.isShowing(mSelProgress))
                    {
                        OrderItemCtrl ctrl = new OrderItemCtrl();
                        ctrl.SetData(order, this);
                        pnlOrders.Controls.Add(ctrl);
                    }
                }
            }
        }

        private void ShowOrderByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type) || (mSelProgress == type))
                return;

            mSelProgress = type;
            chkPending.Checked   = mSelProgress == "PENDING";
            chkProgress.Checked  = mSelProgress == "PROCESSING";
            chkCompleted.Checked = mSelProgress == "COMPLETED";

            ShowOrderList();
        }

        private void chkType_Click(object sender, EventArgs e)
        {
            ShowOrderByType((string)((CheckBox)sender).Tag);
        }

        private void OnAlertTimer(object sender, EventArgs e)
        {
            var user = Global.userInfo;
            if ((user == null) || (user.stores == null))
                return;
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                List<long> storeIds = new List<long>();
                foreach (var store in user.stores)
                {
                    if ((store != null) && (store.storeId > 0))
                        storeIds.Add(store.storeId);
                }
                GetOrderListApi(storeIds, "PENDING");
                FetchReadyRemindersApi(storeIds, "PROCESSING");
                Invoke(new Action(() => {
                    ShowOrderList();
                    EnableControls(true);
                }));
            }));
            th.Start();
        }


        public void OnRejectOrderClick(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;

            CancelOrderForm frm = new CancelOrderForm();
            frm.mOrder = source.mOrder;
            Hide();
            if (frm.ShowDialog() == DialogResult.OK)
                RefreshOrderList();
            Show();
        }

        public void OnAcceptOrderClick(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;

            AcceptOrderForm frm = new AcceptOrderForm();
            frm.mOrder = source.mOrder;
            Hide();
            if (frm.ShowDialog() == DialogResult.OK)
                RefreshOrderList();
            Show();
        }

        public void OnCancelOrderClick(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;

            CancelOrderForm frm = new CancelOrderForm();
            frm.mOrder = source.mOrder;
            frm.mCancelType = "CANCEL";
            Hide();
            if (frm.ShowDialog() == DialogResult.OK)
                RefreshOrderList();
            Show();
        }

        public void OnCompleteOrderClick(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;
            mOrder = source.mOrder;

            var bTest = new List<string>() { "B" };
            var opts = mOrder.abTestKeyOptions;
            bool useMlPrepTime = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts) ||
                            CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);
            bool hasMlFoodPrepTime = useMlPrepTime ? (mOrder.mlFoodPrepTime != null) : false;

            EnableControls(false);
            mErrorMsg = null;
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                OrderReadyApi(hasMlFoodPrepTime);

                Invoke(new Action(() => {
                    if (!string.IsNullOrWhiteSpace(mErrorMsg))
                    {
                        MessageBox.Show(mErrorMsg);
                        EnableControls(true);
                    }
                    else
                        RefreshOrderList();
                }));
            }));
            th.Start();
        }

        public void OnNotifyCompleteClick(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;
            mOrder = source.mOrder;
            throw new NotImplementedException();
        }
        public void OnDelayOrderClick(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;
            mOrder = source.mOrder;
            throw new NotImplementedException();
        }

        public void OnShowOrderDetails(OrderItemCtrl source)
        {
            if ((source == null) || (source.mOrder == null))
                return;

            var order = source.mOrder;
            OrderDetailsForm frm = new OrderDetailsForm();
            frm.mOrderId = order.orderId;
            frm.mStoreId = order.storeId;
            Hide();
            if (frm.ShowDialog() == DialogResult.OK)
                RefreshOrderList();
            Show();
        }


        #region INIT_APIS
        public void PingWebSocketApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v1/ping")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            string version = setting.appVersion;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("client-sign", Global.clientSign);
            request.AddHeader("coupang-pos-version", version);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;
            if (_result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Global.SaveCookies(_result.Cookies);
                if (string.IsNullOrWhiteSpace(setting.deviceId))
                {
                    setting.deviceId = Guid.NewGuid().ToString();
                    Global.SaveAppSettings();
                    Global.RefreshCookies();
                }
            }
            mAsyncActs--;
        }

        public void GetMetaCsInfoApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v1/meta/cs-info")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v1/meta/cs-info", null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSMetaCsInfo> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSMetaCsInfo>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                        Console.WriteLine("CS-Info: " + res.message);
                }
                catch { }
            }
            mAsyncActs--;
        }

        public void VerifyAccountApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v2/auth/verify")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v2/auth/verify", null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSVerifiedAccountInfo> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSVerifiedAccountInfo>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                    {
                        var content = res.content;
                        if (content != null)
                            Global.verifiedStoreList = content.verifiedStoreList;
                    }
                }
                catch { }
            }
            mAsyncActs--;
        }

        public void SwitchEncryptApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v1/switch/encrypt")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v1/switch/encrypt", null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<string> res = JsonConvert.DeserializeObject<CPSDataApiResult<string>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                    {
                        if (Global.setting == null)
                            Global.setting = new CPSAppSettings();
                        Global.setting.encType = res.content;
                        Global.SaveAppSettings();
                    }
                }
                catch { }
            }
            mAsyncActs--;
        }


        private void SinginStoreApi(long storeId)
        {
            if ((storeId < 1) || (Global.setting == null))
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v2/auth/sign-in/store")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var data = new { storeId = storeId };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Origin", "chrome-extension://opbdabemkeebgjjopefgcjjccafieono");
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v2/auth/sign-in/store", data, "POST", "application/json");

            request.AddJsonBody(data);

            var ts = Task.Run(() => client.ExecuteAsync(request));
            ts.Wait();
            IRestResponse _result = ts.Result;
            Global.SaveCookies(_result.Cookies);

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSStoreModel> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSStoreModel>>(text);
                    if (res != null)
                        Console.WriteLine($"SignIn Store: {res.code}, {res.message}");
                }
                catch { }
            }
            mAsyncActs--;
        }

        private void SinginMultiStoreApi(List<long> storeIds)
        {
            if ((storeIds == null) || (storeIds.Count < 1) || (Global.setting == null))
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v2/auth/sign-in/stores")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var data = new { storeIds = storeIds.ToArray() };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Origin", "chrome-extension://opbdabemkeebgjjopefgcjjccafieono");
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v2/auth/sign-in/stores", data, "POST", "application/json");

            request.AddJsonBody(data);

            var ts = Task.Run(() => client.ExecuteAsync(request));
            ts.Wait();
            IRestResponse _result = ts.Result;
            Global.SaveCookies(_result.Cookies);

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<List<CPSStoreModel>> res = JsonConvert.DeserializeObject<CPSDataApiResult<List<CPSStoreModel>>>(text);
                    if (res != null)
                        Console.WriteLine($"SignIn Store: {res.code}, {res.message}");
                }
                catch { }
            }
            mAsyncActs--;
        }

        private void GetConfigInfoApi()
        {
            if (Global.setting == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v2/configuration")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var data = new { storeAbTestKeys = new string[] { "39663" } }; //AB_ID_VOICE_NOTIFICATION = 39663

            var request = new RestRequest(Method.POST);
            request.AddHeader("Origin", "chrome-extension://opbdabemkeebgjjopefgcjjccafieono");
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v2/configuration", data, "POST", "application/json");

            request.AddJsonBody(data);

            var ts = Task.Run(() => client.ExecuteAsync(request));
            ts.Wait();
            IRestResponse _result = ts.Result;
            Global.SaveCookies(_result.Cookies);

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSConfigurationInfo> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSConfigurationInfo>>(text);
                    if (res != null)
                        Console.WriteLine($"Get Configuration: {res.code}, {res.message}");
                }
                catch { }
            }
            mAsyncActs--;
        }

        private void FetchReadyRemindersApi(List<long> storeIds, string status)
        {
            if ((storeIds == null) || (storeIds.Count < 1) || string.IsNullOrWhiteSpace(status) || (Global.setting == null))
                return;
            string path = $"/v2/stores/ready_reminders?sort=OLD&storeIds={string.Join(",", storeIds)}&status={status}&limit=5";
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, null);

            var ts = Task.Run(() => client.ExecuteAsync(request));
            ts.Wait();
            IRestResponse _result = ts.Result;
            Global.SaveCookies(_result.Cookies);

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSConfigurationInfo> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSConfigurationInfo>>(text);
                    if (res != null)
                        Console.WriteLine($"Get Configuration: {res.code}, {res.message}");
                }
                catch { }
            }
            mAsyncActs--;
        }

        #endregion INIT_APIS


        #region ORDER_APIS
        //상태별 주문 리스트 얻기
        private void GetOrderListApi(List<long> storeIds, string status)
        {
            var user = Global.userInfo;
            if ((storeIds == null) || (storeIds.Count < 1) || (user == null) || (Global.setting == null) ||
                string.IsNullOrWhiteSpace(status))
                return;
            string path = $"/v2/stores/orders?storeIds={string.Join(",", storeIds)}&status={status}";
            object data = null;
            if (status == "COMPLETED")
            {
                DateTime today = DateTime.Today;
                DateTime endDate = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59, 999);
                data = new { startDate = today.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz"),
                               endDate = endDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz") };
            }

            var client = new RestClient($"https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, data);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;
            Global.SaveCookies(_result.Cookies);

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<List<CPSOrderEntity>> res = JsonConvert.DeserializeObject<CPSDataApiResult<List<CPSOrderEntity>>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                    {
                        var list = res.content;
                        if (status == mSelProgress)
                            mShowOrders = list;

                    }
                }
                catch { }
            }
            mAsyncActs--;
        }

        //배달 준비 완료 / 포장 완료
        public void OrderReadyApi(bool hasMlFoodPrepTime)
        {
            var user = Global.userInfo;
            if ((user == null) || (mOrder == null))
                return;
            string path = string.Format("/v1/stores/{0}/orders/{1}/ready", mOrder.storeId, mOrder.orderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var data = new
            {
                hasMlFoodPrepTime = hasMlFoodPrepTime
            };

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, data, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;

            IRestResponse _result = tb.Result;
            int code = (int)_result.StatusCode;
            if ((code < 200) || (code > 299))
                mErrorMsg = "주문 완료 실패!";
        }

        public void OrderPickupApi()
        {
            var user = Global.userInfo;
            if ((user == null) || (mOrder == null))
                return;
            string path = string.Format("/v1/stores/{0}/orders/{1}/customer-pickup", mOrder.storeId, mOrder.orderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, null, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;

            IRestResponse _result = tb.Result;
            int code = (int)_result.StatusCode;
            if ((code < 200) || (code > 299))
                mErrorMsg = "주문 완료 실패!";
        }


        #endregion ORDER_APIS


        #region LOGOUT_API
        public void CallLogoutApi()
        {
            var user = Global.userInfo;
            if (user == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v1/sign-out")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v1/sign-out", null, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;
        }
        #endregion LOGOUT_API
    }
}
