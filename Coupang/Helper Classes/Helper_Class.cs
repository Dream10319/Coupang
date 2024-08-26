using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Coupang
{
   public  static class Helper_Class
    {
      
        public  static   JToken Json_Responce(string Containt)
        {
            StringReader reader = new StringReader(Containt);
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                JsonSerializer Deserializer = new JsonSerializer();
                return (JToken)Deserializer.Deserialize<JToken>(jsonReader);
            }
        }
        public static  async Task<IRestResponse> Send_Request(string url, Method _Method, List<Tuple<string, string>> QueryParameters = null, string Request_Payload = null)
        {


            var P_client = new RestClient(url);

#if Test_Proxy
            WebProxy proxy = new WebProxy("192.168.231.134", Int32.Parse("8888"));
            P_client.Proxy = proxy;
#endif


            var P_request = new RestRequest(_Method);

            if (QueryParameters != null)
            {
                foreach (Tuple<string, string> i in QueryParameters)
                {
                    P_request.AddQueryParameter(i.Item1, i.Item2);
                }
            }

            if(Request_Payload!= null)
            {
                P_request.AddJsonBody(Request_Payload);
            }

            P_client.CookieContainer = Cookies_Class.Cookies;
            P_client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";

            P_request.AddHeader("Accept", "application/json");
            P_request.AddHeader("Origin", "chrome-extension://opbdabemkeebgjjopefgcjjccafieono");
            P_request.AddHeader("Content-Type", "application/json");
            P_request.AddHeader("client-sign", "3af24c94f45b2e3c023e286a6a4d41f2");
            P_request.AddHeader("coupang-pos-version", "1.10.15");


            IRestResponse _result = await Task.Run(() => P_client.ExecuteAsync(P_request));
            return _result;
        }


        public static DateTime From_Unix_Timestamp(double   InputValue)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(InputValue).ToLocalTime();
            return dtDateTime;
            /*.ToString ("yyyy-MM-dd hh:mm");*/
        }

        public static double To_Unix_Timestamp(string  InputValue)
        {
            //DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //dtDateTime = dtDateTime.AddMilliseconds(InputValue).ToLocalTime();
            //return dtDateTime.ToString("yyyy-MM-dd hh:mm");
            DateTime c;
            DateTime.TryParseExact(InputValue, "yyyy-MM-dd hh:mm:ss.fff tt", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out c);

            //return (c - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))  .TotalMilliseconds;


            //var xx = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).TotalMilliseconds;
            //return c.AddMilliseconds(-(double )new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).);
            
            var dateTime = new DateTime(2021, 02, 21, 22, 0, 0, DateTimeKind.Utc);
            //var dateWithOffset = new DateTimeOffset(dateTime).ToUniversalTime();
            //long timestamp = dateWithOffset.ToUnixTimeMilliseconds();
            //dateWithOffset.tot

            //TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            TimeSpan t = c-  new   DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) ;
            double  secondsSinceEpoch = (double)t.TotalMilliseconds;

            return secondsSinceEpoch;

        }
    }
    

    public static class Cookies_Class
    {
        public static CookieContainer Cookies= null;

        public class Cookie_Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Path { get; set; }
            public string Domain { get; set; }
            public bool Secure { get; set; }
            public DateTime Expires { get; set; }
        }





        public static void Save_Cookies()
        {
            List<Cookie_Response> co = new List<Cookie_Response>();


     
            foreach (Cookie c in Cookies.GetCookies(new Uri("https://coupang.com")))
            {
                co.Add(new Cookie_Response()
                {
                    Name = c.Name,
                    Value = c.Value,
                    Path = c.Path,
                    Domain = c.Domain,
                    Secure = c.Secure,
                    Expires = c.Expires
                });
              
            }

            StringWriter wr = new StringWriter();

            using (JsonWriter jw = new JsonTextWriter(wr))
            {
                JsonSerializer sz = new JsonSerializer();
                sz.Formatting = Formatting.Indented;
                sz.Serialize(jw, co, typeof(List<Cookie_Response>));
            }


           
            File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Cookies.json", wr.ToString());
        }

        public static void Load_Cookies()
        {

            if (Cookies == null)
            {
                Cookies = new CookieContainer();
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Cookies.json") == true)
            {
                StringReader reader = new StringReader(File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\Cookies.json"));


                List<Cookie_Response> co = new List<Cookie_Response>();

                using (JsonTextReader jr = new JsonTextReader(reader))
                {
                    JsonSerializer sz = new JsonSerializer();
                    co = (List<Cookie_Response>)sz.Deserialize(jr, typeof(List<Cookie_Response>));

                    //MessageBox.Show(co.Count.ToString());

                    foreach (Cookie_Response c in co)
                    {
                        Cookies.Add(new Cookie()
                        {
                            Name = c.Name,
                            Value = c.Value,
                            Path = c.Path,
                            Domain = c.Domain,
                            Secure = c.Secure,
                            Expires = c.Expires
                        });
                    }
                }



            }

        }
    }
}
