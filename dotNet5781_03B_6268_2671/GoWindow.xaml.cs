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
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for GoWindow.xaml
    /// </summary>
    public partial class GoWindow : Window
    {
        private Bus b;
        static Random random = new Random();
        private readonly BusItem busItemWindow;//So that no instance of another bus will be sent ,and there will always be the same instance

        public GoWindow(Bus _bus, BusItem busWindow)
        {
            InitializeComponent();
            this.b = new Bus();
            b = _bus;
            busItemWindow = busWindow;
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)//Captures what the user has entered. If the input is correct (only a number and not a negative number) you can press enter
        {
            if (e.Key == Key.Return)
            {
                var isNumeric = int.TryParse(textBox1.Text, out int n);
                if (!isNumeric || n < 0)//check if the number is numeric and bigger than 0
                {
                    string message = "Incorrect Value";//error messageBox
                    string title = "warning";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);

                }
                else //in case that the input is correct 
                {
                    if (b.totalKilometers - b.kilometersSinceLastTreatment + n > 20000)//Checking if the bus has not traveled the 20,000km since the last treatment
                    {
                        string message = "The bus could not take the ride: pass the 20,000 km since last treatment (" + (b.totalKilometers - b.kilometersSinceLastTreatment) + " km since last treatment)";//error messageBox
                        string title = "Close Window";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                    }
                    else if (n + (b.totalKilometers - b.KilometersAtLastRefueling) > 1200)//Checking if the bus will not go the 1200km until the next refueling
                    {
                        string message = "The bus could not take the ride: The fuel will not hold until the end of the ride (" + (1200 - (b.totalKilometers - b.KilometersAtLastRefueling)) + " km left at the tank)";//error messageBox
                        string title = "Close Window";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                    }
                    else if (b.statusNow == status.midRide)//check if the is in a middle of a ride
                    {
                        string message = "The bus could not take the ride: in a middle of a ride";//error messageBox
                        string title = "Close Window";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                    }
                    else if (b.statusNow == status.refuelingNow)//check if the is refuling now
                    {
                        string message = "The bus could not take the ride: refuling now";//error messageBox
                        string title = "Close Window";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                    }
                    else if (b.statusNow == status.treatmentNow)//check if the is in a tretment
                    {
                        string message = "The bus could not take the ride: in a treatment";//error messageBox
                        string title = "Close Window";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                    }
                    else//in case the input is OK or there is no problem
                    {
                        b.kilometersSinceLastTreatment += n;
                        b.totalKilometers += n;
                        b.statusNow = status.midRide;
                        SolidColorBrush mySCBgo = new SolidColorBrush(Colors.Tomato);
                        mySCBgo.Opacity = 0.6;
                        busItemWindow.itamPanel.Background = mySCBgo;
                        Thread t1 = new Thread(inARide);
                        t1.Start(n);
                    }
                    this.Close();
                }
            }
        }
        private void inARide(object distance)//thread Which activates the countdown by pressing the travel button
        {
            Dispatcher.BeginInvoke((Action)(() => busItemWindow.tbStatus.Text = "Ends the trip in:")); // use dispatcher to access the TextBox in different threads

            int speed = random.Next(20, 50);            
            int sleepTime = ((int)distance / speed) * 6000;
            busItemWindow.countDown((sleepTime / 1000));
            Thread.Sleep(sleepTime);
            b.statusNow = status.readyToGo;
        }
    }
}

