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
    public partial class Menu : Form
    {
        public Menu(string ds)
        {
            InitializeComponent();
            this.ds = ds;
        }
        String ds;
        private void Menu_Load(object sender, EventArgs e)
        {
            SELECT_MENU();
        }

        private void SELECT_MENU()
        {
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_platillo,nombre_platillo,precio_platillo FROM MENU", ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_menu.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elemntos = new ListViewItem(filas["id_platillo"].ToString());
                elemntos.SubItems.Add(filas["nombre_platillo"].ToString());
                elemntos.SubItems.Add(filas["precio_platillo"].ToString());

                listView_menu.Items.Add(elemntos);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu_agregar form = new Menu_agregar(ds);
            form.Show();
            //this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_menu.SelectedItems)
            {
                int id = Convert.ToInt32(lista.Text);

                this.Hide();
                Menu_modificar form = new Menu_modificar(id, ds);
                form.Show();

                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_menu.SelectedItems)
            {
                int id = Convert.ToInt32(lista.Text);


                DialogResult resultado = MessageBox.Show("Esta seguro de borrarlo del menu?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {


                        OleDbConnection conexion = new OleDbConnection(ds);

                        conexion.Open();
                        string insertar = "DELETE FROM MENU WHERE id_platillo = " + id;
                        OleDbCommand cmd = new OleDbCommand(insertar, conexion);

                        cmd.ExecuteNonQuery();
                        conexion.Close();

                    }
                    catch (DBConcurrencyException ex)
                    {
                        MessageBox.Show("Error de concurrencia:\n" + ex.Message, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    lista.Remove();
                }
                else if (resultado == DialogResult.No)
                {

                }
            }
        }
    }
}
