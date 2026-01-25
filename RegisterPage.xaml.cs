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
        List<string> personsemailList = new List<string>();
        List<string> personsphoneList = new List<string>();
        CountriesList cList;
        PersonList pList;
        
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
        public async Task SelectAllPersons()
        {
            pList = await (airportsapi.GetAllPersons());
            foreach (Person person in pList)
            {
                personsemailList.Add(person.Email);
                personsphoneList.Add(person.Telephone);
            }
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
       

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;
            // בשביל מספרי טלפון גלובלים + בהתחלה
            if (phone.StartsWith("+"))
                phone = phone.Substring(1);
            // בדיקה שכל השאר ספרות ולא דברים אחרים
            if (!phone.All(char.IsDigit))
                return false;
            // אורך גלובלי מקובל
            return phone.Length >= 7 && phone.Length <= 15;
        }
        private void ShowError(TextBox box, TextBlock error, string message)
        {
            box.BorderBrush = Brushes.Red;
            error.Text = message;
            error.Visibility = Visibility.Visible;
        }

        private void ClearError(TextBox box, TextBlock error)
        {
            box.ClearValue(Border.BorderBrushProperty);
            error.Visibility = Visibility.Hidden;
        }
        private bool PhoneExists(string phone)
        {
            return personsphoneList.Find(u => u == phone) != null;
        }
        private bool EmailExists(string email)
        {
            return personsemailList.Find(u => u == email) != null;
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;

            SelectAllPersons();
            ClearError(FirstNameTextBox, FirstNameError);
            ClearError(LastNameTextBox, LastNameError);
            ClearError(EmailTextBox, EmailError);
            ClearError(PhoneTextBox, PhoneError); 
            

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
            if (EmailExists(EmailTextBox.Text))
            {
                ShowError(EmailTextBox, EmailError, "Email already exists");
                isValid = false;
            }
            
            if (!IsValidPhone(PhoneTextBox.Text))
            {
                ShowError(PhoneTextBox, PhoneError, "Invalid phone");
                isValid = false;
            }
             if (PhoneExists(PhoneTextBox.Text))
            {
                ShowError(PhoneTextBox, PhoneError, "Phone already exists");
                isValid = false;
            }

            if (!isValid)
                return;
            Passenger newPassenger = new Passenger
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Email = EmailTextBox.Text,
                Telephone = PhoneTextBox.Text,
                PersonCountry = cList[countriesscrollview.SelectedIndex] 
            }; 

            InsertAPassenger(newPassenger);
            
            
        }
        public async void InsertAPassenger(Passenger passenger)
        {
            int x= await airportsapi.InsertAPassenger(passenger);
            if(x>0)
                MessageBox.Show("Registration successful ✔");
            else
                MessageBox.Show("Registration not successful");

        }
        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Homepage());
        }

    }
}
