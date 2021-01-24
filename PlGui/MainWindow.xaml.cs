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
            if(selectedView == Options.Signup)
            {
                wrongPass.Text = "";
                myUsername.Text = "";
                myPassword.Password = "";
            }
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
            if (selectedView == Options.Login)
            {
                wrongPass.Text = "";
                myUsername.Text = "";
                myPassword.Password = "";
                myConfirmPW.Password = "";
                myEmail.Text = "";
            }
            selectedView = Options.Signup;
            signupButton.Opacity = 1;
            loginButton.Opacity = 0.6;
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
                try
                {
                    User loginU = bl.GetUser(myUsername.Text);
                    if (myPassword.Password == loginU.Password)
                    {
                        if (loginU.Admin)
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
                        wrongPass.Text = "Incorrect password";
                }
                catch(BadUserNameException)
                {
                    wrongPass.Text = "Username does not exist";
                }
            }
            else //sign up
            {               
                User newUser = new User();
                newUser.Admin = false;
                newUser.UserName = myUsername.Text;
                newUser.Password = myPassword.Password;
                if (newUser.Password != myConfirmPW.Password)
                {
                    wrongPass.Text = "The passwords do not match";
                    myConfirmPW.Password = "";
                }
                else
                {
                    try
                    {
                        bl.AddUser(newUser);
                        wrongPass.Text = "You have successfully registered!";
                        myUsername.Text = "";
                        myPassword.Password = "";
                        myConfirmPW.Password = "";
                        myEmail.Text = "";
                    }
                    catch (BadUserNameException)
                    {
                        wrongPass.Text = "Username, " + myUsername.Text + ", already in use";
                        myUsername.Text = "";
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
