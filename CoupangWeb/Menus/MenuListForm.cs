
using CoupangWeb.Auth;
using CoupangWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;



namespace CoupangWeb.Menus
{
    public partial class MenuListForm : Form, MenuItemCtrl.IEventCallback
    {
        private int mAsyncActs = 0;
        private int mStoreIndex = -1;       //상점 번호
        private Timer mTimer = new Timer();
        private CPWMenuListData mMenuList = null;


        public MenuListForm()
        {
            InitializeComponent();
        }

        private void MenuListForm_Load(object sender, System.EventArgs e)
        {
            if (!Global.HasStores())
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            ClearValues();

            mTimer.Interval = 2000;
            mTimer.Enabled = true;
            mTimer.Tick += OnLoadTimer;
            EnableControls(false);
        }

        private void MenuListForm_FormClosing(object sender, FormClosingEventArgs e)
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

            txtTotalCount.Text   = "";
            txtOnSaleCount.Text  = "";
            txtSoldOutCount.Text = "";
            txtHiddenCount.Text  = "";
        }

        private void EnableControls(bool enable)
        {
            cmbStores.Enabled   = enable;
            btnRefresh.Enabled  = enable;
            pnlMenus.Enabled    = enable;
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            mTimer.Stop();
            cmbStores.SelectedIndex = 0;
            //RefreshMenuList();
        }

