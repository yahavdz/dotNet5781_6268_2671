//nevo cohen 207962671
//yahav davidzada 318356268
//exercise 3B
//In this exercise we created a graphical interface for handling a bus.
//We created windows to send the vehicle for refueling, treatment, and travel. And another window to insert a new bus into the system.
//The system announced the times when the bus was sent for each of the above missions.
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


        public void showDetails()//showing the details of the bus
        {
            laBusId.Content = detailsBus.busID;
            laTotalKilometers.Content = detailsBus.totalKilometers;
            laAmountFuel.Content = (1200 - (detailsBus.totalKilometers - detailsBus.KilometersAtLastRefueling)).ToString() + "km";
            laActivityStartDate.Content = detailsBus.ActivityStartDate.ToString("MMMM dd, yyyy");
            laLastTreatmentDate.Content = detailsBus.LastTreatmentDate.ToString("MMMM dd, yyyy");
            laStatus.Content = detailsBus.statusNow;
        }

        private void treatment_Click(object sender, RoutedEventArgs e)//buttom to send the bus for treatmen
        {
            if (detailsBus.statusNow == status.readyToGo)//check if the bus is not in a middle of other thread
            {
                SolidColorBrush mySCBtr = new SolidColorBrush(Colors.Khaki);
                mySCBtr.Opacity = 0.6;
                busItemWindow.itamPanel.Background = mySCBtr;
                if (detailsBus.totalKilometers == (detailsBus.KilometersAtLastRefueling + 1200))
                    detailsBus.refueling();
                detailsBus.treatment();
                showDetails();
                Thread t1 = new Thread(inTreatment);
                t1.Start();
            }
            else
            {
                string message = "The bus is in the middle of a process";//error messageBox
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }

        private void fuel_Click(object sender, RoutedEventArgs e)//sending the bus to refuel
        {
            if (detailsBus.statusNow == status.readyToGo && detailsBus.totalKilometers > detailsBus.KilometersAtLastRefueling)//Checks that the status of the bus is ok and also that the bus is not refueled
            {
                detailsBus.refueling();
                SolidColorBrush mySCBfu = new SolidColorBrush(Colors.DarkCyan);
                mySCBfu.Opacity = 0.6;
                busItemWindow.itamPanel.Background = mySCBfu;
                showDetails();
                Thread t1 = new Thread(inRefuel);
                t1.Start();
                FuelWindow secondWindow = new FuelWindow();
                secondWindow.ShowDialog();
            }
            else
            {
                string message = "The bus is in the middle of a process";//error messageBox
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }
        private void inTreatment()//thread Which activates the countdown until the end of the treatment and cancels the use of the bus until the countdown is completed
        {
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.tbStatus.Text = "End treatment in:"));
            busItemWindow.countDown(day / 1000);
            Thread.Sleep(day);
            detailsBus.statusNow = status.readyToGo;
            Dispatcher.BeginInvoke((Action)(() => showDetails()));
        }
        private void inRefuel()//thread Which activates the countdown until the end of refueling and cancels the use of the bus until the countdown is completed
        {
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.tbStatus.Text = "End refuel in:"));
            busItemWindow.countDown((hour* 2) / 1000);
            Thread.Sleep(hour * 2);
            detailsBus.statusNow = status.readyToGo;
            Dispatcher.BeginInvoke((Action)(() => showDetails()));
        }
    }
}
