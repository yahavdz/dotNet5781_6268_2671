using BO;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : UserControl
    {
        public Station currentBus { get; set; }
        public StationDetails(Station _station)
        {
            InitializeComponent();
            currentBus = _station;
            Refresh();
        }

        public void Refresh()
        {

            stationCode.Content = currentBus.Code.ToString() ;
            name.Content = currentBus.Name.ToString();
            address.Content = currentBus.Address.ToString();
            longitude.Content = currentBus.Longitude.ToString() + "°E";
            latitude.Content = currentBus.Latitude.ToString() + "°N";


        }
    }
}
