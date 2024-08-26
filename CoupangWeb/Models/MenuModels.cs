using CoupangWeb.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupangWeb.Models
{
    //API에서의 용어 : 메뉴그룹 - menu, 메뉴 -> dish

    //---------------All menu list API Result

    public class CPWMenuBriefInfo
    {
        public long menuId { get; set; }
        public string menuName { get; set; }
        public long dishId { get; set; }
        public string dishName { get; set; }
        public string description { get; set; }
        public double salePrice { get; set; }
        public string currencyType { get; set; }
        public string displayStatus { get; set; }   //ON_SALE - 판매중, NOT_EXPOSE - 숨김, SOLD_OUT_TODAY - 오늘만 품절
        public string restrictionType { get; set; }
        public bool forceNotExpose { get; set; }
        public List<string> optionDisplayStatuses { get; set; }
        public int? exposeOrder { get; set; }
        public List<CPWShortImageDto> dishImageDtos { get; set; }
        public string dishCreatedAt { get; set; }

        public string GetFirstImage()
        {
            string result = "";
            if ((dishImageDtos != null) && (dishImageDtos.Count > 0))
            {
                foreach (var img in dishImageDtos)
                {
                    if ((img != null) && !string.IsNullOrWhiteSpace(img.imagePath))
                    {
                        result = img.imagePath;
                        break;
                    }
                }
            }
            return result;
        }
    }

    public class CPWMenuGroupInfo
    {
        public long menuId { get; set; }
        public string menuName { get; set; }
        public string exposeStatus { get; set; }    //EXPOSE - 표시, 
        public int exposeOrder { get; set; }
        public List<CPWMenuBriefInfo> dishes { get; set; }

        public CPWMenuGroupInfo()
        {
            menuId = 0;
            menuName = null;
            exposeStatus = null;
            exposeOrder = 0;
            dishes = null;
        }
    }

    public class CPWMenuListStats
    {
        public int total { get; set; }
        public int onSale { get; set; }
        public int soldOutToday { get; set; }
        public int notExpose { get; set; }
    }

    public class CPWMenuListData {
        public List<CPWMenuGroupInfo> menus { get; set; }
        public CPWMenuListStats stat { get; set; }
        public object brandId { get; set; }
        public List<object> managementLevels { get; set; }
        public object badges { get; set; }
        public object latestAlcoholDishCreatedAt { get; set; }
    }



    //---------------------- Menu Details API Result

    public class CPWOptionItemInfo
    {
        public int optionItemId { get; set; }
        public string optionItemName { get; set; }
        public double salePrice { get; set; }
        public string currencyType { get; set; }
        public string displayStatus { get; set; }
        public string restrictionType { get; set; }
        public bool forceNotExpose { get; set; }

        public string GetPriceText()
        {
            if (currencyType == "USD")
                return string.Format("${0:N0}", salePrice);
            return string.Format("{0:N0}원", salePrice);
        }

        public string GetStateText()
        {
            switch (displayStatus)
            {
                case "ON_SALE":
                    return "판매중";
                case "SOLD_OUT_TODAY":
                    return "오늘만 품절";
                case "NOT_EXPOSE":
                    return "숨김";
            }
            return "";
        }
    }

    public class CPWOptionGroupInfo
    {
        public int optionId { get; set; }
        public string optionName { get; set; }
        public int exposeOrder { get; set; }
        public List<CPWOptionItemInfo> optionItems { get; set; }
        public string optionType { get; set; }
        public string exposeStatus { get; set; }
        public object minSelect { get; set; }
        public object maxSelect { get; set; }
    }

    public class CPWMenuDetailData
    {
        public int dishId { get; set; }
        public string dishName { get; set; }
        public string description { get; set; }
        public double salePrice { get; set; }
        public string currencyType { get; set; }
        public string displayStatus { get; set; }   //ON_SALE - 판매중, NOT_EXPOSE - 숨김, SOLD_OUT_TODAY - 오늘만 품절
        public string restrictionType { get; set; }
        public object exposeOrder { get; set; }
        public List<CPWLongImageDto> dishImages { get; set; }
        public List<CPWOptionGroupInfo> options { get; set; }
        public List<CPWMenuGroupInfo> mappingMenus { get; set; }
        public object brandId { get; set; }
        public List<object> managementLevels { get; set; }
        public List<string> badges { get; set; }
        public bool forceNotExpose { get; set; }
        public object latestApprovalRequestStatus { get; set; }
    }


    //---------------------- Menu Update API Parameters
    public class CPWOptionItemSaveDto
    {
        public long optionItemId { get; set; }
        public string displayStatus { get; set; }

        public CPWOptionItemSaveDto() {
            optionItemId = 0;
            displayStatus = null;
        }

        public CPWOptionItemSaveDto(long optionItemId, string displayStatus)
        {
            this.optionItemId = optionItemId;
            this.displayStatus = displayStatus;
        }
    }

    public class CPWOptionMappingDto
    {
        public int optionId { get; set; }
        public int exposeOrder { get; set; }
        public List<CPWOptionItemSaveDto> optionItemSaveDtos { get; set; }

        public CPWOptionMappingDto()
        {
            optionId = 0;
            exposeOrder = 0;
            optionItemSaveDtos = null;
        }

        public CPWOptionMappingDto(int optionId, int exposeOrder, List<CPWOptionItemSaveDto> optionItemSaveDtos)
        {
            this.optionId = optionId;
            this.exposeOrder = exposeOrder;
            this.optionItemSaveDtos = optionItemSaveDtos;
        }
    }

    public class CPWUpdateMenuParams
    {
        public string dishName { get; set; }
        public long fromMenuId { get; set; }
        public long toMenuId { get; set; }
        public string displayStatus { get; set; }
        public string description { get; set; }
        public List<CPWOptionMappingDto> optionMappingDtos { get; set; }
        public double salePrice { get; set; }

        public CPWUpdateMenuParams()
        {
            dishName = null;
            fromMenuId = 0;
            toMenuId = 0;
            displayStatus = null;
            description = null;
            optionMappingDtos = null;
            salePrice = 0;
        }
    }

    //---------------------- Menu Update API Result
    public class CPWUpdatedMenuData
    {
        public long dishId { get; set; }
        public string dishName { get; set; }
        public string description { get; set; }
        public double salePrice { get; set; }
        public string currencyType { get; set; }
        public string displayStatus { get; set; }
        public string restrictionType { get; set; }
    }
}
