using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static System.Windows.Forms.AxHost;

namespace CoupangWeb.Models
{

    //-------------------------- Order Conditional Search Result

    public class CPWOrderedStoreInfo
    {
        public int storeId { get; set; }
        public string paymentStoreId { get; set; }
    }

    public class CPWOrderedMenuOption
    {
        public int optionItemId { get; set; }
        public string optionName { get; set; }
        public int optionQuantity { get; set; }
        public double optionPrice { get; set; }
    }

    public class CPWOrderedMenuItem
    {
        public int orderItemId { get; set; }
        public int dishId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double unitSalePrice { get; set; }
        public double subTotalPrice { get; set; }
        public List<CPWOrderedMenuOption> itemOptions { get; set; }
    }

    public class CPWOrderSupplyPriceInfo
    {
        public double basicSupplyPrice { get; set; }
        public double appliedSupplyPrice { get; set; }
    }

    public class CPWOrderSettlement
    {
        public double commissionTotal { get; set; }
        public double commissionVat { get; set; }
        public CPWOrderSupplyPriceInfo serviceSupplyPrice { get; set; }
        public CPWOrderSupplyPriceInfo paymentSupplyPrice { get; set; }
        public CPWOrderSupplyPriceInfo deliverySupplyPrice { get; set; }
        public CPWOrderSupplyPriceInfo advertisingSupplyPrice { get; set; }
        public double storePromotionAmount { get; set; }
        public int favorableFee { get; set; }
        public string settlementDueDate { get; set; }
        public bool hasSettled { get; set; }
        public double disposableCupFee { get; set; }
        public double subtractAmount { get; set; }
    }

    public class CPWOrderContent
    {
        public long memberSrl { get; set; }
        public object orderId { get; set; }
        public string uniqueOrderId { get; set; }
        public string abbrOrderId { get; set; }
        public long storeId { get; set; }
        public CPWOrderedStoreInfo store { get; set; }
        public double totalAmount { get; set; }
        public double? actuallyAmount { get; set; }
        public double initialSalePrice { get; set; }
        public double salePrice { get; set; }
        public double? discountPrice { get; set; }
        public double? canceledAmount { get; set; }
        public long createdAt { get; set; } //epoch time in milliseconds
        public string status { get; set; }
        public bool partialCanceled { get; set; }
        public int reviewRating { get; set; }
        public List<CPWOrderedMenuItem> items { get; set; }
        public List<CPWOrderedMenuItem> canceledItems { get; set; }
        public List<CPWOrderedMenuItem> currentRemainedItems { get; set; }
        public string note { get; set; }
        public object commissionFee { get; set; }
        public object commissionFeeRate { get; set; }
        public object commissionFeeVatRate { get; set; }
        public long? canceledAt { get; set; } //epoch time in milliseconds
        public CPWOrderSettlement orderSettlement { get; set; }
        public string type { get; set; }

        public string GetOrderedTimeText()
        {
            if (createdAt < 1)
                return "";
            try
            {
                DateTime dt = Global.EpochSeconds2Date(createdAt / 1000).ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm");
            } catch { }
            return "";
        }

        public string GetShortItemsText()
        {
            if ((items == null) || (items.Count < 1))
                return "";
            string result = items[0].name;
            if (items.Count > 1)
                result += string.Format(" 외 {0}건", items.Count - 1);
            return result;
        }

        public string GetPriceText()
        {
            string result = string.Format("{0:N0}원", totalAmount);
            switch (status)
            {
                case "COMPLETED":
                    result += " (정산완료)";
                    break;
                case "CANCELLED":
                    result += " (취소)";
                    break;
            }

            return result;
        }
    }

    public class CPWOrderPageVo
    {
        public List<CPWOrderContent> content { get; set; }
        public int pageNumber { get; set; }
        public int total { get; set; }
        public int totalElements { get; set; }
    }

    public class CPWOrderConditionResult
    {
        public double? totalSalePrice { get; set; }
        public int? totalOrderCount { get; set; }
        public double? avgOrderAmount { get; set; }
        public CPWOrderPageVo orderPageVo { get; set; }
    }
}
