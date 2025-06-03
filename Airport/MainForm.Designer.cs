namespace Airport
{
    partial class MainForm
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
            lblPassport = new Label();
            txtPassport = new TextBox();
            btnSearch = new Button();
            lblStatus = new Label();
            panelContainer = new Panel();
            SuspendLayout();
            // 
            // lblPassport
            // 
            lblPassport.AutoSize = true;
            lblPassport.Location = new Point(12, 9);
            lblPassport.Name = "lblPassport";
            lblPassport.Size = new Size(163, 20);
            lblPassport.TabIndex = 0;
            lblPassport.Text = "Enter Passport Number:";
            // 
            // txtPassport
            // 
            txtPassport.Location = new Point(12, 32);
            txtPassport.Name = "txtPassport";
            txtPassport.Size = new Size(361, 27);
            txtPassport.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(12, 65);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search Ticket";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 97);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 3;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelContainer
            // 
            panelContainer.Location = new Point(12, 134);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(758, 304);
            panelContainer.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelContainer);
            Controls.Add(lblStatus);
            Controls.Add(btnSearch);
            Controls.Add(txtPassport);
            Controls.Add(lblPassport);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPassport;
        private TextBox txtPassport;
        private Button btnSearch;
        private Label lblStatus;
        private Panel panelContainer;
    }
}