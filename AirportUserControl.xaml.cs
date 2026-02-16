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
    /// Interaction logic for AirportUserControl.xaml
    /// </summary>
    public partial class AirportUserControl : UserControl
    {
        public Airports AirportData { get; set; }

        public event Action<Airports> AirportSelected;
        private void SelectAirport_Click(object sender, MouseButtonEventArgs e)
        {
            AirportSelected?.Invoke(AirportData);
        }

        public AirportUserControl(Airports airport)
        {
            InitializeComponent();
            AirportData = airport;

            airportNameText.Text = airport.AirportName;
            countryText.Text = airport.AirPortCountry.CountryName;
        }
        public void SetSelected(bool selected)
        {
            if (selected)
            {
                MainBorder.BorderBrush = Brushes.DeepSkyBlue;
                MainBorder.BorderThickness = new Thickness(2);
                MainBorder.Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.DeepSkyBlue,
                    BlurRadius = 20,
                    ShadowDepth = 0
                };
            }
            else
            {
                MainBorder.BorderBrush = Brushes.Transparent;
                MainBorder.BorderThickness = new Thickness(0);
                MainBorder.Effect = null;
            }
        }


    }
}
