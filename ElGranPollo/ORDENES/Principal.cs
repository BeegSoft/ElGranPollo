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

        string fecha,fecha_ale, ds;
        int band;
        int MedioPollo=0, Pollo=0, PolloYMedio=0, DosPollos=0;

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
            if (radioButton3.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton10_Click(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton8_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {

            }
            else
            {

            }
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
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
