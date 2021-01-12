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
using BO;
using BLApi;
using System.Windows.Media.Animation;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddStationControl.xaml
    /// </summary>
    public partial class AddStationControl : UserControl
    {
        private addOrUpdate myStatos { get; set; }
        private IBL bl { get; set; }
        private StationControl SelectedSC { get; set; }
        private List<StationControl> allStationControls { get; set; }
        public AddStationControl(StationControl _SelectedSC, List<StationControl> _allStationControls, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            allStationControls = _allStationControls;
            SelectedSC = _SelectedSC;
            if (SelectedSC != null)
            {
                myStatos = addOrUpdate.update;
                newStaCode.Visibility = Visibility.Hidden;
                newStaCode.Text = SelectedSC.currentStation.Code.ToString();
                labelCo.Visibility = Visibility.Visible;
                labelCo.Content = SelectedSC.currentStation.Code.ToString();
                newStaAddress.Text = SelectedSC.currentStation.Address;
                newStaLatitude.Text = SelectedSC.currentStation.Latitude.ToString();
                newStaLongitude.Text = SelectedSC.currentStation.Longitude.ToString();
                newStaAccessibility.IsChecked = SelectedSC.currentStation.Accessibility;
                newStaName.Text = SelectedSC.currentStation.Name;
            }
            else
                myStatos = addOrUpdate.add;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Station newSta;
            if (SelectedSC != null)
                newSta = SelectedSC.currentStation;
            else
                newSta = new Station();
            try { newSta.Code = Int32.Parse(newStaCode.Text); }
            catch (FormatException) { newStaCode.Text = "*Invalid number*"; Succeeded = false; }
            newSta.Name = newStaName.Text;
            newSta.Address = newStaAddress.Text;
            newSta.Accessibility = (newStaAccessibility.IsChecked == true);
            try { newSta.Longitude = Convert.ToDouble(newStaLongitude.Text); }
            catch (FormatException) { newStaLongitude.Text = "*Invalid number*"; Succeeded = false; }
            try { newSta.Latitude = Convert.ToDouble(newStaLatitude.Text); }
            catch (FormatException) { newStaLatitude.Text = "*Invalid number*"; Succeeded = false; }

            try
            {
                if (myStatos == addOrUpdate.add)
                    bl.AddStation(newSta);
                else
                    bl.UpdateStation(newSta);
            }
            catch (BadIdException ex)
            {
                newStaCode.Text = ex.Message;
                Succeeded = false;
            }
            catch (BadAddressException ex)
            {
                newStaAddress.Text = "";
                Indication.Content = ex.Message;
                Succeeded = false;
            }
            catch (BadLongitudeLatitudeException ex)
            {
                Indication.Content = ex.Message;
                Succeeded = false;
            }

            if (Succeeded)
            {
                Indication.Content = "Succeeded";
                Indication.Foreground = Brushes.Lime;
                var grid = ((((sender as Button).Parent as Grid).Parent as AddStationControl).Parent as Grid).Parent as Grid;
                var window = ((((grid.Children[2] as Button).Parent as Grid).Parent as Grid).Parent as Grid).Parent;
                if (myStatos == addOrUpdate.add)
                {
                    allStationControls.Add(new StationControl(newSta));
                    (window as MangementWindow).updateList();
                    string msg = "Station " + newSta.Code + " successfully added";
                    MessageBox.Show(msg);
                }
                else
                {
                    SelectedSC.Refresh();
                    string msg = "Station " + newSta.Code + " has been successfully updated";
                    MessageBox.Show(msg);
                }
                Indication.Foreground = Brushes.Lime;
                ((window as Window).FindResource("closeSB") as Storyboard).Begin();
            }
            else
            {
                string temp = Indication.Content.ToString();
                if (temp == "")
                    Indication.Content = "Try again";
                Indication.Foreground = Brushes.Red;
            }
        }
    }
}
