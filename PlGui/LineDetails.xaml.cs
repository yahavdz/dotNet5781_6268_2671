using BLApi;
using BO;
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
using Line = BO.Line;


namespace PlGui
{
    /// <summary>
    /// Interaction logic for LineDetails.xaml
    /// </summary>
    public partial class LineDetails : UserControl
    {

        public Line currentLine { get; set; }
        private List<StationControl> allStationControls { get; set; }
        private List<LineControl> allLineControls { get; set; }

        private IBL bl { get; set; }

        public LineDetails(Line _line, IBL _bl)
        {
            InitializeComponent();
            currentLine = _line;
            bl = _bl;
            allStationControls = new List<StationControl>();
            allLineControls = new List<LineControl>();

            Refresh();
        }

        public void Refresh()
        {
            TimeSpan lt = new TimeSpan();
            lineNum.Content = currentLine.Id.ToString();
            foreach (LineStation ls in currentLine.stations)
            {
                lt += ls.TimeToNextStation;
            }
            foreach (Station s in currentLine.stations)
                allStationControls.Add(new StationControl(s));
            foreach (StationControl sc in allStationControls)
                liststation.Items.Add(sc);
            lineTime.Content = lt.ToString();


        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            // e.Data.GetData()
            object data = e.Data.GetData(typeof(StationControl));
            // ((IList)dragSource.ItemsSource).Remove(data);
            StationControl sc = new StationControl((data as StationControl).currentStation);
            parent.Items.Add(sc);
            LineStation ls = new LineStation();
            ls.Code = sc.currentStation.Code;
            ls.Active = sc.currentStation.Active;
            ls.Address = sc.currentStation.Address;
            ls.Accessibility = sc.currentStation.Accessibility;
            //TODO ls.DistanceToNextStation



            bl.AddStationToLine(currentLine, ls);
        }



        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            daletButton.Opacity = 0.6;
            if (liststation.SelectedItem != null)
            {
                MessageBoxResult popUp = MessageBox.Show("Are you sure you want to delete?", "Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.Yes)
                {

                    StationControl SelectedSC = liststation.SelectedItem as StationControl;
                    bl.DeleteStation(SelectedSC.currentStation.Code);
                    allStationControls.Remove(SelectedSC);

                }
                
            }
            liststation.SelectedItem = null;
            liststation.Items.Clear();
            foreach (StationControl sc in allStationControls)
                liststation.Items.Add(sc);
        }
    }
}



