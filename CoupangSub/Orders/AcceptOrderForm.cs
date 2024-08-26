using CoupangSub.Models;
using Newtonsoft.Json.Linq;
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

namespace CoupangSub.Orders
{
    public partial class AcceptOrderForm : Form
    {
        public CPSOrderEntity mOrder { get; set; }


        private const int TYPE_GENERAL = 0;
        private const int TYPE_SUGGEST = 1;
        private const int TYPE_ENHANCED = 2;

        private int mAsyncActs = 0;
        private string mErrorMsg = "";

        private int mTimeType = 0;
        private long mValue = 15;
        private long mPreValue = 0;
        private long mMinValue = 1;
        private long mMaxValue = 60;
        private long mlTime = 0;
        private bool noMlTime = false;






        public AcceptOrderForm()
        {
            InitializeComponent();
        }

        private void AcceptOrderForm_Load(object sender, EventArgs e)
        {
            if ((Global.userInfo == null) || !Global.HasStores() ||
                (mOrder == null) || string.IsNullOrWhiteSpace(mOrder.orderId))
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            ClearValues();
        }

        private void AcceptOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }
        }

        private void ClearValues()
        {
            var bTest = new List<string>() { "B" };
            var opts = mOrder.abTestKeyOptions;
            bool useRecommendTime   = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME, bTest, opts);
            bool useRecommendTimeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME_V2, bTest, opts);
            bool useDelayBlocks     = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts);
            bool useEnhancedBusyModeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);

            mTimeType = TYPE_GENERAL;
            if (useRecommendTimeV2 || useDelayBlocks || useEnhancedBusyModeV2)
                showEnhancedSettingTime();
            else
            {
                if (useRecommendTime)
                    showSuggestSettingTime();
                else
                    showSettingTime();
            }
        }

        private void showEnhancedSettingTime()
        {
            mTimeType = TYPE_ENHANCED;

            mlTime = mOrder.mlFoodPrepTime ?? 15;
            noMlTime = mOrder.mlFoodPrepTime == null;

            var bTest = new List<string>() { "B" };
            var opts = mOrder.abTestKeyOptions;
            bool useEnhancedBusyModeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);
            bool useDelayOnly = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts);
            bool useDelayBlocks = useDelayOnly || useEnhancedBusyModeV2;
            var abuseMode = mOrder.store.storeModeDto?.abuseMode ?? null;
            var abuseType = mOrder.store.storeModeDto?.abuseType ?? null;
            string isDelayAbuseType = (!noMlTime && useDelayOnly && (abuseMode == "DELAY")) ? abuseType : null;
            string isBusyAbuseType = (!noMlTime && useEnhancedBusyModeV2 && (abuseMode == "BUSY")) ? abuseType : null;

            mValue = mlTime;
            mPreValue = -1;
            mMinValue = 1;
            mMaxValue = noMlTime ? 60 : Math.Max(mlTime + 10, 60);
            if (useDelayBlocks && ((isDelayAbuseType != null) || (isBusyAbuseType != null)) && (mlTime <= 60))
            {
                var storeMode = mOrder.store.storeModeDto;
                long abusedMaxTime = mlTime + ((storeMode != null) ? storeMode.maxPlusPrepareTime : 10);
                mMaxValue = Math.Min(abusedMaxTime, 70);
            }

            Text = noMlTime ? "조리 시간 입력" : (useDelayBlocks ? "예상 조리 시간 변경" : "조리 시간 변경");
            string text = (mOrder.isTutorialOrder() || useDelayBlocks) ? "예상 조리 시간을 입력해주세요." : "정확한 조리 시간을 입력해주세요.";
            if (!noMlTime)
                text += ("\r\n  권장 시간" + mlTime + "분");
            if (useDelayBlocks)
            {
                if (isBusyAbuseType != null)
                {
                    text += "\r\n" + (isBusyAbuseType == "HALF_HOUR" ? "고객 불편을 줄이기 위해 일시적으로 조리 시간 변경과 매장 상태 변경이 제한됩니다." :
                                                                "고객 불편을 줄이기 위해 오늘 마감 시간까지 조리 시간 변경과 매장 상태 변경이 제한됩니다.");
                } else if (isDelayAbuseType != null)
                {
                    text += "\r\n" + (isDelayAbuseType == "HALF_HOUR" ? "고객 불편을 줄이기 위해 일시적으로 조리 시간 변경이 제한됩니다." :
                                                                "고객 불편을 줄이기 위해 오늘 마감 시간까지 조리 시간 변경이 제한됩니다.");
                }
            }
            txtNotices.Text = text;

            cmbTimes.Visible = false;
            pnlTimes.Visible = true; //EnhancedTimeController
            btnMinus5.Visible = true;
            btnPlus5.Visible = true;
            txtError.Visible = true;

            btnOkay.Text = "주문 수락하기(&A)";
            enhancedShowValue();
        }

        private void showSuggestSettingTime()
        {
            mTimeType = TYPE_SUGGEST;

            mlTime = mOrder.mlFoodPrepTime ?? 15;
            noMlTime = mOrder.mlFoodPrepTime == null;

            mValue = mlTime;
            mPreValue = -1;
            mMinValue = 1;
            mMaxValue = mlTime > 60 ? (mlTime + 10) : 60;

            Text = "조리 시간 변경";
            string text = "정확한 조리 시간을 입력해주세요.";
            if (!noMlTime)
                text += ("\r\n  권장 시간" + mlTime + "분");
            txtNotices.Text = text;

            cmbTimes.Visible = false;
            pnlTimes.Visible = true; //BusyTimeController
            btnMinus5.Visible = false;
            btnPlus5.Visible = false;

            btnOkay.Text = "주문 수락하기(&A)";
            suggestShowValue();
        }

        private void showSettingTime()
        {
            mTimeType = TYPE_GENERAL;

            bool isPickupOrder = mOrder.isPickUpOrder();
            bool shownRangeDuration = !isPickupOrder;

            mValue = 20;
            mMinValue = 5;
            mMaxValue = 60;

            Text = "예상 조리 시간";
            txtNotices.Text = isPickupOrder ? "입력한 조리시간이 고객에게 안내됩니다.\r\n정확한 조리 시간을 선택해주세요!" : "‘최대한 짧고 정확한’ 조리시간을 선택해 주세요.";
            txtError.Text = "";

            cmbTimes.Visible = true;
            pnlTimes.Visible = false; //BusyTimeController
            btnMinus5.Visible = false;
            btnPlus5.Visible = false;
            txtError.Visible = false;

            cmbTimes.Items.Clear();
            for (long min = mMinValue; min <= mMaxValue; min += 5)
            {
                string time = shownRangeDuration ? (min < 5 ? "" : string.Format("{0} ~ {1}분", min - 5, min)) : (min + "분");
                cmbTimes.Items.Add(time);
            }
            cmbTimes.SelectedIndex = (int) ((mValue - mMinValue) / 5);

            btnOkay.Text = "확인(&O)";
        }

        private void cmbTimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            long value = mMinValue + (cmbTimes.SelectedIndex * 5);
            if ((value >= mMinValue) && (value <= mMaxValue))
                mValue = value;
        }

        private void suggestHandleMinus()
        {
            if (mValue == 1)
                return;
            if ((mValue - 5) <= 0)
            {
                mPreValue = mValue;
                mValue = 1;
            } else
            {
                if ((mValue == mMaxValue) && (mPreValue != -1))
                    mValue = mPreValue;
                else
                    mValue -= 5;
                mPreValue = -1;
            }
            suggestShowValue();
        }

        private void suggestHandlePlus()
        {
            if (mValue >= mMaxValue)
                return;
            if ((mValue + 5) >= mMaxValue)
            {
                mPreValue = mValue;
                mValue = mMaxValue;
            }
            else
            {
                if ((mValue == 1) && (mPreValue != -1))
                    mValue = mPreValue;
                else
                    mValue += 5;
                mPreValue = -1;
            }
            suggestShowValue();
        }

        private void suggestShowValue()
        {
            txtValue.Text = mValue.ToString();
            bool showAlert = mValue >= Math.Min(mMaxValue, mlTime + 10);
            txtError.Text = (!noMlTime && showAlert) ? "조리 시간이 길면 배달 지연 및 주문 취소가 발생할 수 있습니다." : ""; //busyMode = false always
        }

        private void enhancedHandleMinus(int minus)
        {
            if (mValue == mMinValue)
                return;
            long result = mValue - minus;
            if (result <= mMinValue)
                mValue = mMinValue;
            else
                mValue = result;
            enhancedShowValue();
        }

        private void enhancedHandlePlus(int plus)
        {
            if (mValue >= mMaxValue)
                return;
            long result = mValue + plus;
            if (result <= mMaxValue)
                mValue = mMaxValue;
            else
                mValue = result;
            enhancedShowValue();
        }

        private void enhancedShowValue()
        {
            txtValue.Text = mValue.ToString();
            bool showAlert = mValue > (mlTime + 10);
            txtError.Text = "";
            if (!noMlTime)
            {
                if (showAlert)
                    txtError.Text = (mValue == mMaxValue) ? "더 이상 조리 시간을 추가할 수 없습니다." : "조리 시간이 길면 배달 지연 및 주문 취소가 발생할 수 있습니다.";
                else
                    txtError.Text = "원활한 배달파트너 배정을 위해 매장 이력을 분석한 권장 시간을 활용해주세요.";
            }
            if (noMlTime && (mValue == mMaxValue))
                txtError.Text = "더 이상 조리 시간을 추가할 수 없습니다.";
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            switch (mTimeType)
            {
                case TYPE_SUGGEST:
                    suggestHandleMinus();
                    break;
                case TYPE_ENHANCED:
                    enhancedHandleMinus(1);
                    break;
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            switch (mTimeType)
            {
                case TYPE_SUGGEST:
                    suggestHandlePlus();
                    break;
                case TYPE_ENHANCED:
                    enhancedHandlePlus(1);
                    break;
            }
        }

        private void btnMinus5_Click(object sender, EventArgs e)
        {
            enhancedHandleMinus(5);
        }

        private void btnPlus5_Click(object sender, EventArgs e)
        {
            enhancedHandlePlus(5);
        }

        private void EnableControls(bool enable)
        {
            btnMinus.Enabled = enable;
            btnPlus.Enabled = enable;

            btnMinus5.Enabled = enable;
            btnPlus5.Enabled = enable;

            cmbTimes.Enabled = enable;

            btnOkay.Enabled = enable;
            btnCancel.Enabled = enable;
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            if ((mValue < mMinValue) || (mValue > mMaxValue))
            {
                MessageBox.Show((mTimeType == TYPE_GENERAL) ? "올바른 시간을 선택하세요." : "올바른 시간을 입력하세요.");
                return;
            }

            EnableControls(false);
            mErrorMsg = null;
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                AcceptOrderApi(mValue);

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



        public void AcceptOrderApi(long duration)
        {
            var user = Global.userInfo;
            if ((user == null) || (mOrder == null))
                return;
            string path = string.Format("/v1/stores/{0}/orders/{1}/accept", mOrder.storeId, mOrder.orderId);
            var client = new RestClient("https://pos-api.coupang.com/api" + path)
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var bTest = new List<string>() { "B" };
            var opts = mOrder.abTestKeyOptions;
            bool useEnhancedBusyModeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);
            bool useDelayBlocks = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts);
            var data = new
            {
                duration = duration,
                storeModeDto = (object) null,
                mlFoodPrepTime = (useDelayBlocks || useEnhancedBusyModeV2) ? (mOrder.mlFoodPrepTime ?? null) : null
            };

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, data, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;

            IRestResponse _result = tb.Result;
            int code = (int) _result.StatusCode;
            if ((code < 200) || (code > 299))
                mErrorMsg = "주문 접수 실패!";
        }
    }
}
