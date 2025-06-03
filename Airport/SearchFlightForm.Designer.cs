namespace Airport
{
    partial class SearchFlightForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dateTimePicker1 = new DateTimePicker();
            comboBoxSrc = new ComboBox();
            comboBoxDest = new ComboBox();
            buttonSearch = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(359, 34);
            dateTimePicker1.Margin = new Padding(2, 2, 2, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(248, 27);
            dateTimePicker1.TabIndex = 0;
            // 
            // comboBoxSrc
            // 
            comboBoxSrc.FormattingEnabled = true;
            comboBoxSrc.Location = new Point(38, 36);
            comboBoxSrc.Margin = new Padding(2, 2, 2, 2);
            comboBoxSrc.Name = "comboBoxSrc";
            comboBoxSrc.Size = new Size(150, 28);
            comboBoxSrc.TabIndex = 1;
            comboBoxSrc.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBoxDest
            // 
            comboBoxDest.FormattingEnabled = true;
            comboBoxDest.Location = new Point(198, 36);
            comboBoxDest.Margin = new Padding(2, 2, 2, 2);
            comboBoxDest.Name = "comboBoxDest";
            comboBoxDest.Size = new Size(150, 28);
            comboBoxDest.TabIndex = 2;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(714, 32);
            buttonSearch.Margin = new Padding(2, 2, 2, 2);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(92, 29);
            buttonSearch.TabIndex = 3;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(38, 76);
            flowLayoutPanel1.Margin = new Padding(2, 2, 2, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(769, 386);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 14);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 5;
            label1.Text = "Source";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 14);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 6;
            label2.Text = "Destination";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(359, 14);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(112, 20);
            label3.TabIndex = 7;
            label3.Text = "Departure Date";
            // 
            // SearchFlightForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 506);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(buttonSearch);
            Controls.Add(comboBoxDest);
            Controls.Add(comboBoxSrc);
            Controls.Add(dateTimePicker1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "SearchFlightForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private ComboBox comboBoxSrc;
        private ComboBox comboBoxDest;
        private Button buttonSearch;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
