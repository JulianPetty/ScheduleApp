using System;
using System.Windows.Forms;
using System.IO;
using ScheduleApp.Utils;
using ScheduleApp.Services;
using MySql.Data.MySqlClient;
using ScheduleApp.Forms;

namespace ScheduleApp
{
    public partial class LoginForm : Form
    {
        private readonly LocationService _locationService;
        private string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";

        public LoginForm()
        {
            InitializeComponent();
            //Initialize form components

           _locationService = new LocationService();

            // Set the user's location and culture
            SetUserLocationAndCulture();

            // Translate the labels and buttons based on the selected language
            TranslateLoginForm();

        }

        private void LogLoginAttempt(string username)
        {
            // File path for login history (\ScheduleApp\ScheduleApp\bin\Debug\net8.0-windows)
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login_History.txt");
            // Create the log entry
            string logEntry = $"{DateTime.Now}: {username} logged in.";

            // Append the log entry to the file
            try
            {
                // Ensure thread safety by using a lock for file writing
                lock (logFilePath)
                {
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to write to log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static class SessionInfo
        {
            public static int LoggedInUserId { get; set; } // Store user ID
            public static string LoggedInUsername { get; set; }  // Store username
        }


        private void SetUserLocationAndCulture()
        {
            string userLocation = _locationService.GetUserTimezone();
            Console.WriteLine("User Location: " + userLocation);
            TranslationHelper.SetCultureForLocation(userLocation);
        }

        private void TranslateLoginForm()
        {
            lblUsername.Text = TranslationHelper.GetTranslation("Username");
            lblPassword.Text = TranslationHelper.GetTranslation("Password");
            btnLogin.Text = TranslationHelper.GetTranslation("Login");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Retrieve both userId and userName from the database
                string query = "SELECT userId, userName FROM user WHERE userName = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())  // If the user is found
                {
                    // Store the user ID and username in SessionInfo
                    SessionInfo.LoggedInUserId = Convert.ToInt32(reader["userId"]);
                    SessionInfo.LoggedInUsername = reader["userName"].ToString();

                    LogLoginAttempt(username);

                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Check for upcoming appointments within the next 15 minutes
                    CheckForUpcomingAppointments();

                    // Navigate to the next form
                    this.Hide();  // Hide the login form
                    DashboardForm dashboardForm = new DashboardForm();
                    dashboardForm.ShowDialog();  // Open the dashboard after login
                    this.Close();  // Close the login form
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckForUpcomingAppointments()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Get the user's local time zone
                TimeZoneInfo userTimeZone = TimeZoneInfo.Local;
                DateTime nowInUserTimeZone = DateTime.Now;
                DateTime fifteenMinutesLaterInUserTimeZone = nowInUserTimeZone.AddMinutes(15);

                // Convert times to EST for the query
                DateTime nowInEST = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(nowInUserTimeZone, userTimeZone.Id, "Eastern Standard Time");
                DateTime fifteenMinutesLaterInEST = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(fifteenMinutesLaterInUserTimeZone, userTimeZone.Id, "Eastern Standard Time");

                // Query to check for appointments in the next 15 minutes (in EST)
                string query = @"
                SELECT * FROM appointment
                WHERE userId = @userId
                AND start BETWEEN @now AND @fifteenMinutesLater";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", SessionInfo.LoggedInUserId);
                cmd.Parameters.AddWithValue("@now", nowInEST);
                cmd.Parameters.AddWithValue("@fifteenMinutesLater", fifteenMinutesLaterInEST);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Retrieve the appointment time from the database (assumed to be in Eastern Standard Time)
                    DateTime appointmentTimeEst = Convert.ToDateTime(reader["start"]);

                    // Convert the appointment time to the user's local time zone
                    TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    TimeZoneInfo localUserTimeZone = TimeZoneInfo.Local;  // Get the user's local time zone

                    // Convert the appointment time from EST to the user's local time zone
                    DateTime appointmentTimeInUserTimeZone = TimeZoneInfo.ConvertTime(appointmentTimeEst, estTimeZone, userTimeZone);

                    // Display the appointment time in the user's local time zone
                    MessageBox.Show($"You have an appointment scheduled at {appointmentTimeInUserTimeZone.ToShortTimeString()}!", "Upcoming Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }


    }
}
