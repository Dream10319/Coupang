using CoupangWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangWeb.Menus
{
    public partial class MenuItemCtrl : UserControl
    {
        public CPWMenuBriefInfo mMenuData = null;
        IEventCallback  mCallback = null;

        int mSelStatusIdx = 0;
        string[] STATUS_CODES = { "ON_SALE", "SOLD_OUT_TODAY", "NOT_EXPOSE" };

        public MenuItemCtrl()
        {
            InitializeComponent();

            mMenuData = null;
            txtGroup.Text = "";
            txtName.Text = "";
        }

        public void SetData(CPWMenuBriefInfo data, IEventCallback callback)
        {
            mMenuData = data;
            mCallback = callback;

            txtGroup.Text = data.menuName;
            txtName.Text = data.dishName;
            SetStatus(data.displayStatus);
            string path = data.GetFirstImage();
            if (!string.IsNullOrWhiteSpace(path))
                picImage.ImageLocation = path;
        }

        public void SetStatus(string status)
        {
            mMenuData.displayStatus = status;
            switch (status)
            {
                case "ON_SALE":
                    mSelStatusIdx = 0;
                    break;
                case "SOLD_OUT_TODAY":
                    mSelStatusIdx = 1;
                    break;
                case "NOT_EXPOSE":
                    mSelStatusIdx = 2;
                    break;
            }
            cmbStatus.SelectedIndex = mSelStatusIdx;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int sel = cmbStatus.SelectedIndex;
            if ((sel < 0) || (sel >= STATUS_CODES.Length))
                return;
            if (mSelStatusIdx != sel)
            { //선택이 변경되었을 때에만 API 호출
                if (mCallback != null)
                    mCallback.OnMenuChangeStatus(this, STATUS_CODES[sel]);
            }
        }

        private void onDetails_Click(object sender, EventArgs e)
        {
            if (mCallback != null)
                mCallback.OnMenuShowDetails(this);
        }

        public interface IEventCallback
        {
            void OnMenuShowDetails(MenuItemCtrl source);
            void OnMenuChangeStatus(MenuItemCtrl source, string status);
        }
    }
}
