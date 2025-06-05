using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirportServer.Models;

namespace Airport
{
    public partial class BoardingPassForm : Form
    {
        private readonly BookingResponse _booking;
        private readonly FlightDetails _flight;

        public BoardingPassForm(BookingResponse booking, FlightDetails flight)
        {
            InitializeComponent();
            _booking = booking;
            _flight = flight;
            LoadInfo();
        }

        private void LoadInfo()
        {
            if (_booking.PassengerDetails == null)
            {
                lblName.Text = "Name: Unknown";
            }
            else
            {
                lblName.Text =
                    $"Name: {_booking.PassengerDetails.FirstName} {_booking.PassengerDetails.LastName}";
            }

            lblPassport.Text = $"PASSPORT NUMBER: {_booking.PassportNumber}";
            lblFlight.Text = $"FLIGHT: {_flight.FlightNumber}";
            lblSeat.Text = $"SEAT: {_booking.AssignedSeatNumber}";
            lblOrigin.Text = $"ORIGIN: {_flight.DepartureCity}";
            lblDestination.Text = $"DESTINATION: {_flight.ArrivalCity}";
            lblDate.Text = $"DATE: {_flight.DepartureTime:yyyy/MM/dd}";
            lblTime.Text = $"TIME: {_flight.DepartureTime:HH:mm}";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (s, ev) =>
            {
                Font headerFont = new Font("Arial", 16, FontStyle.Bold);
                Font font = new Font("Arial", 12);
                float y = 100;

                ev.Graphics.DrawString("BOARDING PASS", headerFont, Brushes.Black, 100, y);
                y += 40;

                ev.Graphics.DrawString(lblName.Text, font, Brushes.Black, 100, y);
                y += 30;

                ev.Graphics.DrawString(lblPassport.Text, font, Brushes.Black, 100, y);
                y += 30;

                ev.Graphics.DrawString(
                    lblOrigin.Text + "     " + lblDestination.Text,
                    font,
                    Brushes.Black,
                    100,
                    y
                );
                y += 30;

                ev.Graphics.DrawString(
                    lblDate.Text + "     " + lblTime.Text,
                    font,
                    Brushes.Black,
                    100,
                    y
                );
                y += 30;

                ev.Graphics.DrawString(
                    lblFlight.Text + "     " + lblSeat.Text,
                    font,
                    Brushes.Black,
                    100,
                    y
                );
            };

            try
            {
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = printDoc;
                preview.ShowDialog();
                printDoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing failed: " + ex.Message);
            }
        }

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
