using System;
using System.Security.Cryptography;
using System.Text;

namespace Coupang
{

    public static class Signin_Helper
    {

        public static string Password_Genrator(string publicKey, string text)
        {
           
            byte[] keyBytes = Convert.FromBase64String(publicKey);
            byte[] modulus;
            switch (keyBytes.Length)
            {
                case 94: // 512 bits
                    //modulus = new byte[65];
                    //Array.Copy(keyBytes, 24, modulus, 0, modulus.Length);

                    modulus = new byte[64];
                    Array.Copy(keyBytes, 25, modulus, 0, modulus.Length);

                    break;
                case 162: // 1024 bits
                    modulus = new byte[128];
                    Array.Copy(keyBytes, 29, modulus, 0, modulus.Length);
                    break;
                default:
                    throw new NotSupportedException();
            }

            byte[] publicExponent = new byte[3];
            Array.Copy(keyBytes, keyBytes.Length - 3, publicExponent, 0, 3);

            var para = new RSAParameters();
            para.Modulus = modulus;
            para.Exponent = publicExponent;


            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSAParameters RSAKeyInfo = new RSAParameters();
            RSAKeyInfo.Modulus = modulus;
            RSAKeyInfo.Exponent = publicExponent;
            RSA.ImportParameters(para);


            return Convert.ToBase64String(RSA.Encrypt(Encoding.UTF8.GetBytes(text), false));
        }

     
    }
}