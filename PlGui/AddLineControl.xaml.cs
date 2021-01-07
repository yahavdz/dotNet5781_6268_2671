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
using BL.BO;
using Line = BL.BO.Line;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineControl.xaml
    /// </summary>
    public partial class AddLineControl : UserControl
    {
        private ListBox allLineC { get; set; }
        public AddLineControl(ListBox _allLineC)
        {
            InitializeComponent();
            allLineC = _allLineC;
            //Enum.GetValues(typeof(EffectStyle)).Cast<EffectStyle>();
            newArea.ItemsSource = Enum.GetValues(typeof(Areas));
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool Succeeded = true;
            Line newLine = new Line();
            try { newLine.LineId = Int32.Parse(newLineNum.Text); }
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
