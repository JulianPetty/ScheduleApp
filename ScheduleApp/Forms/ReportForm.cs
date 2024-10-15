using MySql.Data.MySqlClient;
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
    public partial class ReportForm : Form
    {
        private string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";

        public ReportForm()
        {
            InitializeComponent();

            // Load all reports when the form is shown
            LoadAppointmentTypesByMonthReport();
            LoadUserScheduleReport();
            LoadAppointmentsPerUserReport();
        }

        // Method to load the 'Appointment Types by Month' report into the DataGridView
        private void LoadAppointmentTypesByMonthReport()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT type, start FROM appointment";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Convert DataTable to a List of AppointmentReport objects
                var appointments = dataTable.AsEnumerable().Select(row => new AppointmentReport
                {
                    Type = row["type"].ToString(),
                    Month = Convert.ToDateTime(row["start"]).Month
                }).ToList();

                // Use a lambda expression to group the appointments by type and month
                var groupedAppointments = appointments
                    .GroupBy(a => new { a.Type, a.Month })
                    .Select(group => new
                    {
                        AppointmentType = group.Key.Type,
                        Month = group.Key.Month,
                        Count = group.Count()
                    })
                    .OrderBy(g => g.Month)
                    .ToList();

                // Bind the result to the DataGridView
                dataGridViewAppointmentByMonth.DataSource = groupedAppointments;
            }
        }

        public class AppointmentReport
        {
            public string Type { get; set; }
            public int Month { get; set; }
        }


        // Method to load the 'Schedule for Each User' report into the DataGridView
        private void LoadUserScheduleReport()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT u.userName, a.type, a.start, a.end
            FROM appointment a
            JOIN user u ON a.userId = u.userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Convert DataTable to a List of UserSchedule objects
                var userSchedules = dataTable.AsEnumerable().Select(row => new UserSchedule
                {
                    UserName = row["userName"].ToString(),
                    Type = row["type"].ToString(),
                    Start = Convert.ToDateTime(row["start"]),
                    End = Convert.ToDateTime(row["end"])
                }).ToList();

                // Use a lambda expression to group the schedules by user
                var groupedSchedules = userSchedules
                    .GroupBy(s => s.UserName)
                    .Select(group => new
                    {
                        UserName = group.Key,
                        Appointments = group.Select(s => new { s.Type, s.Start, s.End }).ToList()
                    })
                    .ToList();

                // Flatten the result for binding to the DataGridView
                var flattenedSchedules = groupedSchedules.SelectMany(g => g.Appointments.Select(a => new
                {
                    g.UserName,
                    a.Type,
                    a.Start,
                    a.End
                })).ToList();

                // Bind the result to the DataGridView
                dataGridViewUserSchedule.DataSource = flattenedSchedules;
            }
        }

        public class UserSchedule
        {
            public string UserName { get; set; }
            public string Type { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }


        // Method to load the 'Appointments per User' report into the DataGridView
        private void LoadAppointmentsPerUserReport()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT u.userName, a.appointmentId
            FROM appointment a
            JOIN user u ON a.userId = u.userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Convert DataTable to a List of UserAppointmentCount objects
                var userAppointments = dataTable.AsEnumerable().Select(row => new UserAppointmentCount
                {
                    UserName = row["userName"].ToString(),
                    AppointmentId = Convert.ToInt32(row["appointmentId"])
                }).ToList();

                // Use a lambda expression to group and count appointments by user
                var groupedAppointments = userAppointments
                    .GroupBy(ua => ua.UserName)
                    .Select(group => new
                    {
                        UserName = group.Key,
                        AppointmentCount = group.Count()
                    })
                    .ToList();

                // Bind the result to the DataGridView
                dataGridViewAppointmentsPerUser.DataSource = groupedAppointments;
            }
        }

        public class UserAppointmentCount
        {
            public string UserName { get; set; }
            public int AppointmentId { get; set; }
        }

    }

}
