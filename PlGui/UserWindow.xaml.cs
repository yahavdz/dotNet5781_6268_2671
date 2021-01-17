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
        int timeSpeed = 0;
        int hour = 0;
        int min = 0;
        int sec = 0;
        BackgroundWorker worker;
        WatchState nowState = WatchState.Stop;
        public UserWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!e.Cancel)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000 / timeSpeed);
                    sec++;
                    if(hour == 23 && min == 59 && sec == 59)
                    {
                        hour = 0;
                        min = 0;
                        sec = 0;
                    }
                    else if (min == 59 && sec == 59)
                    {
                        hour++;
                        min = 0;
                        sec = 0;
                    }
                    else if (sec == 59)
                    {
                        min++;
                        sec = 0;
                    }
                    worker.ReportProgress(1);
                }
            }
            e.Result = stopwatch.ElapsedMilliseconds;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (sec < 10)
                secTB.Text = "0" + sec.ToString();
            else
                secTB.Text = sec.ToString();
            if(min < 10)
                minTB.Text = "0" + min.ToString();
            else
                minTB.Text = min.ToString();
            if(hour < 10)
                hourTB.Text = "0" + hour.ToString();
            else
                hourTB.Text = hour.ToString();
        }

        private void startOrStop_Click(object sender, RoutedEventArgs e)
        {
            if (nowState == WatchState.Stop && worker.IsBusy != true)
            {
                hour = Convert.ToInt32(hourTB.Text);
                min = Convert.ToInt32(minTB.Text);
                sec = Convert.ToInt32(secTB.Text);
                timeSpeed = Convert.ToInt32(speedTB.Text);
                if (sec > 59 || min > 59 || hour > 23)
                    MessageBox.Show("Invalid time", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else if(timeSpeed == 0)
                    MessageBox.Show("Invalid speed", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    startOrStop.Content = "Stop";
                    worker.RunWorkerAsync(); // Start the asynchronous operation.
                    nowState = WatchState.Start;
                    hourTB.IsReadOnly = true;
                    minTB.IsReadOnly = true;
                    secTB.IsReadOnly = true;
                    speedTB.IsReadOnly = true;
                }
            }
            else if (nowState == WatchState.Start && worker.WorkerSupportsCancellation == true)
            {
                worker.CancelAsync(); // Cancel the asynchronous operation.
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
