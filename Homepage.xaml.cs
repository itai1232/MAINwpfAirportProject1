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
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new RegisterPage());
        }

        private void AirportSearch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FindAirport());
        }

        private void Departures_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DeparturesPage());
        }

        private void Arrivals_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ArrivalsPage());
        }

        private void FlightStatus_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
