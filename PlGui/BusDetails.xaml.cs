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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for BusDetails.xaml
    /// </summary>
    public partial class BusDetails : UserControl
    {
        public Bus currentBus { get; set; }
        public BusDetails(Bus _bus)
        {
            InitializeComponent();
            currentBus = _bus;
            Refresh();
        }

        public void Refresh()
        {
           
            busMileage.Content = currentBus.TotalTrip.ToString() + "km";
            busFuel.Content = currentBus.FuelRemain.ToString() + "km";
            startDate.Content = currentBus.FromDate.ToString();
            busStatus.Content = currentBus.BusStatus.ToString();
            KilometersSinceLastTreatment.Content= currentBus.KilometersSinceLastTreatment.ToString() + "km";
            KilometersSinceLastRefuel.Content = currentBus.KilometersAtLastRefueling.ToString() + "km";
        }
    }
}
