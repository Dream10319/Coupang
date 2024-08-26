using CoupangSub.Models;
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
    //AcceptOrderForm과 유사하게 구성
    public partial class DelayOrderForm : Form
    {
        public CPSOrderEntity mOrder { get; set; }


        private const int TYPE_GENERAL = 0;
        private const int TYPE_SUGGEST = 1;
        private const int TYPE_ENHANCED = 2;
        private const int MAX_DELAY_COUNT = 1;

        private int mAsyncActs = 0;
        private string mErrorMsg = "";

        private int mTimeType = 0;
        private long mValue = 15;
        private bool? mChangeToBusy = null;
        private long? mPrepareFoodDelayTime = null;
        private long mMinValue = 1;
        private long mMaxValue = 60;
        private long? mlFoodPrepTime;
        private long? prepareFoodDuration;
        private string selectedMode = "NORMAL"; //SuggestSettingTime
        private long delayDuration = 10; //SuggestSettingTime
        private bool isBusyModeWithMl = false;


        public DelayOrderForm()
        {
            InitializeComponent();
        }

        private void DelayOrderForm_Load(object sender, EventArgs e)
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

        private void DelayOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }
        }

        void ClearValues()
        {
            var bTest = new List<string>() { "B" };
            var opts = mOrder.abTestKeyOptions;
            bool useRecommendTime = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME, bTest, opts);
            bool useRecommendTimeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME_V2, bTest, opts);
            bool useDelayBlocks = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts);
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

            mlFoodPrepTime = mOrder.mlFoodPrepTime;
            prepareFoodDuration = mOrder.state.prepareFoodDuration;

            var bTest = new List<string>() { "B" };
            var opts = mOrder.abTestKeyOptions;
            bool useEnhancedBusyModeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_ENHANCED_BUSY_MODE_V2, bTest, opts);
            bool useDelayOnly = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_DELAY_BLOCKS, bTest, opts);
            bool useDelayBlocks = useDelayOnly || useEnhancedBusyModeV2;
            var abuseMode = mOrder.store.storeModeDto?.abuseMode ?? null;
            var abuseType = mOrder.store.storeModeDto?.abuseType ?? null;
            string isDelayDelayBlocks = ((mlFoodPrepTime != null) && useDelayOnly && (abuseMode == "DELAY")) ? abuseType : null;
            string isBusyDelayBlocks = ((mlFoodPrepTime != null) && useEnhancedBusyModeV2 && (abuseMode == "BUSY")) ? abuseType : null;
            isBusyModeWithMl = (useEnhancedBusyModeV2 && (mlFoodPrepTime != null) && mOrder.store.storeModeDto?.operationMode == "BUSY");

            mValue = 0;
            mChangeToBusy = null;
            mPrepareFoodDelayTime = null;

            mMinValue = 0;
            mMaxValue = 60;

            Text = "준비 지연";
            string text = "추가할 시간을 입력해주세요.";
            if (useDelayBlocks)
            {
                if (isBusyDelayBlocks != null)
                {
                    text += "\r\n" + (isBusyDelayBlocks == "HALF_HOUR" ? "고객 불편을 줄이기 위해 일시적으로 조리 시간 변경과 매장 상태 변경이 제한됩니다." :
                                                                "고객 불편을 줄이기 위해 오늘 마감 시간까지 조리 시간 변경과 매장 상태 변경이 제한됩니다.");
                }
                else if (isDelayDelayBlocks != null)
                {
                    text += "\r\n" + (isDelayDelayBlocks == "HALF_HOUR" ? "고객 불편을 줄이기 위해 일시적으로 조리 시간 변경이 제한됩니다." :
                                                                "고객 불편을 줄이기 위해 오늘 마감 시간까지 조리 시간 변경이 제한됩니다.");
                }
            }
            txtNotices.Text = text;

            cmbTimes.Visible = false;
            pnlTimes.Visible = true; //EnhancedTimeController
            btnMinus5.Visible = true;
            btnPlus5.Visible = true;
            txtError.Visible = true;
            pnlBusy1.Visible = false;
            pnlBusy2.Visible = false;

            btnOkay.Text = "준비 지연(&D)";
            enhancedShowValue();
        }

        private void showSuggestSettingTime()
        {
            mTimeType = TYPE_SUGGEST;

            mValue = 5;
            mChangeToBusy = null;
            mPrepareFoodDelayTime = null;

            mMinValue = 1;
            mMaxValue = 60;

            Text = "준비 지연";
            txtNotices.Text = "추가할 시간을 입력해주세요.";

            cmbTimes.Visible = false;
            pnlTimes.Visible = true;
            btnMinus5.Visible = false;
            btnPlus5.Visible = false;
            pnlBusy1.Visible = true; //Change To Busy
            pnlBusy2.Visible = false;

            btnOkay.Text = "확인(&O)";
            suggestShowValue();
        }

        private void showSettingTime()
        {
            mTimeType = TYPE_GENERAL;

            bool shownRangeDuration = true;

            mValue = 20;
            mChangeToBusy = null;
            mPrepareFoodDelayTime = null;

            mMinValue = 5;
            mMaxValue = 60;

            Text = "지연 시간 추가";
            txtNotices.Text = "추가 시간을 선택해 주세요.";
            txtError.Text = "";

            cmbTimes.Visible = true;
            pnlTimes.Visible = false; //BusyTimeController
            btnMinus5.Visible = false;
            btnPlus5.Visible = false;
            txtError.Visible = false;
            pnlBusy1.Visible = false;
            pnlBusy2.Visible = false;

            cmbTimes.Items.Clear();
            for (long min = mMinValue; min <= mMaxValue; min += 5)
            {
                string time = shownRangeDuration ? (min < 5 ? "" : string.Format("{0} ~ {1}분", min - 5, min)) : (min + "분");
                cmbTimes.Items.Add(time);
            }
            cmbTimes.SelectedIndex = (int)((mValue - mMinValue) / 5);

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
            long newDelayTime = mValue - 5;

            if (newDelayTime > 0)
                mValue = newDelayTime;
            else
                mValue = 1;
            suggestShowValue();
        }

        private void suggestHandlePlus()
        {
            if (mValue == 1)
                mValue = 5;
            else
            {
                long newDelayTime = mValue + 5;
                if (newDelayTime <= 60)
                    mValue = newDelayTime;
            }
            suggestShowValue();
        }

        private void suggestShowValue()
        {
            txtValue.Text = mValue.ToString();
            bool busyRange = false;
            if ((mlFoodPrepTime != null) && (prepareFoodDuration != null))
                busyRange = (prepareFoodDuration + (mValue * 60)) <= (mlFoodPrepTime + 10) * 60;
            txtError.Text = busyRange ? "조리 시간이 길면 배달 지연 및 주문 취소가 발생할 수 있습니다." : "";

            bool busy = selectedMode == "BUSY";
            mChangeToBusy = busy;
            chkNormal.Checked = !busy;
            chkBusy.Checked = busy;

            pnlBusy2.Visible = busy;
            chkDuration1.Checked = delayDuration == 10;
            chkDuration2.Checked = delayDuration == 20;
            chkDuration3.Checked = delayDuration == 30;
            mPrepareFoodDelayTime = delayDuration;
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
            int delayCount = mOrder.state?.delayCount ?? -1;

            txtValue.Text = mValue.ToString();
            txtError.Text = "";
            if (delayCount >= MAX_DELAY_COUNT)
            {
                txtError.Text = "이미 준비 지연을 하셨습니다. 준비 지연은 1회만 가능합니다.";
                btnOkay.Enabled = false;
                return;
            }

            if ((mValue >= mMaxValue) || (isBusyModeWithMl && (mValue >= 10)))
            {
                txtError.Text = "더 이상 조리 시간을 추가할 수 없습니다.";
                return;
            }

            long maxPlusPrepareTime = mOrder.store.storeModeDto?.maxPlusPrepareTime ?? 10;
            bool warningRange = ((mlFoodPrepTime != null) && (prepareFoodDuration != null) &&
                        ((prepareFoodDuration + mValue * 60) >= ((mlFoodPrepTime + 10) * 60))) ||
                                            (isBusyModeWithMl && (mValue >= maxPlusPrepareTime));
            if (warningRange)
            {
                txtError.Text = "조리 시간이 길면 배달 지연 및 주문 취소가 발생할 수 있습니다.";
                return;
            }

            txtError.Text = isBusyModeWithMl ? "준비 지연은 최대 10분, 1회만 가능합니다." : "준비 지연은 1회만 가능합니다.";
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


        private void chkStatus_Click(object sender, EventArgs e)
        {
            selectedMode = ((CheckBox)sender).Tag as string;
            suggestShowValue();
        }

        private void chkDuration_Click(object sender, EventArgs e)
        {
            delayDuration = Convert.ToInt32(((CheckBox)sender).Tag as string);
            suggestShowValue();
        }

        private void EnableControls(bool enable)
        {
            btnMinus.Enabled = enable;
            btnPlus.Enabled = enable;

            btnMinus5.Enabled = enable;
            btnPlus5.Enabled = enable;

            cmbTimes.Enabled = enable;
            pnlTimes.Enabled = enable;
            pnlBusy1.Enabled = enable;
            pnlBusy2.Enabled = enable;

            chkNormal.Enabled = enable;
            chkBusy.Enabled = enable;
            chkDuration1.Enabled = enable;
            chkDuration2.Enabled = enable;
            chkDuration3.Enabled = enable;

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
                DelayOrderApi(mValue);

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



        public void DelayOrderApi(long duration)
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
            bool useRecommendTime = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME, bTest, opts);
            bool useRecommendTimeV2 = CPSABTestKeyOption.isOption(CPSABTestKeyOption.AB_ID_RECOMMENDED_TIME_V2, bTest, opts);
            object data = null;
            switch (mTimeType)
            {
                case TYPE_ENHANCED:
                case TYPE_GENERAL:
                    data = new
                    {
                        duration = duration
                    };
                    break;
                case TYPE_SUGGEST:
                    if (!useRecommendTime && !useRecommendTimeV2)
                    {
                        data = new
                        {
                            duration = duration
                        };
                    } else
                    {
                        if (selectedMode == "BUSY")
                        {
                            data = new
                            {
                                duration = duration,
                                changeToBusy = true,
                                prepareFoodDelayTime = mPrepareFoodDelayTime
                            };
                        } else
                        {
                            data = new
                            {
                                duration = duration
                            };
                        }
                    }
                    break;
            }

            var request = new RestRequest(Method.POST);
            Global.EncryptRequest<object>(request, Global.TimeInMillis(), path, data, "POST", "application/json");

            Task<IRestResponse> tb = Task.Run(() => client.ExecuteAsync(request));
            tb.Wait();
            mAsyncActs--;

            IRestResponse _result = tb.Result;
            int code = (int)_result.StatusCode;
            if ((code < 200) || (code > 299))
                mErrorMsg = "주문 준비 지연 실패!";
        }
    }
}
