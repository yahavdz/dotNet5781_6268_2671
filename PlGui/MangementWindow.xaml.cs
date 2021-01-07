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
using BL.BO;
using Line = BL.BO.Line;

namespace PlGui
{
    public enum selected { busDis, lineDis, stationDis }
    /// <summary>
    /// Interaction logic for MangementWindow.xaml
    /// </summary>
    public partial class MangementWindow : Window
    {
        private selected selectedView { get; set; }
        public List<BusControl> allBusControls { get; set; }
        private List<LineControl> allLineControls { get; set; }
        private List<StationControl> allStationControls { get; set; }
        public MangementWindow()
        {
            InitializeComponent();
        }

        private void busButton_Click(object sender, RoutedEventArgs e)
        {
            //if(allBusControls.Count == 0)
            //    foreach (Bus _bus in Bl.GetAllBuses())
            //        allBusControls.Add(new BusControl(_bus));
            selectedView = selected.busDis;
            updateList();
        }

        private void lineButton_Click(object sender, RoutedEventArgs e)
        {
            //if (allLineControls.Count == 0)
            //    foreach (Line _line in Bl.GetAllLines())
            //        allLineControls.Add(new LineControl(_line));
            selectedView = selected.lineDis;
            updateList();
        }

        private void stationButton_Click(object sender, RoutedEventArgs e)
        {
            //if (allStationControls.Count == 0)
            //    foreach (Station _station in Bl.GetAllStations())
            //        allStationControls.Add(new StationControl(_station));
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
            //myLBI.Items.Clear();
            //switch (selectedView)
            //{
            //    case selected.busDis:
            //        foreach (BusControl _busControl in allBusControls)
            //            myLBI.Items.Add(_busControl);
            //        break;
            //    case selected.lineDis:
            //        foreach (LineControl _lineControl in allLineControls)
            //            myLBI.Items.Add(_lineControl);
            //        break;
            //    case selected.stationDis:
            //        foreach (StationControl _stationControl in allStationControls)
            //            myLBI.Items.Add(_stationControl);
            //        break;
            //    default:
            //        break;
            //}
        }

        private void busDis_Click(object sender, RoutedEventArgs e)
        {
            if (selectedView != selected.busDis)
                allItems.Items.Clear();
            selectedView = selected.busDis;
            Bus temp = new Bus();
            temp.LicenseNum = 12345678;
            temp.TotalTrip = 15000;
            temp.FuelRemain = 980;
            allItems.Items.Add(new BusControl(temp));
        }

        private void lineDis_Click(object sender, RoutedEventArgs e)
        {
            if (selectedView != selected.lineDis)
                allItems.Items.Clear();
            selectedView = selected.lineDis;
            Line temp = new Line();
            temp.LineId = 466;
            temp.Area = Areas.Jerusalem;
            allItems.Items.Add(new LineControl(temp));
        }

        private void stationDis_Click(object sender, RoutedEventArgs e)
        {
            if (selectedView != selected.stationDis)
                allItems.Items.Clear();
            selectedView = selected.stationDis;
            Station temp = new Station();
            temp.Name = "Dolev st. - Habrosh st.";
            temp.Code = 56789;
            allItems.Items.Add(new StationControl(temp));
        }
    }
}
