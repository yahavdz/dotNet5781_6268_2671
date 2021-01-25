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
            lineNum.Content = currentLine.Id.ToString();
            foreach (Station s in currentLine.stations)
                allStationControls.Add(new StationControl(s));
            foreach (StationControl sc in allStationControls)
                liststation.Items.Add(sc);
            lineTime.Content = currentLine.TotalTime.ToString();
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
            try
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

                LineStation newLineStation = new LineStation();
                newLineStation.Code = sc.currentStation.Code;
                newLineStation.Active = sc.currentStation.Active;
                newLineStation.Address = sc.currentStation.Address;
                newLineStation.Accessibility = sc.currentStation.Accessibility;
                newLineStation.Latitude = sc.currentStation.Latitude;
                newLineStation.Longitude = sc.currentStation.Longitude;
                newLineStation.Name = sc.currentStation.Name;
                newLineStation.DistanceToNextStation = Math.Sqrt(Math.Pow(newLineStation.Latitude * 110.574 - sc.currentStation.Latitude * 110.574, 2) + Math.Pow(newLineStation.Longitude * 111.320 * Math.Cos(sc.currentStation.Latitude) - sc.currentStation.Longitude * 111.320 * Math.Cos(newLineStation.Latitude), 2) * 1.0);


                // change the GUI after adding the station:
                if (index == -1)
                {
                    bl.AddStationToLine(currentLine, newLineStation, parent.Items.Count - 1);
                    parent.Items.Add(sc);
                }
                else
                {
                    bl.AddStationToLine(currentLine, newLineStation, index);
                    parent.Items.Insert(index, sc);
                }
                currentLine = bl.GetLine(currentLine.Id); // update the current line with the new station and total time
                lineTime.Content = currentLine.TotalTime.ToString();
            }
            catch (BO.DuplicateException)
            {
                MessageBoxResult popUp = MessageBox.Show("The station is alredy exist", "ERROR",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                
            }
        }



        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            daletButton.Opacity = 0.6;
            if (liststation.SelectedItem != null)
            {
                MessageBoxResult popUp = MessageBox.Show("Are you sure you want to delete from the line?", "Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.Yes)
                {
                    Station delS = (liststation.SelectedItem as StationControl).currentStation;
                    allStationControls.Remove(liststation.SelectedItem as StationControl);
                    bl.DeleteStationFromLine(currentLine, currentLine.stations.FirstOrDefault(ls => ls.Code == delS.Code));
                }

            }
            liststation.SelectedItem = null;
            liststation.Items.Clear();
            foreach (StationControl sc in allStationControls)
                liststation.Items.Add(sc);
            currentLine = bl.GetLine(currentLine.Id); // update the current line with the new station and total time
            lineTime.Content = currentLine.TotalTime.ToString();
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



