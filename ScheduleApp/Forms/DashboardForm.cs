using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleApp.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void btnCustomerManagement_Click(object sender, EventArgs e)
        {
            CustomerManagementForm customerForm = new CustomerManagementForm();
            customerForm.ShowDialog();  // Open the Customer Management Form
        }

        private void btnAppointmentManagement_Click(object sender, EventArgs e)
        {
            AppointmentManagementForm appointmentManagementForm = new AppointmentManagementForm();
            appointmentManagementForm.ShowDialog();  // Open the Appointment Management Form
        }

        private void btnShowReports_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

    }
}

