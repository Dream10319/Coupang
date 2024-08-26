using CoupangWeb.Auth;
using CoupangWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace CoupangWeb.Reviews
{
    //코멘트 추가, 삭제 API 확인
    public partial class ReviewListForm : Form, ReviewItemCtrl.IEventCallback
    {
        int REVIEW_PAGE_COUNT = 5;

        private int mStoreIndex = -1;       //상점 번호
        private string mTypeSel = "";
        private int mCurPage = 0;           //현재 페이지번호, 0부터 시작
        private int mPageCnt = 1;           //페이지 개수, 1부터 시작

        private Timer mTimer = new Timer();
        private int mAsyncActs = 0;     //API호출 동작 회수

        private DateTime mStartDate = DateTime.MinValue;
        private DateTime mEndDate = DateTime.MinValue;
        private CPWReviewSearchData mReviewList = null;
        private CPWReviewSummaryData mSummary = null;



        public ReviewListForm()
        {
            InitializeComponent();
        }

        private void ReviewListForm_Load(object sender, EventArgs e)
        {
            if (!Global.HasStores())
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            ClearValues();

            mTimer.Enabled = true;
            mTimer.Interval = 2000;
            mTimer.Tick += OnLoadTimer;

            EnableControls(false);
        }

        private void ReviewListForm_FormClosing(object sender, FormClosingEventArgs e)
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
            foreach (var shop in Global.shopList)
                cmbStores.Items.Add(shop.GetDisplayName());
            mStoreIndex = 0;
            cmbStores.SelectedIndex = 0;

            InitDates();
            SetViewType("EXPOSE");

            txtAvgRating.Text = "";
            txtTotalCount.Text = "";
            txtReplyCount.Text = "";
            txtNoReplyCount.Text = "";

            txtPageNo.Text = "";
        }

        private void InitDates()
        {
            DateTime td = DateTime.Today;
            //DateTime dm = td.AddYears(-5);
            //dtcStart.MinDate = dm;
            //dtcEnds.MinDate = dm;
            dtcStart.MaxDate = td;
            dtcEnds.MaxDate = td;

            dtcEnds.Value = td;
            dtcStart.Value = td.AddDays(-7);

            mStartDate = dtcStart.Value;
            mEndDate = dtcEnds.Value;
        }

        private void EnableControls(bool enable)
        {
            cmbStores.Enabled = enable;

            dtcStart.Enabled = enable;
            dtcEnds.Enabled = enable;

            btnInquiry.Enabled = enable;

            chkTotal.Enabled = enable;
            chkNoRes.Enabled = enable;
            chkBlock.Enabled = enable;

            btnFirstPage.Enabled = enable;
            btnLastPage.Enabled = enable;
            btnNextPage.Enabled = enable;
            btnPrevPage.Enabled = enable;
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Stop();
            mTimer.Tick -= OnLoadTimer;
            mPageCnt = 0;
            mCurPage = 0;
            RefreshReviewList(true, 0);
        }

        private void SetViewType(string type)
        {
            mTypeSel = type;
            chkTotal.Checked = mTypeSel == "EXPOSE";
            chkNoRes.Checked = mTypeSel == "BLIND";
            chkBlock.Checked = mTypeSel == "SUSPEND";
        }

        private void cmbStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newIdx = cmbStores.SelectedIndex;
            if (mStoreIndex != newIdx)
            {
                mStoreIndex = newIdx;
                mPageCnt = 0;
                mCurPage = 0;
                RefreshReviewList(false, 0);
            }
        }

        private void chkType_Click(object sender, EventArgs e)
        {
            string type = (string) ((CheckBox)sender).Tag;
            if (mTypeSel != type)
            {
                SetViewType(type);
                mPageCnt = 0;
                mCurPage = 0;
                RefreshReviewList(false, 0);
            }
        }

        private bool IsSelectionChanged()
        {
            return (mStoreIndex != cmbStores.SelectedIndex) || !mStartDate.Equals(dtcStart.Value) || mEndDate.Equals(dtcEnds.Value);
        }

        private void ChangeSelection()
        {
            if (IsSelectionChanged())
            {
                btnFirstPage.Enabled = false;
                btnLastPage.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNextPage.Enabled = false;
            }
            else
                AdjustPages();
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
            /*var diff = DateTime.Today.AddMonths(-6) - dtcStart.Value;
            if (diff.Days > 0)
            {
                MessageBox.Show("최대 6개월 조회가 가능합니다.");
                return false;
            }
            diff = DateTime.Today.AddMonths(-6) - dtcEnds.Value;
            if (diff.Days > 0)
            {
                MessageBox.Show("최대 6개월 조회가 가능합니다.");
                return false;
            }*/
            var diff = dtcStart.Value - dtcEnds.Value;
            if (diff.Days > 0)
            {
                MessageBox.Show("종료일이 시작일보다 빠릅니다.");
                return false;
            }
            mStartDate = dtcStart.Value;
            mEndDate = dtcEnds.Value;
            return true;
        }

        private void btnInquiry_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;
            if (IsSelectionChanged())
            {
                mPageCnt = 0;
                mCurPage = 0;
                RefreshReviewList(false, 0);
            }
            else
                RefreshReviewList(false, mCurPage);
        }

        public void OnChangedReply(ReviewItemCtrl source)
        {

        }

        private void ShowSummaryInfo()
        {
            if (mSummary != null)
            {
                txtAvgRating.Text    = string.Format("평균별점 (최근 12주 기준)\r\n{0}", mSummary.averageTotalRating);
                txtTotalCount.Text   = string.Format("전체\r\n{0}개", mSummary.totalReviewCount);
                txtReplyCount.Text   = string.Format("답변\r\n{0}개", mSummary.totalReplyCount);
                txtNoReplyCount.Text = string.Format("미답변\r\n{0}개", mSummary.totalNoReplyCount);
            } else
            {
                txtAvgRating.Text    = "";
                txtTotalCount.Text   = "";
                txtReplyCount.Text   = "";
                txtNoReplyCount.Text = "";
            }
        }

        private void AdjustPages()
        {
            if ((mReviewList != null) && (mCurPage < 1))
                mPageCnt = (mReviewList.total + 4) / 5;

            if (mPageCnt < 1)
            {
                btnFirstPage.Visible = false;
                btnLastPage.Visible = false;
                btnNextPage.Visible = false;
                btnPrevPage.Visible = false;
                txtPageNo.Visible = false;
                return;
            }

            btnFirstPage.Visible = true;
            btnLastPage.Visible = true;
            btnNextPage.Visible = true;
            btnPrevPage.Visible = true;
            txtPageNo.Visible = true;
            if (mPageCnt < mCurPage)
                mCurPage = mPageCnt - 1;
            txtPageNo.Text = (mPageCnt < 1) ? "" : (string.Format("{0} / {1}", mCurPage + 1, mPageCnt));
            bool enable = mCurPage > 0;
            btnFirstPage.Enabled = enable;
            btnPrevPage.Enabled = enable;
            enable = mCurPage < (mPageCnt - 1);
            btnLastPage.Enabled = enable;
            btnNextPage.Enabled = enable;
        }

        private void ShowReviews()
        {
            pnlReviews.Controls.Clear();
            try
            {
                CPWShopData store = Global.GetStore(mStoreIndex);
                if ((mReviewList != null) && (store != null) && (store.id > 0))
                {
                    foreach (var item in mReviewList.content)
                    {
                        ReviewItemCtrl ctrl = new ReviewItemCtrl();
                        ctrl.SetForm(this);
                        ctrl.SetData(item, store.id, this);
                        pnlReviews.Controls.Add(ctrl);
                    }
                }
            }
            catch { }
        }

        private void RefreshReviewList(bool loading, int page)
        {
            if ((mAsyncActs > 0) || (page < 0))
                return;
            mAsyncActs = 0;
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                SearchReviewsApi(page);
//                if (loading)
                    GetReviewSummaryApi();

                Invoke(new Action(() => {
                    EnableControls(true);

//                    if (loading)
                        ShowSummaryInfo();

                    AdjustPages();
                    ShowReviews();
                }));
            }));
            th.Start();
        }

        private void GoToPage(int target)
        {
            int page = target;
            if (page < 0) page = 0;
            if (page >= mPageCnt)
                page = mPageCnt - 1;
            if (page != mCurPage)
            {
                mCurPage = page;
                RefreshReviewList(false, page);
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            GoToPage(mCurPage - 1);
        }
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            GoToPage(mCurPage + 1);
        }
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            GoToPage(0);
        }
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            GoToPage(mPageCnt - 1);
        }


        public void SearchReviewsApi(int page)
        {
            var store = Global.GetStore(mStoreIndex);
            if ((page < 0) || (store == null) || (store.id < 1))
                return;

            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/reviews/search")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/reviews");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddQueryParameter("storeId", store.id.ToString());
            request.AddQueryParameter("page", (page + 1).ToString());
            request.AddQueryParameter("statusType", mTypeSel);
            request.AddQueryParameter("startDateTime", mStartDate.ToString("yyyy-MM-dd"));
            request.AddQueryParameter("exclusiveEndDateTime", mEndDate.ToString("yyyy-MM-dd"));
            request.AddQueryParameter("size", REVIEW_PAGE_COUNT.ToString());


            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<CPWReviewSearchData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWReviewSearchData>>(text);
                    if ((res != null) && (res.data != null))
                        mReviewList = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        public void GetReviewSummaryApi()
        {
            var store = Global.GetStore(mStoreIndex);
            if ((store == null) || (store.id < 1))
                return;
            var client = new RestClient("https://store.coupangeats.com/api/v1/merchant/reviews/summary")
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/menu/");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddQueryParameter("storeId", store.id.ToString());

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<CPWReviewSummaryData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWReviewSummaryData>>(text);
                    if ((res != null) && (res.data != null))
                        mSummary = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }
    }
}
