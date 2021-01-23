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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for PanelControl.xaml
    /// </summary>
    public partial class PanelControl : UserControl
    {
        public PanelControl(int lineNum, string nameSta, int min)
        {
            InitializeComponent();
            nameLastStaLa.Content = nameSta;
            if (lineNum > 0)
            {
                lineNumLa.Content = lineNum.ToString();
                if (min < 31)
                {
                    BGgrid.Background = Brushes.MediumSeaGreen;
                    arrivelTimeLa.Content = min.ToString();
                }
                else
                {
                    BGgrid.Background = Brushes.PeachPuff;
                    arrivelTimeLa.Content = "-" + (60 - min).ToString();
                }
            }
        }
    }
}
