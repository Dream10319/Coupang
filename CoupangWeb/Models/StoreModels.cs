using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupangWeb.Models
{

    //Shop List API Result
    public class CPWShopCategory
    {
        public long storeId { get; set; }
        public long categoryId { get; set; }
        public string categoryType { get; set; }
        public bool mainCategory { get; set; }
    }

    public class CPWShopImageInfo
    {
        public long id { get; set; }
        public long storeId { get; set; }
        public string imagePath { get; set; }
        public string fileName { get; set; }
        public string imageUrl { get; set; }
        public int exposeOrder { get; set; }
    }

    public class CPWShopData
    {
        public long id { get; set; }
        public object brandId { get; set; }
        public long merchantId { get; set; }
        public List<int> categoryIds { get; set; }
        public string name { get; set; }
        public string nameEn { get; set; }
        public string description { get; set; }
        public string descriptionEn { get; set; }
        public string repName { get; set; }
        public string telNo { get; set; }
        public string bizNo { get; set; }
        public string approvalStatus { get; set; }
        public string approvalStatusText { get; set; }
        public string zipNo { get; set; }
        public string address { get; set; }
        public string addressEn { get; set; }
        public string addressDetail { get; set; }
        public string addressDetailEn { get; set; }
        public object locationDesc { get; set; }
        public string parkingSpotTips { get; set; }
        public List<object> parkingSpotTipImages { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double serviceFeeRatio { get; set; }
        public List<CPWShopImageInfo> images { get; set; }
        public string taxBaseType { get; set; }
        public object authorId { get; set; }
        public DateTime createdAt { get; set; }
        public object storeManagerAccount { get; set; }
        public object storeNotice { get; set; }
        public object countryOrigin { get; set; }
        public object nutritionFacts { get; set; }
        public object allergyIngredients { get; set; }
        public bool allowEdit { get; set; }
        public bool allowBankEdit { get; set; }
        public string bankImagePath { get; set; }
        public string bizImagePath { get; set; }
        public string paymentStoreId { get; set; }
        public bool posIntegrated { get; set; }
        public object posProvider { get; set; }
        public object externalId { get; set; }
        public string managerName { get; set; }
        public string managerTelNo { get; set; }
        public List<object> storeManagementLevels { get; set; }
        public List<CPWShopCategory> categories { get; set; }
        public object approvalDate { get; set; }
        public DateTime? openDate { get; set; }
        public List<string> supportedOrderTypes { get; set; }
        public bool parkingAvailable { get; set; }
        public object integratedMerchantId { get; set; }
        public object wowLevelExpireAt { get; set; }

        public string GetDisplayName()
        {
            return string.Format("{0}({1})", name, id);
        }
    }




}
