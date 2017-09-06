
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Networking.Proximity;
using System;
using Windows.UI.Core;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;


namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProximityDeviceScenario : Page
    {
        // A pointer back to the main page.  This is needed if you want to call methods in MainPage such
        // as NotifyUser()
        MainPage rootPage = MainPage.Current;
        private Windows.Networking.Proximity.ProximityDevice _proximityDevice;
        private long _publishedMessageId = -1;
        private long _subscribedMessageId = -1;

        public ProximityDeviceScenario()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;

            _proximityDevice = ProximityDevice.GetDefault();
            if (_proximityDevice != null)
            {
                // This scenario demonstrates using publish/subscribe in order to publish a message from on PC to the other using
                // the proximity infrastructure.
                // For example, a PC device can publish a contact card or a photo url which can be then used by a PC that 
                // subscribed to the message to identify the device involved in the proximity "tap".
                ProximityDevice_PublishMessageButton.Click += new RoutedEventHandler(ProximityDevice_PublishMessage);
                ProximityDevice_SubscribeForMessageButton.Click += new RoutedEventHandler(ProximityDevice_SubscribeForMessage);
                ProximityDevice_PublishMessageButton.Visibility = Visibility.Visible;
                ProximityDevice_SubscribeForMessageButton.Visibility = Visibility.Visible;
                ProximityDevice_PublishMessageText.Visibility = Visibility.Visible;
            }

            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ChangeVisualState(rootPage.ActualWidth);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProximityDevice_PublishMessageText.Text = "";
            if (_proximityDevice == null)
            {
                rootPage.NotifyUser("No proximity device found", NotifyType.ErrorMessage);
            }
            ProximityDeviceOutputText.Text = "";
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (_proximityDevice != null)
            {
                if (_publishedMessageId != -1)
                {
                    _proximityDevice.StopPublishingMessage(_publishedMessageId);
                    _publishedMessageId = -1;
                }
                if (_subscribedMessageId != -1)
                {
                    _proximityDevice.StopSubscribingForMessage(_subscribedMessageId);
                    _subscribedMessageId = 1;
                }
            }
        }

        void ProximityDevice_PublishMessage(object sender, RoutedEventArgs e)
        {
            if (_publishedMessageId == -1)
            {
                rootPage.NotifyUser("", NotifyType.ErrorMessage);
                String publishText = ProximityDevice_PublishMessageText.Text;
                ProximityDevice_PublishMessageText.Text = ""; // clear the input after publishing.
                if (publishText.Length > 0)
                {
                    _publishedMessageId = _proximityDevice.PublishMessage("Windows.SampleMessageType", publishText);
                    rootPage.NotifyUser("Tap with another device to transmit.", NotifyType.StatusMessage);
                    ProximityDeviceOutputText.Text += "Published: " + publishText + "\n";
                }
                else
                {
                    rootPage.NotifyUser("Please enter the amount", NotifyType.ErrorMessage);
                }
            }
            else
            {
                rootPage.NotifyUser("This only supports transfering one transaction at a time.", NotifyType.ErrorMessage);
            }
        }

        void MessageReceived(ProximityDevice proximityDevice, ProximityMessage message)
        {
            var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                ProximityDeviceOutputText.Text += "Message received: " + message.DataAsString + "\n";

            });
        }

        private async Task WriteToFileAsync()
        {
            var fileBytes = Encoding.UTF8.GetBytes(ProximityDevice_PublishMessageText.Text.ToCharArray());
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dataFolder = await localFolder.CreateFolderAsync("DataFolder", CreationCollisionOption.OpenIfExists);
            var file = await dataFolder.CreateFileAsync("DataFile.txt", CreationCollisionOption.ReplaceExisting);


            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }

        }

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            ChangeVisualState(e.Size.Width);
        }

        void ChangeVisualState(double width)
        {
            if (width < 768)
            {
                VisualStateManager.GoToState(this, "Below768Layout", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "DefaultLayout", false);
            }
        }

        void ProximityDevice_SubscribeForMessage(object sender, RoutedEventArgs e)
        {
            if (_subscribedMessageId == -1)
            {
                _subscribedMessageId = _proximityDevice.SubscribeForMessage("Windows.SampleMessageType", MessageReceived);
                rootPage.NotifyUser("Subscribed for proximity pay, enter proximity to receive.", NotifyType.StatusMessage);
            }
            else
            {
                rootPage.NotifyUser("This only supports transfering one transaction at a time.", NotifyType.ErrorMessage);
            }
        }

        private void homepage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1),null);
        }

        private async void ProximityDevice_PublishMessageButton_Click(object sender, RoutedEventArgs e)
        {
            await WriteToFileAsync(); 
        }

      
    }
}
