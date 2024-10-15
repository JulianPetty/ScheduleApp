namespace ScheduleApp.Forms
{
    partial class CustomerForm
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
            txtName = new TextBox();
            txtPhone = new TextBox();
            txtAddress = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            comboBoxCity = new ComboBox();
            comboBoxCountry = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            txtPostalCode = new TextBox();
            label7 = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(305, 109);
            txtName.Name = "txtName";
            txtName.Size = new Size(171, 23);
            txtName.TabIndex = 0;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(305, 155);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(171, 23);
            txtPhone.TabIndex = 1;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(305, 198);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(171, 23);
            txtAddress.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(218, 372);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(411, 372);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(239, 41);
            label1.Name = "label1";
            label1.Size = new Size(233, 28);
            label1.TabIndex = 5;
            label1.Text = "Add/Edit Customer";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(225, 111);
            label2.Name = "label2";
            label2.Size = new Size(43, 16);
            label2.TabIndex = 6;
            label2.Text = "Name";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(224, 157);
            label5.Name = "label5";
            label5.Size = new Size(44, 16);
            label5.TabIndex = 9;
            label5.Text = "Phone";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(214, 205);
            label6.Name = "label6";
            label6.Size = new Size(54, 16);
            label6.TabIndex = 10;
            label6.Text = "Address";
            // 
            // comboBoxCity
            // 
            comboBoxCity.FormattingEnabled = true;
            comboBoxCity.Location = new Point(305, 323);
            comboBoxCity.Name = "comboBoxCity";
            comboBoxCity.Size = new Size(171, 23);
            comboBoxCity.TabIndex = 11;
            comboBoxCity.SelectedIndexChanged += comboBoxCity_SelectedIndexChanged;
            // 
            // comboBoxCountry
            // 
            comboBoxCountry.FormattingEnabled = true;
            comboBoxCountry.Location = new Point(305, 280);
            comboBoxCountry.Name = "comboBoxCountry";
            comboBoxCountry.Size = new Size(171, 23);
            comboBoxCountry.TabIndex = 12;
            comboBoxCountry.SelectedIndexChanged += comboBoxCountry_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(216, 282);
            label3.Name = "label3";
            label3.Size = new Size(52, 16);
            label3.TabIndex = 13;
            label3.Text = "Country";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(239, 325);
            label4.Name = "label4";
            label4.Size = new Size(29, 16);
            label4.TabIndex = 14;
            label4.Text = "City";
            // 
            // txtPostalCode
            // 
            txtPostalCode.AcceptsReturn = true;
            txtPostalCode.Location = new Point(305, 240);
            txtPostalCode.Name = "txtPostalCode";
            txtPostalCode.Size = new Size(171, 23);
            txtPostalCode.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(191, 242);
            label7.Name = "label7";
            label7.Size = new Size(77, 16);
            label7.TabIndex = 16;
            label7.Text = "Postal Code";
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label7);
            Controls.Add(txtPostalCode);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBoxCountry);
            Controls.Add(comboBoxCity);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtAddress);
            Controls.Add(txtPhone);
            Controls.Add(txtName);
            Name = "CustomerForm";
            Text = "CustomerForm";
            Load += CustomerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label6;
        private ComboBox comboBoxCity;
        private ComboBox comboBoxCountry;
        private Label label3;
        private Label label4;
        private TextBox txtPostalCode;
        private Label label7;
    }
}