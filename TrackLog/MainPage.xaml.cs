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
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using System.Device.Location;

namespace TrackLog
{
    public partial class MainPage : PhoneApplicationPage
    {

        ProgressIndicator progress;

        public MainPage()
        {
            try
            {
                InitializeComponent();
                currentDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                App.diagLog(ex.Message);
            }
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

        private void Pivot_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Map_Loaded_1(object sender, RoutedEventArgs e)
        {
        }

        private DateTime _currentDate;
        public DateTime currentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                curDate1.Text = value.ToString("yyyy-MM-dd");
            }
        }

        private void Pivot_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (Pivot1.SelectedIndex == 1)
            {
                drawPath(currentDate);
            }
        }

        private void drawPath(DateTime date)
        {
            Map1.MapElements.Clear();
            MapPolyline polygon = new MapPolyline();
            //polygon.StrokeColor = ;
            polygon.StrokeThickness = 3;
            polygon.Path = new GeoCoordinateCollection();
            List<string> coordinates = App.read(App.getFileName(App.LOC_LOG, date));
            double maxLat = -90;
            double maxLon = -180;
            double minLat = 90;
            double minLon = 180;
            foreach (string coord in coordinates)
            {
                string[] line = coord.Split(new string[] { App.DELIMITER }, StringSplitOptions.RemoveEmptyEntries);
                double lat;
                double lon;
                if (double.TryParse(line[1], out lat) && double.TryParse(line[2], out lon))
                {
                    maxLat = Math.Max(maxLat, lat);
                    maxLon = Math.Max(maxLon, lon);
                    minLat = Math.Min(minLat, lat);
                    minLon = Math.Min(minLon, lon);
                    polygon.Path.Add(new GeoCoordinate(lat, lon));
                }
            }
            if (polygon.Path.Count > 0)
            {
                // Map1.Center = new GeoCoordinate(minLat + ((maxLat - minLat) / 2), minLon, ((maxLon - minLon) / 2));
                Map1.MapElements.Add(polygon);
                //Map1.SetView(new LocationRectangle(maxLat, minLon, maxLat, maxLon));
                Map1.SetView(LocationRectangle.CreateBoundingRectangle(polygon.Path));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            changeDate(1);
        }

        private void changeDate(int diff)
        {
            currentDate = currentDate.AddDays(diff);
            drawPath(currentDate);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            changeDate(-1);
        }

        /*
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            for (int i = 12; i <= 14; i++)
            {
                string f = "loc2013-11-" + i + ".csv";
                List<string> ls = App.read(f);
                var file = store.OpenFile(f, System.IO.FileMode.Truncate, System.IO.FileAccess.Write);
                using (StreamWriter reader = new StreamWriter(file))
                {
                    foreach (string l in ls)
                    {
                        string nl = l.Replace(",", "");
                        string[] nls = nl.Split(' ');
                        if (Convert.ToDateTime(nls[0]) < Convert.ToDateTime("2013-11-14T23:56:57+00:00"))
                        {
                            string two = nls[2];
                            nls[2] = nls[1];
                            nls[1] = two;
                        }
                        nl = string.Join(App.DELIMITER, nls);
                        reader.WriteLine(nl);
                    }
                }
            }
            TextBlock1.Text += "\nDone\n";
        }
        */
    }
}