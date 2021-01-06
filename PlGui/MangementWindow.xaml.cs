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
        private List<BusControl> allBusControls { get; set; }
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
            BL.BLImp myBLImp = new BL.BLImp();
            switch (selectedView)
            {
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
            }
            updateList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedView)
            {
                case selected.busDis:
                    break;
                case selected.lineDis:
                    AddOrUpdateLine newWin = new AddOrUpdateLine(null);
                    newWin.ShowDialog();
                    break;
                case selected.stationDis:
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
            myLBI.Items.Clear();
            switch (selectedView)
            {
                case selected.busDis:
                    foreach (BusControl _busControl in allBusControls)
                        myLBI.Items.Add(_busControl);
                    break;
                case selected.lineDis:
                    foreach (LineControl _lineControl in allLineControls)
                        myLBI.Items.Add(_lineControl);
                    break;
                case selected.stationDis:
                    foreach (StationControl _stationControl in allStationControls)
                        myLBI.Items.Add(_stationControl);
                    break;
                default:
                    break;
            }
        }
    }
}
