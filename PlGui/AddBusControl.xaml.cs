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
    enum addOrUpdate { add, update};
    /// <summary>
    /// Interaction logic for AddBusControl.xaml
    /// </summary>
    public partial class AddBusControl : UserControl
    {
        private ListBox allBusC { get; set; }
        private addOrUpdate myStatos { get; set; }
        private IBL bl { get; set; }
        private BusControl SelectedBC { get; set; }
        private List<BusControl> allBusControls { get; set; }
        public AddBusControl(BusControl _SelectedBC, List<BusControl> _allBusControls, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            allBusControls = _allBusControls;
            SelectedBC = _SelectedBC;
            if (SelectedBC != null)
            {
                myStatos = addOrUpdate.update;
                newBusId.Text = SelectedBC.currentBus.LicenseNum.ToString();
                newBusId.IsReadOnly = true;
                newTotalKilometers.Text = SelectedBC.currentBus.TotalTrip.ToString();
                newTotalFuel.Text = SelectedBC.currentBus.FuelRemain.ToString();
                newLastTreatment.Text = SelectedBC.currentBus.KilometersSinceLastTreatment.ToString();
                newActivityStart.SelectedDate = SelectedBC.currentBus.FromDate;
            }
            else
                myStatos = addOrUpdate.add;

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Bus newBus;
            if (SelectedBC != null)
                newBus = SelectedBC.currentBus;
            else
                newBus = new Bus();
            newBus.FromDate = newActivityStart.DisplayDate;
            try { newBus.LicenseNum = Int32.Parse(newBusId.Text); }
            catch(FormatException) { newBusId.Text = "*Invalid number*"; Succeeded = false; }
            try { newBus.TotalTrip = Int32.Parse(newTotalKilometers.Text); }
            catch (FormatException) { newTotalKilometers.Text = "*Invalid number*"; Succeeded = false; }
            try { newBus.KilometersSinceLastTreatment = Int32.Parse(newLastTreatment.Text); }
            catch (FormatException) { newLastTreatment.Text = "*Invalid number*"; Succeeded = false; }
            try { newBus.FuelRemain = Int32.Parse(newTotalFuel.Text);
                newBus.KilometersAtLastRefueling = newBus.TotalTrip - newBus.FuelRemain; }
            catch (FormatException) { newTotalFuel.Text = "*Invalid number*"; Succeeded = false; }

            try
            {
                if (myStatos == addOrUpdate.add)
                    bl.AddBus(newBus);
                else
                    bl.UpdateBus(newBus);
            }
            catch (BadIdException ex)
            {
                newBusId.Text = ex.Message;
                Succeeded = false;
            }
            catch (BadTimeException ex)
            {
                Indication.Content = ex.Message;
                Succeeded = false;
            }
            catch (BadTotalTripException ex)
            {
                newTotalKilometers.Text = ex.Message;
                Succeeded = false;
            }
            catch (BadFuelException ex)
            {
                newTotalFuel.Text = ex.Message;
                Succeeded = false;
            }
            catch (BadKilometersException ex)
            {
                Indication.Content = ex.Message;
                Succeeded = false;
            }

            if (Succeeded)
            {
                newBusId.Text = "";
                newTotalKilometers.Text = "";
                newTotalFuel.Text = "";
                newLastTreatment.Text = "";
                newActivityStart.SelectedDate = null;
                if (myStatos == addOrUpdate.add)
                {
                    Indication.Content = "Added successfully";
                    allBusControls.Add(new BusControl(newBus));
                }
                else
                {
                    Indication.Content = "Updated successfully";
                    SelectedBC.Refresh();
                }
                Indication.Foreground = Brushes.Lime;
                
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
