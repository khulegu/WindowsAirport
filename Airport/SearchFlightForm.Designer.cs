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
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(660, 55);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(400, 39);
            dateTimePicker1.TabIndex = 0;
            // 
            // comboBoxSrc
            // 
            comboBoxSrc.FormattingEnabled = true;
            comboBoxSrc.Location = new Point(61, 57);
            comboBoxSrc.Name = "comboBoxSrc";
            comboBoxSrc.Size = new Size(242, 40);
            comboBoxSrc.TabIndex = 1;
            comboBoxSrc.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBoxDest
            // 
            comboBoxDest.FormattingEnabled = true;
            comboBoxDest.Location = new Point(385, 57);
            comboBoxDest.Name = "comboBoxDest";
            comboBoxDest.Size = new Size(242, 40);
            comboBoxDest.TabIndex = 2;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(1083, 48);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(150, 46);
            buttonSearch.TabIndex = 3;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(61, 121);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1249, 618);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 22);
            label1.Name = "label1";
            label1.Size = new Size(87, 32);
            label1.TabIndex = 5;
            label1.Text = "Source";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(385, 22);
            label2.Name = "label2";
            label2.Size = new Size(136, 32);
            label2.TabIndex = 6;
            label2.Text = "Destination";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 810);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(buttonSearch);
            Controls.Add(comboBoxDest);
            Controls.Add(comboBoxSrc);
            Controls.Add(dateTimePicker1);
            Name = "Form1";
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
    }
}
