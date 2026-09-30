using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace joculet
{
    public partial class Form2 : Form
    {
        bool ok = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = false;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Joc.mdf;Integrated Security=True;Connect Timeout=30;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sqlCommand = "SELECT * from name;";
            //MessageBox.Show(sqlCommand);
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (reader[1].ToString() == textBox1.Text)
                {
                    ok = true;
                    conn.Close();
                    break;
                }
            }
            conn.Close();

            try
            {
                if (textBox1.Text != string.Empty)
                {
                    if (ok == false)
                    {
                        conn.Open();
                        string sqlCommand1 = String.Format("INSERT INTO name (nume, scor) VALUES('{0}', {1});", textBox1.Text.ToString(), 0);
                        SqlCommand cmd1 = new SqlCommand(sqlCommand1, conn);
                        SqlDataReader reader1 = cmd1.ExecuteReader();
                        conn.Close();

                        Form4 form4 = new Form4(textBox1.Text, this);
                        form4.Show();
                        this.Hide();
                    }
                    else
                    {
                        Form4 form4 = new Form4(textBox1.Text, this);
                        form4.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Name needs to be at least one character.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
    }
}
