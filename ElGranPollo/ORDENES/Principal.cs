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

using System.Globalization;
using System.Reflection;
using System.Threading;

namespace ElGranPollo
{
    public partial class Pricipal : Form
    {
        public Pricipal(string fecha,string ds,int band)
        {
            InitializeComponent();
            this.fecha = fecha;
            this.ds = ds;
            this.band = band;

            label10.Visible = false;
            label9.Visible = false;
            dateTimePicker1.Visible = false;
            listView_fechas.Visible = false;
            button5.Visible = false;
            button6.Visible = false;

            groupBox5.Enabled = false;
        }

        string fecha,fecha_ale, ds, nombre_platillo;
        int band, id_orden, precio_pagar;

        private void Pricipal_Load(object sender, EventArgs e)
        {

            if (band == 3)
            {
                estadisticasToolStripMenuItem.Visible = false;
                gastosToolStripMenuItem.Visible = false;

                button12.Visible = false;
                groupBox4.Visible = false;
                listView_Historial.Location = new Point(123, 68);
            }

           // SELECT_HISTORIAL(fecha);

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (radio_medio.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (radio_uno.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            if (radio_unomedio.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            if (radio_dos.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton10_Click(object sender, EventArgs e)
        {
            if (radio_adobado.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton8_Click(object sender, EventArgs e)
        {
            if (radio_chiltepin.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            if (radio_chipotle.Checked)
            {

            }
            else
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fecha_ale = dateTimePicker1.Text;
            SELECT_HISTORIAL(fecha_ale);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_fechas.SelectedItems)
            {
                fecha_ale = lista.Text;                
            }
            SELECT_HISTORIAL(fecha_ale);
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Preguntar si desea terminar la orden
            DialogResult resultado = MessageBox.Show("Esta seguro de TERMINAR la orden?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();

                string insertar = "INSERT INTO PLATILLO VALUES (@id_orden, @nombre_platillo, @pagar)";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@id_orden", id_orden);
                cmd.Parameters.AddWithValue("@nombre_platillo", nombre_platillo);
                cmd.Parameters.AddWithValue("@pagar", precio_pagar);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Datos agregados correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();


                LIMPIEZA();
                pagar form = new pagar(id_orden, fecha, ds);
                form.Show();
            }
        }

        private void LIMPIEZA()
        {
            //POLLOS
            radio_medio.Checked = true;
            radio_uno.Checked = false;
            radio_unomedio.Checked = false;
            radio_dos.Checked = false;

            //TIPO DE POLLO
            radio_adobado.Checked = true;
            radio_chiltepin.Checked = false;
            radio_chipotle.Checked = false;

            //EXTRAS
            tortilla.Value = 1;
            salsa.Value = 1;
            ensalada.Value = 1;
            frijol.Value = 1;
            cebolla.Value = 1;

        }

        

        private void button12_Click(object sender, EventArgs e)
        {
            SELECT_HISTORIAL(fecha);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label10.Visible = false;
                label9.Visible = true;
                dateTimePicker1.Visible = true;
                listView_fechas.Visible = false;
                button5.Visible = true;
                button6.Visible = false;
                listView_Historial.Items.Clear();
            }
            else
            {
                label10.Visible = false;
                label9.Visible = false;
                dateTimePicker1.Visible = false;
                listView_fechas.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label10.Visible = true;
                label9.Visible = false;
                dateTimePicker1.Visible = false;
                listView_fechas.Visible = true;
                button5.Visible = false;
                button6.Visible = true;
                listView_Historial.Items.Clear();
            }
            else
            {
                label10.Visible = false;
                label9.Visible = false;
                dateTimePicker1.Visible = false;
                listView_fechas.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox5.Enabled = true;
        }
        
        private void SELECT_HISTORIAL(string date)
        {
            OleDbDataAdapter adaptador5 = new OleDbDataAdapter("SELECT id_orden, observacion, total FROM ORDEN WHERE fecha = '" + date + "'", ds);

            DataSet dataset5 = new DataSet();
            DataTable tabla5 = new DataTable();

            adaptador5.Fill(dataset5);
            tabla5 = dataset5.Tables[0];
            this.listView_Historial.Items.Clear();
            for (int i = 0; i < tabla5.Rows.Count; i++)
            {
                DataRow filas2 = tabla5.Rows[i];
                ListViewItem elemntos5 = new ListViewItem(filas2["id_orden"].ToString());
                elemntos5.SubItems.Add(filas2["observacion"].ToString());
                elemntos5.SubItems.Add(filas2["total"].ToString());

                listView_Historial.Items.Add(elemntos5);
            }

            label3.Text = date;
        }        
    }
}
