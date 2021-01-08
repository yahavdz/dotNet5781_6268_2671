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
using BLApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for BusControl.xaml
    /// </summary>
    public partial class BusControl : UserControl
    {
        public Bus currentBus { get; set; }
        public BusControl(Bus _bus)
        {
            InitializeComponent();
            currentBus = _bus;
            busNum.Content = currentBus.LicenseNum.ToString().Insert(4, "-");
            busMileage.Content = currentBus.TotalTrip.ToString() + "km";
            busFuel.Content = currentBus.FuelRemain.ToString() + "km";
        }
    }
}
