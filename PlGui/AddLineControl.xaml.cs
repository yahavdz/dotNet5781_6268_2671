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
    /// Interaction logic for AddLineControl.xaml
    /// </summary>
    public partial class AddLineControl : UserControl
    {
        private ListBox allLineC { get; set; }
        private IBL bl { get; set; }
        public AddLineControl(ListBox _allLineC, IBL _bl)
        {
            InitializeComponent();
            allLineC = _allLineC;
            bl = _bl;
            newArea.ItemsSource = Enum.GetValues(typeof(Areas));
            newFirstSta.ItemsSource = bl.GetAllStations();
            newFirstSta.DisplayMemberPath = "Name";

            newLastSta.ItemsSource = bl.GetAllStations();
            newLastSta.DisplayMemberPath = "Name";

            LineControl SelectedLC = allLineC.SelectedItem as LineControl;
            if (SelectedLC != null)
            {
                newLineNum.Text = SelectedLC.currentLine.Id.ToString();
                newArea.SelectedIndex = (int)SelectedLC.currentLine.Area;
                newFirstSta.Visibility = Visibility.Hidden;
                newLastSta.Visibility = Visibility.Hidden;
                labelFS.Text += bl.GetStations(SelectedLC.currentLine.FirstStation).Name;
                labelLS.Text += bl.GetStations(SelectedLC.currentLine.LastStation).Name;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Line newLine = new Line();
            try { newLine.Id = Int32.Parse(newLineNum.Text); }
            catch (FormatException) { newLineNum.Text = "*Invalid number*"; Succeeded = false; }
            if (newArea.SelectedItem != null)
                newLine.Area = (Areas)Enum.Parse(typeof(Areas), newArea.SelectedItem.ToString());
            else
            {
                Indication.Content = "No Area selected";
                Succeeded = false;
            }
            //List<Station> newListSta = new List<Station>();
            //newListSta.Add(newFirstSta.SelectedItem as Station);
            //newListSta.Add(newLastSta.SelectedItem as Station);
            //newLine.stations = newListSta;

            //try { bl.AddLine(newLine); }
            //catch (BadIdException myEx)
            //{
            //    newLineNum.Text = "";
            //    Indication.Content = myEx.Message;
            //    Succeeded = false;
            //}


            if (Succeeded)
            {
                newLineNum.Text = "";
                Indication.Content = "Succeeded";
                Indication.Foreground = Brushes.Lime;
                allLineC.Items.Add(new LineControl(newLine));
            }
            else
            {
                string temp = Indication.Content.ToString();
                if (temp == "" || temp == "Succeeded")
                    Indication.Content = "Try again";
                Indication.Foreground = Brushes.Red;
            }
        }
    }
}
