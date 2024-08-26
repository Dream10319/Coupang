using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupangSub.Models
{
    public class TOrderItemOption
    {
        public string optionItemId { get; set; } // 아이템 옵션 아이디
        public string optionName { get; set; } // 아이템 옵션 이름
        public int optionQuantity { get; set; } // 아이템 옵션 수량
        public long optionPrice { get; set; } // 아이템 옵션 가격
        public bool? isAlcohol { get; set; }

        public long getTotalPrice()
        {
            return optionPrice * optionQuantity;
        }
    }

    public class TOrderItem
    {
        public string dishId { get; set; } // 아이템 아이디
        public string name { get; set; } // 아이템 이름
        public long unitSalePrice { get; set; } // 아이템 가격
        public int quantity { get; set; } // 아이템 수량
        public List<TOrderItemOption> itemOptions { get; set; } // 아이템 옵션 목록
        public bool isAlcohol { get; set; }

        public long getTotalDishPrice()
        {
            return unitSalePrice * quantity;
        }
    }


    public class CPSDishDetail //TDishDetail
    {
        public long storeId { get; set; }
        public long? menuId { get; set; }
        public long dishId { get; set; }
        public long? salePrice { get; set; }
        public string dishName { get; set; }
        public string description { get; set; }
        public int? exposeOrder { get; set; }
        public string displayStatus { get; set; }
    }


    public class CPSMenuDetail // TMenuDetail
    {
        public long storeId { get; set; }
        public long menuId { get; set; }
        public string menuName { get; set; }
        public string description { get; set; }
        public string exposeStatus { get; set; }
        public int exposeOrder { get; set; }
        public List<TDishDetail> dishes { get; set; }
    }

    public class CPSSoldOutRequest //TSoldOutRequest
    {
        public long id { get; set; }
        public bool onlyToday { get; set; }
    }

}
