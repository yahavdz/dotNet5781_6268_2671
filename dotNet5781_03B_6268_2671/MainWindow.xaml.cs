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
using dotNet5781_02_6268_4032;
using dotNet5781_01_6268_4032;

namespace dotNet5781_03B_6268_2671
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bus> busList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Random random = new Random();
            for (int i = 0 ; i < 10 ; i++)
            {
                Bus bus = new Bus();
                int k;
                bool isStationAlreadyExist;
                do// do while to check if the key code is already exist
                {
                    k = random.Next(1, 999999);
                    isStationAlreadyExist = busList.Any(s => s.busID == k);
                } while (isStationAlreadyExist);
                bus.busID = k / 1000;
                bus.mileage= random.Next(1,200000);

            }
        }
    }
}
