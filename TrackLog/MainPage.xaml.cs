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

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = App.read(App.DIAG_LOG);
            TextBlock1.Text += App.read(App.LOC_LOG);
        }

        
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            /*
            TextBlock1.Text = await App.upload(App.DIAG_LOG);
            TextBlock1.Text += await App.upload(App.LOC_LOG);
            */
            TextBlock1.Text = "";
            TextBlock1.Text = await App.uploadAll(); 
        }
    }
}