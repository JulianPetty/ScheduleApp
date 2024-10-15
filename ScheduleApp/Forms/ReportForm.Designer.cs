namespace ScheduleApp.Forms
{
    partial class ReportForm
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            dataGridViewAppointmentByMonth = new DataGridView();
            dataGridViewUserSchedule = new DataGridView();
            dataGridViewAppointmentsPerUser = new DataGridView();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointmentByMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUserSchedule).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointmentsPerUser).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(88, 24);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(583, 394);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridViewAppointmentByMonth);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(575, 366);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Appointments By Month";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridViewUserSchedule);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(575, 366);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Schedule By User";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridViewAppointmentsPerUser);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(575, 366);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Appointments Per User";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAppointmentByMonth
            // 
            dataGridViewAppointmentByMonth.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAppointmentByMonth.Location = new Point(3, 3);
            dataGridViewAppointmentByMonth.Name = "dataGridViewAppointmentByMonth";
            dataGridViewAppointmentByMonth.Size = new Size(569, 360);
            dataGridViewAppointmentByMonth.TabIndex = 0;
            // 
            // dataGridViewUserSchedule
            // 
            dataGridViewUserSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUserSchedule.Location = new Point(3, 3);
            dataGridViewUserSchedule.Name = "dataGridViewUserSchedule";
            dataGridViewUserSchedule.Size = new Size(572, 360);
            dataGridViewUserSchedule.TabIndex = 0;
            // 
            // dataGridViewAppointmentsPerUser
            // 
            dataGridViewAppointmentsPerUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAppointmentsPerUser.Location = new Point(6, 6);
            dataGridViewAppointmentsPerUser.Name = "dataGridViewAppointmentsPerUser";
            dataGridViewAppointmentsPerUser.Size = new Size(563, 354);
            dataGridViewAppointmentsPerUser.TabIndex = 0;
                       
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "ReportForm";
            Text = "ReportForm";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointmentByMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUserSchedule).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAppointmentsPerUser).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private DataGridView dataGridViewAppointmentByMonth;
        private TabPage tabPage2;
        private DataGridView dataGridViewUserSchedule;
        private TabPage tabPage3;
        private DataGridView dataGridViewAppointmentsPerUser;
    }
}