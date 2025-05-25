using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirportLib.Models;

namespace Airport
{
    public partial class FlightControl : UserControl
    {
        private Flight flight;
        public FlightControl(Flight flight)
        {
            this.flight = flight;
            InitializeComponent();

            labelFlightNumber.Text = flight.FlightNumber;
            labelDepartureTime.Text = flight.DepartureTime.ToString("HH:mm");
            labelArrivalTime.Text = flight.ArrivalTime.ToString("HH:mm");
            labelPrice.Text = flight.FlightNumber.ToString();
        }
    }
}
