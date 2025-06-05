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
            btnFinish = new Button();
            lblOrigin = new Label();
            lblDestination = new Label();
            lblDate = new Label();
            lblTime = new Label();
            btnPrint = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 9);
            lblName.Name = "lblName";
            lblName.Size = new Size(129, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Зорчигчийн нэр: ";
            // 
            // lblPassport
            // 
            lblPassport.AutoSize = true;
            lblPassport.Location = new Point(12, 40);
            lblPassport.Name = "lblPassport";
            lblPassport.Size = new Size(152, 20);
            lblPassport.TabIndex = 1;
            lblPassport.Text = "Пасспортны дугаар: ";
            // 
            // lblFlight
            // 
            lblFlight.AutoSize = true;
            lblFlight.Location = new Point(12, 138);
            lblFlight.Name = "lblFlight";
            lblFlight.Size = new Size(141, 20);
            lblFlight.TabIndex = 2;
            lblFlight.Text = "Нислэгийн дугаар: ";
            // 
            // lblSeat
            // 
            lblSeat.AutoSize = true;
            lblSeat.Location = new Point(240, 138);
            lblSeat.Name = "lblSeat";
            lblSeat.Size = new Size(121, 20);
            lblSeat.TabIndex = 3;
            lblSeat.Text = "Суудлын дугаар:";
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(240, 192);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(94, 29);
            btnFinish.TabIndex = 5;
            btnFinish.Text = "Хаах";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Click += btnFinish_Click;
            // 
            // lblOrigin
            // 
            lblOrigin.AutoSize = true;
            lblOrigin.Location = new Point(12, 73);
            lblOrigin.Name = "lblOrigin";
            lblOrigin.Size = new Size(108, 20);
            lblOrigin.TabIndex = 6;
            lblOrigin.Text = "Хөдлөх газар: ";
            // 
            // lblDestination
            // 
            lblDestination.AutoSize = true;
            lblDestination.Location = new Point(240, 73);
            lblDestination.Name = "lblDestination";
            lblDestination.Size = new Size(97, 20);
            lblDestination.TabIndex = 7;
            lblDestination.Text = "Хүрэх газар: ";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(12, 104);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(116, 20);
            lblDate.TabIndex = 8;
            lblDate.Text = "Хөдлөх он сар: ";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(240, 104);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(37, 20);
            lblTime.TabIndex = 9;
            lblTime.Text = "Цаг:";
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(12, 192);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(94, 29);
            btnPrint.TabIndex = 10;
            btnPrint.Text = "Хэвлэх";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // BoardingPassForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(513, 294);
            Controls.Add(btnPrint);
            Controls.Add(lblTime);
            Controls.Add(lblDate);
            Controls.Add(lblDestination);
            Controls.Add(lblOrigin);
            Controls.Add(btnFinish);
            Controls.Add(lblSeat);
            Controls.Add(lblFlight);
            Controls.Add(lblPassport);
            Controls.Add(lblName);
            Name = "BoardingPassForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Нислэгийн мэдээлэл";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblPassport;
        private Label lblFlight;
        private Label lblSeat;
        private Button btnFinish;
        private Label lblOrigin;
        private Label lblDestination;
        private Label lblDate;
        private Label lblTime;
        private Button btnPrint;
    }
}