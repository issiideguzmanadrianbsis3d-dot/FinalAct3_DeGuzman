using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DeGuzmanAct_3
{
    public partial class Form2 : Form
    {
        // Connection string pointing to your XAMPP MySQL database
        private string connectionString = "server=localhost;database=InventoryDB;uid=root;pwd=;";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Mask password characters for security
            textBoxPassword.PasswordChar = '•';
        }

        // Login Button Click Event (wired to button1 from the designer)
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            // 1. Validate fields are not empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check credentials in MySQL database
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Login successful! Welcome to the system.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear fields
                            textBoxUsername.Clear();
                            textBoxPassword.Clear();

                            // TODO: Open your main Inventory Dashboard here later!
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxPassword.Clear();
                            textBoxPassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Leave empty or use for something if needed
        }

        // Active link click event wired up by the designer
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 registerForm = new Form1();
            registerForm.Show();
            this.Hide();
        }
    }
}
