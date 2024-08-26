using CoupangSub.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoupangSub.Orders
{
    public partial class OrderItemCtrl : UserControl
    {
        private const int TAG_ACCEPT = 1;   //접수
        private const int TAG_REJECT = 2;   //거부
        private const int TAG_DELAY  = 3;   //완료알림 발송
        private const int TAG_COMPLETE = 4; //완료
        private const int TAG_CANCEL = 5;   //취소
        

        public CPSOrderEntity mOrder { get; set; }
        IEventCallback mCallback;


        public OrderItemCtrl()
        {
            InitializeComponent();
            mOrder = null;
            mCallback = null;

            txtName.Text = "";
            txtTime.Text = "";
            txtMenuName.Text = "";
            txtAddress.Text = "";
            txtPayment.Text = "";

            btnAction1.Text = "";
            btnAction2.Text = "";
            btnAction3.Text = "";
        }

        public void SetData(CPSOrderEntity data, IEventCallback callback)
        {
            mOrder = data;
            mCallback = callback;

            bool isDelivery = mOrder.isDeliveryOrder();
            bool isTotalCancelled = mOrder.isTotalCancelled();
            bool isPartialCancelled = mOrder.isPartialCancelled();

            txtName.Text = string.Format("{0} {1}", isDelivery ? "배달" : "포장",
                                        (mOrder.pickupOrderId > 0) ? mOrder.pickupOrderId.ToString() : mOrder.abbrOrderId);

            txtTime.Text = Global.EpochSeconds2Date(mOrder.orderedAt.dateTime).ToString("yyyy-MM-dd HH:mm:ss");
            var items = mOrder.items;
            if (items != null)
            {
                int count = items.Count;
                txtMenuName.Text = (count > 1) ? string.Format("{0} 외 {1}건", items[0].name, count - 1) : items[0].name;
            }    

            if (isDelivery && (mOrder.destination != null))
            {
                var loc = mOrder.destination.location;
                if (loc != null)
                    txtAddress.Text = loc.address ?? "";
            }
            else
                txtAddress.Text = "";
            List<string> values = new List<string>();
            if (!isPartialCancelled && !isTotalCancelled)
                values.Add(isTotalCancelled ? "취소금액" : "최종금액");
            values.Add(mOrder.salePrice.ToString("N0") + "원");
            txtPayment.Text = string.Join(" ", values);

            switch (mOrder.status)
            {
                case "CREATED": // 주문 생성
                case "PAYMENT_APPROVED": // 접수 대기
                    btnAction1.Visible = mOrder.useNotDeliveredPickUpOrder(); //disabledDelayButton
                    btnAction1.Tag = TAG_DELAY;
                    btnAction1.Text = "준비 지연";
                    btnAction2.Visible = true;
                    btnAction2.Tag = TAG_REJECT;
                    btnAction2.Text = "주문 거부";
                    btnAction3.Visible = true;
                    btnAction3.Tag = TAG_ACCEPT;
                    btnAction3.Text = "주문 접수";
                    break;
                case "ACCEPTED": //접수된 상태, 진행중
                    btnAction1.Visible = mOrder.useNotDeliveredPickUpOrder(); //disabledDelayButton
                    btnAction1.Tag = TAG_DELAY;
                    btnAction1.Text = "준비 지연";
                    btnAction2.Visible = true;
                    btnAction2.Tag = TAG_CANCEL;
                    btnAction2.Text = "주문 취소";
                    btnAction3.Visible = true;
                    btnAction3.Tag = TAG_COMPLETE;
                    btnAction3.Text = "주문 완료";
                    break;
                case "COMPLETED": //완료된 상태
                case "CANCELLED": //거부/취소된 상태
                    btnAction1.Visible = false;
                    btnAction2.Visible = false;
                    btnAction3.Visible = false;
                    break;
            }
        }

        private void btnAction_Click(object sender, System.EventArgs e)
        {
            if ((mCallback == null) || !(sender is Button))
                return;
            switch (Convert.ToInt32(((Button)sender).Tag))
            {
                case TAG_DELAY:
                    mCallback.OnDelayOrderClick(this);
                    break;
                case TAG_CANCEL:
                    mCallback.OnCancelOrderClick(this);
                    break;
                case TAG_ACCEPT:
                    mCallback.OnAcceptOrderClick(this);
                    break;
                case TAG_REJECT:
                    mCallback.OnRejectOrderClick(this);
                    break;
                case TAG_COMPLETE:
                    mCallback.OnCompleteOrderClick(this);
                    break;
            }
        }

        private void txtName_Click(object sender, System.EventArgs e)
        {
            if (mCallback != null)
                mCallback.OnShowOrderDetails(this);
        }

        public interface IEventCallback
        {
            void OnRejectOrderClick(OrderItemCtrl source);  //거부
            void OnCancelOrderClick(OrderItemCtrl source);  //취소
            void OnDelayOrderClick(OrderItemCtrl source);   //준비 지연
            void OnAcceptOrderClick(OrderItemCtrl source);  //접수

            void OnCompleteOrderClick(OrderItemCtrl source);    //완료
            void OnNotifyCompleteClick(OrderItemCtrl source);   //완료 알림

            

            void OnShowOrderDetails(OrderItemCtrl source);
        }
    }
}
