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

namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow()
        {
            InitializeComponent();  
        }
        public Bus detailsBus { get; set; }

        public void showDetails()
        {
            laBusId.Content = detailsBus.busID;
            laTotalKilometers.Content = detailsBus.totalKilometers;
            laAmountFuel.Content = 1200 - (detailsBus.totalKilometers - detailsBus.KilometersAtLastRefueling);
            laActivityStartDate.Content = detailsBus.ActivityStartDate.ToString("MMMM dd, yyyy");
            laLastTreatmentDate.Content = detailsBus.LastTreatmentDate.ToString("MMMM dd, yyyy");
            laStatus.Content = detailsBus.statusNow;
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            GoWindow secondWindow = new GoWindow(detailsBus);
            secondWindow.ShowDialog();
        }

        private void fuel_Click(object sender, RoutedEventArgs e)
        {
            detailsBus.refueling();
            laAmountFuel.Content = 1200;
        }
    }
}
