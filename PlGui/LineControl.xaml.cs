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
    /// Interaction logic for LineControl.xaml
    /// </summary>
    public partial class LineControl : UserControl
    {
        public Line currentLine { get; set; }
        public LineControl(Line _line)
        {
            InitializeComponent();
            currentLine = _line;
            lineId.Text = lineId.Text + " " + currentLine.LineId;
            lineArea.Text = lineArea.Text + " " + currentLine.Area;
            allStation.ItemsSource = currentLine.lineList;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
