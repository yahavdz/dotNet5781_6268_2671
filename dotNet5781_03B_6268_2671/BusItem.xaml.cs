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
        public BusItem(Bus _bus)
        {
            InitializeComponent();
            myLab.Content = _bus.busID;
            this.currentBus = new Bus();
            currentBus = _bus;
        }

        private void Button_Go(object sender, RoutedEventArgs e)
        {
            if (currentBus.statusNow == status.readyToGo)
            {
                GoWindow secondWindow = new GoWindow(currentBus, this);
                secondWindow.ShowDialog();
            }
        }

        private void Button_Fuel(object sender, RoutedEventArgs e)
        {

            if (currentBus.statusNow == status.readyToGo && currentBus.totalKilometers > currentBus.KilometersAtLastRefueling)
            {
                this.currentBus.refueling();
                itamPanel.Background = Brushes.LightSkyBlue;
                Thread t1 = new Thread(inRefuel);
                t1.Start();
                FuelWindow secondWindow = new FuelWindow();
                secondWindow.ShowDialog();
            }
            else
            {
                string message = "The Bus can't refule now";
                string title = "warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }
        private void inRefuel()
        {
            Dispatcher.BeginInvoke((Action)(() => tbStatus.Text = "End refuel in:")); // use dispatcher to access the TextBox in different threads
            countDown(hour * 2 / 1000);
            Thread.Sleep(hour * 2);
            currentBus.statusNow = status.readyToGo;
            Dispatcher.BeginInvoke((Action)(() => itamPanel.Background = Brushes.LightGreen)); // use dispatcher to access the TextBox in different threads
            Dispatcher.BeginInvoke((Action)(() => tbStatus.Text = ""));
        }
        public void countDown(int time)
        {
            _time = TimeSpan.FromSeconds(time);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, System.Windows.Application.Current.Dispatcher);

            _timer.Start();

        }

    }
}
