namespace ScheduleApp.Forms
{
    partial class AppointmentManagementForm
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
            monthCalendar = new MonthCalendar();
            dataGridViewAppointments = new DataGridView();
            btnAddAppointment = new Button();
            btnEditAppointment = new Button();
            btnDeleteAppointment = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointments).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(30, 74);
            monthCalendar.MaxSelectionCount = 31;
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 0;
            monthCalendar.DateChanged += monthCalendar_DateChanged;
            monthCalendar.DateSelected += monthCalendar_DateSelected;
            // 
            // dataGridViewAppointments
            // 
            dataGridViewAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAppointments.Location = new Point(300, 30);
            dataGridViewAppointments.Name = "dataGridViewAppointments";
            dataGridViewAppointments.Size = new Size(600, 350);
            dataGridViewAppointments.TabIndex = 1;
            // 
            // btnAddAppointment
            // 
            btnAddAppointment.Location = new Point(300, 400);
            btnAddAppointment.Name = "btnAddAppointment";
            btnAddAppointment.Size = new Size(120, 30);
            btnAddAppointment.TabIndex = 2;
            btnAddAppointment.Text = "Add Appointment";
            btnAddAppointment.UseVisualStyleBackColor = true;
            btnAddAppointment.Click += btnAddAppointment_Click;
            // 
            // btnEditAppointment
            // 
            btnEditAppointment.Location = new Point(450, 400);
            btnEditAppointment.Name = "btnEditAppointment";
            btnEditAppointment.Size = new Size(120, 30);
            btnEditAppointment.TabIndex = 3;
            btnEditAppointment.Text = "Edit Appointment";
            btnEditAppointment.UseVisualStyleBackColor = true;
            btnEditAppointment.Click += btnEditAppointment_Click;
            // 
            // btnDeleteAppointment
            // 
            btnDeleteAppointment.Location = new Point(600, 400);
            btnDeleteAppointment.Name = "btnDeleteAppointment";
            btnDeleteAppointment.Size = new Size(120, 30);
            btnDeleteAppointment.TabIndex = 4;
            btnDeleteAppointment.Text = "Delete Appointment";
            btnDeleteAppointment.UseVisualStyleBackColor = true;
            btnDeleteAppointment.Click += btnDeleteAppointment_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 264);
            label1.Name = "label1";
            label1.Size = new Size(248, 45);
            label1.TabIndex = 5;
            label1.Text = "Month View: Drag from 1st of the month\r\n to last of the month to view all appointments\r\n for the month.";
            // 
            // AppointmentManagementForm
            // 
            ClientSize = new Size(940, 450);
            Controls.Add(label1);
            Controls.Add(monthCalendar);
            Controls.Add(dataGridViewAppointments);
            Controls.Add(btnAddAppointment);
            Controls.Add(btnEditAppointment);
            Controls.Add(btnDeleteAppointment);
            Name = "AppointmentManagementForm";
            Text = "Appointment Management";
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.DataGridView dataGridViewAppointments;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Button btnEditAppointment;
        private System.Windows.Forms.Button btnDeleteAppointment;
        private Label label1;
    }
}