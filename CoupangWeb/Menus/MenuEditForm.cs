using CoupangWeb.Auth;
using CoupangWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;



namespace CoupangWeb.Menus
{
    public partial class MenuEditForm : Form
    {
        public long mStoreId = 0;    //상점 아이디
        public long mGroupId = 0;    //메뉴 그룹 아이디
        public long mMenuId  = 0;    //메뉴 아이디

        private int mAsyncActs = 0;
        private Timer mTimer = new Timer();
        private string mReferer = "";
        private CPWMenuDetailData mDetails;
        private List<CPWMenuGroupInfo> mGroupList;
        private List<CPWMenuGroupInfo> mShowGroups;
        private string mCurStatus = "";


        public MenuEditForm()
        {
            InitializeComponent();
        }

        private void MenuEditForm_Load(object sender, EventArgs e)
        {
            if ((mStoreId < 1) || (mGroupId < 1) || (mMenuId < 1))
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            mReferer = string.Format("https://store.coupangeats.com/merchant/management/menu/stores/{0}/menu-group/{1}/menus/{2}", mStoreId, mGroupId, mMenuId);
            mGroupList = null;

            mTimer.Interval = 2000;
            mTimer.Enabled = true;
            mTimer.Tick += OnLoadTimer;

            EnableControls(false);
            ClearValues();
        }

        private void MenuEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mTimer.Stop();
            while (mAsyncActs > 0)
            { //이미 다른 동작이 진행중이면 기다리기
                Task.Delay(1000).Wait();
            }
        }

        private void ClearValues()
        {
            cmbGroup.Items.Clear();
            lstOptions.Items.Clear();
            lstOptions.Columns.Clear();
            lstOptions.Columns.Add("그룹명", 150, HorizontalAlignment.Left);
            lstOptions.Columns.Add("옵션명", 150, HorizontalAlignment.Left);
            lstOptions.Columns.Add("가격", 100, HorizontalAlignment.Right);
            lstOptions.Columns.Add("상태", 100, HorizontalAlignment.Left);


        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Stop();
            RefreshDetails();
        }

        private void EnableControls(bool enable)
        {
            cmbGroup.Enabled  = enable;
            txtName.ReadOnly  = !enable;
            txtPrice.ReadOnly = !enable;
            
            chkOnSaleStatus.Enabled  = enable;
            chkSoldOutToday.Enabled = enable;
            chkHiddenStatus.Enabled  = enable;

            pnlImages.Enabled  = enable;
            txtDescription.ReadOnly = !enable;

            lstOptions.Enabled = enable;
            
            UseWaitCursor = !enable;
        }

