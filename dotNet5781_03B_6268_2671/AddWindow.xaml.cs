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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }
        public List<Bus> _busList { get; set; }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            Bus newBus = new Bus();
            int len = newBusId.Text.Length;
            //------>
            bool checkLT = true;
            if (newLastTreatment.DisplayDate > DateTime.Today)
                checkLT = false;
            if (checkLT)
                newBus.LastTreatmentDate = newLastTreatment.DisplayDate;
            else
                Indication.Content = "*Invalid Last Treatment Date*";
            //------>
            bool checkAS = true;
            newBus.ActivityStartDate = newActivityStart.DisplayDate;
            if (len == 8 && (newBus.ActivityStartDate.Year < 2018 || newBus.ActivityStartDate > DateTime.Today))
                checkAS = false;
            if (len == 7 && newBus.ActivityStartDate.Year >= 2018)
                checkAS = false;

            if (!checkAS)
                Indication.Content = "*Invalid Activity Start Date*";
            //------>
            bool checkBusId = true;
            newBus.busID = newBusId.Text;
            if (len > 0 && (len == 7 || len == 8))
            {
                for (int i = 0; i < len; i++)
                    if (newBus.busID[i] < '0' && newBus.busID[i] > '9')
                    {
                        checkBusId = false;
                        break;
                    }
            }
            else
                checkBusId = false;

            if (checkBusId)
            {
                if (len == 7)
                    newBus.busID = newBus.busID.Insert(2, "-");
                else //len==8
                    newBus.busID = newBus.busID.Insert(3, "-");
                newBus.busID = newBus.busID.Insert(6, "-");
            }
            else
                newBusId.Text = "*Invalid license number*";
            //------>
            bool checkTotalKm = true;
            for (int i = 0; i < newTotalKilometers.Text.Length; i++)
                if (newTotalKilometers.Text[i] < '0' || newTotalKilometers.Text[i] > '9')
                {
                    checkTotalKm = false;
                    break;
                }

            if (newTotalKilometers.Text.Length > 0 && newTotalKilometers.Text.Length < 10 && checkTotalKm)
                newBus.totalKilometers = Convert.ToInt32(newTotalKilometers.Text);
            else
                newTotalKilometers.Text = "*Invalid number*";
            //------>
            bool checkFuel = false;
            if (checkTotalKm)
            {
                checkFuel = true;
                for (int i = 0; i < newTotalFuel.Text.Length; i++)
                    if (newTotalFuel.Text[i] < '0' || newTotalFuel.Text[i] > '9')
                    {
                        checkFuel = false;
                        break;
                    }

                int totalFuel = -1;
                if (newTotalFuel.Text.Length == 0 || newTotalFuel.Text.Length > 9)
                    checkFuel = false;
                else
                {
                    totalFuel = Convert.ToInt32(newTotalFuel.Text);
                    if (totalFuel <= 1200)
                        totalFuel = newBus.totalKilometers - (1200 - totalFuel);
                    else
                        checkFuel = false;
                }
                if (checkFuel && totalFuel >= 0)
                    newBus.KilometersAtLastRefueling = totalFuel;
                else
                {
                    newTotalFuel.Text = "*Invalid number*";
                    checkFuel = false;
                }
            }
            else
                newTotalFuel.Text = "";

            if (checkBusId && checkTotalKm && checkFuel && checkAS && checkLT)
            {
                _busList.Add(newBus);
                newBusId.Text = "";
                newTotalKilometers.Text = "";
                newTotalFuel.Text = "";
                newActivityStart.SelectedDate = null;
                newLastTreatment.SelectedDate = null;
                Indication.Content = "Succeeded";
                Indication.Foreground = Brushes.Green;
            }
            else
            {
                string temp = Indication.Content.ToString();
                if (temp != "*Invalid Last Treatment Date*" && temp != "*Invalid Activity Start Date*")
                    Indication.Content = "Try again";
                Indication.Foreground = Brushes.Red;
            }
        }
    }
}