        private void cmbStores_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int newIdx = cmbStores.SelectedIndex;
            if (mStoreIndex != newIdx)
            {
                mStoreIndex = newIdx;
                RefreshMenuList();
            }
        }

        private void ShowMenuList()
        {
            //Show Groups and Menus
            pnlMenus.Controls.Clear();
            if ((mMenuList != null) && (mMenuList.menus != null) && (mMenuList.menus.Count > 0))
            {
                foreach (var group in mMenuList.menus)
                {
                    if ((group == null) || (group.dishes == null))
                        continue;
                    foreach (var menu in group.dishes)
                    {
                        MenuItemCtrl ctrl = new MenuItemCtrl();
                        ctrl.SetData(menu, this);
                        pnlMenus.Controls.Add(ctrl);
                    }
                }
            }

            //Show Count
            var stats = mMenuList.stat;
            if (stats != null)
            {
                txtTotalCount.Text   = string.Format("전체\r\n{0}개", stats.total);
                txtOnSaleCount.Text  = string.Format("판매중\r\n{0}개", stats.onSale);
                txtSoldOutCount.Text = string.Format("오늘만 품절\r\n{0}개", stats.soldOutToday);
                txtHiddenCount.Text  = string.Format("메뉴 숨김\r\n{0}개", stats.notExpose);
            } else
            {
                txtTotalCount.Text   = "";
                txtOnSaleCount.Text  = "";
                txtSoldOutCount.Text = "";
                txtHiddenCount.Text  = "";
            }
        }

        private void RefreshMenuList()
        {
            var store = Global.GetStore(mStoreIndex);
            if ((mAsyncActs > 0) || (store == null))
                return;
            mAsyncActs = 0;
            EnableControls(false);
            Thread th = new Thread(new ParameterizedThreadStart((object f) =>
            {
                GetMenuDishesApi(store.id);
                Invoke(new Action(() => {
                    EnableControls(true);
                    ShowMenuList();
                }));
            }));
            th.Start();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            RefreshMenuList();
        }

        public void OnMenuShowDetails(MenuItemCtrl source)
        {
            var store = Global.GetStore(mStoreIndex);
            if ((source == null) || (store.id < 1) || (source.mMenuData == null) || (store == null))
                return;

            var menu = source.mMenuData;
            MenuEditForm frm = new MenuEditForm();
            frm.mStoreId = store.id;
            frm.mGroupId = menu.menuId;
            frm.mMenuId  = menu.dishId;
            Hide();
            if (frm.ShowDialog() == DialogResult.OK)
                RefreshMenuList();
            Show();
        }

        public void OnMenuChangeStatus(MenuItemCtrl source, string status)
        {
            var store = Global.GetStore(mStoreIndex);
            if ((store == null) || (store.id < 1) || (source == null) ||
                (source.mMenuData == null) || string.IsNullOrWhiteSpace(status))
                return;

            string msg = "";
            switch (status)
            {
                case "ON_SALE":         msg = "메뉴상태를 판매중으로 변경하시겠습니까?"; break;
                case "SOLD_OUT_TODAY":  msg = "메뉴상태를 오늘만 품절로 변경하시겠습니까?"; break;
                case "NOT_EXPOSE":      msg = "메뉴상태를 메뉴 숨김으로 변경하시겠습니까?"; break;
            }
            if (MessageBox.Show(msg, "메뉴 상태 변경", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var dish = source.mMenuData;
                EnableControls(false);
                Thread th = new Thread(new ParameterizedThreadStart((object f) =>
                {
                    ChangeMenuStatusApi(store.id, dish.dishId, status);
                }));
                th.Start();
            }
        }

        //같은 메뉴가 여러 그룹에 종속된 경우도 있으므로 상태변수를 수정을 별도로 진행
        private void UpdateMenuStatus(long dishId, string status)
        {
            if ((dishId < 1) || string.IsNullOrWhiteSpace(status))
                return;

            if (mMenuList != null)
            {
                var groups = mMenuList.menus;
                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        var dishes = group.dishes;
                        if (dishes != null)
                        {
                            foreach (var dish in dishes)
                            {
                                if ((dish != null) && (dish.dishId == dishId))
                                    dish.displayStatus = status;
                            }
                        }
                    }
                }
            }

            foreach (var item in pnlMenus.Controls)
            {
                if (item is MenuItemCtrl)
                {
                    var ctrl = item as MenuItemCtrl;
                    if ((ctrl != null) && (ctrl.mMenuData != null) && (ctrl.mMenuData.dishId == dishId))
                        ctrl.SetStatus(status);
                }    
            }
        }



        //메뉴 목록 얻기
        public void GetMenuDishesApi(long storeId)
        {
            if (storeId < 1)
                return;
            var client = new RestClient(string.Format("https://store.coupangeats.com/api/v1/merchant/web/stores/{0}/all-menu-dishes", storeId))
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
            

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    CPWCommonApiResult<CPWMenuListData> res = JsonConvert.DeserializeObject<CPWCommonApiResult<CPWMenuListData>>(text);
                    if ((res != null) && (res.data != null))
                        mMenuList = res.data;
                }
                catch { }
            }
            mAsyncActs--;
        }

        //Can change the status of multiple parameters
        public void ChangeMenuStatusApi(long storeId, long dishId, string status)
        {
            if ((storeId < 1) || (dishId < 1) || string.IsNullOrWhiteSpace(status))
                return;
            var client = new RestClient(string.Format("https://store.coupangeats.com/api/v1/merchant/web/catalog/stores/{0}/dishes/update-status", storeId))
            {
                CookieContainer = Global.cookies,
                Timeout = -1
            };
            client.UserAgent = Global.userAgent;
            mAsyncActs++;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Referer", "https://store.coupangeats.com/merchant/management/menu/");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddJsonBody(new object[]
            {
                new
                {
                    dishId = dishId,
                    displayStatus = status
                }
            });

            Task<IRestResponse> ta = Task.Run(() => client.ExecuteAsync(request));
            ta.Wait();
            IRestResponse _result = ta.Result;
            Global.SaveCookies(_result.Cookies);
            string text = _result.Content;
            Invoke(new Action(() => {
                EnableControls(true);
                string msg = "메뉴 상태 변경 실패!";
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        CPWCommonApiResult<bool> res = JsonConvert.DeserializeObject<CPWCommonApiResult<bool>>(text);
                        if (res != null)
                        {
                            if ((res.code == "SUCCESS") && res.data)
                            {
                                msg = "";
                                UpdateMenuStatus(dishId, status);
                            } else
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
            }));
        }
    }
}
