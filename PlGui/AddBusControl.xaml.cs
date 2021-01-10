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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddBusControl.xaml
    /// </summary>
    public partial class AddBusControl : UserControl
    {
        private ListBox allBusC { get; set; }
        public AddBusControl(ListBox _allBusC)
        {
            InitializeComponent();
            allBusC = _allBusC;
            BusControl SelectedBC = allBusC.SelectedItem as BusControl;
            if(SelectedBC != null)
            {
                newBusId.Text = SelectedBC.currentBus.LicenseNum.ToString(); ;
                newTotalKilometers.Text = SelectedBC.currentBus.TotalTrip.ToString();
                newTotalFuel.Text = SelectedBC.currentBus.FuelRemain.ToString();
                newLastTreatment.Text = SelectedBC.currentBus.KilometersSinceLastTreatment.ToString();
                newActivityStart.SelectedDate = SelectedBC.currentBus.FromDate;
            }

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Bus newBus = new Bus();
            newBus.FromDate = newActivityStart.DisplayDate;
            try { newBus.LicenseNum = Int32.Parse(newBusId.Text); }
            catch(FormatException) { newBusId.Text = "*Invalid number*"; Succeeded = false; }
            try { newBus.TotalTrip = Int32.Parse(newTotalKilometers.Text); }
            catch (FormatException) { newTotalKilometers.Text = "*Invalid number*"; Succeeded = false; }
            try { newBus.KilometersSinceLastTreatment = Int32.Parse(newLastTreatment.Text); }
            catch (FormatException) { newLastTreatment.Text = "*Invalid number*"; Succeeded = false; }
            try { newBus.FuelRemain = Int32.Parse(newTotalFuel.Text); }
            catch (FormatException) { newTotalFuel.Text = "*Invalid number*"; Succeeded = false; }

            //try { bl.AddBus(newBus); }
            //catch (BadIdException e)
            //{
            //    newBusId.Text = e.Message;
            //    Succeeded = false;
            //}
            //catch (BadTimeException e)
            //{
            //    Indication.Content = e.Message;
            //    Succeeded = false;
            //}
            //catch (BadTotalTripException e)
            //{
            //    newTotalKilometers.Text = e.Message;
            //    Succeeded = false;
            //}
            //catch (BadFuelException e)
            //{
            //    newTotalFuel.Text = e.Message;
            //    Succeeded = false;
            //}
            //catch (BadKilometersException e)
            //{
            //    Indication.Content = e.Message;
            //    Succeeded = false;
            //}

            if (Succeeded)
            {
                newBusId.Text = "";
                newTotalKilometers.Text = "";
                newTotalFuel.Text = "";
                newLastTreatment.Text = "";
                newActivityStart.SelectedDate = null;
                Indication.Content = "Succeeded";
                Indication.Foreground = Brushes.Lime;
                allBusC.Items.Add(new BusControl(newBus));
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
