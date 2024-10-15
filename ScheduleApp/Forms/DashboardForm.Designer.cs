namespace ScheduleApp.Forms
{
    partial class DashboardForm
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
            btnCustomerManagement = new Button();
            btnAppointmentManagement = new Button();
            btnShowReports = new Button();
            lblWelcome = new Label();
            SuspendLayout();
            // 
            // btnCustomerManagement
            // 
            btnCustomerManagement.Location = new Point(187, 102);
            btnCustomerManagement.Name = "btnCustomerManagement";
            btnCustomerManagement.Size = new Size(200, 50);
            btnCustomerManagement.TabIndex = 0;
            btnCustomerManagement.Text = "Manage Customers";
            btnCustomerManagement.UseVisualStyleBackColor = true;
            btnCustomerManagement.Click += btnCustomerManagement_Click;
            // 
            // btnAppointmentManagement
            // 
            btnAppointmentManagement.Location = new Point(187, 193);
            btnAppointmentManagement.Name = "btnAppointmentManagement";
            btnAppointmentManagement.Size = new Size(200, 50);
            btnAppointmentManagement.TabIndex = 1;
            btnAppointmentManagement.Text = "Manage Appointments";
            btnAppointmentManagement.UseVisualStyleBackColor = true;
            btnAppointmentManagement.Click += btnAppointmentManagement_Click;
            // 
            // btnShowReports
            // 
            btnShowReports.Location = new Point(187, 281);
            btnShowReports.Name = "btnShowReports";
            btnShowReports.Size = new Size(200, 50);
            btnShowReports.TabIndex = 2;
            btnShowReports.Text = "View Reports";
            btnShowReports.UseVisualStyleBackColor = true;
            btnShowReports.Click += btnShowReports_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(206, 22);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(157, 37);
            lblWelcome.TabIndex = 3;
            lblWelcome.Text = "Dashboard";
            // 
            // DashboardForm
            // 
            ClientSize = new Size(642, 436);
            Controls.Add(btnCustomerManagement);
            Controls.Add(btnAppointmentManagement);
            Controls.Add(btnShowReports);
            Controls.Add(lblWelcome);
            Name = "DashboardForm";
            Text = "Home Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnCustomerManagement;
        private System.Windows.Forms.Button btnAppointmentManagement;
        private System.Windows.Forms.Button btnShowReports;
        private System.Windows.Forms.Label lblWelcome;
    }
}