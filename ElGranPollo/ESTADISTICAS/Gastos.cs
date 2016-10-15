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
    public partial class Gastos : Form
    {
        public Gastos(string ds)
        {
            InitializeComponent();
            this.ds = ds;            
            label2.Visible = false;
            label1.Visible = false;
            dateTimePicker1.Visible = false;
            listView_fechas.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }

        string ds, fecha;
        private void Gastos_Load(object sender, EventArgs e)
        {
            textBox_descripcion.Focus();
            DateTime fechahoy = DateTime.Now;
            fecha = fechahoy.ToString("d");

            label_fecha.Text = fecha;
            

            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT FECHA.fecha FROM FECHA ORDER BY FECHA.id ASC", ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_fechas.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elementos = new ListViewItem(filas["fecha"].ToString());
                listView_fechas.Items.Add(elementos);
            }

            SELECT_GASTOS();
            SUMA_GASTOS();
            GANANCIAS();
        }

        private void SELECT_GASTOS()
        {
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_gastos, fecha, descripcion, gasto FROM GASTOS WHERE fecha = '" + fecha + "'", ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_gastos.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elementos = new ListViewItem(filas["id_gastos"].ToString());
                elementos.SubItems.Add(filas["fecha"].ToString());
                elementos.SubItems.Add(filas["descripcion"].ToString());
                elementos.SubItems.Add(filas["gasto"].ToString());

                listView_gastos.Items.Add(elementos);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
 

        int ventas, gastos, ganancias;

        private void GANANCIAS()
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string select = "SELECT Venta_total, Gastos FROM FECHA WHERE fecha='" + fecha + "'";
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ventas = reader.GetInt32(0);
                        gastos = reader.GetInt32(1);

                        ganancias = ventas - gastos;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ACTUALIZAR LAS GANANCIAS
            string actualizar = "UPDATE FECHA SET Ganancia = @Ganancia WHERE fecha = '" + fecha + "'";
            OleDbCommand cmd3 = new OleDbCommand(actualizar, conexion);
            cmd3.Parameters.AddWithValue("@Ganancia", ganancias);

            cmd3.ExecuteNonQuery();

            conexion.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();
            if ((textBox_descripcion.Text == "") || (textBox_gasto.Text == ""))
            {
                MessageBox.Show("No has introduccido la informacion necesaria", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_descripcion.Focus();
            }
            else
            {
                string insertar = "INSERT INTO GASTOS (fecha, descripcion, gasto) VALUES (@fecha, @descripcion, @gasto)";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@descripcion", textBox_descripcion.Text);
                cmd.Parameters.AddWithValue("@gasto", Convert.ToInt32(textBox_gasto.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Datos agregados correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();

                SELECT_GASTOS();

                SUMA_GASTOS();

                GANANCIAS();

                textBox_descripcion.Clear();
                textBox_gasto.Clear();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label2.Visible = false;
                label1.Visible = true;
                dateTimePicker1.Visible = true;
                listView_fechas.Visible = false;
                button2.Visible = true;
                button3.Visible = false;
                listView_gastos.Items.Clear();
            }
            else
            {
                label2.Visible = false;
                label1.Visible = false;
                dateTimePicker1.Visible = false;
                listView_fechas.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }            
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label2.Visible = true;
                label1.Visible = false;
                dateTimePicker1.Visible = false;
                listView_fechas.Visible = true;
                button2.Visible = false;
                button3.Visible = true;
                listView_gastos.Items.Clear();
            }
            else
            {
                label2.Visible = false;
                label1.Visible = false;
                dateTimePicker1.Visible = false;
                listView_fechas.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fecha = dateTimePicker1.Text;
            label_fecha.Text = fecha;
            textBox_descripcion.Focus();
            SELECT_GASTOS();
            SUMA_GASTOS();
            GANANCIAS();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_fechas.SelectedItems)
            {
                fecha = lista.Text;
                label_fecha.Text = fecha;
            }
            textBox_descripcion.Focus();
            SELECT_GASTOS();
            SUMA_GASTOS();
            GANANCIAS();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_gastos.SelectedItems)
            {
                int id = Convert.ToInt32(lista.Text);


                DialogResult resultado = MessageBox.Show("Esta seguro de borrarlo de los gastos?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        OleDbConnection conexion = new OleDbConnection(ds);

                        conexion.Open();
                        string insertar = "DELETE FROM GASTOS WHERE id_gastos = " + id;
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

            SELECT_GASTOS();
            SUMA_GASTOS();
            GANANCIAS();
        }

        private void SUMA_GASTOS()
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();
            
            
            //SUMAR TODOS LOS GASTOS

            string sql = "select SUM(gasto) from GASTOS WHERE fecha = '" + fecha + "'";
            OleDbCommand cmd2 = new OleDbCommand(sql, conexion); //Conexion es tu objeto conexion                                            

            string suma_gasto = ((cmd2.ExecuteScalar()).ToString());
            if (suma_gasto == "")
            {
                suma_gasto = "0";
            }            
            //-------------------
            textBox_total.Text = suma_gasto;

            //ACTUALIZAR LOS GASTOS DE LA FECHA

            string actualizar = "UPDATE FECHA SET gastos = @gastos WHERE fecha = '" + fecha + "'";
            OleDbCommand cmd3 = new OleDbCommand(actualizar, conexion);
            cmd3.Parameters.AddWithValue("@gastos", suma_gasto);

            cmd3.ExecuteNonQuery();

            conexion.Close();
        }
    }
}
