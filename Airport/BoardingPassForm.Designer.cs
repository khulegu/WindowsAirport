namespace Airport
{
    partial class BoardingPassForm
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
            lblName = new Label();
            lblPassport = new Label();
            lblFlight = new Label();
            lblSeat = new Label();
            btnPrint = new Button();
            btnFinish = new Button();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 9);
            lblName.Name = "lblName";
            lblName.Size = new Size(56, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name: ";
            // 
            // lblPassport
            // 
            lblPassport.AutoSize = true;
            lblPassport.Location = new Point(12, 40);
            lblPassport.Name = "lblPassport";
            lblPassport.Size = new Size(71, 20);
            lblPassport.TabIndex = 1;
            lblPassport.Text = "Passport: ";
            // 
            // lblFlight
            // 
            lblFlight.AutoSize = true;
            lblFlight.Location = new Point(12, 76);
            lblFlight.Name = "lblFlight";
            lblFlight.Size = new Size(53, 20);
            lblFlight.TabIndex = 2;
            lblFlight.Text = "Flight: ";
            // 
            // lblSeat
            // 
            lblSeat.AutoSize = true;
            lblSeat.Location = new Point(12, 110);
            lblSeat.Name = "lblSeat";
            lblSeat.Size = new Size(45, 20);
            lblSeat.TabIndex = 3;
            lblSeat.Text = "Seat: ";
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(12, 409);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(94, 29);
            btnPrint.TabIndex = 4;
            btnPrint.Text = "Print Boarding Pass";
            btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(230, 409);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(94, 29);
            btnFinish.TabIndex = 5;
            btnFinish.Text = "Close";
            btnFinish.UseVisualStyleBackColor = true;
            // 
            // printDocument1
            // 

            // 
            // BoardingPassForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(336, 450);
            Controls.Add(btnFinish);
            Controls.Add(btnPrint);
            Controls.Add(lblSeat);
            Controls.Add(lblFlight);
            Controls.Add(lblPassport);
            Controls.Add(lblName);
            Name = "BoardingPassForm";
            Text = "BoardingPassForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblPassport;
        private Label lblFlight;
        private Label lblSeat;
        private Button btnPrint;
        private Button btnFinish;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}