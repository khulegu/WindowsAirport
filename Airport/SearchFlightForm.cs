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
    }
}
