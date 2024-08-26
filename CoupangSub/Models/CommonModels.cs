using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CoupangSub.Models
{
    public class CPSDataApiResult<T>
    {
        public string code { get; set; }
        public string message { get; set; }

        public T content { get; set; }

        public CPSDataApiResult()
        {
            code = null;
            message = null;
            content = default(T);
        }
    }

    public class CPSAppSettings
    {
        public string appType { get; set; }
        public string appVersion { get; set; }
        public string encType { get; set; } //Current Encryption Type, "A" - Encrypt password in RSA, Other - No encryption for password
        public string pubKey { get; set; }  //Public key for RSA to hide password
        public string aesKey { get; set; }  //AES key to hide other data
        public string deviceId { get; set; } //UUID of logged in device
        public string sessionId { get; set; } //Session Id

        public CPSAppSettings()
        {
            appType = "COUPANG_POS";
            appVersion = "1.9.0";
            encType = "A";
            pubKey = "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAJzE1obe1GiSE6rqaIWjreZ8NmXur3dYgJPths2FDnNtN3Mwgl0ZNDc7RUZwgE4LZf9E2Tf3JmLpRIKXHGXhCyUCAwEAAQ==";
            aesKey = "pGpTb8sFLnxijedRAMrKmn238i+ladJd9Y7k0RmDpxk=";
            deviceId = null;
            sessionId = null;
        }
    }

    //
    public class CPSMetaCsInfo {
        public string phone { get; set; }
        public string email { get; set; }
    }

    //configuration
    public class CPSConfigurationInfo
    {
        public int autoRefreshDelay { get; set; }
        public Dictionary<string, string> storeAbResult { get; set; }
        public Dictionary<string, string> integratedStoreAbResult { get; set; }
        public Dictionary<string, string> deviceAbResult { get; set; }
        public Dictionary<string, string> config { get; set; }

        public CPSConfigurationInfo()
        {
            autoRefreshDelay = 0;
            storeAbResult = null;
            integratedStoreAbResult = null;
            deviceAbResult = null;
            config = null;
        }
    }


    public class CPSDateTimeInfo
    {
        public long dateTime { get; set; }
        public long elapsedSeconds { get; set; }
    }
}
