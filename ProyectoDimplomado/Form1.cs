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
    public partial class Form1 : Form
    {
        float total = 0.0f;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Location = new Point(0, 0);
            label5.Text = "Le atiende: " + Usuarios.Usuario;
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, 0);
            label2.Text = DateTime.Now.ToLongTimeString() + DateTime.Now.ToLongDateString();
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label1.Height);
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height);
            dataGridView1.Location = new Point(10, label1.Height + label2.Height + label3.Height);
            dataGridView1.Width = this.Width - 20;
            dataGridView1.Height = this.Height / 4 * 3;
            textBox1.Location = new Point(10, label1.Height + label2.Height + label3.Height + dataGridView1.Height);
            textBox1.Width = this.Width - 20;
            label4.Location = new Point(this.Width - label4.Width -100, label1.Height + label2.Height + label3.Height + dataGridView1.Height + textBox1.Height);
            button1.Location = new Point(10, label1.Height + label2.Height + label3.Height + dataGridView1.Height + textBox1.Height);
            textBox1.Focus();         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString() + DateTime.Now.ToLongDateString(); 
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                    calculartotal();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar producto por que ya no tienes");
                }

            }
            if (e.KeyValue == 13) {
                MessageBox.Show("");
                MySqlConnection conn = new MySqlConnection("server=localhost; user=root; database=proyectodimplomado; SSL Mode = none");
                
                try
                {
                    conn.Open();
                    MySqlCommand comando = new MySqlCommand("SELECT * FROM productos WHERE IdProducto = "+textBox1.Text,conn);
                    MySqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows ) 
                    {
                        reader.Read();
                        dataGridView1.Rows.Add("1", reader.GetString(1), reader.GetString(2), reader.GetString(2));
                        calculartotal();
                        textBox1.Clear(); 
                    } 
                    else 
                    {
                        MessageBox.Show("El producto no se ha encontrado");
                        textBox1.Clear();
                        textBox1.Focus(); 
                    }
                }
                catch (Exception error) {

                    MessageBox.Show(error.ToString(),"Error 417", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            
            }
        }

        private void calculartotal() {
            total = 0.0f;
            for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                total+=float.Parse(dataGridView1[3, i].Value.ToString()); 
            }
            label4.Text = "Total: $"+total.ToString(); 
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'p')
            {
                e.Handled = true;
                //MessageBox.Show("¿Vas a pagar?" + textBox1.Text);
                try
                {
                    float pago = float.Parse(textBox1.Text);
                    label4.Text = "Cambio $" + (pago - total);
                    textBox1.Clear();
                    dataGridView1.Rows.Clear();
                    
                }
                catch(Exception) 
                {
                    label4.Text = "Cantidad incorrecta";
                }
                textBox1.Clear(); 
                textBox1.Focus();
            }

           
        }

        
    }
}
