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
using BL.BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddStationControl.xaml
    /// </summary>
    public partial class AddStationControl : UserControl
    {
        private ListBox allStationC { get; set; }
        public AddStationControl(ListBox _allStationC)
        {
            InitializeComponent();
            allStationC = _allStationC;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Station newSta = new Station();
            try { newSta.Code = Int32.Parse(newStaCode.Text); }
            catch (FormatException) { newStaCode.Text = "*Invalid number*"; Succeeded = false; }
            newSta.Name = newStaName.Text;
            newSta.Address = newStaAddress.Text;
            newSta.Accessibility = newStaAccessibility.IsEnabled;
            try { newSta.Longitude = Convert.ToDouble(newStaLongitude.Text); }
            catch (FormatException) { newStaLongitude.Text = "*Invalid number*"; Succeeded = false; }
            try { newSta.Latitude = Convert.ToDouble(newStaLatitude.Text); }
            catch (FormatException) { newStaLatitude.Text = "*Invalid number*"; Succeeded = false; }

            //try { bl.AddStation(newStation); }
            //catch (BadIdException e)
            //{
            //    newStaCode.Text = e.Message;
            //    Succeeded = false;
            //}
            //catch (BadAddressException e)
            //{
            //    newStaAddress.Text = "";
            //    Indication.Content = e.Message;
            //    Succeeded = false;
            //}
            //catch (BadLongitudeLatitudeException e)
            //{
            //    Indication.Content = e.Message;
            //    Succeeded = false;
            //}

            if (Succeeded)
            {
                newStaCode.Text = "";
                newStaName.Text = "";
                newStaAddress.Text = "";
                newStaLatitude.Text = "";
                newStaLongitude.Text = "";
                newStaAccessibility.IsChecked = null;
                Indication.Content = "Succeeded";
                Indication.Foreground = Brushes.Lime;
                allStationC.Items.Add(new StationControl(newSta));
            }
            else
            {
                string temp = Indication.Content.ToString();
                if (temp == "" || temp == "Succeeded")
                    Indication.Content = "Try again";
                Indication.Foreground = Brushes.Red;
            }
        }
    }
}
