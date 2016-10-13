using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ElGranPollo
{
    public partial class Menu_modificar : Form
    {
        public Menu_modificar(int id, string ds)
        {
            InitializeComponent();
            this.id = id;
            this.ds = ds;
        }
        int id;
        string nombre_platillo;
        int precio_platillo;
        String ds;
        private void Menu_modificar_Load(object sender, EventArgs e)
        {
            

            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string select = "SELECT nombre_platillo, precio_platillo FROM MENU WHERE id_platillo=" + id;
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {


                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nombre_platillo = reader.GetString(0);
                        precio_platillo = reader.GetInt32(1);

                    }
                }
                else
                {
                    MessageBox.Show("No se pudo", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                reader.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBox_nombre.Text = nombre_platillo;
            textBox_precio.Text = precio_platillo.ToString();
            conexion.Close();
        }

        private void UPDATE_MENU()
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "UPDATE MENU SET nombre_platillo= @nombre_platillo, precio_platillo = @precio_platillo WHERE id_platillo=" + id;
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);
            cmd.Parameters.AddWithValue("@nombre_platillo", textBox_nombre.Text);
            cmd.Parameters.AddWithValue("@precio_platillo", textBox_precio.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Datos modificados correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conexion.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox_nombre.TextLength == 0 || textBox_precio.TextLength == 0)
            {
                MessageBox.Show("Tienes Campos vacios para continuar", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (textBox_nombre.TextLength >= 15)
                {
                    MessageBox.Show("No pudes poner un nombre muy largo trata de reducirlo o abreviarlo un poco", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox_nombre.Clear();
                    textBox_nombre.Focus();
                }
                else
                {

                    try
                    {
                        UPDATE_MENU();
                    }

                    catch (DBConcurrencyException ex)
                    {
                        MessageBox.Show("Error de concurrencia:\n" + ex.Message, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Menu form = new Menu(ds);
                    form.Show();

                    this.Hide();
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu form = new Menu(ds);
            form.Show();

            this.Hide();
        }
    }
}
