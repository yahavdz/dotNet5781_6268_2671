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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UserControl = System.Windows.Controls.UserControl;

namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for BusItem.xaml
    /// </summary>
    public partial class BusItem : UserControl
    {
        public Bus currentBus { get; set; }
        private const int day = 144000;
        private const int hour = 6000;
        DispatcherTimer _timer;
        TimeSpan _time;
        SolidColorBrush mySolidColorBrush = new SolidColorBrush(Colors.LightGreen);
        
        public BusItem(Bus _bus)
        {
            InitializeComponent();
            myLab.Content = _bus.busID;
            this.currentBus = new Bus();
            currentBus = _bus;
            mySolidColorBrush.Opacity = 0.6;
        }

        private void Button_Go(object sender, RoutedEventArgs e)//A button that sends for travel
        {
            if (currentBus.statusNow == status.readyToGo)
            {
                GoWindow secondWindow = new GoWindow(currentBus, this); 
                secondWindow.ShowDialog();
            }
            else
            {
                string message = "The Bus can't go to a ride now";//error messageBox
                string title = "warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }

        private void Button_Fuel(object sender, RoutedEventArgs e)//Button that sends for refueling
        {

            if (currentBus.statusNow == status.readyToGo && currentBus.totalKilometers > currentBus.KilometersAtLastRefueling)//Checks that the status of the bus is ok and also that the bus is not refueled
            {
                this.currentBus.refueling();
                SolidColorBrush mySCBfu = new SolidColorBrush(Colors.DarkCyan);
                mySCBfu.Opacity = 0.6;
                itamPanel.Background = mySCBfu;
                //itamPanel.Background.Opacity = new Double();
                //itamPanel.Background.Opacity = 0.6;
                Thread t1 = new Thread(inRefuel);
                t1.Start();
                FuelWindow secondWindow = new FuelWindow();
                secondWindow.ShowDialog();
            }
            else
            {
                string message = "The Bus can't refule now";//error messageBox
                string title = "warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }
        private void inRefuel()//thread Which activates the countdown until the end of refueling and cancels the use of the bus until the countdown is completed
        {
            Dispatcher.BeginInvoke((Action)(() => tbStatus.Text = "End refuel in:")); // use dispatcher to access the TextBox in different threads
            countDown(hour * 2 / 1000);
            Thread.Sleep(hour * 2);
            currentBus.statusNow = status.readyToGo;           
        }
        public void countDown(int time)// Bonus, counting down the seconds and displaying them in the main row
        {
            _time = TimeSpan.FromSeconds(time);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    Dispatcher.BeginInvoke((Action)(() => tbTime.Text = ""));
                    Dispatcher.BeginInvoke((Action)(() => itamPanel.Background = mySolidColorBrush));
                    Dispatcher.BeginInvoke((Action)(() => tbStatus.Text = ""));
                }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, System.Windows.Application.Current.Dispatcher);

            _timer.Start();
        }

    }
}
