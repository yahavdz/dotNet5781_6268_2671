﻿using BLApi;
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
using Line = BO.Line;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : UserControl
    {
        public Station currentSta { get; set; }
        private List<LineControl> allLineControls { get; set; }
        private IBL bl { get; set; }
        public StationDetails(Station _station, IBL _bl)
        {
            InitializeComponent();
            currentSta = _station;
            bl = _bl;
            allLineControls = new List<LineControl>();
            Refresh();
        }

        public void Refresh()
        {

            stationCode.Content = currentSta.Code.ToString() ;
            name.Content = currentSta.Name.ToString();
            address.Content = currentSta.Address.ToString();
            longitude.Content = currentSta.Longitude.ToString() + "°E";
            latitude.Content = currentSta.Latitude.ToString() + "°N";

            IEnumerable<Line> linesWithStation = bl.GetAllLinesBy(l => l.stations.Any(s => s.Code == currentSta.Code));
            foreach (Line line in linesWithStation)
            {
                LineControl lineControl = new LineControl(line);
                allLineControls.Add(lineControl);
                listLine.Items.Add(lineControl);
            }
        }
    }
}
