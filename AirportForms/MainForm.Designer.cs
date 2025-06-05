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
            lblPassport.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPassport.Location = new Point(28, 19);
            lblPassport.Name = "lblPassport";
            lblPassport.Size = new Size(190, 25);
            lblPassport.TabIndex = 0;
            lblPassport.Text = "Пасспортны дугаар:";
            // 
            // txtPassport
            // 
            txtPassport.Location = new Point(28, 47);
            txtPassport.Name = "txtPassport";
            txtPassport.Size = new Size(361, 27);
            txtPassport.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.AutoSize = true;
            btnSearch.Location = new Point(28, 125);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(119, 30);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Тасалбар хайх";
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
            panelContainer.Location = new Point(12, 161);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(692, 277);
            panelContainer.TabIndex = 4;
            panelContainer.Paint += panelContainer_Paint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(721, 450);
            Controls.Add(panelContainer);
            Controls.Add(lblStatus);
            Controls.Add(btnSearch);
            Controls.Add(txtPassport);
            Controls.Add(lblPassport);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Онгоцны билет захиалгын систем";
            KeyDown += textPasword_keydown;
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