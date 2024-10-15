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
using static ScheduleApp.LoginForm;

namespace ScheduleApp.Forms
{
    public partial class CustomerForm : Form
    {
        public static string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";
        private int? customerId = null;  // Nullable customer ID for editing

        // Constructor for adding a new customer
        public CustomerForm()
        {
            InitializeComponent();
        }

        // Constructor for editing an existing customer
        public CustomerForm(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            LoadCustomerDetails(customerId);
        }

        // Load customer details into the form for editing
        private void LoadCustomerDetails(int customerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT c.customerName, a.address, a.address2, a.phone, a.cityId, ct.countryId
                FROM customer c
                JOIN address a ON c.addressId = a.addressId
                JOIN city ci ON a.cityId = ci.cityId
                JOIN country ct ON ci.countryId = ct.countryId
                WHERE c.customerId = @customerId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtName.Text = reader["customerName"].ToString();
                    txtAddress.Text = reader["address"].ToString();
                    txtPhone.Text = reader["phone"].ToString();

                    // Set country and load related cities
                    var countryId = Convert.ToInt32(reader["countryId"]);
                    comboBoxCountry.SelectedValue = countryId;
                    LoadCities(countryId);

                    // Set the city
                    comboBoxCity.SelectedValue = Convert.ToInt32(reader["cityId"]);
                }
            }
        }


        private void LoadCountries()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT countryId, country FROM country";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxCountry.Items.Add(new KeyValuePair<int, string>(
                        Convert.ToInt32(reader["countryId"]),
                        reader["country"].ToString()));
                }

                comboBoxCountry.DisplayMember = "Value"; // Show country name
                comboBoxCountry.ValueMember = "Key"; // Store countryId
            }
        }

        private void LoadCities(int countryId)
        {
            comboBoxCity.Items.Clear();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT cityId, city FROM city WHERE countryId = @countryId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxCity.Items.Add(new KeyValuePair<int, string>(
                        Convert.ToInt32(reader["cityId"]),
                        reader["city"].ToString()));
                }

                comboBoxCity.DisplayMember = "Value"; // Show city name
                comboBoxCity.ValueMember = "Key"; // Store cityId
            }
        }


        // Save button click event (handles both adding and editing)
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())  // Validate form inputs
            {
                if (customerId == null)
                {
                    AddCustomer();  // Add a new customer
                }
                else
                {
                    UpdateCustomer();  // Update an existing customer
                }

                this.Close();  // Close the form after saving
            }
        }

        // Method to validate the form inputs  (no empty fields/phone number)
        private bool ValidateForm()
        {
            // Ensure no fields are empty
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate phone number to allow only digits and dashes
            string phonePattern = @"^[0-9-]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), phonePattern))
            {
                MessageBox.Show("Phone number can only contain digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // Method to add a new customer
        private void AddCustomer()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                if (comboBoxCity.SelectedItem == null)
                {
                    MessageBox.Show("Please select a city.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the selected cityId
                var selectedCity = (KeyValuePair<int, string>)comboBoxCity.SelectedItem;
                int cityId = selectedCity.Key;

                // Insert into address table, including createDate, lastUpdate, createdBy, and lastUpdateBy
                string addressQuery = @"
                INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) 
                VALUES (@address, @address2, @cityId, @postalCode, @phone, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

                MySqlCommand addressCmd = new MySqlCommand(addressQuery, conn);
                addressCmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                addressCmd.Parameters.AddWithValue("@address2", "");
                addressCmd.Parameters.AddWithValue("@cityId", cityId);
                addressCmd.Parameters.AddWithValue("@postalCode", txtPostalCode.Text.Trim());
                addressCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                addressCmd.Parameters.AddWithValue("@createDate", DateTime.Now);  // Set createDate to current timestamp
                addressCmd.Parameters.AddWithValue("@createdBy", SessionInfo.LoggedInUsername);  // Set createdBy to logged-in user
                addressCmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);  // Set lastUpdate to current timestamp
                addressCmd.Parameters.AddWithValue("@lastUpdateBy", SessionInfo.LoggedInUsername);  // Set lastUpdateBy to logged-in user
                addressCmd.ExecuteNonQuery();

                // Get the newly inserted addressId
                long addressId = addressCmd.LastInsertedId;

                // Insert into customer table, including lastUpdate and lastUpdateBy
                string customerQuery = @"
                INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) 
                VALUES (@customerName, @addressId, 1, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

                MySqlCommand customerCmd = new MySqlCommand(customerQuery, conn);
                customerCmd.Parameters.AddWithValue("@customerName", txtName.Text.Trim());
                customerCmd.Parameters.AddWithValue("@addressId", addressId);
                customerCmd.Parameters.AddWithValue("@createDate", DateTime.Now);  // Set createDate to current timestamp
                customerCmd.Parameters.AddWithValue("@createdBy", SessionInfo.LoggedInUsername);  // Set createdBy to logged-in user
                customerCmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);  // Set lastUpdate to current timestamp
                customerCmd.Parameters.AddWithValue("@lastUpdateBy", SessionInfo.LoggedInUsername);  // Set lastUpdateBy to logged-in user
                customerCmd.ExecuteNonQuery();

                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }







        // Method to update an existing customer
        private void UpdateCustomer()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                if (comboBoxCity.SelectedItem == null)
                {
                    MessageBox.Show("Please select a city.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the selected cityId
                var selectedCity = (KeyValuePair<int, string>)comboBoxCity.SelectedItem;
                int cityId = selectedCity.Key;

                // First, update the address table
                string addressQuery = @"
                UPDATE address a
                JOIN customer c ON a.addressId = c.addressId
                SET a.address = @address, a.address2 = @address2, a.cityId = @cityId, a.phone = @phone, a.lastUpdate = @lastUpdate, a.lastUpdateBy = @lastUpdateBy
                WHERE c.customerId = @customerId";

                MySqlCommand addressCmd = new MySqlCommand(addressQuery, conn);
                addressCmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                addressCmd.Parameters.AddWithValue("@address2", "");
                addressCmd.Parameters.AddWithValue("@cityId", cityId);
                addressCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                addressCmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);  // Set lastUpdate to current timestamp
                addressCmd.Parameters.AddWithValue("@lastUpdateBy", SessionInfo.LoggedInUsername);  // Set lastUpdateBy to logged-in user
                addressCmd.Parameters.AddWithValue("@customerId", customerId);
                addressCmd.ExecuteNonQuery();

                // Then, update the customer table
                string customerQuery = @"
                UPDATE customer
                SET customerName = @customerName, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy
                WHERE customerId = @customerId";

                MySqlCommand customerCmd = new MySqlCommand(customerQuery, conn);
                customerCmd.Parameters.AddWithValue("@customerName", txtName.Text.Trim());
                customerCmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);  // Set lastUpdate to current timestamp
                customerCmd.Parameters.AddWithValue("@lastUpdateBy", SessionInfo.LoggedInUsername);  // Set lastUpdateBy to logged-in user
                customerCmd.Parameters.AddWithValue("@customerId", customerId);
                customerCmd.ExecuteNonQuery();

                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        // Cancel button click event to close the form without saving
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();  // Close the form
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCountry.SelectedItem != null)
            {
                // Get the selected countryId
                var selectedCountry = (KeyValuePair<int, string>)comboBoxCountry.SelectedItem;
                int countryId = selectedCountry.Key;

                // Load the cities for the selected country
                LoadCities(countryId);
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCountries();
        }
    }
}
