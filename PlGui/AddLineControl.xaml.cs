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
using System.Windows.Media.Animation;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineControl.xaml
    /// </summary>
    public partial class AddLineControl : UserControl
    {
        private addOrUpdate myStatos { get; set; }
        private IBL bl { get; set; }
        private LineControl SelectedLC { get; set; }
        private List<LineControl> allLineControls { get; set; }
        public AddLineControl(LineControl _SelectedLC, List<LineControl> _allLineControls, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            allLineControls = _allLineControls;
            SelectedLC = _SelectedLC;

            newArea.ItemsSource = Enum.GetValues(typeof(Areas));
            newFirstSta.ItemsSource = bl.GetAllStations();
            newFirstSta.DisplayMemberPath = "Name";

            newLastSta.ItemsSource = bl.GetAllStations();
            newLastSta.DisplayMemberPath = "Name";

            if (SelectedLC != null)
            {
                myStatos = addOrUpdate.update;
                newLineNum.Text = SelectedLC.currentLine.Id.ToString();
                newArea.SelectedIndex = (int)SelectedLC.currentLine.Area;
                newFirstSta.Visibility = Visibility.Hidden;
                newLastSta.Visibility = Visibility.Hidden;
                labelFS.Text += bl.GetStations(SelectedLC.currentLine.FirstStation).Name;
                labelLS.Text += bl.GetStations(SelectedLC.currentLine.LastStation).Name;
            }
            else
                myStatos = addOrUpdate.add;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Line newLine;
            if (SelectedLC != null)
                newLine = SelectedLC.currentLine;
            else
                newLine = new Line();
            try { newLine.Code = Int32.Parse(newLineNum.Text); }
            catch (FormatException) { newLineNum.Text = "*Invalid number*"; Succeeded = false; }
            if (newArea.SelectedItem != null)
                newLine.Area = (Areas)Enum.Parse(typeof(Areas), newArea.SelectedItem.ToString());
            else
            {
                Indication.Content = "No Area selected";
                Succeeded = false;
            }
            if (myStatos == addOrUpdate.add)
            {
                if (newFirstSta.SelectedItem != null && newLastSta.SelectedItem != null)
                {
                    newLine.FirstStation = (int)((newFirstSta.SelectedItem as Station).Code);
                    newLine.LastStation = (int)((newLastSta.SelectedItem as Station).Code);
                }
                else
                {
                    Indication.Content = "Need to choose two stations";
                    Succeeded = false;
                }
            }

            try
            {
                if (myStatos == addOrUpdate.add)
                    bl.AddLine(newLine);
                else
                    bl.UpdateLine(newLine);
            }
            catch (BadIdException ex)
            {
                newLineNum.Text = "";
                Indication.Content = ex.Message;
                Succeeded = false;
            }


            if (Succeeded)
            {
                Indication.Content = "Succeeded";
                Indication.Foreground = Brushes.Lime;
                var grid = ((((sender as Button).Parent as Grid).Parent as AddLineControl).Parent as Grid).Parent as Grid;
                var window = ((((grid.Children[2] as Button).Parent as Grid).Parent as Grid).Parent as Grid).Parent;
                if (myStatos == addOrUpdate.add)
                {
                    allLineControls.Add(new LineControl(newLine));
                    (window as MangementWindow).updateList();
                    string msg = "Line " + newLine.Code + " successfully added";
                    MessageBox.Show(msg);
                }
                else
                {
                    SelectedLC.Refresh();
                    string msg = "Line " + newLine.Code + " has been successfully updated";
                    MessageBox.Show(msg);
                }
                Indication.Foreground = Brushes.Lime;
                ((window as Window).FindResource("closeSB") as Storyboard).Begin();
            }
            else
            {
                string temp = Indication.Content.ToString();
                if (temp == "")
                    Indication.Content = "Try again";
                Indication.Foreground = Brushes.Red;
            }
        }
    }
}
