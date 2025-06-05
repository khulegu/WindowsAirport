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
    public partial class FlightStatusForm : Form
    {
        private FlightApiClient _flightApiClient;
        public FlightStatusForm()
        {
            InitializeComponent();
            _flightApiClient = new FlightApiClient(new HttpClient { BaseAddress = new Uri("http://localhost:50866") });
            cmbStatus.DataSource = Enum.GetValues(typeof(FlightStatus));
            LoadFlightsAsync();
        }

        private async Task LoadFlightsAsync()
        {
            try
            {
                var flights = await _flightApiClient.GetAllFlightsAsync();
                dataGridView1.DataSource = flights;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load flights: " + ex.Message);
            }
        }

        private async void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a flight.");
                return;
            }

            var selectedFlight = (Flight)dataGridView1.CurrentRow.DataBoundItem;
            var newStatus = (FlightStatus)cmbStatus.SelectedItem;

            try
            {
                bool success = await _flightApiClient.UpdateFlightStatusAsync(selectedFlight.Id, newStatus);
                if (success)
                {
                    MessageBox.Show("Flight status updated.");
                    await LoadFlightsAsync();
                }
                else
                {
                    MessageBox.Show("Failed to update flight status.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message);
            }
        }
    }
}
