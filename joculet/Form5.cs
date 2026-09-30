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

namespace joculet
{
    public partial class Form5 : Form
    {
        int scorm1 = -1, scorm2  = -1, scorm3 = -1;
        string nume1 = "", nume2 = "", nume3 = "";
        int id1, id2, id3;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            scorm1 = -1; scorm2 = -1; scorm3 = -1;
            nume1 = ""; nume2 = ""; nume3 = "";

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Joc.mdf;Integrated Security=True;Connect Timeout=30;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sqlCommand = "SELECT * from name;";
            //MessageBox.Show(sqlCommand);
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if ( scorm1 <= Convert.ToInt32(reader[2]))
                {
                    scorm3 = scorm2;
                    nume3 = nume2;
                    id3 = id2;

                    scorm2 = scorm1;
                    nume2 = nume1;
                    id2 = id1;

                    scorm1 = Convert.ToInt32(reader[2]);
                    nume1 = reader[1].ToString();
                    id1 = Convert.ToInt32(reader[0]);
                }
                else if (scorm2 <= Convert.ToInt32(reader[2]))
                {
                    scorm3 = scorm2;
                    nume3 = nume2;
                    id3 = id2;

                    scorm2 = Convert.ToInt32(reader[2]);
                    nume2 = reader[1].ToString();
                    id2 = Convert.ToInt32(reader[0]);
                }
                else if (scorm3 <= Convert.ToInt32(reader[2]))
                {
                    scorm3 = Convert.ToInt32(reader[2]);
                    nume3 = reader[1].ToString();
                    id3 = Convert.ToInt32(reader[0]);
                }
            }

            label1.Text = scorm1.ToString();
            //label1.FontSize = 36;
            label2.Text = nume1;

            label4.Text = scorm2.ToString();
            label3.Text = nume2;

            label6.Text = scorm3.ToString();
            label5.Text = nume3;
            conn.Close();

            label7.Text = string.Empty;
            label7.Text = "The rest: \n";

            string connectionString1 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Joc.mdf;Integrated Security=True;Connect Timeout=30;";
            SqlConnection conn1 = new SqlConnection(connectionString1);
            conn1.Open();
            string sqlCommand1 = "SELECT * from name;";
            //MessageBox.Show(sqlCommand);
            SqlCommand cmd1 = new SqlCommand(sqlCommand1, conn1);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            
            while (reader1.Read())
            {
                if (Convert.ToInt32(reader1[0]) != id1 && Convert.ToInt32(reader1[0]) != id2 && Convert.ToInt32(reader1[0]) != id3)
                {
                    label7.Text += reader1[1].ToString() + " (scor: " + reader1[2].ToString() + ")\n";
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
