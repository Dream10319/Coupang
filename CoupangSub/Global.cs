using CoupangSub.Models;
using CoupangSub.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangSub
{
    public class Global
    {
        public const int TEST_MODE = 1;

        //client-sign: MD5(userAgent)

        public static string userAgent { get; set; }
        public static string clientSign { get; set; }
        public static CPSLoginResult userInfo { get; set; }
        public static List<CPSStoreModel> verifiedStoreList { get; set; }
        public static CPSAppSettings setting { get; set; }
        public static CookieContainer cookies { get; set; }
        public static Dictionary<string, string> cookieValues { get; set; }
        public static Dictionary<string, string> cookieDomains { get; set; }



        public static void Init()
        {
            userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36"; //GetChromeUserAgent();
            GetClientSign();
            cookies = new CookieContainer();
            cookieValues = new Dictionary<string, string>();
            cookieDomains = new Dictionary<string, string>();
            LoadAppSettings();
            ClearValues();

            string text = DecryptAes("U2FsdGVkX19Qq/LAJBaY3f+xSo4ciSXnNo5frnS4PdfJbfSgxexipnXrHVgLGXCcEpqE2pY4/jJGAZHFf0g4BXU6B7avSXo4sdGPYfswDjQ+EEBD+8QRZRSiDvdfMFCPHkg1ukcOH04wFmdeMk1BUQ==", setting.aesKey);
            Console.WriteLine(text);
        }

        public static void ClearValues()
        {
            userInfo = null;
            verifiedStoreList = null;
        }

        private static void GetClientSign()
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(userAgent);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                clientSign = ByteArrayToHexString(hashBytes); //IN .NET 5 +, Convert.ToHexString(hashBytes); 
            }
        }

        public static void LoadAppSettings()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "settings.json");
                string text = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(text))
                    setting = JsonConvert.DeserializeObject<CPSAppSettings>(text);
            }
            catch { }
            if (setting == null)
            {
                setting = new CPSAppSettings();
                SaveAppSettings();
                RefreshCookies();
            }
        }

        public static void SaveAppSettings()
        {
            if (setting == null)
                return;
            try
            {
                string path = Path.Combine(Application.StartupPath, "settings.json");
                if (File.Exists(path))
                    File.Delete(path);
                File.WriteAllText(path, JsonConvert.SerializeObject(setting, Formatting.Indented), Encoding.UTF8);
            }
            catch { }
        }


        public static bool HasStores()
        {
            if ((userInfo == null) || (userInfo.stores == null) || (userInfo.stores.Count < 1))
                return false;
            return true;
        }

        public static CPSStoreModel GetStore(int index)
        {
            if (userInfo == null)
                return null;
            var shopList = userInfo.stores;
            if ((shopList != null) && (index >= 0) && (index < shopList.Count) && (shopList[index].storeId > 0))
                return shopList[index];
            return null;
        }

        public static CPSStoreModel GetStoreById(long no)
        {
            if (userInfo == null)
                return null;
            var shopList = userInfo.stores;
            if ((shopList != null) && (shopList.Count > 0))
            {
                foreach (var shop in shopList)
                {
                    if (shop.storeId == no)
                        return shop;
                }
            }
            return null;
        }


        public static void RefreshCookies()
        {
            if (cookieValues == null)
                cookieValues = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(setting.deviceId) && !string.IsNullOrWhiteSpace(setting.sessionId))
            {
                if (cookieValues.ContainsKey("app-type"))
                    cookieValues["app-type"] = "COUPANG_POS";
                else
                    cookieValues.Add("app-type", "COUPANG_POS");

                string version = setting.appVersion;
                if (cookieValues.ContainsKey("coupang-pos-version"))
                    cookieValues["coupang-pos-version"] = version;
                else
                    cookieValues.Add("coupang-pos-version", version);
                if (cookieValues.ContainsKey("version"))
                    cookieValues["version"] = version;
                else
                    cookieValues.Add("version", version);

                if (cookieValues.ContainsKey("device-id"))
                    cookieValues["device-id"] = setting.deviceId;
                else
                    cookieValues.Add("device-id", setting.deviceId);

                if (cookieValues.ContainsKey("SESSION"))
                    cookieValues["SESSION"] = setting.sessionId;
                else
                    cookieValues.Add("SESSION", setting.sessionId);
            }

            cookies = new CookieContainer();
            if (cookieValues != null)
            {
                foreach (var key in cookieValues.Keys)
                {
                    cookies.Add(new Cookie(key, cookieValues[key], "/",
                                ((cookieDomains != null) && cookieDomains.ContainsKey(key)) ?
                                                        cookieDomains[key] : ".coupang.com"));
                }
            }
        }

        /*public static string GetChromeUserAgent()
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
        }*/

        public static void SaveCookies(IList<RestResponseCookie> list)
        {
            if (list == null)
                return;
            bool changed = false;   //changed in cookies
            bool setUp = false;     //changed in settings
            try
            {
                foreach (var current in list)
                {
                    string key = current.Name;
                    string value = current.Value; //value may be empty
                    string domain = current.Domain;
                    if (string.IsNullOrWhiteSpace(key) ||
                        string.IsNullOrWhiteSpace(domain))
                        continue;
                    
                    //Save Domain
                    if ((domain != "coupang.com") && (domain != ".coupang.com"))
                    {
                        if (cookieDomains == null)
                            cookieDomains = new Dictionary<string, string>();
                        if (cookieDomains.ContainsKey(key))
                            cookieDomains[key] = domain;
                        else
                            cookieDomains.Add(key, domain);
                    }
                    //Save some fields into settings
                    if (key == "device-id")
                    {
                        if (setting == null)
                            setting = new CPSAppSettings();
                        setting.deviceId = value;
                        if (!setUp)
                            setUp = true;
                    }
                    if (key == "SESSION")
                    {
                        if (setting == null)
                            setting = new CPSAppSettings();
                        setting.sessionId = value;
                        if (!setUp)
                            setUp = true;
                    }

                    if (cookieValues == null)
                        cookieValues = new Dictionary<string, string>();
                    string old = "";
                    if (cookieValues.ContainsKey(key))
                    {
                        old = cookieValues[key];
                        cookieValues[key] = value;
                    }   
                    else
                        cookieValues.Add(key, value);
                    if (!changed)
                        changed = old != value;
                }
            }
            catch { }
            if (setUp)
                SaveAppSettings();
            if (changed)
                RefreshCookies();
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


        //Encrypt Password with Asymmetric Key
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || (setting == null) || string.IsNullOrWhiteSpace(setting.pubKey))
                return null;

            //Parse Public Key
            string key = setting.pubKey;
            PemReader pr = new PemReader(new StringReader(key));
            AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKey);

            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);
            var data = Encoding.UTF8.GetBytes(password);
            var cipher = csp.Encrypt(data, false);
            return Convert.ToBase64String(cipher);
        }

        //Encrypt / Decrypt data
        private static void DeriveKeyAndIv(byte[] passphrase, byte[] salt, int iterations, out byte[] key, out byte[] iv)
        {
            var hashList = new List<byte>();

            var preHashLength = passphrase.Length + (salt?.Length ?? 0);
            var preHash = new byte[preHashLength];

            Buffer.BlockCopy(passphrase, 0, preHash, 0, passphrase.Length);
            if (salt != null)
                Buffer.BlockCopy(salt, 0, preHash, passphrase.Length, salt.Length);

            var hash = MD5.Create();
            var currentHash = hash.ComputeHash(preHash);

            for (var i = 1; i < iterations; i++)
            {
                currentHash = hash.ComputeHash(currentHash);
            }

            hashList.AddRange(currentHash);

            while (hashList.Count < 48) // for 32-byte key and 16-byte iv
            {
                preHashLength = currentHash.Length + passphrase.Length + (salt?.Length ?? 0);
                preHash = new byte[preHashLength];

                Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
                Buffer.BlockCopy(passphrase, 0, preHash, currentHash.Length, passphrase.Length);
                if (salt != null)
                    Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + passphrase.Length, salt.Length);

                currentHash = hash.ComputeHash(preHash);

                for (var i = 1; i < iterations; i++)
                {
                    currentHash = hash.ComputeHash(currentHash);
                }

                hashList.AddRange(currentHash);
            }

            hash.Clear();
            key = new byte[32];
            iv = new byte[16];
            hashList.CopyTo(0, key, 0, 32);
            hashList.CopyTo(32, iv, 0, 16);
        }

        public static string DecryptAes(string encryptedString, string passphrase)
        {
            if (string.IsNullOrWhiteSpace(encryptedString) || string.IsNullOrEmpty(passphrase))
                return null;
            // encryptedString is a base64-encoded string starting with "Salted__" followed by a 8-byte salt and the
            // actual ciphertext. Split them here to get the salted and the ciphertext
            var base64Bytes = Convert.FromBase64String(encryptedString);
            if ((base64Bytes == null) || (base64Bytes.Length < 16))
                return null;

            var saltBytes = new byte[8]; //base64Bytes[8..16];
            var cipherTextBytes = new byte[base64Bytes.Length - 16]; //base64Bytes[16..];
            for (int i = 0; i < 8; i++)
                saltBytes[i] = base64Bytes[i + 8];
            for (int i = 0, k = 16; k < base64Bytes.Length; i++, k++)
                cipherTextBytes[i] = base64Bytes[k];

            // get the byte array of the passphrase
            var passphraseBytes = Encoding.UTF8.GetBytes(passphrase);

            // derive the key and the iv from the passphrase and the salt, using 1 iteration
            // (cryptojs uses 1 iteration by default)
            DeriveKeyAndIv(passphraseBytes, saltBytes, 1, out var keyBytes, out var ivBytes);

            // create the AES decryptor
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                // here are the config that cryptojs uses by default
                // https://cryptojs.gitbook.io/docs/#ciphers
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                var decryptor = aes.CreateDecryptor(keyBytes, ivBytes);

                // example code on MSDN https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0
                using (var msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // read the decrypted bytes from the decrypting stream and place them in a string.
                            return srDecrypt.ReadToEnd();
                        }   
                    }   
                }   
            }
        }

        public static string EncryptAes(string plainText, string passphrase)
        {
            if (string.IsNullOrWhiteSpace(plainText) || string.IsNullOrEmpty(passphrase))
                return null;

            Random rnd = new Random();
            byte[] saltBytes = new byte[8];
            for (int i = 0; i < 8; i++)
                saltBytes[i] = (byte)(rnd.Next() & 255);

            // get the byte array of the passphrase
            var passphraseBytes = Encoding.UTF8.GetBytes(passphrase);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // derive the key and the iv from the passphrase and the salt, using 1 iteration
            // (cryptojs uses 1 iteration by default)
            DeriveKeyAndIv(passphraseBytes, saltBytes, 1, out var keyBytes, out var ivBytes);

            byte[] encrypted = null;
            // create the AES encryptor
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                // here are the config that cryptojs uses by default
                // https://cryptojs.gitbook.io/docs/#ciphers
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                var encryptor = aes.CreateEncryptor(keyBytes, ivBytes);

                // example code on MSDN https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        csEncrypt.FlushFinalBlock();
                        //read the encrypted bytes from the encrypting stream
                        encrypted = msEncrypt.ToArray();
                    }
                }
                if (encrypted == null)
                    return null;

                //Base64 encode the prefix, salt and encrypted bytes
                var firstBytes = new byte[] { 0x53, 0x61, 0x6C, 0x74, 0x65, 0x64, 0x5F, 0x5F };
                var wholeBytes = new byte[firstBytes.Length + saltBytes.Length + encrypted.Length];
                Buffer.BlockCopy(firstBytes, 0, wholeBytes, 0, firstBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, wholeBytes, firstBytes.Length, saltBytes.Length);
                Buffer.BlockCopy(encrypted, 0, wholeBytes, firstBytes.Length + saltBytes.Length, encrypted.Length);
                return Convert.ToBase64String(wholeBytes);
            }
        }

        public static void EncryptRequest<T>(IRestRequest request, long timestamp, string url, T data,
                        string method = "GET", string contentType = "application/x-www-form-urlencoded")
        {
            if ((setting == null) || (request == null))
                return;

            bool isFileUpload = contentType == "multipart/form-data";
            string signature = "pos-signature";

            Dictionary<string, object> headers = new Dictionary<string, object>();
            if ((isFileUpload && (method == "POST")) || (method == "GET") || (method == "DELETE"))
                headers.Add("data", null);
            else
                headers.Add("data", data);
            headers.Add("timeStamp", timestamp);
            headers.Add("url", url);
            string text = JsonConvert.SerializeObject(headers);
            signature = EncryptAes(text, setting.aesKey);

            if (!isFileUpload && (method == "POST"))
                contentType = "application/json";

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", contentType);
            request.AddHeader("coupang-pos-version", setting.appVersion);
            request.AddHeader("client-sign", clientSign);
            request.AddHeader("pos-signature", signature);
            request.AddHeader("pos-timestamp", timestamp.ToString());
            request.AddHeader("pos-signature-version", "0.0.1-aes");
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
