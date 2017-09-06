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
    public sealed partial class change_password : Page
    {
        public change_password()
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

        private void chng_pwd_Click(object sender, RoutedEventArgs e)
        {
            var old = old_pin.Text;
            var new1 = new_pin1.Text;
            var new2 = new_pin2.Text;
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var old_get = (string)localSettings.Values["PIN"];

            if (old_get == old)
            {
                if (new1 == new2)
                {
                    localSettings.Values["PIN"] = new1;
                    chng_pwd.IsEnabled = false;
                    chng_text.Text = "Password Successfully Changed!";
                }
                else
                {
                    chng_text.Text = "The two passwords donot match!";
                }
            }
            else
            {
                chng_text.Text = "Wrong Password!";
            }
           
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1),null);
        }
    }
}
