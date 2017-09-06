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
using Windows.UI.Core;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class history : Page
    {
        public history()
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
        private async void btnRead_Click(object sender, RoutedEventArgs e)
        {
        await ReadToFileAsync();
         
        }

        private async Task ReadToFileAsync()
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (localFolder != null)
            {
                var dataFolder = await localFolder.GetFolderAsync("DataFolder");
                var file = await dataFolder.OpenStreamForReadAsync("DataFile.txt");

                using (var streamReader = new StreamReader(file))
                {
                    txtBoxRead.Text ="Rs." + streamReader.ReadToEnd() + " sent to ACJP114";
                }

            }
        }
    }
}
