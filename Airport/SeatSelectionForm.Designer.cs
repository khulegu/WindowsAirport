namespace Airport
{
    partial class SeatSelectionForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblInstruction = new Label();
            panelSeats = new Panel();
            lblSelectedSeat = new Label();
            btnConfirm = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Location = new Point(12, 9);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(95, 20);
            lblInstruction.TabIndex = 0;
            lblInstruction.Text = "Select a seat:";
            // 
            // panelSeats
            // 
            panelSeats.Location = new Point(12, 32);
            panelSeats.Name = "panelSeats";
            panelSeats.Size = new Size(595, 275);
            panelSeats.TabIndex = 1;
            // 
            // lblSelectedSeat
            // 
            lblSelectedSeat.AutoSize = true;
            lblSelectedSeat.Location = new Point(12, 325);
            lblSelectedSeat.Name = "lblSelectedSeat";
            lblSelectedSeat.Size = new Size(109, 20);
            lblSelectedSeat.TabIndex = 0;
            lblSelectedSeat.Text = "Selected: None";
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(12, 348);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(94, 29);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Confirm Selection";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(112, 348);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // SeatSelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnConfirm);
            Controls.Add(lblSelectedSeat);
            Controls.Add(panelSeats);
            Controls.Add(lblInstruction);
            Name = "SeatSelectionForm";
            Text = "SeatSelectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label lblInstruction;
        private Panel panelSeats;
        private Label lblSelectedSeat;
        private Button btnConfirm;
        private Button btnBack;
    }
}