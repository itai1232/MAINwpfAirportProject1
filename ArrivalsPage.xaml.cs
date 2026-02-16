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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAINwpfAirportProject
{
    /// <summary>
    /// Interaction logic for ArrivalsPage.xaml
    /// </summary>
    public partial class ArrivalsPage : Page
    {
        Airportsapi airportapi = new Airportsapi();
        AirportsList aList;

        private Airports PreSelectAirport; // airport that came from FindAirport (can be null)

        public ArrivalsPage()
        {
            InitializeComponent();
            LoadAirports();
        }

        public ArrivalsPage(Airports airport)
        {
            InitializeComponent();
            PreSelectAirport = airport;
            LoadAirports();
        }

        private async Task LoadAirports()
        {
            aList = await airportapi.GetAllAirports();

            airportComboBox.Items.Clear();
            foreach (Airports a in aList)
                airportComboBox.Items.Add(a.AirportName);

            modeComboBox.Items.Clear();
            modeComboBox.Items.Add("Upcoming"); // future
            modeComboBox.Items.Add("Past");     // past
            modeComboBox.SelectedIndex = 0;

            // ✅ pre-select AFTER airports list is loaded
            if (PreSelectAirport != null)
            {
                for (int i = 0; i < aList.Count; i++)
                {
                    if (aList[i].AirportName == PreSelectAirport.AirportName)
                    {
                        airportComboBox.SelectedIndex = i; // will trigger SelectAirport automatically
                        break;
                    }
                }
            }
        }

        private int CompareArrivalTimeAscending(Flight a, Flight b)
        {
            return a.ArrivalTime.CompareTo(b.ArrivalTime);
        }

        private int CompareArrivalTimeDescending(Flight a, Flight b)
        {
            return b.ArrivalTime.CompareTo(a.ArrivalTime);
        }

        private async void SelectAirport(object sender, SelectionChangedEventArgs e)
        {
            if (airportComboBox.SelectedIndex < 0)
                return;

            if (modeComboBox.SelectedIndex < 0)
                return;

            Airports selectedAirport = aList[airportComboBox.SelectedIndex];

            bool showUpcoming = (modeComboBox.SelectedIndex == 0);

            FlightList allFlights = await airportapi.GetAllFlights();

            List<Flight> filtered = new List<Flight>();

            // Filter flights (arrivals to selected airport)
            foreach (Flight f in allFlights)
            {
                if (f.DestinationAirport != null)
                {
                    if (f.DestinationAirport.AirportName == selectedAirport.AirportName)
                    {
                        bool isUpcoming = f.ArrivalTime >= DateTime.Now;

                        if (showUpcoming && isUpcoming)
                            filtered.Add(f);

                        if (!showUpcoming && !isUpcoming)
                            filtered.Add(f);
                    }
                }
            }

            // Sort flights
            if (showUpcoming)
            {
                filtered.Sort(CompareArrivalTimeAscending);
                infoText.Text = "Upcoming arrivals to: " + selectedAirport.AirportName;
            }
            else
            {
                filtered.Sort(CompareArrivalTimeDescending);
                infoText.Text = "Past arrivals to: " + selectedAirport.AirportName;
            }

            flightsGrid.ItemsSource = filtered;
        }

        private void BackToHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Homepage());
        }
    }
}
