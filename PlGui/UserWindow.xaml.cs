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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;
using BO;
using BLApi;
using Line = BO.Line;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IBL bl;
        WatchState nowState = WatchState.Stop;
        private List<StationControl> allStationControls { get; set; }
        private int SelectedStation { get; set; }

        public UserWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            SelectedStation = -1;
            allStationControls = new List<StationControl>();
            foreach (Station _station in bl.GetAllStations())
                allStationControls.Add(new StationControl(_station));

            foreach (StationControl _SC in allStationControls)
                allItems.Items.Add(_SC);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        
        public void updateTime(TimeSpan newTS)
        {
            if (newTS.Seconds < 10)
                secTB.Text = "0" + newTS.Seconds.ToString();
            else
                secTB.Text = newTS.Seconds.ToString();
            if(newTS.Minutes < 10)
                minTB.Text = "0" + newTS.Minutes.ToString();
            else
                minTB.Text = newTS.Minutes.ToString();
            if(newTS.Hours < 10)
                hourTB.Text = "0" + newTS.Hours.ToString();
            else
                hourTB.Text = newTS.Hours.ToString();
        }

        public void updateBus(List<LineTiming> linesInSta)
        {
            panelList.Items.Clear();    
            foreach (LineTiming lt in linesInSta)
                panelList.Items.Add(new PanelControl(lt.LineNum, lt.StationName, lt.ArrivalTime.Minutes));
            if (linesInSta.Count == 0)
                panelList.Items.Add(new PanelControl(-1, "No lines coming soon", -1));
        }

        private void startOrStop_Click(object sender, RoutedEventArgs e)
        {
            if (nowState == WatchState.Stop)
            {
                int hour = Convert.ToInt32(hourTB.Text);
                int min = Convert.ToInt32(minTB.Text);
                int sec = Convert.ToInt32(secTB.Text);
                int timeSpeed = Convert.ToInt32(speedTB.Text);
                if (sec > 59 || min > 59 || hour > 23)
                    MessageBox.Show("Invalid time", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (timeSpeed == 0)
                    MessageBox.Show("Invalid speed", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    startOrStop.Background = Brushes.Tomato;
                    Action<TimeSpan> doUpdateTime = updateTime;
                    startOrStop.Content = "Stop";
                    bl.StartSimulator(new TimeSpan(hour, min, sec), timeSpeed, doUpdateTime);
                    nowState = WatchState.Start;
                    hourTB.IsReadOnly = true;
                    minTB.IsReadOnly = true;
                    secTB.IsReadOnly = true;
                    speedTB.IsReadOnly = true;
                }
            }
            else if (nowState == WatchState.Start)
            {
                startOrStop.Background = Brushes.LightGreen;
                bl.StopSimulator();                
                startOrStop.Content = "Start";
                panelList.Items.Clear();
                nowState = WatchState.Stop;
                hourTB.IsReadOnly = false;
                minTB.IsReadOnly = false;
                secTB.IsReadOnly = false;
                speedTB.IsReadOnly = false;
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            panelList.Items.Clear();
            linesInStaLB.Items.Clear();
            StationControl SelectedSC = allItems.SelectedItem as StationControl;
            SelectedStation = SelectedSC.currentStation.Code;
            Station s = bl.GetStations(SelectedStation);
            Action<List<LineTiming>> doUpdateBus = updateBus;
            bl.SetStationPanel(SelectedStation, doUpdateBus);

            staNameLa.Content = s.Name.Substring(1);
            staCodeLa.Content = s.Code;
            foreach (Line l in bl.GetAllLinesBy(_l => _l.stations.FirstOrDefault(_s => _s.Code == s.Code) != null))
                linesInStaLB.Items.Add(new SignControl(l));

            stationCodeDe.Content = s.Code.ToString();
            nameDe.Content = s.Name.ToString();
            addressDe.Content = s.Address.ToString();
            longitudeDe.Content = s.Longitude.ToString() + "°E";
            latitudeDe.Content = s.Latitude.ToString() + "°N";
            AccessibleDe.Content = s.Accessibility.ToString();
        }
    }
}
