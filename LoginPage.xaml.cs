using AirportService;
using Model;
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
        Airportsapi airportsapi = new Airportsapi();
        List<string> personsemailList = new List<string>();
        List<string> personsfirstnameList = new List<string>();
        List<string> personslastnameList = new List<string>();
        PersonList pList;
        public LoginPage()
        {
            InitializeComponent();
        }
        public async Task SelectAllPersons()
        {
            pList = await (airportsapi.GetAllPersons());
            foreach (Person person in pList)
            {
                personsemailList.Add(person.Email);
                personsfirstnameList.Add(person.FirstName);
                personslastnameList.Add(person.LastName);
            }
        }
        private void ClearError(TextBox box, TextBlock error)
        {
            box.ClearValue(Border.BorderBrushProperty);
            error.Visibility = Visibility.Hidden;
        }
        private void ShowError(TextBox box, TextBlock error, string message)
        {
            box.BorderBrush = Brushes.Red;
            error.Text = message;
            error.Visibility = Visibility.Visible;
        }
        private bool IsValidName(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || text.Length < 2)
                return false;

            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                    return false;
            }

            return true;
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Contains("@") && email.Contains(".");
        }

        private void BackToRegister_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage());
        }
        private bool UserExists(string firstname, string lastname, string email)
        {
            return pList.Find(u => u.FirstName.ToLower() == firstname && u.LastName.ToLower() ==lastname&& u.Email.ToLower() ==email) != null;
        }
       

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SelectAllPersons();
            bool isValid = true;
            ClearError(FirstNameTextBox, FirstNameError);
            ClearError(LastNameTextBox, LastNameError);
            ClearError(EmailTextBox, EmailError);

            if (!IsValidName(FirstNameTextBox.Text))
            {
                ShowError(FirstNameTextBox, FirstNameError, "Invalid first name");
                isValid = false;
            }

            if (!IsValidName(LastNameTextBox.Text))
            {
                ShowError(LastNameTextBox, LastNameError, "Invalid last name");
                isValid = false;
            }
            if (!IsValidEmail(EmailTextBox.Text))
            {
                ShowError(EmailTextBox, EmailError, "Invalid email");
                isValid = false;
            }
            if (!isValid)
            {
                MessageBox.Show("fix the errors");
                return;
            }
                
            if(UserExists(FirstNameTextBox.Text.ToLower(),LastNameTextBox.Text.ToLower(),EmailTextBox.Text.ToLower())==false)
            {
                MessageBox.Show("user does not exist");
                return;
            }
            MessageBox.Show("user exists");

        }
        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Homepage());
        }


    }
}
