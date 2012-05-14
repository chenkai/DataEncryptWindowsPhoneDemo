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
using System.Security.Cryptography;
using DataEncryptBuildDemo.DataEncryptCommon.DESDataEncrypt;

namespace DataEncryptBuildDemo.DataEncryptCommon
{
    /// <summary>
    /// Des And TripleDES DataEncrypt Operator
    /// Author:chenkai Date:14/5 2012
    /// </summary>
    public class Des_DataEncrypt
    {
        /// <summary>
        /// TripleDes  Data Encrypt With Ot Encrypt Key Operator
        /// </summary>
        /// <param name="sourceContent">Source Need to TripleDes Encrpt Data</param>
        /// <returns>Encrypt Data Byte[] String</returns>
        public static byte[] TripleDesEncryptWithOutKey(string sourceContent)
        {
            if (string.IsNullOrEmpty(sourceContent))
                return null;

            var toEncryptSourceStr = Encoding.UTF8.GetBytes(sourceContent);
            TripleDESCryptoServiceProvider tripleDesEncryptProvider = new TripleDESCryptoServiceProvider();
            ICryptoTransform encryptTransform=tripleDesEncryptProvider.CreateEncryptor();
            byte[] encryptToBytes = encryptTransform.TransformFinalBlock(toEncryptSourceStr, 0, toEncryptSourceStr.Length);

            return encryptToBytes;
        }

        /// <summary>
        /// TripleDes Data DeEncrypt With Out Encrypt Key Operator
        /// </summary>
        /// <param name="encryptBytes">Encrypt Byte Array</param>
        /// <returns>DeEncrypt SourceContent String</returns>
        public static string TripleDesDeEncryptWithOutKey(byte[] encryptBytes)
        {
            if (encryptBytes == null || encryptBytes.Length <= 0)
                return string.Empty;

            TripleDESCryptoServiceProvider tripleDesProvider = new TripleDESCryptoServiceProvider();
            ICryptoTransform deEncryptTransform = tripleDesProvider.CreateDecryptor();
            var deEncryptBytes = deEncryptTransform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);
            var deEncryptFormatStr = Encoding.UTF8.GetString(deEncryptBytes, 0, deEncryptBytes.Length);

            return deEncryptFormatStr;
        }

        /// <summary>
        /// TripleDes Data Encrypt Use IVKey Operator
        /// </summary>
        /// <param name="sourceContent">Source Content</param>
        /// <param name="encryptKey">Encrypt Key</param>
        /// <returns>Encrypt Bytes  Array</returns>
        public static byte[] TripleDesEncryptUseIvKey(string sourceContent, byte[] encryptIVKey)
        {
            if (string.IsNullOrEmpty(sourceContent) || encryptIVKey == null || encryptIVKey.Length <= 0)
                return null;

            var toEncryptSourceStr = Encoding.UTF8.GetBytes(sourceContent);
            TripleDESCryptoServiceProvider tripleDesProvider = new TripleDESCryptoServiceProvider();

            //No Seting Pading

            var key = tripleDesProvider.Key; //Save Key
            IsolatedStorageCommon.IsolatedStorageSettingHelper.AddIsolateStorageObj("EncryptKey", key);
            ICryptoTransform encryptTransform = tripleDesProvider.CreateEncryptor(key, encryptIVKey);
            var encryptBytes = encryptTransform.TransformFinalBlock(toEncryptSourceStr, 0, toEncryptSourceStr.Length);

            return encryptBytes;
        }



        /// <summary>
        /// Triple Des DeEncrypt Operator Use IvKey
        /// </summary>
        /// <param name="encryptKey">Encrypt key can be null</param>
        /// <param name="ivKey">Iv</param>
        /// <param name="encryptBytes">EncryptBytes</param>
        /// <returns>Return String </returns>
        public static string TripleDesDeEncryptUseIvKey(byte[] encryptKey, byte[] ivKey, byte[] encryptBytes)
        {
            if (encryptBytes == null || encryptBytes.Length <= 0)
                return string.Empty;

            TripleDESCryptoServiceProvider tripleDesProvider = new TripleDESCryptoServiceProvider();

            if (encryptKey == null)
                encryptKey = IsolatedStorageCommon.IsolatedStorageSettingHelper.GetIsolateStorageByObj("EncryptKey") as byte[];
            ICryptoTransform deEncryptTransform = tripleDesProvider.CreateDecryptor(encryptKey, ivKey);
            var DecryptBytes = deEncryptTransform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);
            string unDecryptFomatStr = Encoding.UTF8.GetString(DecryptBytes, 0, DecryptBytes.Length);

            return unDecryptFomatStr;          
        }
    }
}
