using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;



namespace CoupangWeb.Auth
{
    //Login Token Information
    public class CPWLoginTokenInfo
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public long accountId { get; set; }
        public string unifyToken { get; set; }
    }

    //Login API Result
    public class CPWLoginSessionData
    {
        public long id { get; set; }
        public string loginId { get; set; }
        public string name { get; set; }
        public object email { get; set; }
        public CPWLoginTokenInfo token { get; set; }
        public long merchantId { get; set; }
        public string type { get; set; }
        public object responsibleStoreId { get; set; }
        public object stores { get; set; }
        public object logIdentifier { get; set; }
        public bool changePasswordRequired { get; set; }
        public bool initialPasswordBrandAccount { get; set; }
        public object resolution { get; set; }
        public object osType { get; set; }
        public object appVersion { get; set; }
        public object spoofingId { get; set; }
        public object isB { get; set; }
    }

    //Profile Agreement information
    public class CPWAgreementInfo
    {
        public CPWBoolAgreement marketingCollectionAndUse { get; set; }
        public CPWAdvertisingInfo dayAdvertising { get; set; }
        public CPWAdvertisingInfo nightAdvertising { get; set; }
        public CPWBoolAgreement emailAdvertising { get; set; }
    }

    //Profile API Result    whoami
    public class CPWUserProfileData
    {
        public long accountId { get; set; }
        public long merchantId { get; set; }
        public string loginId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string mobileNo { get; set; }
        public string type { get; set; }
        public object responsibleStoreId { get; set; }
        public string createdBy { get; set; }
        public CPWAgreementInfo agreement { get; set; }
    }

    //Account Info API Result 
    public class CPWUserBankAccountData
    {
        public long merchantId { get; set; }
        public long storeId { get; set; }
        public string accountType { get; set; }
        public string accountNumber { get; set; }
        public string bankCode { get; set; }
        public string bankName { get; set; }
        public string ownerName { get; set; }
        public string storeName { get; set; }
        public string bankImagePath { get; set; }
        public string bizImagePath { get; set; }
        public List<string> operationCertificate { get; set; }
        public object menuImagePaths { get; set; }
        public object screenShotImagePaths { get; set; }
        public string ownerBirthday { get; set; }
        public string bizType { get; set; }
        public string bizCondition { get; set; }
        public object corpNo { get; set; }
        public string paymentStoreId { get; set; }
        public string merchantType { get; set; }
    }


    //Logout API result
    public class CPWLogoutResultData
    {
        public object actionLog { get; set; }
    }
}
