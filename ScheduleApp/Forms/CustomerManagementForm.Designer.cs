namespace ScheduleApp
{
    partial class CustomerManagementForm
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
            dataGridViewCustomers = new DataGridView();
            btnAddCustomer = new Button();
            btnDeleteCustomer = new Button();
            label1 = new Label();
            btnEditCustomer = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustomers).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCustomers
            // 
            dataGridViewCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCustomers.Location = new Point(70, 161);
            dataGridViewCustomers.Name = "dataGridViewCustomers";
            dataGridViewCustomers.Size = new Size(671, 261);
            dataGridViewCustomers.TabIndex = 0;
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.Location = new Point(210, 119);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(104, 23);
            btnAddCustomer.TabIndex = 1;
            btnAddCustomer.Text = "Add Customer";
            btnAddCustomer.UseVisualStyleBackColor = true;
            btnAddCustomer.Click += btnAddCustomer_Click;
            // 
            // btnDeleteCustomer
            // 
            btnDeleteCustomer.Location = new Point(474, 119);
            btnDeleteCustomer.Name = "btnDeleteCustomer";
            btnDeleteCustomer.Size = new Size(117, 23);
            btnDeleteCustomer.TabIndex = 2;
            btnDeleteCustomer.Text = "Delete Customer";
            btnDeleteCustomer.UseVisualStyleBackColor = true;
            btnDeleteCustomer.Click += btnDeleteCustomer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(238, 38);
            label1.Name = "label1";
            label1.Size = new Size(324, 32);
            label1.TabIndex = 3;
            label1.Text = "Customer Management";
            // 
            // btnEditCustomer
            // 
            btnEditCustomer.Location = new Point(345, 119);
            btnEditCustomer.Name = "btnEditCustomer";
            btnEditCustomer.Size = new Size(93, 23);
            btnEditCustomer.TabIndex = 4;
            btnEditCustomer.Text = "Edit Customer";
            btnEditCustomer.UseVisualStyleBackColor = true;
            btnEditCustomer.Click += btnEditCustomer_Click;
            // 
            // CustomerManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEditCustomer);
            Controls.Add(label1);
            Controls.Add(btnDeleteCustomer);
            Controls.Add(btnAddCustomer);
            Controls.Add(dataGridViewCustomers);
            Name = "CustomerManagementForm";
            Text = "CustomerManagementForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewCustomers;
        private Button btnAddCustomer;
        private Button btnDeleteCustomer;
        private Label label1;
        private Button btnEditCustomer;
    }
}