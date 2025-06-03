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
    public partial class BoardingPassForm : Form
    {
        public BoardingPassForm(/*Passenger passenger*/)
        {
            InitializeComponent();
            LoadInfo();
        }

        private void LoadInfo()
        {
            // Stub: Fill labels with passenger and flight info
            lblName.Text = "Name: John Doe";
            lblPassport.Text = "Passport: AA123456";
            lblFlight.Text = "Flight: XY789";
            lblSeat.Text = "Seat: 12";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // TODO: Use PrintDocument to print boarding pass
            MessageBox.Show("Printing boarding pass...");
        }
    }
}
