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
            lineArea.Content = currentLine.Area.ToString();
        }

        private new bool IsMouseOver(StationControl item, DragEventArgs e)
        {
            // We need to use MouseUtilities to figure out the cursor
            // coordinates because, during a drag-drop operation, the WPF
            // mechanisms for getting the coordinates behave strangely.

            Rect bounds = VisualTreeHelper.GetDescendantBounds(item);
            Point mousePos = e.GetPosition(item);
            return bounds.Contains(mousePos);
        }
        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;

            int index = -1;
            int i = 0;
            foreach (StationControl item in parent.Items)
            {
                if (IsMouseOver(item, e))
                {
                    index = i;
                    break;
                }
                i++;
            }

            object data = e.Data.GetData(typeof(StationControl));
            StationControl sc = new StationControl((data as StationControl).currentStation);

            if (index == -1)
            {
                parent.Items.Add(sc);
                index = parent.Items.Count;
            }
            else
            {
                parent.Items.Insert(index, sc);
            }

            LineStation ls = new LineStation();
            ls.Code = sc.currentStation.Code;
            ls.Active = sc.currentStation.Active;
            ls.Address = sc.currentStation.Address;
            ls.Accessibility = sc.currentStation.Accessibility;
            ls.LineId = currentLine.Id;
            ls.Latitude = sc.currentStation.Latitude;
            ls.Longitude = sc.currentStation.Longitude;
            ls.Name = sc.currentStation.Name;

            //TODO ls.DistanceToNextStation
            bl.AddStationToLine(currentLine, ls, index);
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
public static class WpfDomHelper
{
    public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
    {

        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject == null) return null;

        T parent = parentObject as T;
        if (parent != null)
            return parent;
        else
            return FindParent<T>(parentObject);
    }
}



