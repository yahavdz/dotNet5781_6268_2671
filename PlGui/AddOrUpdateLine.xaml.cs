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
using BL.BO;
using Line = BL.BO.Line;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddNewLine.xaml
    /// </summary>
    public partial class AddOrUpdateLine : Window
    {
        public Line currentLine { get; set; }
        public AddOrUpdateLine(Line _line)
        {
            InitializeComponent();
            if(_line != null)
            {
                currentLine = _line;
                showDetails();
            }
        }

        private void showDetails()
        {

        }
    }
}
