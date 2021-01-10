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
using BO;
using BLApi;
using Line = BO.Line;
using System.Windows.Media.Animation;

namespace PlGui
{
    public enum selected { busDis, lineDis, stationDis, nullDis }
    /// <summary>
    /// Interaction logic for MangementWindow.xaml
    /// </summary>
    public partial class MangementWindow : Window
    {
        IBL bl;
        private selected selectedView { get; set; }
        public List<BusControl> allBusControls { get; set; }
        private List<LineControl> allLineControls { get; set; }
        private List<StationControl> allStationControls { get; set; }
        public MangementWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            allBusControls = new List<BusControl>();
            allLineControls = new List<LineControl>();
            allStationControls = new List<StationControl>();
            selectedView = selected.nullDis;
        }

        private void busDis_Click(object sender, RoutedEventArgs e)
        {
            bool Continued = true;
            if(AddGrid.Width == 270)
            {
                MessageBoxResult popUp = MessageBox.Show("There are unsaved changes, are you sure you want to exit?", "ERROR",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.No)
                    Continued = false;
                else
                {
                    Storyboard sb = this.FindResource("closeSB") as Storyboard;
                    sb.Begin();
                }
            }
            if (Continued)
            {
                if (allBusControls.Count == 0)
                    foreach (Bus _bus in bl.GetAllBuses())
                        allBusControls.Add(new BusControl(_bus));
                selectedView = selected.busDis;
                updateList();
            }
        }

        private void lineDis_Click(object sender, RoutedEventArgs e)
        {
            bool Continued = true;
            if (AddGrid.Width == 270)
            {
                MessageBoxResult popUp = MessageBox.Show("There are unsaved changes, are you sure you want to exit?", "ERROR",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.No)
                    Continued = false;
                else
                {
                    Storyboard sb = this.FindResource("closeSB") as Storyboard;
                    sb.Begin();
                }
            }
            if (Continued)
            {
                if (allLineControls.Count == 0)
                    foreach (Line _line in bl.GetAllLines())
                        allLineControls.Add(new LineControl(_line));
                selectedView = selected.lineDis;
                updateList();
            }
        }

        private void stationDis_Click(object sender, RoutedEventArgs e)
        {
            bool Continued = true;
            if (AddGrid.Width == 270)
            {
                MessageBoxResult popUp = MessageBox.Show("There are unsaved changes, are you sure you want to exit?", "ERROR",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.No)
                    Continued = false;
                else
                {
                    Storyboard sb = this.FindResource("closeSB") as Storyboard;
                    sb.Begin();
                }
            }
            if (Continued)
            {
                if (allStationControls.Count == 0)
                    foreach (Station _station in bl.GetAllStations())
                        allStationControls.Add(new StationControl(_station));
                selectedView = selected.stationDis;
                updateList();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (allItems.SelectedItem != null)
            {
                MessageBoxResult popUp = MessageBox.Show("Are you sure you want to delete?", "Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.Yes)
                {
                    switch (selectedView)
                    {
                        case selected.busDis:
                            BusControl SelectedBC = allItems.SelectedItem as BusControl;
                            bl.DeleteBus(SelectedBC.currentBus.LicenseNum);
                            allBusControls.Remove(SelectedBC);
                            break;
                        case selected.lineDis:
                            LineControl SelectedLC = allItems.SelectedItem as LineControl;
                            bl.DeleteLine(SelectedLC.currentLine.Id);
                            allLineControls.Remove(SelectedLC);
                            break;
                        case selected.stationDis:
                            StationControl SelectedSC = allItems.SelectedItem as StationControl;
                            bl.DeleteStation(SelectedSC.currentStation.Code);
                            allStationControls.Remove(SelectedSC);
                            break;
                        default:
                            break;
                    }
                    updateList();
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("goBigSB") as Storyboard;
            sb.Begin();
            allItems.SelectedItem = null;
            UserControl usc = null;
            USCgrid.Children.Clear();
            switch (selectedView)
            {
                case selected.busDis:
                    usc = new AddBusControl(allItems);
                    USCgrid.Children.Add(usc);
                    break;
                case selected.lineDis:
                    usc = new AddLineControl(allItems);
                    USCgrid.Children.Add(usc);
                    break;
                case selected.stationDis:
                    usc = new AddStationControl(allItems);
                    USCgrid.Children.Add(usc);
                    break;
                default:
                    break;
            }
            updateList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (allItems.SelectedItem != null)
            {
                Storyboard sb = this.FindResource("goBigSB") as Storyboard;
                sb.Begin();
                UserControl usc = null;
                USCgrid.Children.Clear();
                switch (selectedView)
                {
                    case selected.busDis:
                        usc = new AddBusControl(allItems);
                        USCgrid.Children.Add(usc);
                        break;
                    case selected.lineDis:
                        usc = new AddLineControl(allItems);
                        USCgrid.Children.Add(usc);
                        break;
                    case selected.stationDis:
                        usc = new AddStationControl(allItems);
                        USCgrid.Children.Add(usc);
                        break;
                    default:
                        break;
                }
                updateList();
            }
        }

        private void updateList()
        {
            allItems.Items.Clear();
            switch (selectedView)
            {
                case selected.busDis:
                    foreach (BusControl _busControl in allBusControls)
                        allItems.Items.Add(_busControl);
                    break;
                case selected.lineDis:
                    foreach (LineControl _lineControl in allLineControls)
                        allItems.Items.Add(_lineControl);
                    break;
                case selected.stationDis:
                    foreach (StationControl _stationControl in allStationControls)
                        allItems.Items.Add(_stationControl);
                    break;
                default:
                    break;
            }
        }

    }
}
