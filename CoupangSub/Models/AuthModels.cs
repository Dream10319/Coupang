using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoupangSub.Models
{
    public class CPSAccountInfo
    {
        public long merchantId { get; set; }
        public string type { get; set; }
    }

    public class CPSLoginResult
    {
        public CPSAccountInfo account { get; set; }
        public List<CPSStoreModel> stores { get; set; }
        public object unifyToken { get; set; }
        public object accountId { get; set; }

        public CPSLoginResult() {
            account = null;
            stores = null;
            unifyToken = null;
            accountId = null;
        }

        public string GetFirstStoreName()
        {
            if ((stores == null) || (stores.Count < 1))
                return "";
            return stores[0].storeName ?? "";
        }
    }

    public class CPSVerifiedAccountInfo
    {
        public string accountId { get; set; }
        public List<CPSStoreModel> verifiedStoreList { get; set; }

        public CPSVerifiedAccountInfo()
        {
            accountId = null;
            verifiedStoreList = null;
        }
    }
}
