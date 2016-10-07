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
    public partial class Menu_agregar : Form
    {
        public Menu_agregar(string ds)
        {
            InitializeComponent();
            this.ds = ds;
        }
        int id;
        String ds;
        private void Menu_agregar_Load(object sender, EventArgs e)
        {            
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string select = "SELECT MAX(id_platillo) FROM MENU";
            OleDbCommand cmd = new OleDbCommand(select, conexion);

            try
            {


                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0) + 1;
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
            conexion.Close();
        }

        private void INSERT_MENU()
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "INSERT INTO MENU VALUES (@id_platillo, @nombre_platillo, @precio_platillo)";
            OleDbCommand cmd = new OleDbCommand(insertar, conexion);
            cmd.Parameters.AddWithValue("@id_platillo", id);
            cmd.Parameters.AddWithValue("@nombre_platillo", textBox_nombre.Text);
            cmd.Parameters.AddWithValue("@precio_platillo", textBox_precio.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Datos agregados correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conexion.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int convertir = 0;
            bool convertido = int.TryParse(textBox_precio.Text, out convertir);

            if (textBox_nombre.TextLength == 0 || textBox_precio.TextLength == 0)
            {
                MessageBox.Show("Tienes Campos vacios para continuar", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if (convertido == false) {
                MessageBox.Show("El campo 'Precio' solo puede contener numeros", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        INSERT_MENU();
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

                    this.Close();
                }
            }            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu form = new Menu(ds);
            form.Show();

            this.Close();
        }

        private void textBox_precio_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
