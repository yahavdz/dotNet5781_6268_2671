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
                if (i == 1 || i == 4 || i == 7 || i == 9)//Ensure that there will be both license plates from 2018-2020 and from 2018 onwards
                    bus.busID = random.Next(1, 99) + "-" + random.Next(1, 999) + "-" + random.Next(1, 99);
                else
                    bus.busID = random.Next(1, 999) + "-" + random.Next(1, 99) + "-" + random.Next(1, 999);
                bus.mileage= random.Next(1,200000);//set the mileage to be random from 1 to 200,000
                DateTime start = new DateTime(1995, 1, 1);//set a random date
                int range = (DateTime.Today - start).Days;
                bus.treatmentDate = start.AddDays(random.Next(range));
                bus.mileageFuel= random.Next(1,1200);
            }
            
        }
    }
}
