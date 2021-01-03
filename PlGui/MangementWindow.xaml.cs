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

namespace PlGui
{
    public enum selected { busDis, lineDis, stationDis }
    /// <summary>
    /// Interaction logic for MangementWindow.xaml
    /// </summary>
    public partial class MangementWindow : Window
    {
        private selected selectedView { get; set; }
        public MangementWindow()
        {
            InitializeComponent();
        }

        private void busButton_Click(object sender, RoutedEventArgs e)
        {
            selectedView = selected.busDis;
        }

        private void lineButton_Click(object sender, RoutedEventArgs e)
        {
            selectedView = selected.lineDis;
            updateList();
        }

        private void stationButton_Click(object sender, RoutedEventArgs e)
        {
            selectedView = selected.stationDis;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedView)
            {
                case selected.busDis:
                    break;
                case selected.lineDis:
                    //(myLBI.SelectedItem as LineControl).currentLine  .DeleteLine();
                    break;
                case selected.stationDis:
                    break;
                default:
                    break;
            }
            updateList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedView)
            {
                case selected.busDis:
                    break;
                case selected.lineDis:
                    AddOrUpdateLine newWin = new AddOrUpdateLine(null);
                    newWin.ShowDialog();
                    break;
                case selected.stationDis:
                    break;
                default:
                    break;
            }
            updateList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedView)
            {
                case selected.busDis:
                    break;
                case selected.lineDis:
                    AddOrUpdateLine newWin = new AddOrUpdateLine((myLBI.SelectedItem as LineControl).currentLine);
                    newWin.ShowDialog();
                    break;
                case selected.stationDis:
                    break;
                default:
                    break;
            }
        }

        private void updateList() 
        {
            switch (selectedView)
            {
                case selected.busDis:
                    break;
                case selected.lineDis:
                    //    --->for all lines in the DS
                    //    foreach _line in BLI.listLine:
                    //            myLBI.Items.Add(LineControl(_line));
                    //
                    break;
                case selected.stationDis:
                    break;
                default:
                    break;
            }
        }
    }
}
