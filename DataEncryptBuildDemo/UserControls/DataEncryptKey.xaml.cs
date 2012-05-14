using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.IO;
using System.IO.IsolatedStorage;

namespace DataEncryptBuildDemo.UserControls
{
    public partial class DataEncryptKey : UserControl
    {
        public DataEncryptKey()
        {
            InitializeComponent();
        }

        private void CancelEncryptKey_BT_Click(object sender, RoutedEventArgs e)
        {
            this.CloseMeAsPopup();
        }

        private void SetEncryptKey_BT_Click(object sender, RoutedEventArgs e)
        {
            string hmacEncryptKey=this.DataEncryptKey_TB.Text.Trim();
            if (string.IsNullOrEmpty(hmacEncryptKey))
            {
                MessageBox.Show("Please Setting the HAMC Md5 Encrypt Key?", "Confirm Input", MessageBoxButton.OK);
                this.DataEncryptKey_TB.Focus();
            }
            else
            {
                IsolatedStorageCommon.IsolatedStorageSettingHelper.AddIsolateStorageObj("HMACMD5Key", hmacEncryptKey);
                this.CloseMeAsPopup();
            }
        }
    }
}
