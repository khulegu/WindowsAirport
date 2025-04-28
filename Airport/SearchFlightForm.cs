namespace Airport
{
    public partial class SearchFlightForm : Form
    {
        public SearchFlightForm()
        {
            InitializeComponent();

            FillComboBoxValues(comboBoxSrc);
            FillComboBoxValues(comboBoxDest);
        }
        private void FillComboBoxValues(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            foreach (string airport in getAirports())
            {
                comboBox.Items.Add(airport);
            }
        }

        private List<string> getAirports()
        {
            return ["UBN", "PEK", "ICN"];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string src = comboBoxSrc.Text;
            string dest = comboBoxDest.Text;
            if (src == "" || dest == "")
            {
                MessageBox.Show("Please select both source and destination airports.");
                return;
            }

            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < 10; i++)
            {

                flowLayoutPanel1.Controls.Add(new FlightControl());
            }


            //// Assuming FlightManager is a class that handles flight data
            //FlightManager flightManager = new FlightManager();
            //List<Flight> flights = flightManager.SearchFlights(src, dest);
            //if (flights.Count == 0)
            //{
            //    MessageBox.Show("No flights found.");
            //    return;
            //}
            // Display the flights in a new form or a grid view
        }
    }
}
