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
    /// Interaction logic for DeparturesPage.xaml
    /// </summary>
    public partial class DeparturesPage : Page
    {
        Airportsapi airportapi = new Airportsapi();
        AirportsList aList;

        private Airports PreSelectAirport;

        public DeparturesPage()
        {
            InitializeComponent();
            LoadAirports();
        }

        public DeparturesPage(Airports airport)
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

            // ✅ preselect AFTER airports are loaded
            if (PreSelectAirport != null)
            {
                for (int i = 0; i < aList.Count; i++)
                {
                    if (aList[i].AirportName == PreSelectAirport.AirportName)
                    {
                        airportComboBox.SelectedIndex = i; // triggers SelectAirport automatically
                        break;
                    }
                }
            }
        }

        // Sort ascending
        private int CompareTakeOffTimeAscending(Flight a, Flight b)
        {
            return a.TakeOffTime.CompareTo(b.TakeOffTime);
        }

        // Sort descending
        private int CompareTakeOffTimeDescending(Flight a, Flight b)
        {
            return b.TakeOffTime.CompareTo(a.TakeOffTime);
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

            // Filter departures (from selected airport)
            foreach (Flight f in allFlights)
            {
                if (f.CurrentAirport != null)
                {
                    if (f.CurrentAirport.AirportName == selectedAirport.AirportName)
                    {
                        bool isUpcoming = f.TakeOffTime >= DateTime.Now;

                        if (showUpcoming && isUpcoming)
                            filtered.Add(f);

                        if (!showUpcoming && !isUpcoming)
                            filtered.Add(f);
                    }
                }
            }

            // Sort
            if (showUpcoming)
            {
                filtered.Sort(CompareTakeOffTimeAscending);
                infoText.Text = "Upcoming departures from: " + selectedAirport.AirportName;
            }
            else
            {
                filtered.Sort(CompareTakeOffTimeDescending);
                infoText.Text = "Past departures from: " + selectedAirport.AirportName;
            }

            flightsGrid.ItemsSource = filtered;
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Homepage());
        }
    }
}
