using CoupangSub.Models;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace CoupangSub.Orders
{
    public partial class OrderDetailsForm : Form
    {
        public long mStoreId { get; set; }
        public string mOrderId { get; set; }


        private int mAsyncActs = 0;
        private Timer mTimer = new Timer();

        private CPSOrderDetail mDetails = null;
        private string mCustomerPhone = "";
        private string mCourierPhone = "";


        public OrderDetailsForm()
        {
            InitializeComponent();
            mOrderId = "";
            mStoreId = 0;
            mDetails = null;
            mCustomerPhone = "";
            mCourierPhone  = "";
        }

        private void OrderDetailsForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mOrderId) || (mStoreId < 1) || !Global.HasStores())
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            mTimer.Enabled = true;
            mTimer.Interval = 2000;
            mTimer.Tick += OnLoadTimer;

            ClearValues();
        }

        private void OrderDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                GetOrderDetailsApi(); //orderDetailApi
                if ((mDetails != null) && !mDetails.isTutorialOrder())
                {
                    bool isNewProcessingItem = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_PROCESSING_DESIGN, new List<string>() { "C", "D" }, mDetails.abTestKeyOptions);

                    //배달 대행사가 선택 된 경우 그 연락처 얻기, courierSafeNumberApi
                    if (isNewProcessingItem)
                        GetCourierSafeNumberApi();

                    //고객 연락처 얻기, customerSafeNumberApi
                    GetCustomerSafeNumberApi();
                }
                Invoke(new Action(() =>
                {
                    if (mDetails == null)
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                    }
                    EnableControls(false);
                    UseWaitCursor = false;
                    ShowOrderDetails();
                }));
            }));
            th.Start();
        }

        private void ClearValues()
        {
            lstMenus.Columns.Clear();
            lstMenus.Columns.Add("메뉴", 200, HorizontalAlignment.Left);
            lstMenus.Columns.Add("수량", 60, HorizontalAlignment.Center);
            lstMenus.Columns.Add("금액", 120, HorizontalAlignment.Right);
            lstMenus.Columns.Add("상태", 40, HorizontalAlignment.Center);

            txtShopName.Text = "";
            txtOrderNo.Text = "";
            txtOrderTm.Text = "";
            txtCustomerOrderCount.Text = "";
            txtContacts.Text = "";
            txtRequests.Text = "";
            txtAddress.Text = "";
            lblTotalAmount.Text = "";
            txtTotalAmount.Text = "";
        }

        private void EnableControls(bool enable)
        {
            lstMenus.Enabled = enable;
        }

        private void ShowOrderDetails()
        {
            bool isDelivery = mDetails.isDeliveryOrder();
            //var bTest = new List<string>() { "B" };
            //var opts = mDetails.abTestKeyOptions;
            //bool useDelayBlocks = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts) ||
            //                    CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);
            //bool isCourierAssigned = mDetails.isCourierAssigned();
            
            //bool isAndon = isDelivery && !isCourierAssigned && (mDetails.courierSupplyStatusType != null);
            //bool isFoodReady = mDetails.state.statusValue == "MERCHANT_READY";
            //bool isCourierPriority = isDelivery && !isCourierAssigned && isFoodReady && !useDelayBlocks; // delayBlocks 에서 삭제
            //bool shownEdpNumber = isDelivery && isCourierAssigned;
            //bool isNewProcessingItem = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_PROCESSING_DESIGN, new List<string>() { "C", "D" }, opts);
            //bool isMlPrepTime = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME, bTest, opts) ||
            //                    CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME_V2, bTest, opts) ||
            //                    CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts) ||
            //                    CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);

            string title = isDelivery ? "배달" : "포장";
            Text = string.Format("주문 상세 정보 - {0} {1}", title, (mDetails.pickupOrderId > 0) ?
                                        mDetails.pickupOrderId.ToString() : mDetails.abbrOrderId);

            bool isTotalCancelled = mDetails.isTotalCancelled();
            bool isPartialCancelled = mDetails.isPartialCancelled();

            var shop = Global.GetStoreById(mStoreId);
            if (shop != null)
                txtShopName.Text = shop.storeName ?? "";
            txtOrderNo.Text = mDetails.orderId;
            txtOrderTm.Text = Global.EpochSeconds2Date(mDetails.orderedAt.dateTime).ToString("yyyy-MM-dd HH:mm:ss");

            string text = "고객 주문 횟수: " + mDetails.customerOrderCount;
            var state = mDetails.state;
            if ((state != null) && !string.IsNullOrWhiteSpace(state.statusText))
                text += (" 상태: " + state.statusText);
            txtCustomerOrderCount.Text = text;

            List<string> contacts = new List<string>();
            if (!string.IsNullOrWhiteSpace(mCustomerPhone))
                contacts.Add("고객: " + mCustomerPhone);
            if (!string.IsNullOrWhiteSpace(mCourierPhone))
                contacts.Add("배달: " + mCourierPhone);
            txtContacts.Text = string.Join(", ", contacts);

            if (mDetails.hasAlcohol == true)
                txtRequests.Text = mDetails.alcoholName + " " + mDetails.alcoholNote;
            else
                txtRequests.Text = "";

            if (isDelivery && (mDetails.destination != null))
            {
                var loc = mDetails.destination.location;
                if (loc != null)
                    txtAddress.Text = loc.address ?? "";
            }
            else
                txtAddress.Text = "";

            List<string> values = new List<string>();
            lblTotalAmount.Text = isTotalCancelled ? "취소금액" : "최종금액";
            if (!isPartialCancelled && !isTotalCancelled)
                values.Add(mDetails.calculateTotalQuantity().ToString("N0") + "건");
            values.Add(mDetails.salePrice.ToString("N0") + "원");
            txtTotalAmount.Text = string.Join(", ", values);

            lstMenus.Items.Clear();
            ListViewItem item = null;
            foreach (var od in mDetails.items) {
                item = new ListViewItem(od.name);
                item.Tag = od;
                item.SubItems.Add(od.quantity.ToString("N0") + "개");
                item.SubItems.Add(od.getTotalDishPrice().ToString("N0") + "원");
                //상태, 알코올 포함, 취소된 메뉴 상태
                string msg = "";
                if (od.isAlcohol)
                    msg = "알코올 포함";
                var cancelled = mDetails.cancelledItems;
                if (cancelled != null)
                {
                    int index = cancelled.FindIndex(r => r.dishId == od.dishId);
                    if ((index >= 0) && (index < cancelled.Count))
                    {
                        if (!string.IsNullOrWhiteSpace(msg))
                            msg += ", ";
                        msg += "취소됨";
                    }
                }
                item.SubItems.Add(msg);
                lstMenus.Items.Add(item);
                //옵션들의 추가
                if (od.itemOptions != null)
                {
                    foreach (var opt in od.itemOptions)
                    {
                        item = new ListViewItem(" ㄴ " + opt.optionName);
                        item.Tag = opt;
                        item.SubItems.Add(opt.optionQuantity.ToString("N0") + "개");
                        item.SubItems.Add(opt.getTotalPrice().ToString("N0") + "원");
                        item.SubItems.Add("");
                        lstMenus.Items.Add(item);
                    }
                }
            }
            if (isPartialCancelled)
            {
                item = new ListViewItem("최초 주문금액");
                item.Tag = null;
                item.SubItems.Add("");
                item.SubItems.Add(mDetails.initialSalePrice.ToString("N0") + "원");
                item.SubItems.Add("");
                lstMenus.Items.Add(item);
                item = new ListViewItem("취소금액");
                item.Tag = null;
                item.SubItems.Add("");
                item.SubItems.Add("-" + mDetails.canceledPrice.ToString("N0") + "원");
                item.SubItems.Add("");
                lstMenus.Items.Add(item);
            }
        }



        public void GetOrderDetailsApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            string path = string.Format("/v1/stores/{0}/orders/{1}", mStoreId, mOrderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSOrderDetail> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSOrderDetail>>(text);
                    if (res != null)
                    {
                        if (res.code == "SUCCESS")
                            mDetails = res.content;
                    }
                }
                catch { }
            }
            mAsyncActs--;
        }

        public void GetCourierSafeNumberApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            string path = string.Format("/v1/safe-number/{0}/orders/{1}/courier", mStoreId, mOrderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSSafeNumberInfo> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSSafeNumberInfo>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                        mCourierPhone = res.content?.safeNumber ?? "";
                }
                catch { }
            }
            mAsyncActs--;
        }

        public void GetCustomerSafeNumberApi()
        {
            var setting = Global.setting;
            if (setting == null)
                return;
            string path = string.Format("/v1/safe-number/{0}/orders/{1}/customer", mStoreId, mOrderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, null);

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            IRestResponse _result = tb.Result;

            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPSDataApiResult<CPSSafeNumberInfo> res = JsonConvert.DeserializeObject<CPSDataApiResult<CPSSafeNumberInfo>>(text);
                    if ((res != null) && (res.code == "SUCCESS"))
                        mCustomerPhone = res.content?.safeNumber ?? "";
                }
                catch { }
            }
            mAsyncActs--;
        }
    }
}
