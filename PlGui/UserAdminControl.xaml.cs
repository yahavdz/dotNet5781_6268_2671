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
using BLApi;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UserControl.xaml
    /// </summary>
    public partial class UserAdminControl : UserControl
    {
        private IBL bl { get; set; }
        private User curUser { get; set; }
        public UserAdminControl(User user, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            curUser = user;

            userName.Content = curUser.UserName;
            if (curUser.Admin)
            {
                userAdmin.Content = "Yes";
                BecomeAdmin.Visibility = Visibility.Hidden;
            }
            else
                userAdmin.Content = "No";

        }

        private void BecomeAdmin_Click(object sender, RoutedEventArgs e)
        {
            userAdmin.Content = "Yes";
            BecomeAdmin.Visibility = Visibility.Hidden;
            curUser.Admin = true;
            bl.UpdateUser(curUser);
        }
    }
}
