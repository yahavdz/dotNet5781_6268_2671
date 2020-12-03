using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Windows.Forms;

namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private Bus detailsBus;
        private readonly BusItem busItemWindow;
        private const int day = 144000;
        private const int hour = 6000;
        public DetailsWindow(BusItem busWindow)
        {
            InitializeComponent();
            busItemWindow = busWindow;
            detailsBus = busItemWindow.currentBus;
        }


        public void showDetails()
        {
            laBusId.Content = detailsBus.busID;
            laTotalKilometers.Content = detailsBus.totalKilometers;
            laAmountFuel.Content = (1200 - (detailsBus.totalKilometers - detailsBus.KilometersAtLastRefueling)).ToString() + "km";
            laActivityStartDate.Content = detailsBus.ActivityStartDate.ToString("MMMM dd, yyyy");
            laLastTreatmentDate.Content = detailsBus.LastTreatmentDate.ToString("MMMM dd, yyyy");
            laStatus.Content = detailsBus.statusNow;
        }

        private void treatment_Click(object sender, RoutedEventArgs e)
        {
            if (detailsBus.statusNow == status.readyToGo)
            {
                busItemWindow.itamPanel.Background = Brushes.Khaki;
                busItemWindow.tbStatus.Text = "End the treatment in:";
                detailsBus.treatment();
                showDetails();
                Thread t1 = new Thread(inTreatment);
                t1.Start();
            }
            else
            {
                string message = "The bus is in the middle of a process";
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }

        private void fuel_Click(object sender, RoutedEventArgs e)
        {
            if (detailsBus.statusNow == status.readyToGo && detailsBus.totalKilometers > detailsBus.KilometersAtLastRefueling)
            {
                busItemWindow.tbStatus.Text = "End refuel in:";
                busItemWindow.itamPanel.Background = Brushes.LightSkyBlue;
                detailsBus.refueling();
                showDetails();
                Thread t1 = new Thread(inRefuel);
                t1.Start();
                FuelWindow secondWindow = new FuelWindow();
                secondWindow.ShowDialog();
            }
            else
            {
                string message = "The bus is in the middle of a process";
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }
        private void inTreatment()
        {
            Thread.Sleep(day);
            detailsBus.statusNow = status.readyToGo;
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.itamPanel.Background = Brushes.LightGreen));
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.tbStatus.Text = ""));
            Dispatcher.BeginInvoke((Action)(() => showDetails()));
        }
        private void inRefuel()
        {
            Thread.Sleep(hour * 2);
            detailsBus.statusNow = status.readyToGo;
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.itamPanel.Background = Brushes.LightGreen));
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.tbStatus.Text = ""));
            Dispatcher.BeginInvoke((Action)(() => showDetails()));
        }
    }
}
