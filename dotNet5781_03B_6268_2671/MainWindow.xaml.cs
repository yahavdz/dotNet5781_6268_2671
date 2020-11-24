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

            //Initialization ten buses
            for (int i = 0 ; i < 10 ; i++)
            {
                
                Bus bus = new Bus();
                if (i == 1 || i == 4 || i == 7 || i == 9)//Ensure that there will be both license plates from 2018-2020 and from 2018 onwards
                    bus.busID = random.Next(10, 100) + "-" + random.Next(1, 1000) + "-" + random.Next(1, 100);
                else
                    bus.busID = random.Next(100, 1000) + "-" + random.Next(1, 100) + "-" + random.Next(1, 1000);

                //set a random date from the last year
                DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day - 365);
                bus.treatmentDate = start.AddDays(random.Next(365));

                bus.mileage = random.Next(20000,200000);//set the mileage to be random from 20,000km to 200,000km
                bus.mileageTreatment = bus.mileage - random.Next(1, 20000);
                bus.mileageFuel= bus.mileage - random.Next(1,1200);

                if (i != 1) //It has been more than a year since the treatment of the second bus
                    bus.statusNow = status.readyToGo;
                
                busList.Add(bus);
            }
            busList[1].treatmentDate = new DateTime(2019, 11, 1); //More than a year has passed since the last treatment
            busList[2].mileageTreatment = busList[2].mileage - 19000; //Close to the next treatment (in mileage)
            busList[3].mileageFuel = busList[2].mileage - 1100; //Bus with little fuel


        }
    }
}
