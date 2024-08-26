using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CoupangWeb.Models
{
    //----------------- Pickup Agreement API Result
    public class CPWStoreAgreement
    {
        public int storeId { get; set; }
        public string storeName { get; set; }
        public bool agreed { get; set; }
    }

    public class CPWPickupAgreementData
    {
        public List<CPWStoreAgreement> storeAgreements { get; set; }
        public bool target { get; set; }
    }


    //-------------------- Review Search API Result
    public class CPWReviewReply
    {
        public int orderReviewReplyId { get; set; }
        public int orderReviewId { get; set; }
        public string content { get; set; }
        public int respondentId { get; set; }
        public DateTime createdAt { get; set; }
        public string orderReviewReplyStatusType { get; set; }
        public string orderReviewReplyRespondentType { get; set; }
    }

    public class CPWReviewOrderInfo
    {
        public int dishId { get; set; }
        public DateTime createdAt { get; set; }
        public string dishName { get; set; }
    }

    public class CPWReviewContent
    {
        public int orderReviewId { get; set; }
        public int storeId { get; set; }
        public object orderId { get; set; }
        public string abbrOrderId { get; set; }
        public string comment { get; set; }
        public string memberId { get; set; }
        public List<string> images { get; set; }
        public List<CPWReviewReply> replies { get; set; }
        public double rating { get; set; }
        public string statusType { get; set; }
        public List<object> tags { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public DateTime orderedAt { get; set; }
        public List<CPWReviewOrderInfo> orderInfo { get; set; }
        public int orderCount { get; set; }
        public string customerName { get; set; }
        public string orderType { get; set; }


        public string GetDateText()
        {
            return (createdAt != null) ? createdAt.ToString("yyyy-MM-dd") : "";
        }

        public string GetRatingText()
        {
            switch ((int)rating)
            {
                case 0:
                    return "☆☆☆☆☆";
                case 1:
                    return "★☆☆☆☆";
                case 2:
                    return "★★☆☆☆";
                case 3:
                    return "★★★☆☆";
                case 4:
                    return "★★★★☆";
                case 5:
                    return "★★★★★";
            }
            return "☆☆☆☆☆";
        }

        public string GetMenusText()
        {
            string result = "";
            if (orderInfo != null)
            {
                List<string> lines = new List<string>() { "주문메뉴" };
                foreach (var menu in orderInfo)
                {
                    if ((menu != null) && !string.IsNullOrWhiteSpace(menu.dishName))
                        lines.Add(menu.dishName);
                }
                result = string.Join("\r\n", lines.ToArray());
            }
            return result;
        }

        public string GetOrderInfoText()
        {
            string result = "";
            List<string> lines = new List<string>();
            if (lines != null)
            {
                if (orderedAt != null)
                    lines.Add("주문일자 " + orderedAt.ToString("yyyy-MM-dd"));
                lines.Add("주문번호 " + (abbrOrderId ?? ""));
                switch (orderType)
                {
                    case "PICKUP":
                        lines.Add("수령방식 포장");
                        break;
                    case "REGULAR":
                        lines.Add("수령방식 배달");
                        break;
                }
                result = string.Join("\r\n", lines.ToArray());
            }
            return result;
        }
    }

    public class CPWReviewSearchData
    {
        public List<CPWReviewContent> content { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int total { get; set; }
    }


    //--------------------------- Review Summary API Result
    public class CPWReviewSummaryData
    {
        public int storeId { get; set; }
        public double averageTotalRating { get; set; }
        public int totalReviewCount { get; set; }
        public int totalReplyCount { get; set; }
        public int totalNoReplyCount { get; set; }
    }


}
