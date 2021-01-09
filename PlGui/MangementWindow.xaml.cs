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

namespace PlGui
{
    public enum selected { busDis, lineDis, stationDis }
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
        }

        private void busDis_Click(object sender, RoutedEventArgs e)
        {
            bool Continued = true;
            if(AddGrid.Width == 270)
            {
                MessageBoxResult popUp = MessageBox.Show("There are unsaved changes, are you sure you want to exit?", "ERROR",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes,
                MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                if (popUp == MessageBoxResult.No)
                    Continued = false;
                else
                    AddGrid.Width = 0.0;
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
            if (allLineControls.Count == 0)
                foreach (Line _line in bl.GetAllLines())
                    allLineControls.Add(new LineControl(_line));
            selectedView = selected.lineDis;
            updateList();
        }

        private void stationDis_Click(object sender, RoutedEventArgs e)
        {
            if (allStationControls.Count == 0)
                foreach (Station _station in bl.GetAllStations())
                    allStationControls.Add(new StationControl(_station));
            selectedView = selected.stationDis;
            updateList();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //BL.BLImp myBLImp = new BL.BLImp();
            //switch (selectedView)
            //{
            //case selected.busDis:
            //    BusControl SelectedBC = myLBI.SelectedItem as BusControl;
            //    myBLImp.DeleteBus(SelectedBC.currentBus.LicenseNum);
            //    allBusControls.Remove(SelectedBC);
            //    break;
            //case selected.lineDis:
            //    LineControl SelectedLC = myLBI.SelectedItem as LineControl;
            //    myBLImp.DeleteLine(SelectedLC.currentLine.LineId);
            //    allLineControls.Remove(SelectedLC);
            //    break;
            //case selected.stationDis:
            //    StationControl SelectedSC = myLBI.SelectedItem as StationControl;
            //    myBLImp.DeleteStation(SelectedSC.currentStation.code);
            //    allStationControls.Remove(SelectedSC);
            //    break;
            //default:
            //    break;
            //}
            updateList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
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
            //switch (selectedView)
            //{
            //    case selected.busDis:
            //        AddOrUpdateBus newBusWin = new AddOrUpdateBus((myLBI.SelectedItem as BusControl).currentBus);
            //        newBusWin.ShowDialog();
            //        break;
            //    case selected.lineDis:
            //        AddOrUpdateLine newLineWin = new AddOrUpdateLine((myLBI.SelectedItem as LineControl).currentLine);
            //        newLineWin.ShowDialog();
            //        break;
            //    case selected.stationDis:
            //        AddOrUpdateStation newStationWin = new AddOrUpdateStation((myLBI.SelectedItem as StationControl).currentStation);
            //        newStationWin.ShowDialog();
            //        break;
            //    default:
            //        break;
            //}
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
