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
    /// Interaction logic for FuelWindow.xaml
    /// </summary>
    public partial class FuelWindow : Window
    {
        public FuelWindow()
        {
            InitializeComponent();
        }
    }
}
