using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_6268_4032;

namespace dotNet5781_03A_6268_4032
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BusCollection BusCo { get; set; }
        private BusLine currentDisplayBusLine;//A private field that will keep the current line displayed.

        public MainWindow()
        {
            InitializeComponent();
            BusCo = new BusCollection();
            Random random = new Random();
            for (int i = 0; i < 10; i++)//A loop that aims to insert stations, random distance, random time, station address, station ID number into a bus line and insert the bus line into the collection
            {
                List<BusLineStation> stations = new List<BusLineStation>();
                for (int j = 0; j < 5; j++)
                {
                    int k;
                    bool isStationAlreadyExist;
                    do// do while to check if the key code is already exist
                    {
                        k = random.Next(1, 999999);
                        isStationAlreadyExist = stations.Any(s => s.BusStationKey == k);
                    } while (isStationAlreadyExist);

                    BusLineStation bls = new BusLineStation(k, "even shmuel " + (k/1000).ToString());//insert to bus line a key and address
                    bls.Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0;//random latitude
                    bls.Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3;//random longitude
                    bls.TimeFromPreviousStation = random.Next(10,120);//random time between station
                    if (j == 0)
                        bls.DistanceFromPreviousStation = 0;
                    else
                        bls.DistanceFromPreviousStation = Math.Sqrt(Math.Pow(bls.Latitude * 110.574 - stations[j - 1].Latitude * 110.574, 2) + Math.Pow(bls.Longitude * 111.320 * Math.Cos(bls.Latitude) - stations[j - 1].Longitude * 111.320 * Math.Cos(bls.Latitude), 2) * 1.0);

                    stations.Add(bls);
                    k++;
                }
                int w;
                bool isLineAlreadyExist;
                do// do while to check if the line is already exist
                {
                    w = random.Next(1, 1000);
                    isLineAlreadyExist = BusCo.BusLines.Any(s => s.busLine == w);
                } while (isLineAlreadyExist);
                int area = random.Next(0, 4);// add a random area to bus lune
                BusLine line = new BusLine(stations, w, stations[0], stations[4], (Area)area);
                BusCo.addBus(line);

            }



            cbBusLines.ItemsSource = BusCo.BusLines;
            cbBusLines.DisplayMemberPath = " busLine ";
            cbBusLines.SelectedIndex = 0;
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)//When a change of selection event occurs in ComboBox there is a call to the method that displays the details of the selected line. Perform a casting-down to the selected value and retrieve the line number that will be sent to the method that displays the details.
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).busLine);
        }
        private void ShowBusLine(int index)//The line selected to be displayed is obtained from the collection we defined by the indexer. The DataContext component is updated in the top grid and in the ListBox.
        {
            currentDisplayBusLine = BusCo[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }

       
    }
}
