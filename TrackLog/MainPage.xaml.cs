using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TrackLog.Resources;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Networking.NetworkOperators;
using System.IO.IsolatedStorage;
using System.IO;
using System.Globalization;
using Microsoft.Live;

namespace TrackLog
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {

            InitializeComponent();
            
            //SmsInterceptor.SmsReceived += SmsInterceptor_SmsReceived;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void SmsInterceptor_SmsReceived(object sender, object e)
        {
            speak();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            speak();
        }

        private async void speak()
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            await ss.SpeakTextAsync(TextBox1.Text);
            App.diagLog("Spoke " + TextBox1.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = App.read(App.DIAG_LOG);
            TextBlock1.Text += App.read(App.LOC_LOG);
        }

        
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //ProgressIndicator progressIndicator = new ProgressIndicator();
            //progressIndicator.IsIndeterminate = true;
            //progressIndicator.Text = "Uploading " + App.DIAG_LOG;
            TextBlock1.Text = await App.upload(App.DIAG_LOG);
            //progressIndicator.Text = "Uploading " + App.LOC_LOG;
            TextBlock1.Text += await App.upload(App.LOC_LOG);
            //progressIndicator.IsVisible = false;
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}