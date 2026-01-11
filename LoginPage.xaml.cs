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

namespace MAINwpfAirportProject
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BackToRegister_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage());
        }

        private void TogglePassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                // Show password
                PasswordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                TogglePasswordButton.Content = "👁";
            }
            else
            {
                // Hide password
                PasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
                TogglePasswordButton.Content = "🔒";
            }
        }
        private void Password_Changed(object sender, RoutedEventArgs e)
        {
            if (sender == PasswordTextBox && PasswordTextBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Password = PasswordTextBox.Text;
            }
            else if (sender == PasswordBox && PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordTextBox.Text = PasswordBox.Password;
            }
        }
    }
}
