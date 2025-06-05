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
            lblInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInstruction.Location = new Point(12, 24);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(171, 20);
            lblInstruction.TabIndex = 0;
            lblInstruction.Text = "Та суудлаа сонгоно уу:";
            // 
            // panelSeats
            // 
            panelSeats.AutoScroll = true;
            panelSeats.BackColor = SystemColors.ControlLightLight;
            panelSeats.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panelSeats.Location = new Point(89, 57);
            panelSeats.Name = "panelSeats";
            panelSeats.Size = new Size(217, 275);
            panelSeats.TabIndex = 1;
            // 
            // lblSelectedSeat
            // 
            lblSelectedSeat.AutoSize = true;
            lblSelectedSeat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedSeat.Location = new Point(12, 360);
            lblSelectedSeat.Name = "lblSelectedSeat";
            lblSelectedSeat.Size = new Size(80, 20);
            lblSelectedSeat.TabIndex = 0;
            lblSelectedSeat.Text = "Сонгосон:";
            // 
            // btnConfirm
            // 
            btnConfirm.AutoSize = true;
            btnConfirm.BackColor = Color.PaleGreen;
            btnConfirm.Location = new Point(3, 422);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(128, 30);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Баталгаажуулах";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = SystemColors.Control;
            btnBack.Location = new Point(146, 423);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 3;
            btnBack.Text = "Буцах";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // SeatSelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(402, 464);
            Controls.Add(btnBack);
            Controls.Add(btnConfirm);
            Controls.Add(lblSelectedSeat);
            Controls.Add(panelSeats);
            Controls.Add(lblInstruction);
            Name = "SeatSelectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Суудал сонгох";
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