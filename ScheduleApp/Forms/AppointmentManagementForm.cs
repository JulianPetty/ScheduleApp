using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ScheduleApp.Forms
{
    public partial class AppointmentManagementForm : Form
    {
        private string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";
        private DateTime? lastSelectedMonth = null;


        public AppointmentManagementForm()
        {
            InitializeComponent();
            monthCalendar.DateChanged += monthCalendar_DateChanged; // Handle month and day change event
        }

        // Handle when a new date is selected in the calendar
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start;  // Get the selected date

            // If the selected month has changed, load all appointments for that month
            if (lastSelectedMonth == null || selectedDate.Month != lastSelectedMonth.Value.Month || selectedDate.Year != lastSelectedMonth.Value.Year)
            {
                lastSelectedMonth = selectedDate;
                LoadAppointmentsForMonth(selectedDate);  // Load appointments for the selected month
            }
            else
            {
                // Otherwise, filter for the specific day within the month
                LoadAppointmentsForDay(selectedDate);
            }
        }



        // Load appointments for the entire month of the selected date
        private void LoadAppointmentsForMonth(DateTime selectedDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Query to get all appointments for the selected month
                string query = @"
        SELECT a.appointmentId, c.customerName, a.type, a.start, a.end
        FROM appointment a
        JOIN customer c ON a.customerId = c.customerId
        WHERE MONTH(a.start) = @month AND YEAR(a.start) = @year";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@month", selectedDate.Month);
                cmd.Parameters.AddWithValue("@year", selectedDate.Year);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Log number of appointments for debugging
                Console.WriteLine($"Appointments for {selectedDate.Month}/{selectedDate.Year}: {dt.Rows.Count}");

                // Convert appointment times from EST to user's local time zone
                foreach (DataRow row in dt.Rows)
                {
                    DateTime startEst = Convert.ToDateTime(row["start"]);
                    DateTime endEst = Convert.ToDateTime(row["end"]);
                    row["start"] = ConvertToUserTimeZone(startEst);
                    row["end"] = ConvertToUserTimeZone(endEst);
                }

                // Bind the DataTable to the DataGridView (show all appointments for the month)
                dataGridViewAppointments.DataSource = dt;

                // Explicitly refresh DataGridView to ensure it's updated
                dataGridViewAppointments.Refresh();
            }
        }



        // Filter appointments by a specific day
        private void LoadAppointmentsForDay(DateTime selectedDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Query to get appointments for the selected day
                string query = @"
                SELECT a.appointmentId, c.customerName, a.type, a.start, a.end
                FROM appointment a
                JOIN customer c ON a.customerId = c.customerId
                WHERE DATE(a.start) = @selectedDate";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@selectedDate", selectedDate.ToString("yyyy-MM-dd"));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Convert appointment times to the user's local time zone
                foreach (DataRow row in dt.Rows)
                {
                    DateTime startEst = Convert.ToDateTime(row["start"]);
                    DateTime endEst = Convert.ToDateTime(row["end"]);
                    row["start"] = ConvertToUserTimeZone(startEst);
                    row["end"] = ConvertToUserTimeZone(endEst);
                }

                // Bind the DataTable to the DataGridView and refresh it
                dataGridViewAppointments.DataSource = dt;
                dataGridViewAppointments.Refresh();
            }
        }




        private DateTime ConvertToUserTimeZone(DateTime estTime)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;  // Get the user's local time zone
            DateTime userTime = TimeZoneInfo.ConvertTime(estTime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"), userTimeZone);
            return userTime;
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            AppointmentForm appointmentForm = new AppointmentForm();
            appointmentForm.ShowDialog();
            LoadAppointmentsForMonth(monthCalendar.SelectionStart);  // Reload appointments after adding
        }

        private void btnEditAppointment_Click(object sender, EventArgs e)
        {
            if (dataGridViewAppointments.SelectedRows.Count > 0)
            {
                int selectedAppointmentId = Convert.ToInt32(dataGridViewAppointments.SelectedRows[0].Cells["appointmentId"].Value);
                AppointmentForm appointmentForm = new AppointmentForm(selectedAppointmentId);
                appointmentForm.ShowDialog();
                LoadAppointmentsForMonth(monthCalendar.SelectionStart);  // Reload appointments after editing
            }
            else
            {
                MessageBox.Show("Please select an appointment to edit.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewAppointments.SelectedRows.Count > 0)
                {
                    int selectedAppointmentId = Convert.ToInt32(dataGridViewAppointments.SelectedRows[0].Cells["appointmentId"].Value);

                    var confirmResult = MessageBox.Show("Are you sure to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM appointment WHERE appointmentId = @appointmentId";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@appointmentId", selectedAppointmentId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Appointment deleted successfully!");
                        LoadAppointmentsForMonth(monthCalendar.SelectionStart);  // Reload appointments after deletion
                    }
                }
                else
                {
                    MessageBox.Show("Please select an appointment to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Get the selected start and end date
            DateTime selectedStartDate = e.Start;
            DateTime selectedEndDate = e.End;

            // Check if a single day or multiple days (like a month) is selected
            if (selectedStartDate.Date == selectedEndDate.Date)
            {
                // Single day selected, load appointments for the specific day
                LoadAppointmentsForDay(selectedStartDate);
            }
            else
            {
                // Multiple days selected (e.g., a full month), load appointments for the selected range
                LoadAppointmentsForMonth(selectedStartDate);
            }
        }

    }
}
