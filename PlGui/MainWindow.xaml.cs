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
    public enum WatchState { Start, Stop }
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
            selectedView = Options.Login;
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
            wrongPass.Text = "";
            approval.Content = "Sign up";
            confirmPWlab.Visibility = Visibility.Visible;
            myConfirmPW.Visibility = Visibility.Visible;
            emaillab.Visibility = Visibility.Visible;
            myEmail.Visibility = Visibility.Visible;
        }

        private void approval_Click(object sender, RoutedEventArgs e)
        {
            wrongPass.Text = "";
            if (selectedView == Options.Login)
            {
                try
                {
                    BO.User user = bl.GetUser(myUsername.Text);
                    if (user.HashPassword == myPassword.Password.GetHashCode())
                    {
                        passwordLab.Foreground = Brushes.Black;
                        if (user.Admin)
                        {
                            MangementWindow mangementWindow = new MangementWindow(bl);
                            mangementWindow.ShowDialog();
                        }
                        else
                        {
                            UserWindow userWindow = new UserWindow(bl);
                            userWindow.ShowDialog();
                        }
                        wrongPass.Text = "";
                        myUsername.Text = "";
                        myPassword.Password = "";
                    }
                    else
                    {
                        wrongPass.Text = "Incorrect password. Please try again";
                    }
                }
                catch(BO.BadUserNameException ex)
                {
                    wrongPass.Text = "Username does not exist. Please try again";
                }
            }
            else
            {
                // register:
                if (myPassword.Password != myConfirmPW.Password)
                {
                    wrongPass.Text = "Password is different from the confirm password.\nPlease try again";
                }
                else
                {
                    try
                    {
                        BO.User user = new User();
                        user.UserName = myUsername.Text;
                        user.HashPassword = myPassword.Password.GetHashCode();
                        user.Active = true;
                        bl.AddUser(user);

                        // change to login window:
                        selectedView = Options.Login;
                        loginButton.Opacity = 1;
                        signupButton.Opacity = 0.6;
                        approval.Content = "Log in";
                        confirmPWlab.Visibility = Visibility.Hidden;
                        myConfirmPW.Visibility = Visibility.Hidden;
                        emaillab.Visibility = Visibility.Hidden;
                        myEmail.Visibility = Visibility.Hidden;

                    }
                    catch (BO.BadUserNameException ex)
                    {
                        wrongPass.Text = "Username already exist. Please try again";
                    }
                }

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
