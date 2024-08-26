using CoupangWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace CoupangWeb.Orders
{
    public partial class OrderStatsForm : Form
    {
        private const int ROW_COUNT = 10;

        private int mCurPage = 0;       //현재 페이지번호
        private int mPageCnt = 1;       //Page Count

        private Timer mTimer = new Timer();
        private int mAsyncActs = 0;     //API호출 동작 회수

        private int mStoreIndex = -1;
        private DateTime mStartDate = DateTime.MinValue;
        private DateTime mEndDate = DateTime.MinValue;





        public OrderStatsForm()
        {
            InitializeComponent();
        }

        private void OrderStatsForm_Load(object sender, EventArgs e)
        {
            if (!Global.HasStores())
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            ClearValues();
            EnableControls(false);

            mTimer.Enabled = true;
            mTimer.Interval = 2000;
            mTimer.Tick += OnLoadTimer;
        }

        private void OrderStatsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mTimer.Stop();
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }
        }

        private void ClearValues()
        {
            //Store names
            cmbStores.Items.Clear();
//            cmbStores.Items.Add("가게 전체");
            foreach (var shop in Global.shopList)
                cmbStores.Items.Add(shop.GetDisplayName());
            mStoreIndex = 0;
            cmbStores.SelectedIndex = 0;

            //Columns for List
            lstOrders.Columns.Add("주문일", 200, HorizontalAlignment.Left);
            lstOrders.Columns.Add("주문번호", 170, HorizontalAlignment.Center);
            lstOrders.Columns.Add("주문내역", 300, HorizontalAlignment.Left);
            lstOrders.Columns.Add("매출액", 300, HorizontalAlignment.Left);

            mEndDate = DateTime.Today;
            mStartDate = mEndDate.AddDays(-7);
            dtcEnds.Value = mEndDate;
            dtcStart.Value = mStartDate;

            txtStats.Text = "";
            txtPageNo.Text = "";
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Stop();
            RefreshOrderList(true, 0);
        }

        private void EnableControls(bool enable)
        {
            cmbStores.Enabled = enable;
            btnInquiry.Enabled = enable;

            dtcStart.Enabled = enable;
            dtcEnds.Enabled = enable;

            btnFirstPage.Enabled = enable;
            btnLastPage.Enabled = enable;
            btnNextPage.Enabled = enable;
            btnPrevPage.Enabled = enable;
        }



        private bool IsSelectionChanged()
        {
            return (mStoreIndex != cmbStores.SelectedIndex) || !mStartDate.Equals(dtcStart.Value) || !mEndDate.Equals(dtcEnds.Value);
        }

        private void ChangeSelection()
        {
            if (IsSelectionChanged())
            {
                btnFirstPage.Enabled = false;
                btnLastPage.Enabled  = false;
                btnPrevPage.Enabled  = false;
                btnNextPage.Enabled  = false;
            }
            else
                AdjustPages();
        }

        private void cmbStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelection();
        }

        private void dtcStart_ValueChanged(object sender, EventArgs e)
        {
            ChangeSelection();
        }

        private void dtcEnds_ValueChanged(object sender, EventArgs e)
        {
            ChangeSelection();
        }

        private bool ValidateInputs()
        {
            //Check If valid parameters
            int index = cmbStores.SelectedIndex;
            if ((index < 0) || (index >= cmbStores.Items.Count))
                return false;

            var dtEnds = dtcEnds.Value;
            var dStart = dtcStart.Value;
            /*var dLimit = dtEnds.AddMonths(-6);
            var diff = dStart - dLimit;
            if (diff.Days < 0)
            {
                MessageBox.Show("최대 6개월 조회가 가능합니다.");
                return false;
            }*/
            var diff = dtEnds - dStart;
            if (diff.Days < 0)
            {
                MessageBox.Show("종료일이 시작일보다 빠릅니다.");
                return false;
            }

            //Set Values Here
            mStoreIndex = cmbStores.SelectedIndex;

            mStartDate = dtcStart.Value;
            mEndDate = dtcEnds.Value;
            return true;
        }

        private void RefreshOrderList(bool loading, int page)
        {
            if (mAsyncActs > 0)
                return;
            mAsyncActs = 0;
            if (!ValidateInputs())
                return;
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                GetOrderStatsApi(page);
            }));
            th.Start();
        }

        private void btnInquiry_Click(object sender, EventArgs e)
        {
            if (IsSelectionChanged())
                RefreshOrderList(false, 0);
            else
                RefreshOrderList(false, mCurPage);
        }

        private void AdjustPages()
        {
            if (mCurPage >= mPageCnt)
                mCurPage = mPageCnt - 1;
            txtPageNo.Text = (mPageCnt < 1) ? "" : string.Format("{0} / {1}", mCurPage + 1, mPageCnt);
            bool enable = mCurPage > 0;
            btnFirstPage.Enabled = enable;
            btnPrevPage.Enabled  = enable;
            enable = mCurPage < (mPageCnt - 1);
            btnLastPage.Enabled  = enable;
            btnNextPage.Enabled  = enable;
        }

        private void ShowOrders(List<CPWOrderContent> contents)
        {
            lstOrders.Items.Clear();
            if (contents != null)
            {
                foreach (var order in contents)
                {
                    if (order != null)
                    {
                        ListViewItem item = new ListViewItem(order.GetOrderedTimeText());
                        item.SubItems.Add(order.abbrOrderId ?? "");
                        item.SubItems.Add(order.GetShortItemsText());
                        item.SubItems.Add(order.GetPriceText());
                        lstOrders.Items.Add(item);
                    }
                }
            }
        }

        private void GoToPage(int target)
        {
            int page = target;
            if (page < 0) page = 0;
            if (page >= mPageCnt) page = mPageCnt - 1;
            if (page != mCurPage)
            {
                mCurPage = page;
                RefreshOrderList(false, page);
            }
        }


        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            GoToPage(0);
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            GoToPage(mCurPage - 1);
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            GoToPage(mCurPage + 1);
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            GoToPage(mPageCnt - 1);
        }




        private async void GetOrderStatsApi(int page)
        {
            var store = Global.GetStore(mStoreIndex);
            if ((store == null) || (store.id < 1) || (page < 0))
            {
                Invoke(new Action(() => {
                    EnableControls(true);
                }));
                return;
            }

            mAsyncActs++;
            string url = string.Format("https://store.coupangeats.com/api/v1/merchant/web/order/condition");
            var client = new RestClient(url)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Origin", "https://store.coupangeats.com");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/orders/" + store.id);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddJsonBody(new
            {
                pageNumber = page,
                pageSize = ROW_COUNT,
                storeId = store.id,
                startDate = Global.EpochSeconds4Date(mStartDate) * 1000,
                endDate = Global.EpochSeconds4Date(mEndDate) * 1000 + 86400000 - 1
            });

            IRestResponse _result = await Task.Run(() => client.ExecuteAsync(request));
            Invoke(new Action(() => {
                Global.SaveCookies(_result.Cookies);

                string text = _result.Content;
                string msg = "주문 통계 조회에 실패했습니다.";
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        CPWOrderConditionResult res = JsonConvert.DeserializeObject<CPWOrderConditionResult>(text);
                        if (res != null)
                        {
                            txtStats.Text = string.Format("매출액: {1}원   주문 수: {0:N0}건   평균 주문 금액: {2}원", res.totalOrderCount ?? 0,
                                                    (res.totalSalePrice ?? 0).ToString("0.##"), (res.avgOrderAmount ?? 0).ToString("0.##"));
                            msg = "";
                            var pageVo = res.orderPageVo;
                            if (pageVo != null)
                            {
                                mPageCnt = pageVo.total;
                                var list = pageVo.content;
                                if (list != null)
                                    ShowOrders(list);
                            }
                            mCurPage = page;
                            AdjustPages();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                mAsyncActs--;
                if (mAsyncActs < 1)
                    EnableControls(true);
                if (!string.IsNullOrWhiteSpace(msg))
                    MessageBox.Show(msg);
            }));
        }
    }
}
