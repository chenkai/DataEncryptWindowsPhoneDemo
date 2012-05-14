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
using Microsoft.Phone.Controls;

using DataEncryptBuildDemo.DataEncryptCommon;
namespace DataEncryptBuildDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();          
        }      

        private void DataEncryptOperator_BT_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateUserInput())
            {
                //Control UIElement Status
                this.NeedDataEncryptData_TB.IsEnabled = false;
                this.DataEncryptType_LP.IsEnabled = false;
                this.EncryptData_TB.IsEnabled = false;

                //Get Control EncryptData 
                string inputNeedEncryptStr = this.NeedDataEncryptData_TB.Text.Trim();
                string dataEncryptOperator = (this.DataEncryptType_LP.SelectedItem as ListPickerItem).Content as string;
               
                //DataEncrypt operator
                switch (dataEncryptOperator)
                {
                    case"MD5":
                        string encryptDataStr=DataEncryptCommon.DataEncryptHelper.ExcuteDataEncrypt(inputNeedEncryptStr, string.Empty, DataEncryptType.MD5);
                        if(!string.IsNullOrEmpty(encryptDataStr))
                            this.EncryptData_TB.Text = encryptDataStr;
                        break;
                    case"HMAC_MD5":
                        
                        //string encryptDataStr=DataEncryptCommon.DataEncryptHelper.ExcuteDataEncrypt(inputNeedEncryptStr
                        break;
                    case"DES":
                        break;
                    default:
                        break;
                }

            }
        }

        public bool ValidateUserInput()
        {
            bool isValidate = true;
            string inputNeedToEncryptStr = this.NeedDataEncryptData_TB.Text.Trim();
            if (string.IsNullOrEmpty(inputNeedToEncryptStr))
            {
                MessageBox.Show("Please input the Data you want to Encrypt ?", "Confirm Input", MessageBoxButton.OK);
                this.NeedDataEncryptData_TB.Focus();
                isValidate = false;
            }
            return isValidate;
        }

        private void DataEncryptType_LP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataEncryptType_LP == null)
                return;

            string dataEncryptOperator = (this.DataEncryptType_LP.SelectedItem as ListPickerItem).Content as string;
            if (string.IsNullOrEmpty(dataEncryptOperator))
                return;
            if(dataEncryptOperator.Equals("HMAC_MD5"))
                this.EncryptKey_SP.Visibility = Visibility.Visible;
        }
    }
}