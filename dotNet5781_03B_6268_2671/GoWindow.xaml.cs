using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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



namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for GoWindow.xaml
    /// </summary>
    public partial class GoWindow : Window
    {
        private Bus b;//TODO add to the 
        public GoWindow(Bus _bus)
        {
            InitializeComponent();
            this.b = new Bus();
            b = _bus;
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            MyLabel.Content = MyTextBox.Text;
            var isNumeric = int.TryParse(MyTextBox.Text, out int n);
            if (!isNumeric)
            {
                string message = "Incorrect Value";
                string title = "warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);

            }
            else //TODO dates
            {
                if(b.totalKilometers - b.kilometersSinceLastTreatment + n > 20000)
                {
                    string message = "The bus could not take the ride: pass the 20,000 km since last treatment("+ (b.totalKilometers-b.kilometersSinceLastTreatment)+ " km since last treatment)";//TODO
                    string title = "Close Window";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                }
                else if (n + (b.totalKilometers - b.KilometersAtLastRefueling) > 1200)
                {
                    string message = "The bus could not take the ride: The fuel will not hold until the end of the ride(" + (1200-(b.totalKilometers - b.KilometersAtLastRefueling))+ " km left at the tank)";//TODO
                    string title = "Close Window";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                }
                 else if(b.statusNow == status.midRide)
                {
                    string message = "The bus could not take the ride: in a middle of a ride";
                    string title = "Close Window";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                }
                else if (b.statusNow == status.refuelingNow)
                {
                    string message = "The bus could not take the ride: refuling now";
                    string title = "Close Window";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                }
                else if (b.statusNow == status.treatmentNow)
                {
                    string message = "The bus could not take the ride: in a treatment";
                    string title = "Close Window";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                }
                else
                {
                    b.kilometersSinceLastTreatment += n;
                    b.totalKilometers += n;
                    b.statusNow = status.midRide;
                }
                this.Close();
            }
            
        }

       
    }
}

