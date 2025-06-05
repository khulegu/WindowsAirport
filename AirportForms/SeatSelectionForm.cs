using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirportServer.Models;

namespace Airport
{
    public partial class SeatSelectionForm : Form
    {
        private SeatInfo? selectedSeat;
        private readonly CheckInApiClient _apiClient;
        private readonly int _flightId;
        private readonly string _passport;

        private List<SeatInfo> _currentSeats = new();
        public SeatSelectionForm(int flightId, string passport)
        {
            InitializeComponent();
            _flightId = flightId;
            _passport = passport;
            _apiClient = new CheckInApiClient("http://localhost:50866/"); // or use config
            LoadSeats();
        }

        private async void LoadSeats()
        {
            var seats = await _apiClient.GetSeatsAsync(_flightId);
            if (seats == null)
            {
                MessageBox.Show("Failed to load seat map.");
                return;
            }

            DisplaySeats(seats);
        }

        private void DisplaySeats(List<SeatInfo> seats)
        {
            panelSeats.Controls.Clear();

            int cols = 3;
            int size = 50;
            int margin = 5;

            for (int i = 0; i < seats.Count; i++)
            {
                var seat = seats[i];
                Button btn = new Button();
                btn.Text = seat.SeatNumber;
                btn.Width = btn.Height = size;
                btn.BackColor = seat.IsOccupied ? Color.Red : Color.Green;
                btn.Enabled = !seat.IsOccupied;

                btn.Click += (s, e) =>
                {
                    selectedSeat = seat;
                    lblSelectedSeat.Text = $"Сонгосон: {seat.SeatNumber}";
                    lblInstruction.Font = new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Bold,
                        GraphicsUnit.Point,
                        0
                    );
                    ;
                };

                btn.Location = new Point(
                    (i % cols) * (size + margin),
                    (i / cols) * (size + margin)
                );
                panelSeats.Controls.Add(btn);
            }
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selectedSeat == null)
            {
                MessageBox.Show("Та суудлаа эхэлж сонгоно уу.");
                return;
            }
            MessageBox.Show($"Пасспортны дугаар: {_passport}, Нислэгиийн дугаар: {_flightId}");
            var request = new AssignSeatRequest
            {
                FlightId = _flightId,
                PassportNumber = _passport,
                SeatNumber = selectedSeat.SeatNumber,
            };
            var (success, message) = await _apiClient.AssignSeatAsync(request);

            if (!success)
            {
                MessageBox.Show(
                    $"Суудал бүртгүүлэлт амжилтгүй: {message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            var updatedBooking = await _apiClient.GetBookingAsync(_passport);
            if (updatedBooking == null)
            {
                MessageBox.Show("Зорчигчийн мэдээллийг оруулахад алдаа гарлаа.");
                return;
            }
            var flight = await _apiClient.GetFlightDetailsAsync(updatedBooking.FlightId);
            if (flight == null)
            {
                MessageBox.Show("Нислэг олдсонгүй");
            }
            BoardingPassForm bpForm = new BoardingPassForm(updatedBooking, flight);
            bpForm.ShowDialog();
            this.Close();
        }

        private async void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
