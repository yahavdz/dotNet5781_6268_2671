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
using Line = BO.Line;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationControl.xaml
    /// </summary>
    public partial class StationControl : UserControl
    {
        public Station currentStation { get; set; }
        public StationControl(Station _station)
        {
            InitializeComponent();
            currentStation = _station;
            Refresh();
        }

        public void Refresh()
        {
            nameSta.Content = currentStation.Name;
            codeSta.Content = currentStation.Code;
        }
    }
}
