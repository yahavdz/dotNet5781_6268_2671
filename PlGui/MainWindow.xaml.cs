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

namespace PlGui
{
    public enum Options { Login, Signup }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        private Options selectedView { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            selectedView = Options.Signup;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            selectedView = Options.Login;
            loginButton.Opacity = 1;
            signupButton.Opacity = 0.6;
            approval.Content = "Log in";
            confirmPWlab.Visibility = Visibility.Hidden;
            myConfirmPW.Visibility = Visibility.Hidden;
            emaillab.Visibility = Visibility.Hidden;
            myEmail.Visibility = Visibility.Hidden;
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            selectedView = Options.Signup;
            signupButton.Opacity = 1;
            loginButton.Opacity = 0.6;
            passwordLab.Foreground = Brushes.Black;
            approval.Content = "Sign up";
            confirmPWlab.Visibility = Visibility.Visible;
            myConfirmPW.Visibility = Visibility.Visible;
            emaillab.Visibility = Visibility.Visible;
            myEmail.Visibility = Visibility.Visible;
        }

        private void approval_Click(object sender, RoutedEventArgs e)
        {
            if (selectedView == Options.Login)
            {
                if (myUsername.Text == "aaa" && myPassword.Password == "1234")
                {
                    passwordLab.Foreground = Brushes.Black;
                    myUsername.Text = "";
                    myPassword.Password = "";
                    MangementWindow mangementWindow = new MangementWindow(bl);
                    mangementWindow.ShowDialog();
                }
                else if (myUsername.Text == "zzz" && myPassword.Password == "0000")
                {
                    passwordLab.Foreground = Brushes.Black;
                    myUsername.Text = "";
                    myPassword.Password = "";
                    UserWindow userWindow = new UserWindow(bl);
                    userWindow.ShowDialog();
                }
                else
                    passwordLab.Foreground = Brushes.Red;
            }
            else
            {

            }
        }
        private void OnKeyDownHandlerLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && selectedView == Options.Login)
                approval_Click(sender, e);
        }

        private void OnKeyDownHandlerSginin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && selectedView == Options.Signup)
                approval_Click(sender, e);
        }
    }
}
