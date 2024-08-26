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
using Timer = System.Windows.Forms.Timer;


namespace CoupangSub.Orders
{
    public partial class CancelOrderForm : Form
    {
        private int mAsyncActs = 0;
        private string mErrorMsg = "";
        private Timer mTimer = new Timer();
        private List<CPSCancelCode> mCancelCodes;

        public string mCancelType { get; set; }   //REDIRECT (Default value), CANCEL
        public CPSOrderEntity mOrder { get; set; }


        public CancelOrderForm()
        {
            InitializeComponent();
        }

        private void CancelOrderForm_Load(object sender, EventArgs e)
        {
            if ((mOrder == null) || !mOrder.isCancellable())
            {
                Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(mCancelType))
                mCancelType = "REJECT";

            Text = mCancelType == "REJECT" ? "주문거부사유 선택" : "주문취소사유 선택";
            pnlMenus.Visible = false;

            mTimer.Enabled = true;
            mTimer.Interval = 2000;
            mTimer.Tick += OnLoadTimer;

            mCancelCodes = null;
            listFoods();
        }

        private void CancelOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Tick -= OnLoadTimer;
            mTimer.Stop();
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                GetCancelCodesApi();
                Invoke(new Action(() => {
                    EnableControls(true);
                    ShowCancelReasons();
                }));
            }));
            th.Start();
        }

        private void EnableControls(bool enable)
        {
            clbReasons.Enabled = enable;
            pnlMenus.Enabled = enable;
            foreach (var ctrl in pnlMenus.Controls)
            {
                if (ctrl is BlockMenuItemCtrl)
                    (ctrl as BlockMenuItemCtrl).EnableMenu(enable);
            }

            btnOkay.Enabled = enable;
            btnCancel.Enabled = enable;
        }

        private void listFoods()
        {
            pnlMenus.Controls.Clear();
            if ((mOrder == null) || (mOrder.items == null))
                return;
            foreach (var menu in mOrder.items)
            {
                BlockMenuItemCtrl ctrl = new BlockMenuItemCtrl();
                ctrl.SetData(menu);
                pnlMenus.Controls.Add(ctrl);
            }
        }

        private void ShowCancelReasons()
        {
            if (mCancelCodes == null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            clbReasons.Items.Clear();
            foreach (var item in mCancelCodes)
                clbReasons.Items.Add(item.message);
        }

        private void clbReasons_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            for (int ix = 0; ix < clbReasons.Items.Count; ++ix)
                if (ix != index) clbReasons.SetItemChecked(ix, false);
            if ((mCancelCodes == null) || (index < 0) || (index >= mCancelCodes.Count))
                return;
            pnlMenus.Visible = (mCancelCodes[index].cancellationReasonId == CPSCancelCode.SOLD_OUT);
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            var sel = clbReasons.CheckedIndices;
            string msg = mCancelType == "REJECT" ? "거부사유를 선택해주세요." : "취소사유를 선택해주세요.";
            if ((sel == null) || (sel.Count < 1) || (mCancelCodes == null))
            {
                MessageBox.Show(msg);
                return;
            }
            int index = sel[0];
            string cancelCode = mCancelCodes[index].cancellationReasonId;
            if (string.IsNullOrWhiteSpace(cancelCode))
            {
                MessageBox.Show(msg);
                return;
            }
            bool needBlock = mCancelCodes[index].cancellationReasonId == CPSCancelCode.SOLD_OUT;
            if (needBlock)
            {
                MessageBox.Show("품절처리할 메뉴와 옵션들을 선택해주세요.");
                return;
            }

            EnableControls(false);
            mErrorMsg = null;
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                if (mCancelType == "REJECT")
                    RejectOrderApi(cancelCode);
                else
                    CancelOrderApi(cancelCode);

                Invoke(new Action(() => {
                    if (!string.IsNullOrWhiteSpace(mErrorMsg))
                    {
                        MessageBox.Show(mErrorMsg);
                        EnableControls(true);
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }));
            }));
            th.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }




        public void GetCancelCodesApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            var client = new RestClient("https://pos-api.coupang.com/api/v1/codes/cancel-reasons")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), "/v1/codes/cancel-reasons", null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<List<CPSCancelCode>> res = JsonConvert.DeserializeObject<CPSDataApiResult<List<CPSCancelCode>>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                        Console.WriteLine("CS-Info: " + res.message);
                }
                catch { }
            }
        }

        public void RejectOrderApi(string cancelCode)
        {
            var user = Global.userInfo;
            if ((user == null) || (mOrder == null))
                return;
            string path = string.Format("/v1/stores/{0}/orders/{1}/reject", mOrder.storeId, mOrder.orderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var data = new
            {
                cancelReasonId = cancelCode
            };

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, data, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;

            IRestResponse _result = tb.Result;
            int code = (int)_result.StatusCode;
            if ((code < 200) || (code > 299))
                mErrorMsg = "주문 거부 실패!";
        }

        public void CancelOrderApi(string cancelCode)
        {
            var user = Global.userInfo;
            if ((user == null) || (mOrder == null))
                return;
            string path = string.Format("/v1/stores/{0}/orders/{1}/cancel", mOrder.storeId, mOrder.orderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var data = new
            {
                cancelReasonId = cancelCode
            };

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, data, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;

            IRestResponse _result = tb.Result;
            int code = (int)_result.StatusCode;
            if ((code < 200) || (code > 299))
                mErrorMsg = "주문 취소 실패!";
        }

    }
}