        private void RefreshDetails()
        {
            if (mAsyncActs > 0)
                return;
            mAsyncActs = 0;
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                GetMenuDetailsApi();
                GetMenuGroupsApi();

                Invoke(new Action(() => {
                    if (mDetails != null)
                    {
                        EnableControls(true);
                        ShowDetails();
                    }
                    else
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                    }
                }));
            }));
            th.Start();
        }

        private void ShowDetails()
        {
            if (mDetails == null)
                return;

            //메뉴 그룹
            cmbGroup.Items.Clear();
            if (mGroupList != null)
            {
                mShowGroups = new List<CPWMenuGroupInfo>();
                for (int i = 0; i < mGroupList.Count; i++)
                {
                    long groupId = mGroupList[i].menuId;
                    if (groupId != mGroupId)
                    { //이 메뉴가 매핑된 그룹들 가운데서 현재 선택된 그룹만 표시한다. 매핑안된 그룹들도 표시하여 그쪽으로 전환할수 있다.
                        int index = -1;
                        var maps = mDetails.mappingMenus;
                        if ((maps != null) && (maps.Count > 0))
                        {
                            index = maps.FindIndex(r => r.menuId == groupId);
                            if ((index >= 0) && (index < maps.Count))
                                continue;
                        }
                    }
                    mShowGroups.Add(mGroupList[i]);
                }
                int sel = 0;
                for (int i = 0; i < mShowGroups.Count;i++)
                {
                    if (mShowGroups[i].menuId == mGroupId)
                        sel = i;
                    cmbGroup.Items.Add(mShowGroups[i].menuName);
                }
                cmbGroup.SelectedIndex = sel; //현재 선택된 그룹을 콤보 박스에서 선택
            }

            //이름, 가격
            txtName.Text  = mDetails.dishName ?? "";
            txtPrice.Text = mDetails.salePrice.ToString();
            //상태
            SetMenuStatus(mDetails.displayStatus);

            //사진
            pnlImages.Controls.Clear();
            var imageList = mDetails.dishImages;
            if (imageList != null)
            {
                int count = 0;
                foreach (var image in imageList)
                {
                    if ((image != null) && !string.IsNullOrWhiteSpace(image.imageFullPath) && image.imageFullPath.StartsWith("http"))
                    {
                        PictureBox pic = new PictureBox();
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.ImageLocation = image.imageFullPath;
                        pic.Size = new Size(80, 80);
                        pic.Margin = new Padding((count > 0) ? 10 : 0, 0, 0, 0);
                        pnlImages.Controls.Add(pic);
                        count++;
                    }
                }
            }

            //설명
            txtDescription.Text = mDetails.description ?? "";

            //옵션
            lstOptions.Items.Clear();
            var optionGroups = mDetails.options;
            if (optionGroups != null)
            {
                foreach (var group in optionGroups)
                {
                    var optList = group.optionItems;
                    if (optList == null)
                        continue;
                    foreach (var option in optList)
                    {
                        ListViewItem item = new ListViewItem(group.optionName ?? "");
                        item.SubItems.Add(option.optionItemName ?? "");
                        item.SubItems.Add(option.GetPriceText());
                        item.SubItems.Add(option.GetStateText());
                        lstOptions.Items.Add(item);
                    }
                }
            }
        }

        //가격 - 수자만 입력
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void SetMenuStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return;
            if (mCurStatus != status)
            {
                mCurStatus = status;
                chkOnSaleStatus.Checked = status == "ON_SALE";
                chkSoldOutToday.Checked = status == "SOLD_OUT_TODAY";
                chkHiddenStatus.Checked = status == "NOT_EXPOSE";
            }
        }

        private void chkStatus_Click(object sender, EventArgs e)
        {
            SetMenuStatus(((CheckBox)sender).Tag as string);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInputs(out CPWUpdateMenuParams param)
        {
            param = new CPWUpdateMenuParams();
            if ((mShowGroups == null) || (mShowGroups.Count < 1))
                return false;

            param.fromMenuId = mGroupId;
            //그룹
            int index = cmbGroup.SelectedIndex;
            if ((index < 0) || (index >= mShowGroups.Count))
            {
                MessageBox.Show("메뉴 그룹을 선택해주세요.");
                return false;
            }
            param.toMenuId = mShowGroups[index].menuId;

            //메뉴명
            string text = txtName.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("메뉴명을 입력해주세요.");
                txtName.Focus();
                return false;
            }
            param.dishName = text;

            //가격 100000000000000000000이하
            text = txtPrice.Text;
            double price = 0.0;
            try
            {
                price = (!string.IsNullOrWhiteSpace(text)) ? double.Parse(text) : 0.0;
            } catch
            {
                price = 0.0;
            }
            if (price < 0.1)
            {
                MessageBox.Show("메뉴 가격은 0원이 될 수 없습니다.");
                txtPrice.Focus();
                return false;
            } else if (price > 100000000000000000000.0)
            {
                MessageBox.Show("올바른 금액을 입력해주세요.");
                txtPrice.Focus();
                return false;
            }
            param.salePrice = price;

            //상태
            if (string.IsNullOrWhiteSpace(mCurStatus))
            {
                MessageBox.Show("상태를 선택해주세요.");
                chkOnSaleStatus.Focus();
                return false;
            }
            param.displayStatus = mCurStatus;

            //설명
            param.description = txtDescription.Text;

            //옵션 리스트
            param.optionMappingDtos = new List<CPWOptionMappingDto>();
            if ((mDetails.options != null) && (param.optionMappingDtos != null))
            {
                foreach (var group in mDetails.options)
                {
                    if (group == null)
                        continue;
                    var map = new CPWOptionMappingDto(group.optionId, group.exposeOrder, null);
                    var items = group.optionItems;
                    if (items != null)
                    {
                        map.optionItemSaveDtos = new List<CPWOptionItemSaveDto>();
                        if (map.optionItemSaveDtos != null)
                        {
                            foreach (var item in items)
                                map.optionItemSaveDtos.Add(new CPWOptionItemSaveDto(item.optionItemId, item.displayStatus));
                        }
                    }
                    param.optionMappingDtos.Add(map);
                }
            }

            return true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            CPWUpdateMenuParams param = null;
            if (!ValidateInputs(out param))
                return;
            if (mAsyncActs > 0)
                return;
            mAsyncActs = 0;
            EnableControls(true);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                UpdateMenuDetailsApi(param);
            }));
            th.Start();
        }


        public void GetMenuDetailsApi()
        {
            var client = new RestClient(string.Format("https://store.coupangeats.com/api/v1/merchant/web/stores/{0}/dishes/{1}/detail", mStoreId, mMenuId))
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Referer", mReferer);
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
                    CPWCommonApiResult<CPWMenuDetailData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWMenuDetailData>>(text);
                    if ((res != null) && (res.data != null))
                        mDetails = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }

        public void GetMenuGroupsApi()
        {
            var client = new RestClient(string.Format("https://store.coupangeats.com/api/v1/merchant/web/stores/{0}/only-menus", mStoreId))
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Referer", mReferer);
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
                    CPWCommonApiResult<List<CPWMenuGroupInfo>> res = JsonConvert.DeserializeObject<CPWCommonApiResult<List<CPWMenuGroupInfo>>>(text);
                    if ((res != null) && (res.data != null))
                        mGroupList = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }

        public void UpdateMenuDetailsApi(CPWUpdateMenuParams param)
        {
            if (param == null)
            {
                Invoke(new Action(() =>
                {
                    EnableControls(true);
                }));
                return;
            }

            var client = new RestClient(string.Format("https://store.coupangeats.com/api/v1/merchant/web/catalog/stores/{0}/dishes/{1}/update", mStoreId, mMenuId))
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Referer", mReferer);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddJsonBody(param);

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            Invoke(new Action(() => {
                EnableControls(true);
                string msg = "메뉴 갱신 실패!";
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        CPWCommonApiResult<CPWUpdatedMenuData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWUpdatedMenuData>>(text);
                        if (res != null)
                        {
                            if ((res.code == "SUCCESS") && (res.data != null))
                                msg = "";
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
