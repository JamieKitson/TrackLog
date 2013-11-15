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
using System.Threading.Tasks;

namespace TrackLog
{
    public partial class MainPage : PhoneApplicationPage
    {

        ProgressIndicator progress;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = App.tail(App.DIAG_LOG);
            TextBlock1.Text += "\n";
            TextBlock1.Text += App.tail(App.LOC_LOG);
        }

        private void statusUpdate(string status)
        {
            TextBlock1.Text += status + "\n";
            if (progress != null)
                progress.Text = status;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            progress = new ProgressIndicator();
            progress.IsVisible = true;
            progress.IsIndeterminate = true;
            progress.Text = "Uploading files...";

            SystemTray.SetProgressIndicator(this, progress);

            TextBlock1.Text = "";
            await App.uploadAll(new Progress<string>(statusUpdate));

            SystemTray.SetProgressIndicator(this, null);
        }
    }
}