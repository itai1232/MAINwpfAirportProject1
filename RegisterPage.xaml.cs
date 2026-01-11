using AirportService;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    /// 

   public partial class RegisterPage : Page
    {
        List<string> countriesList = new List<string>();
        Airportsapi airportsapi=new Airportsapi();
        CountriesList cList;
        public RegisterPage()
        {
            InitializeComponent();
            SelectAllCountries();

        }

        public async Task SelectAllCountries()
        {
            //פה הייתה בעיה והמורה רותי עזרה
           // try {
                cList = await (airportsapi.GetAllCountries());
           // }
         // catch(Exception e) { throw new Exception(e.Message); }
            
            foreach (Countries country in cList) 
            { 
                countriesList.Add(country.CountryName);
            }
            countriesscrollview.ItemsSource = countriesList;
        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }

        private void SelectCountry(object sender, SelectionChangedEventArgs e)
        {
            if (countriesscrollview.SelectedIndex >= 0)
            {
                Countries c = cList[countriesscrollview.SelectedIndex];           
            }
        }
        private void TogglePassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                // Show password
                PasswordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                TogglePasswordButton.Content = "🔒";
            }
            else
            {
                // Hide password
                PasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
                TogglePasswordButton.Content = "👁";
            }
        }
        private void ToggleConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordBox.Visibility == Visibility.Visible)
            {
                // Show password
                ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
                ConfirmPasswordBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordTextBox.Visibility = Visibility.Visible;
                ToggleConfirmPasswordButton.Content = "🔒";
            }
            else
            {
                // Hide password
                ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
                ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordBox.Visibility = Visibility.Visible;
                ToggleConfirmPasswordButton.Content = "👁";
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
        private void ConfirmPassword_Changed(object sender, RoutedEventArgs e)
        {
            if (sender == ConfirmPasswordTextBox && ConfirmPasswordTextBox.Visibility == Visibility.Visible)
            {
                ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
            }
            else if (sender == ConfirmPasswordBox && ConfirmPasswordBox.Visibility == Visibility.Visible)
            {
                ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
            }
        }
    }
}
