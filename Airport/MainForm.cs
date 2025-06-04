using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airport
{
    public partial class MainForm : Form
    {
        private CheckInApiClient _apiClient;

        public MainForm()
        {
            InitializeComponent();
            _apiClient = new CheckInApiClient("http://localhost:50866/"); // Use your API URL

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string passport = txtPassport.Text;
            if (string.IsNullOrEmpty(passport))
            {
                MessageBox.Show("Please enter a passport number.");
                return;
            }

            var booking = await _apiClient.GetBookingAsync(passport);
            if (booking == null)
            {
                lblStatus.Text = "Passenger not found";
                return;
            }

            if (booking.IsCheckedIn)
            {
                var flight = await _apiClient.GetFlightDetailsAsync(booking.FlightId);
                if (flight == null)
                {
                    MessageBox.Show("Flight not found");
                }
                new BoardingPassForm(booking,flight).ShowDialog();
                return;
            }

            lblStatus.Text = $"Found: {booking.PassengerName} on flight {booking.FlightNumber}";
            new SeatSelectionForm(booking.FlightId, passport).ShowDialog();
        }
    }
}
