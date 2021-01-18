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
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;
using BO;
using BLApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IBL bl;
        WatchState nowState = WatchState.Stop;

        public UserWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        
        public void updateTime(TimeSpan newTS)
        {
            if (newTS.Seconds < 10)
                secTB.Text = "0" + newTS.Seconds.ToString();
            else
                secTB.Text = newTS.Seconds.ToString();
            if(newTS.Minutes < 10)
                minTB.Text = "0" + newTS.Minutes.ToString();
            else
                minTB.Text = newTS.Minutes.ToString();
            if(newTS.Hours < 10)
                hourTB.Text = "0" + newTS.Hours.ToString();
            else
                hourTB.Text = newTS.Hours.ToString();
        }

        private void startOrStop_Click(object sender, RoutedEventArgs e)
        {
            if (nowState == WatchState.Stop)
            {
                int hour = Convert.ToInt32(hourTB.Text);
                int min = Convert.ToInt32(minTB.Text);
                int sec = Convert.ToInt32(secTB.Text);
                int timeSpeed = Convert.ToInt32(speedTB.Text);
                if (sec > 59 || min > 59 || hour > 23)
                    MessageBox.Show("Invalid time", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else if(timeSpeed == 0)
                    MessageBox.Show("Invalid speed", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    startOrStop.Content = "Stop";
                    bl.StartSimulator(new TimeSpan(hour, min, sec), timeSpeed, updateTime);
                    nowState = WatchState.Start;
                    hourTB.IsReadOnly = true;
                    minTB.IsReadOnly = true;
                    secTB.IsReadOnly = true;
                    speedTB.IsReadOnly = true;
                }
            }
            else if (nowState == WatchState.Start)
            {
                MessageBoxResult popUp = MessageBox.Show("Are you sure you want to stop the system?", "Watch",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question,
               MessageBoxResult.Yes);
                if (popUp == MessageBoxResult.Yes)
                {
                    bl.StopSimulator();
                    startOrStop.Content = "Start";
                    nowState = WatchState.Stop;
                    hourTB.IsReadOnly = false;
                    minTB.IsReadOnly = false;
                    secTB.IsReadOnly = false;
                    speedTB.IsReadOnly = false;
                }

            }
        }
    }
}
