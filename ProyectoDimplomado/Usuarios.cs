using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProyectoDimplomado
{
    public partial class Usuarios : Form
    {
        public  static String Usuario = ""; 
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {

        }

       private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost; user=root; database=proyectodimplomado; SSL Mode = none");

            try
            {
                conn.Open();
                MySqlCommand comando = new MySqlCommand("SELECT * FROM Usuarios WHERE Usuario = '" + textBox1.Text + "' AND Password = '"+textBox2.Text+"'", conn);
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read(); 
                    Usuario = reader.GetString(2); 
                    Form1 forma = new Form1();
                    forma.Show();
                    this.Hide();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus(); 
                }
                else
                {
                    MessageBox.Show("El usuario o password son incorrectos");
                    textBox1.Clear();
                    textBox1.Focus();
                    textBox2.Clear();
                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.ToString(), "Error 417", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
