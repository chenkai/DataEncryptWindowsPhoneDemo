using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DataEncryptBuildDemo.DataEncryptCommon
{
    /// <summary>
    /// HMACMD Data Encrypt Operator
    /// Author:chenkai Data:6/7/2011
    /// </summary>
    public class HMACMD5DataEncrypt
    {
        /// <summary>  
        /// HMAC_MD5 DataEncrypt  
        /// </summary>  
        /// <param name="original">明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>返回加密的字符串</returns>  
        public static string HMAC_MD5(string original, string key)
        {
            byte[] b_tmp;
            byte[] b_tmp1;
            if (key == null)
            {
                return null;
            }
            byte[] digest = new byte[512];
            byte[] k_ipad = new byte[64];
            byte[] k_opad = new byte[64];

            byte[] source = System.Text.UTF8Encoding.UTF8.GetBytes(key);
            //System.Security.Cryptography.MD5 shainner = new MD5CryptoServiceProvider();

            for (int i = 0; i < 64; i++)
            {
                k_ipad[i] = 0 ^ 0x36;
                k_opad[i] = 0 ^ 0x5c;
            }

            try
            {
                if (source.Length > 64)
                {
                    //shainner = new MD5CryptoServiceProvider();
                    source = MD5Core.GetHash(source);//shainner.ComputeHash(source);
                }

                for (int i = 0; i < source.Length; i++)
                {
                    k_ipad[i] = (byte)(source[i] ^ 0x36);
                    k_opad[i] = (byte)(source[i] ^ 0x5c);
                }

                b_tmp1 = System.Text.UTF8Encoding.UTF8.GetBytes(original);//内容
                b_tmp = Adding(k_ipad, b_tmp1);


                //shainner = new MD5CryptoServiceProvider();
                digest = MD5Core.GetHash(b_tmp); //shainner.ComputeHash(b_tmp);
                b_tmp = Adding(k_opad, digest);


                //shainner = new MD5CryptoServiceProvider();
                digest = MD5Core.GetHash(b_tmp); //shainner.ComputeHash(b_tmp);
                return ByteToString(digest);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>  
        /// 填充byte  
        /// </summary>  
        /// <param name="a"></param>  
        /// <param name="b"></param>  
        /// <returns></returns>  
        private static byte[] Adding(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }

        /// <summary>  
        /// Byte To String  
        /// </summary>  
        /// <param name="buff"></param>  
        /// <returns></returns>  
        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format  
            }
            return (sbinary);
        }  
    }
}
