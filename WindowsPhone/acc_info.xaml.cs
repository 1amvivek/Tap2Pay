using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class acc_info : Page
    {
        public acc_info()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var settingName = txtSettingName.Text;
            var settingAccNum = txtSettingAccNum.Text;
            var settingPIN = txtSettingPIN.Text;
            var settingDOB = txtSettingDOB.Text;
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Name"] = settingName;
            localSettings.Values["AccNum"] = settingAccNum;
            localSettings.Values["DOB"] = settingDOB;
            localSettings.Values["PIN"] = settingPIN;
            add.IsEnabled = false;
            message.Text= "Added Successfully!";
           
        }

      

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSettingName.Text = string.Empty;
            txtSettingAccNum.Text = string.Empty;
            txtSettingDOB.Text = string.Empty;
            txtSettingPIN.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1),null);
        }
    }
}
