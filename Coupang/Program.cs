using System;
using System.Net;
using System.Windows.Forms;


namespace Coupang
{
    static class Program
    {
   
     

     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
           
            Application.EnableVisualStyles();

#if Test_Proxy
           

            ServicePointManager.ServerCertificateValidationCallback +=
            delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                 System.Security.Cryptography.X509Certificates.X509Chain chain,
                                 System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
             return true; // **** Always accept
            };

#else
             ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; 
#endif



            Cookies_Class.Load_Cookies();

            Application.SetCompatibleTextRenderingDefault(false);

            Form1 main_frm = new Form1();
          

      

            Application.Run(main_frm);
         
           
        }
    }
}
