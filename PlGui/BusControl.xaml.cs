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
    /// Interaction logic for BusControl.xaml
    /// </summary>
    public partial class BusControl : UserControl
    {
        public Bus currentBus { get; set; }
        public BusControl(Bus _bus)
        {
            InitializeComponent();
            currentBus = _bus;
            busId.Text = currentBus.LicenseNum.ToString();
            totalKilometers.Text = currentBus.TotalTrip.ToString() + "km";
            amountFuel.Text = currentBus.FuelRemain.ToString() + "km";
            activityStartDate.Text = currentBus.FromDate.ToString("dd - MMM - yy");
            //lastTreatmentDate.Text = currentBus.TreatmentDate.ToString("dd - MMM - yy");
            busStatus.Text = currentBus.BusStatus.ToString();
        }

        private void doFuel_Click(object sender, RoutedEventArgs e)
        {
            //if (currentBus.FuelRemain != 0.0)
            //    bl.doFuel(currentBus.LicenseNum);
        }

        private void doTreatment_Click(object sender, RoutedEventArgs e)
        {
            //if (currentBus.KilometersSinceLastTreatment > 20000 || currentBus.TreatmentDate.Year == DateTime.Now.Year)
            //    bl.doTreatment(currentBus.LicenseNum);
        }
    }
}
