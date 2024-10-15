using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using static ScheduleApp.LoginForm;

namespace ScheduleApp.Forms
{
    public partial class AppointmentForm : Form
    {
        private string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";
        private int? appointmentId = null;  // Nullable int for handling new vs existing appointments

        // Constructor for adding a new appointment
        public AppointmentForm()
        {
            InitializeComponent();
            LoadCustomers();         // Populate customer dropdown
            LoadAppointmentTypes();  // Populate appointment type dropdown
        }

        public AppointmentForm(int appointmentId)
        {
            InitializeComponent();
            LoadCustomers();         // Populate customer dropdown
            LoadAppointmentTypes();  // Populate appointment type dropdown
            this.appointmentId = appointmentId;
            LoadAppointmentDetails(appointmentId);  // Load existing appointment details
        }

        private void LoadCustomers()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT customerId, customerName FROM customer";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                var customerList = new List<KeyValuePair<int, string>>();

                while (reader.Read())
                {
                    int customerId = Convert.ToInt32(reader["customerId"]);
                    string customerName = reader["customerName"].ToString();
                    customerList.Add(new KeyValuePair<int, string>(customerId, customerName));
                }

                comboBoxCustomer.DataSource = new BindingSource(customerList, null);
                comboBoxCustomer.DisplayMember = "Value";  // Display the customer name
                comboBoxCustomer.ValueMember = "Key";      // Use the customer ID internally
            }
        }

        private void LoadAppointmentTypes()
        {
            var appointmentTypes = new List<string>
    {
        "Consultation",
        "Follow-up",
        "Scrum",
        "Presentation",
        "Review",
        "Other"
    };

            comboBoxType.DataSource = appointmentTypes;
        }


        private bool IsAppointmentTimeValid(int customerId, DateTime start, DateTime end)
        {
            // Check business hours (9 AM - 5 PM, Monday to Friday)
            if (!IsWithinBusinessHours(start, end))
            {
                MessageBox.Show("The appointment must be scheduled during business hours: 9 AM - 5 PM EST, Monday to Friday.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  // Appointment is outside business hours
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Query to check for overlapping appointments for the same customer
                string query = @"
                SELECT COUNT(*) FROM appointment 
                WHERE customerId = @customerId 
                AND ((@start < end AND @end > start))";  // Overlap condition

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

                int overlapCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (overlapCount > 0)
                {
                    MessageBox.Show("The selected time overlaps with an existing appointment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;  // Appointment overlaps
                }

                return true;  // Valid appointment time
            }
        }

        private void LoadAppointmentDetails(int appointmentId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM appointment WHERE appointmentId = @appointmentId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Populate the form fields with the existing appointment details
                    comboBoxCustomer.SelectedValue = reader["customerId"];
                    comboBoxType.SelectedItem = reader["type"].ToString();

                    // Convert the stored EST time to the user's local time zone for displaying in the form
                    DateTime startEst = Convert.ToDateTime(reader["start"]);
                    DateTime endEst = Convert.ToDateTime(reader["end"]);

                    // Convert from EST to the user's local time zone
                    dateTimePickerStart.Value = ConvertToUserTimeZone(startEst);
                    dateTimePickerEnd.Value = ConvertToUserTimeZone(endEst);
                }
            }
        }

        private DateTime ConvertToUserTimeZone(DateTime estTime)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;  // Get the user's local time zone
            DateTime userTime = TimeZoneInfo.ConvertTime(estTime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"), userTimeZone);
            return userTime;
        }


        private bool IsWithinBusinessHours(DateTime start, DateTime end)
        {
            // Convert times to Eastern Standard Time (EST)
            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime startEst = TimeZoneInfo.ConvertTime(start, estTimeZone);
            DateTime endEst = TimeZoneInfo.ConvertTime(end, estTimeZone);

            // Check if the appointment is within 9 AM to 5 PM, Monday to Friday
            if (startEst.Hour < 9 || endEst.Hour > 17 ||
                startEst.DayOfWeek == DayOfWeek.Saturday || startEst.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            return true;
        }

        private void AddAppointment()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    if (comboBoxCustomer.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Get the selected customerId
                    var selectedCustomer = (KeyValuePair<int, string>)comboBoxCustomer.SelectedItem;
                    int customerId = selectedCustomer.Key;

                    // Get the selected appointment type
                    string appointmentType = comboBoxType.SelectedItem.ToString();

                    // Convert appointment times to EST
                    DateTime startEst = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimePickerStart.Value, "Eastern Standard Time");
                    DateTime endEst = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimePickerEnd.Value, "Eastern Standard Time");

                    // Validation for overlapping appointments
                    if (!IsAppointmentTimeValid(customerId, startEst, endEst))
                    {
                        MessageBox.Show("The selected time overlaps with an existing appointment or is outside business hours (9 AM - 5 PM EST).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Insert the new appointment into the database
                    string query = @"
                    INSERT INTO appointment (customerId, userId, type, start, end, createDate, createdBy, lastUpdate, lastUpdateBy, title, description, location, contact, url)
                    VALUES (@customerId, @userId, @type, @start, @end, @createDate, @createdBy, @lastUpdate, @lastUpdateBy, @title, @description, @location, @contact, @url)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@userId", SessionInfo.LoggedInUserId);  // Use the actual logged-in user ID
                    cmd.Parameters.AddWithValue("@type", appointmentType);
                    cmd.Parameters.AddWithValue("@start", startEst);
                    cmd.Parameters.AddWithValue("@end", endEst);
                    cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@createdBy", SessionInfo.LoggedInUsername);  // Use username for createdBy
                    cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", SessionInfo.LoggedInUsername);  // Use username for lastUpdateBy

                    // Default values for 'not needed' fields
                    cmd.Parameters.AddWithValue("@title", "");           // Empty string for title
                    cmd.Parameters.AddWithValue("@description", "");     // Empty string for description
                    cmd.Parameters.AddWithValue("@location", "");        // Empty string for location
                    cmd.Parameters.AddWithValue("@contact", "");         // Empty string for contact
                    cmd.Parameters.AddWithValue("@url", "");             // Empty string for URL

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();  // Close the form after saving
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateAppointment(int appointmentId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    if (comboBoxCustomer.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Get the selected customerId
                    var selectedCustomer = (KeyValuePair<int, string>)comboBoxCustomer.SelectedItem;
                    int customerId = selectedCustomer.Key;

                    // Get the selected appointment type
                    string appointmentType = comboBoxType.SelectedItem.ToString();

                    // Convert appointment times to EST
                    DateTime startEst = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimePickerStart.Value, "Eastern Standard Time");
                    DateTime endEst = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimePickerEnd.Value, "Eastern Standard Time");

                    // Validation for overlapping appointments
                    if (!IsAppointmentTimeValid(customerId, startEst, endEst))
                    {
                        MessageBox.Show("The selected time overlaps with an existing appointment or is outside business hours (9 AM - 5 PM EST).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update the existing appointment in the database
                    string query = @"
                UPDATE appointment 
                SET customerId = @customerId, userId = @userId, type = @type, start = @start, end = @end, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy,
                    title = @title, description = @description, location = @location, contact = @contact, url = @url
                WHERE appointmentId = @appointmentId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@userId", SessionInfo.LoggedInUserId);  // Use the actual logged-in user ID
                    cmd.Parameters.AddWithValue("@type", appointmentType);
                    cmd.Parameters.AddWithValue("@start", startEst);
                    cmd.Parameters.AddWithValue("@end", endEst);
                    cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", SessionInfo.LoggedInUsername);  // Use username for lastUpdateBy
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                    // Default values for 'not needed' fields
                    cmd.Parameters.AddWithValue("@title", "");           // Empty string for title
                    cmd.Parameters.AddWithValue("@description", "");     // Empty string for description
                    cmd.Parameters.AddWithValue("@location", "");        // Empty string for location
                    cmd.Parameters.AddWithValue("@contact", "");         // Empty string for contact
                    cmd.Parameters.AddWithValue("@url", "");             // Empty string for URL

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Appointment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();  // Close the form after saving
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Other existing methods (IsAppointmentTimeValid, DeleteAppointment, etc.)

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (appointmentId == null) // Check if we're adding a new appointment
            {
                AddAppointment();
            }
            else
            {
                UpdateAppointment(appointmentId.Value);  // Pass the existing appointmentId for updates
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();  // Close the form
        }
    }
}
