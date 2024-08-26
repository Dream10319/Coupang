using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupangSub.Models
{
    public class CPSABTestKeyOption
    {
        public const long AB_ID_EDP_ETA = 24804;
        public const long AB_ID_PROCESSING_DESIGN = 26748;
        public const long AB_ID_EDP_ACTIVE_ASSIGN = 29652;
        public const long AB_ID_EDP_DELAY_COMM_ITERATION = 30843;
        public const long AB_ID_CLEAR_TEMP_FOLDER = 35480;
        public const long AB_ID_RECOMMENDED_TIME = 36635;
        public const long AB_ID_RECOMMENDED_TIME_V2 = 38594;
        public const long AB_ID_VOICE_NOTIFICATION = 39663;
        public const long AB_ID_DELAY_BLOCKS = 37450;
        public const long AB_ID_ENHANCED_BUSY_MODE_V2 = 34874;

        public long testId { get; set; }
        public string testOption { get; set; }  //"A" ~ "D"

        public CPSABTestKeyOption()
        {
            testId = 0;
            testOption = null;
        }

        public static bool isOption(long id, List<string> targets, List<CPSABTestKeyOption> abOptions)
        {
            var abOption = abOptions?.Find(r => r.testId == id);
            if (abOption != null)
            {
                var foundOption = targets.Find(v => v == abOption.testOption);
                return foundOption != null;
            }
            return false;
        }
    }

    //SOLD_OUT = "1", // 품절
    //ORDER_RUSH = "2", // 주문폭주 // TODO Mock PROD DEV환경별 ID값이 다르니 추후 서버에서 받은 값으로만 하도록 수정 필요
    //CLOSED_TODAY = "4", // 금일 휴업 // TODO Mock
    //ALREADY_CLOSED = "19", // 금일 휴업 // TODO Mock
    //INCORRECT_MENU = "5", // 메뉴상이 // TODO Mock
    //CUSTOMER_REQUEST = "17", // 고객 요청
    //CUSTOMER_REQUEST_PICKUP = "27", // 고객 요청
    //ETC_BUSINESS_CLOSED_OR_CHANGED = "16", // 기타 > 폐업/사업자 변경 // TODO Mock
    //ETC_OTHERS = "18", // 기타 > 기타 // TODO Mock
    //ETC_DONT_KNOW_USAGE = "15", // 기타 > 사용법 모름 // TODO Mock
    //ETC_TEST_ORDER = "7", // 기타 > 테스트 주문 // TODO Mock
    //ORDERED_AMOUNT = "6", // 주문 금액 // TODO Mock
    //OTHERS = "3", // 기타 사유
    //CUSTOMER_ADDITIONAL_MENU = "86", // 고객 요청 > 메뉴 추가 주문 // TODO Mock
    //CUSTOMER_WRONG_MENU = "89", // 고객 요청 > 메뉴 잘못 주문 // TODO Mock
    //DELIVERY_DELAY = "88", // 고객 요청 > 배달 지연 // TODO Mock
    //CUSTOMER_CANCEL = "87", // 고객 요청 > 변심 // TODO Mock
    public class CPSOrderCancelReason
    {
        public string cancellationReasonId { get; set; }
        public string message { get; set; }
    }

    public class CPSReceiver
    {
        public string mobile { get; set; }
        public string name { get; set; }
    }

    public class CPSLocation
    {
        public string abbrAddress { get; set; }
        public string address { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class CPSDestination
    {
        public CPSLocation location { get; set; }
        public CPSReceiver receiver { get; set; }
    }
    

    public class CPSCourierInfo
    {
        public string courierId { get; set; } // 쿠리어 ID
        public string name { get; set; } // 쿠리어 이름
        public long acceptedDuration { get; set; } // 수락 기간
        public string phone { get; set; } // 쿠리어 연락처
    }

    public class ProcessingOrderModel
    {
        public long storeId { get; set; }
        public string abbrOrderId { get; set; }
        public string orderId { get; set; }
        public string courierSupplyStatusType { get; set; } //"OTHER", "RAIN", "RAIN_SNOW", "TYPHOON", "SNOW"
        public string andonStatusLevel { get; set; } //ANDON_NONE, "ANDON_ONE", "ANDON_TWO", "ANDON_THREE"
        public long preparationRemainingTime { get; set; }
        public long? estimatedDeliveryAssignMinTime { get; set; }
        public bool? isDelivery { get; set; }
        public bool? isFoodReady { get; set; }
        public bool? isCourierAssigned { get; set; }
        public bool? shownEdpSafeNumberButton { get; set; }
        public bool? showCustomerRecognitionBox { get; set; }
        public bool? requiredDeliveryCompleted { get; set; }
        public bool? isTargetForNewUI { get; set; }
        public long? mlFoodPrepTime { get; set; }
        public string storeName { get; set; }
    }

    public class TMenuDetail
    {
        public long storeId { get; set; }
        public long menuId { get; set; }
        public string menuName { get; set; }
        public string description { get; set; }
        public string exposeStatus { get; set; }
        public long exposeOrder { get; set; }
        public List<TDishDetail> dishes { get; set; }
    }

    public class TDishDetail
    {
        public long storeId { get; set; }
        public long? menuId { get; set; }
        public long dishId { get; set; }
        public long? salePrice { get; set; }
        public string dishName { get; set; }
        public string description { get; set; }
        public long? exposeOrder { get; set; }
        public string displayStatus { get; set; }
    }

    public class TSoldOutRequest
    {
        public long id { get; set; }
        public bool onlyToday { get; set; }
    }

    public class CPSEdpAssignInfo
    {
        public string descriptionType { get; set; }
        public long noticeEndTime { get; set; }
        public long noticeLeftTime { get; set; }


    }

    public class StoreModeDto
    {
        public string operationMode { get; set; } //"NORMAL", "BUSY"
        public string abuseType { get; set; } //"HALF_HOUR", "ONE_DAY"
        public string abuseMode { get; set; } //"DELAY", "BUSY"
        public long prepareFoodDelayTime { get; set; }
        public long maxPlusPrepareTime { get; set; }
        public int? maxDelayCount { get; set; }
    }

    public class CPSExtraDeliveryInfo
    {
        public long discountedDeliveryFee { get; set; } // 할인되어 실제 고객이 계산해야할 배달 금액
        public long deliveryFee { get; set; } // 배달비

        public CPSExtraDeliveryInfo()
        {
            discountedDeliveryFee = 0;
            deliveryFee = 0;
        }
    }

    public class CPSExtraPaymentInfoDto {
        public long cashAmount { get; set; } // 쿠팡 캐시
        public object extraPayAmountInfo { get; set; }
    }

    public class CPSOrderStateInfo
    {
        public long pickUpRemainingTime { get; set; } // 쿠리어 오고 있는 시간
        public long preparationRemainingTime { get; set; } // 남은 조리 시간 초단위
        public long deliveryRemainingTime { get; set; } // 남은 배달 시간
        public long? estimatedDeliveryTime { get; set; } // 배달 소요 시간
        public string statusText { get; set; } // 주문 상태 텍스트
        public string statusValue { get; set; } // 주문 상태 값
        public string estimatedCustomerPickUpTime { get; set; }
        public long? estimatedDeliveryAssignMinTime { get; set; } // 남은 EDP 도착 최소 예정시간 초단위 // v1.5.6 // Deprecated
        public long? estimatedDeliveryAssignMaxTime { get; set; } // 남은 EDP 도착 최대 예정시간 초단위 // v1.5.6 // Deprecated
        public string estimatedPickUpTime { get; set; } // EDP 도착 예정 시간
        public string courierStatus { get; set; } // from v1.6.1, "COURIER_ASSIGNING" - 배정전, "COURIER_AWAITING_ACCEPTANCE" -  수락대기, "COURIER_ACCEPTED" - 배정완료+이동중, "COURIER_ARRIVED" - 이동중 + 매장도착
        public long? prepareFoodDuration { get; set; }
        public int? delayCount { get; set; }
    }

    

    public class CPSOrderEntity
    {
        public List<CPSABTestKeyOption> abTestKeyOptions;
        public long pickupOrderId { get; set; }
        public string abbrOrderId { get; set; }
        public string orderTitle { get; set; }
        public string orderId { get; set; }
        public long storeId { get; set; }
        public CPSStoreDetails store { get; set; }
        public string customerName { get; set; }
        public CPSDateTimeInfo orderedAt { get; set; }
        public long? mlFoodPrepTime { get; set; }
        public long deliveryFee { get; set; }
        public long distanceFee { get; set; }
        public long smallOrderFee { get; set; }
        public string paymentMethodType { get; set; } //페이 타입  "CARD" - 카드 결제, "BANK" - 계좌이체, "BALANCE" - 쿠페이머니, null - 쿠팡캐시
        public long paymentAmount { get; set; }
        public long canceledPaymentAmount { get; set; }
        public long coupangCash { get; set; }
        public long canceledCoupangCash { get; set; }
        public CPSDestination destination { get; set; }
        public long salePrice { get; set; } // 주문 금액
        /** @deprecated  POS 앱의 하위 호환성을 위해 해당 값은 discountDishPrice + discountDeliveryPrice 값의 합산으로 보여지며, 만약 주문상태가 전체취소인 경우에는 해당 값 discountDishPrice 값만 노출시켜준다. */
        public long discountPrice { get; set; } // 할인 금액
        public long canceledPrice { get; set; } // 취소금액
        public long initialSalePrice { get; set; } // 최초 주문 금액(부분 취소일 경우 노출)
        public CPSExtraDeliveryInfo extraDeliveryInfo  { get; set; }
        public long customerOrderCount { get; set; }
        public CPSExtraPaymentInfoDto extraPaymentInfoDto { get; set; }
        public long totalAmount { get; set; } // 결제 금액
        public List<TOrderItem> items { get; set; }// 주문 아이템 목록
        public List<TOrderItem> cancelledItems { get; set; } // 취소 주문 아이템 목록
        public long discountDishPrice { get; set; }
        public long discountDeliveryPrice { get; set; }
        public CPSEdpAssignInfo edpAssignInfo { get; set; }
        public string status { get; set; } //주문상태  "CREATED" - 주문 생성, "PAYMENT_APPROVED" - 접수 대기, "ACCEPTED" - 주문 수락, "PICKED_UP" - 픽업 완료, "COMPLETED" - 배달 완료, "CANCELLED" - 주문 취소
        public CPSOrderStateInfo state { get; set; }
        public CPSCourierInfo courier { get; set; } // deprecated from v1.5.4
        public bool? courierAssigned { get; set; } // v1.5.4
        public string courierSupplyStatusType { get; set; } // 배달 지연 상태값 // v1.5.4 //"OTHER", "RAIN", "RAIN_SNOW", "TYPHOON", "SNOW"
        public string andonStatusLevel { get; set; }  // 우천시 상태값 // v1.5.6 //ANDON_NONE, "ANDON_ONE", "ANDON_TWO", "ANDON_THREE"
        public string note { get; set; }
        public bool? reviewed { get; set; }
        public double? reviewRating { get; set; }
        public bool? cancelled { get; set; }
        public string orderServiceType { get; set; }    //"PICKUP" - 픽업주문, "DELIVERY" - 배달주문, null - 모두
        public bool? hasAlcohol { get; set; }
        public string alcoholName { get; set; }
        public string alcoholNote { get; set; }

        public bool isShowing(string progress)
        {
            switch (progress)
            {
                case "PENDING":
                    return (status == "CREATED") || (status == "PAYMENT_APPROVED");
                case "PROCESSING":
                    return (status == "ACCEPTED");
                case "COMPLTED":
                    return (status == "PICKED_UP") || (status == "COMPLETED") || (status == "CANCELLED");
            }
            return false;
        }

        public bool isCancellable()
        {
            if ((storeId < 1) || string.IsNullOrWhiteSpace(orderId))
                return false;
            return (status == "CREATED") || (status == "RECEIPT");
        }

        public bool isCourierAssigned()
        {
            string status = state?.courierStatus ?? "";
            return (status == "COURIER_ACCEPTED") || // 배정완료+이동중
                    (status == "COURIER_ARRIVED"); // 이동중 + 매장도착
        }

        public bool isPickUpOrder()
        {
            return orderServiceType == "PICKUP";
        }

        public bool isDeliveryOrder()
        {
            return orderServiceType == "DELIVERY";
        }

        public bool isTotalCancelled()
        {
            return status == "CANCELLED";
        }

        public bool isPartialCancelled()
        {
            return !isTotalCancelled() && ((cancelledItems?.Count ?? 0) > 0) && (initialSalePrice > 0) && (canceledPrice > 0);
        }

        public bool isTutorialOrder()
        {
            return orderId == "tutorialOrderId";
        }

        public int calculateTotalQuantity()
        {
            int total = 0;
            if (items == null)
            {
                foreach (var item in items)
                    total += item.quantity;
            }
            return total;
        }

        public bool useNotDeliveredPickUpOrder()
        {
            return (orderServiceType == "PICKUP") && (state.statusValue == "MERCHANT_READY");
        }
    }

    public class CPSOrderDetail : CPSOrderEntity
    {
        public List<CPSOrderCancelReason> cancelReasons { get; set; } // 주문 거절 또는 취소시 주문 사유 목록
    }



    public class CPSCancelCode
    {
        public const string SOLD_OUT = "1";

        public string cancellationReasonId { get; set; }
        public string message { get; set; }
        public List<object> childCancellationReasonList { get; set; }
    }


    public class  CPSSafeNumberInfo
    {
        public string safeNumber { get; set; }
    }
}
