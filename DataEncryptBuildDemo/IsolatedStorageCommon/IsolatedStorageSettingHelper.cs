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

using System.IO.IsolatedStorage;
namespace DataEncryptBuildDemo.IsolatedStorageCommon
{
    public class IsolatedStorageSettingHelper
    {
        /// <summary>
        /// Add IsolateStorage Setting
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Is Add</returns>
        public static bool AddIsolateStorageObj(string key, object value)
        {
            bool isAdd = false;
            if (!string.IsNullOrEmpty(key) && value != null)
            {
                IsolatedStorageSettings.ApplicationSettings[key] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
                isAdd = true;
            }
            return isAdd;
        }


        /// <summary>
        /// Get IsolateStorage Save Value To String Format
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>String Format</returns>
        public static string GetIsolateStorageByStr(string key)
        {
            string storeStr = string.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                if (IsolateStorageKeyIsExist(key))
                    storeStr = IsolatedStorageSettings.ApplicationSettings[key].ToString();
            }
            return storeStr;
        }


        /// <summary>
        /// Get IsolateStorage Save Value To Object Format
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>object Format</returns>
        public static object GetIsolateStorageByObj(string key)
        {
            object storeObj = null;
            if (!string.IsNullOrEmpty(key))
            {
                if (IsolateStorageKeyIsExist(key))
                    storeObj = IsolatedStorageSettings.ApplicationSettings[key];
            }
            return storeObj;
        }


        /// <summary>
        /// IsolateStorage Is Exists
        /// </summary>
        /// <param name="key">Storage Key</param>
        /// <returns>Is Exists</returns>
        public static bool IsolateStorageKeyIsExist(string key)
        {
            bool isExist = false;
            if (!string.IsNullOrEmpty(key))
            {
                foreach (string currentkey in IsolatedStorageSettings.ApplicationSettings.Keys)
                {
                    if (currentkey.Equals(key))
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            return isExist;
        }


        /// <summary>
        /// Remove The IsolateStorage KeyValuePair
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Is Remove</returns>
        public static bool RemoveIsolateStorageByKey(string key)
        {
            bool IsRemove = false;
            if (!string.IsNullOrEmpty(key))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(key);
                IsolatedStorageSettings.ApplicationSettings.Save();
                IsRemove = true;
            }
            return IsRemove;
        }
    }
}

