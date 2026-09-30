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
    public partial class Form4 : Form
    {
        Random rnd = new Random();
        int pozCurenta = 0;
        int scor = 0, scorm = -1;
        string nume1 = "";
        int r = 0;
        public Form4(string text)
        {
            InitializeComponent();
        }

        public Form4(string nume, Form2 form2)
        {
            InitializeComponent();
            nume1 = nume;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            //MessageBox.Show(nume1.ToString());
            timer1.Start();

            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);

            int nrCuloare = rnd.Next(0, 9);
            if (nrCuloare % 2 == 0)
            {
                Random rnd2 = new Random();
                int p = rnd2.Next(0, 20);
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\imag\\" + p.ToString() + ".png";
                Image img = Image.FromFile(path);

                pb.Image = img;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pb.BackColor = Color.Red;
            }

            int x = rnd.Next(200, 900);
            int y = rnd.Next(30, 500);

            pb.Location = new Point(x, y);
            this.Controls.Add(pb);

            pb.Click += daClick;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void daClick(object sender, EventArgs e)
        {
            PictureBox currentPictureBox = (PictureBox)sender;
            if (currentPictureBox.BackColor == Color.Red)
            {
                scor--;
                
            }
            else
            {
                scor++;
                r--;

                if (scor % 5 == 0)
                {
                    timer1.Interval /= 2;
                }
            }
            label2.Text = scor.ToString();

            

            this.Controls.Remove(currentPictureBox);
        }

        private void daClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form4_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);

            int nrCuloare = rnd.Next(0, 9);
            if (nrCuloare % 2 == 0)
            {
                Random rnd2 = new Random();
                int p = rnd2.Next(0, 20);
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\imag\\" + p.ToString() + ".png";
                Image img = Image.FromFile(path);

                pb.Image = img;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                r++;
            }
            else
            {
                pb.BackColor = Color.Red;
            }

            int x = rnd.Next(200, 900);
            int y = rnd.Next(30, 500);

            pb.Location = new Point(x, y);
            this.Controls.Add(pb);

            pb.Click += daClick;

            //r++;

            if (r == 10)
            {
                timer1.Stop();

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Joc.mdf;Integrated Security=True;Connect Timeout=30;";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                string sqlCommand = "SELECT * from name WHERE nume = '" + nume1 + "';";
                //MessageBox.Show(sqlCommand);
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    scorm = Convert.ToInt32(reader[2]);
                }
                conn.Close();

                if (scorm < scor)
                {
                    conn.Open();
                    try
                    {
                        string comanda = String.Format("UPDATE name SET scor='{0}' WHERE nume='" + nume1 + "';", scor.ToString());
                        //MessageBox.Show(sqlCommand);
                        SqlCommand sqlCommand1 = new SqlCommand(comanda, conn);
                        sqlCommand1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }

                MessageBox.Show("Good job!!! \nYour score is: " + scor.ToString());
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
        }
    }
}
