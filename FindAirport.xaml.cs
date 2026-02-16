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
    /// Interaction logic for FindAirport.xaml
    /// </summary>
    public partial class FindAirport : Page
    {
        Airportsapi airportapi = new Airportsapi();
        AirportsList aList;
        Airports selectedAirport;
        AirportUserControl currentlySelectedCard;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            aList = await airportapi.GetAllAirports();
        }
        public FindAirport()
        {
            InitializeComponent();
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = ((TextBox)sender).Text.ToLower();

            resultsPanel.Children.Clear();

            foreach (Airports airport in aList)
            {
                if (airport.AirportName.ToLower().StartsWith(input))
                {
                    AirportUserControl card = new AirportUserControl(airport);
                    card.AirportSelected += AirportChosen;

                    resultsPanel.Children.Add(card);
                }
            }
        }
       
        private void AirportChosen(Airports airport)
        {
            selectedAirport = airport;
            if (currentlySelectedCard != null)
                currentlySelectedCard.SetSelected(false);
            foreach (AirportUserControl card in resultsPanel.Children)
            {
                if (card.AirportData == airport)
                {
                    currentlySelectedCard = card;
                    card.SetSelected(true);
                    break;
                }
            }

            arrivalsButton.IsEnabled = true;
            departuresButton.IsEnabled = true;
        }
        private void Arrivals_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArrivalsPage(selectedAirport));
        }
        private void Departures_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeparturesPage(selectedAirport));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
}
