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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bus bus;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mangementButton_Click(object sender, RoutedEventArgs e)
        {
            MangementWindow mangementWindow = new MangementWindow();
            mangementWindow.ShowDialog();
        }
    }
}
