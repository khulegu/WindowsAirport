namespace Airport
{
    partial class FlightStatusForm
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
            lblTitle = new Label();
            dataGridView1 = new DataGridView();
            cmbStatus = new ComboBox();
            lblChangeStatus = new Label();
            btnUpdateStatus = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(55, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Flights:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 46);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(748, 188);
            dataGridView1.TabIndex = 0;
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(124, 237);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(151, 28);
            cmbStatus.TabIndex = 2;
            // 
            // lblChangeStatus
            // 
            lblChangeStatus.AutoSize = true;
            lblChangeStatus.Location = new Point(12, 240);
            lblChangeStatus.Name = "lblChangeStatus";
            lblChangeStatus.Size = new Size(106, 20);
            lblChangeStatus.TabIndex = 3;
            lblChangeStatus.Text = "Change Status:";
            // 
            // btnUpdateStatus
            // 
            btnUpdateStatus.Location = new Point(12, 287);
            btnUpdateStatus.Name = "btnUpdateStatus";
            btnUpdateStatus.Size = new Size(94, 29);
            btnUpdateStatus.TabIndex = 4;
            btnUpdateStatus.Text = "Update";
            btnUpdateStatus.UseCompatibleTextRendering = true;
            btnUpdateStatus.UseVisualStyleBackColor = true;
            btnUpdateStatus.Click += btnUpdateStatus_Click;
            // 
            // FlightStatusForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnUpdateStatus);
            Controls.Add(lblChangeStatus);
            Controls.Add(cmbStatus);
            Controls.Add(dataGridView1);
            Controls.Add(lblTitle);
            Name = "FlightStatusForm";
            Text = "FlightStatusForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dataGridView1;
        private ComboBox cmbStatus;
        private Label lblChangeStatus;
        private Button btnUpdateStatus;
    }
}