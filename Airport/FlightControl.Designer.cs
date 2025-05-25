namespace Airport
{
    partial class FlightControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightControl));
            labelFlightNumber = new Label();
            labelDepartureTime = new Label();
            labelArrivalTime = new Label();
            labelPrice = new Label();
            pictureBox1 = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelFlightNumber
            // 
            labelFlightNumber.Anchor = AnchorStyles.Left;
            labelFlightNumber.AutoSize = true;
            labelFlightNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFlightNumber.Location = new Point(3, 42);
            labelFlightNumber.Name = "labelFlightNumber";
            labelFlightNumber.Size = new Size(107, 32);
            labelFlightNumber.TabIndex = 0;
            labelFlightNumber.Text = "UBN123";
            // 
            // labelDepartureTime
            // 
            labelDepartureTime.Anchor = AnchorStyles.None;
            labelDepartureTime.AutoSize = true;
            labelDepartureTime.Location = new Point(692, 42);
            labelDepartureTime.Name = "labelDepartureTime";
            labelDepartureTime.Size = new Size(71, 32);
            labelDepartureTime.TabIndex = 1;
            labelDepartureTime.Text = "18:00";
            // 
            // labelArrivalTime
            // 
            labelArrivalTime.Anchor = AnchorStyles.None;
            labelArrivalTime.AutoSize = true;
            labelArrivalTime.Location = new Point(825, 42);
            labelArrivalTime.Name = "labelArrivalTime";
            labelArrivalTime.Size = new Size(71, 32);
            labelArrivalTime.TabIndex = 2;
            labelArrivalTime.Text = "22:00";
            // 
            // labelPrice
            // 
            labelPrice.Anchor = AnchorStyles.None;
            labelPrice.AutoSize = true;
            labelPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPrice.Location = new Point(902, 42);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(84, 32);
            labelPrice.TabIndex = 3;
            labelPrice.Text = "1800$";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(769, 33);
            pictureBox1.MinimumSize = new Size(50, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(labelFlightNumber, 0, 0);
            tableLayoutPanel1.Controls.Add(labelPrice, 4, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 2, 0);
            tableLayoutPanel1.Controls.Add(labelArrivalTime, 3, 0);
            tableLayoutPanel1.Controls.Add(labelDepartureTime, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(989, 116);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // FlightControl
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(tableLayoutPanel1);
            Name = "FlightControl";
            Size = new Size(989, 116);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelFlightNumber;
        private Label labelDepartureTime;
        private Label labelArrivalTime;
        private Label labelPrice;
        private PictureBox pictureBox1;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
