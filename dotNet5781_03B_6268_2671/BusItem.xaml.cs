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


namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for BusItem.xaml
    /// </summary>
    public partial class BusItem : UserControl
    {
        public Bus currentBus { get; set; }
        public BusItem(Bus _bus)
        {
            InitializeComponent();
            myLab.Content = _bus.busID;
            this.currentBus = new Bus();
            currentBus = _bus;
        }

        private void Button_Go(object sender, RoutedEventArgs e)
        {
            GoWindow secondWindow = new GoWindow(currentBus);
            secondWindow.ShowDialog();
        }

        private void Button_Fuel(object sender, RoutedEventArgs e)
        {
            this.currentBus.refueling();
            FuelWindow secondWindow = new FuelWindow();
            secondWindow.ShowDialog();
        }
       
    }
}
