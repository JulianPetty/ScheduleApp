using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ScheduleApp.Forms;

namespace ScheduleApp
{
    public partial class CustomerManagementForm : Form
    {
        private string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";

        public CustomerManagementForm()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT c.customerId, c.customerName, a.address, a.phone 
            FROM customer c
            JOIN address a ON c.addressId = a.addressId";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the DataTable to the DataGridView
                dataGridViewCustomers.DataSource = dt;
            }
        }


        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Open the CustomerForm for adding a new customer
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();

            // Reload customers after adding a new one
            LoadCustomers();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                int customerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["customerId"].Value);

                // Open the CustomerForm for editing the selected customer
                CustomerForm customerForm = new CustomerForm(customerId);
                customerForm.ShowDialog();

                // Reload customers after editing
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                int customerId = Convert.ToInt32(dataGridViewCustomers.SelectedRows[0].Cells["customerId"].Value);

                var confirmResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        // First, get the addressId for this customer
                        string getAddressIdQuery = "SELECT addressId FROM customer WHERE customerId = @customerId";
                        MySqlCommand getAddressIdCmd = new MySqlCommand(getAddressIdQuery, conn);
                        getAddressIdCmd.Parameters.AddWithValue("@customerId", customerId);
                        int addressId = Convert.ToInt32(getAddressIdCmd.ExecuteScalar());

                        // Delete the customer
                        string deleteCustomerQuery = "DELETE FROM customer WHERE customerId = @customerId";
                        MySqlCommand deleteCustomerCmd = new MySqlCommand(deleteCustomerQuery, conn);
                        deleteCustomerCmd.Parameters.AddWithValue("@customerId", customerId);
                        deleteCustomerCmd.ExecuteNonQuery();

                        // Delete the associated address
                        string deleteAddressQuery = "DELETE FROM address WHERE addressId = @addressId";
                        MySqlCommand deleteAddressCmd = new MySqlCommand(deleteAddressQuery, conn);
                        deleteAddressCmd.Parameters.AddWithValue("@addressId", addressId);
                        deleteAddressCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Customer and associated address deleted successfully!");
                    LoadCustomers();  // Reload the list of customers
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

