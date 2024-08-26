using CoupangWeb.Auth;
using CoupangWeb.Models;
using Microsoft.Win32;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoupangWeb
{
    public class Global
    {
        public const int TEST_MODE = 1;

        public static string userAgent { get; set; }
        public static CPWLoginSessionData userInfo { get; set; }
        public static List<CPWShopData> shopList { get; set; }
        public static CookieContainer cookies { get; set; }

        public static void Init()
        {
            userAgent = GetChromeUserAgent();
            cookies = new CookieContainer();
            ClearValues();
        }

        public static void ClearValues()
        {
            userInfo = null;
            shopList = null;
        }

        public static string GetChromeUserAgent()
        {
            //return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            string version = "";
            string path = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", "").ToString();
            if (path.Length < 1)
                path = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", "").ToString();
            if (path.Length > 0)
                version = FileVersionInfo.GetVersionInfo(path).FileVersion;
            if (version.Length < 1)
                version = "113.0.0.0";
            else
            {
                string[] parts = version.Split('.');
                version = parts[0] + ".0.0.0";
            }
            string template = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{VERSION} Safari/537.36";
            return template.Replace("{VERSION}", version);
        }

        public static bool HasStores()
        {
            return (shopList != null) && (shopList.Count > 0);
        }

        public static CPWShopData GetStore(int index)
        {
            if ((shopList != null) && (index >= 0) && (index < shopList.Count) && (shopList[index].id > 0))
                return shopList[index];
            return null;
        }

        public static string GetStoreNameById(long shopId)
        {
            if ((shopList != null) && (shopList.Count > 0))
            {
                foreach (var shop in shopList)
                {
                    if (shop.id == shopId)
                        return shop.name;
                }
            }
            return "";
        }


        public static void SaveCookies(IList<RestResponseCookie> list)
        {
            if (list == null)
                return;
            foreach (var current in list)
            {
                try
                {
                    string domain = current.Domain;
                    if (string.IsNullOrWhiteSpace(domain))
                        continue;

                    bool found = false;
                    Uri uri = null;
                    //Remove preceding dots of domain
                    int index = 0;
                    while (domain[index] == '.')
                        index++;
                    if (index > 0)
                        domain = domain.Substring(index);
                    if (Uri.TryCreate("https://" + domain + "/", UriKind.Absolute, out uri))
                    {
                        CookieCollection oldList = cookies.GetCookies(uri);
                        if (oldList != null)
                        {
                            for (int i = 0; i < oldList.Count; i++)
                            {
                                Cookie oldItem = oldList[i];
                                if (oldItem == null)
                                    continue;
                                string od = oldItem.Domain;
                                if (current.Name.Equals(oldItem.Name) && current.Path.Equals(oldItem.Path) &&
                                    current.Port.Equals(oldItem.Port) && current.Value.Equals(oldItem.Value))
                                {
                                    if (od.Equals(domain) || od.Equals("." + domain))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        if (Uri.TryCreate("http://" + domain + "/", UriKind.Absolute, out uri))
                        {
                            CookieCollection oldList = cookies.GetCookies(uri);
                            if (oldList != null)
                            {
                                for (int i = 0; i < oldList.Count; i++)
                                {
                                    Cookie oldItem = oldList[i];
                                    if (oldItem == null)
                                        continue;
                                    string od = oldItem.Domain;
                                    if (current.Name.Equals(oldItem.Name) && current.Path.Equals(oldItem.Path) &&
                                        current.Port.Equals(oldItem.Port) && current.Value.Equals(oldItem.Value))
                                    {
                                        if (od.Equals(domain) || od.Equals("." + domain))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!found)
                        cookies.Add(new Cookie(current.Name, current.Value, current.Path, domain));
                }
                catch { }
            }
        }

        public static long TimeInMillis()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.UtcNow - origin;
            return (long)Math.Floor(diff.TotalMilliseconds);
        }

        public static long TimeInSeconds()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.UtcNow - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        public static long EpochSeconds4Date(DateTime dt)
        {
            if (dt == null)
                return 0;
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = dt - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        public static DateTime EpochSeconds2Date(long epochSeconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(epochSeconds).DateTime;
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            if ((data == null) || (data.Length < 1))
                return "";
            return string.Join(string.Empty, data.Select(x => x.ToString("X2"))).ToLower();
        }
    }
}
