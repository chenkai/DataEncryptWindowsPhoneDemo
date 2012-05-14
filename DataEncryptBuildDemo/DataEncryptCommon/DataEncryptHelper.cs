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

using System.Text;

namespace DataEncryptBuildDemo.DataEncryptCommon
{
    public enum DataEncryptType
    {
        MD5,
        HMACMD5,
        DES,
        TripleDES 
    }
    public class DataEncryptHelper
    {
        /// <summary>
        /// Excute Data Encrypt Operator
        /// </summary>
        /// <param name="sourceContent">Source Encrypt Data</param>
        /// <param name="encryptKey">Encrypt Key</param>
        /// <param name="dataEncryptType">Data Encrypt Operator Type</param>
        /// <returns>Excute Data Encrypt String</returns>
        public static string ExcuteDataEncrypt(string sourceContent, string encryptKey, DataEncryptType dataEncryptType)
        {
            string dataEncryptStr = string.Empty;
            if (string.IsNullOrEmpty(sourceContent))
                return null;

            switch (dataEncryptType)
            {
                case DataEncryptType.MD5:
                    dataEncryptStr = MD5Core.GetHashString(sourceContent);//MD5加密
                    break;
                case DataEncryptType.HMACMD5:
                    dataEncryptStr=HMACMD5DataEncrypt.HMAC_MD5(sourceContent, encryptKey);//HMACMD5加密
                    break;
                case DataEncryptType.DES:
                  
                    break;
                case DataEncryptType.TripleDES:
                    byte[] dataEncryptBytes=Des_DataEncrypt.TripleDesEncryptWithOutKey(sourceContent);
                    dataEncryptStr =UTF8Encoding.UTF8.GetString(dataEncryptBytes,0,dataEncryptBytes.Length);
                    break;
                    break;
                default:
                    break;
            }
            return dataEncryptStr;
        }
    }
}
