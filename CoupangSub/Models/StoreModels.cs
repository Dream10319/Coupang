using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupangSub.Models
{
    public class CPSStoreModel
    {
        public long storeId { get; set; }
        public string storeName { get; set; }
        public long originMerchantId { get; set; }
        public bool integrated { get; set; }
        public List<string> supportedOrderTypes { get; set; }

        public CPSStoreModel()
        {
            storeId = 0;
            storeName = null;
            originMerchantId = 0;
            integrated = false;
            supportedOrderTypes = null;
        }
    }


    public class CPSStoreDetails
    {
        public string zipNo { get; set; }
        public string telNo { get; set; }
        public double reviewRating { get; set; }
        public string openStatus { get; set; }
        public string nextOpenAt { get; set; }
        public string name { get; set; }
        public double? longitude { get; set; }
        public double? latitude { get; set; }
        public List<string> imagePaths { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string bizNo { get; set; }
        public string addressDetail { get; set; }
        public string address { get; set; }
        public string countryOriginText { get; set; }
        public StoreModeDto storeModeDto { get; set; }
    }


    public class CPSStoreReminder
    {
        public List<CPSOrderEntity> orders;
        public List<string> dismissedOrders;
        public CPSStoreReminder()
        {
            orders = null;
            dismissedOrders = null;
        }
    }
}
