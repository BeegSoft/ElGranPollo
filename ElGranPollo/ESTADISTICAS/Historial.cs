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
    public partial class Historial : Form
    {
        public Historial(string ds,string fecha)
        {
            InitializeComponent();
            this.ds = ds;
            this.fecha = fecha;
        }

        //CONEXIONES
        string ds,fecha;
        int fechaa, fechab;

        private void SELECT_FECHA()
        {
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

        }

        private void Historial_Load(object sender, EventArgs e)
        {
            textBox_clientes.Text = "0";
            textBox_empanadas.Text = "0";
            textBox_gastos.Text = "0";
            textBox_ganancias.Text = "0";
            textBox_ventas.Text = "0";
            //-------------------------------
            SELECT_FECHA();
            //------------------------------

            //separacion del contenido del calendario
            DateTime fechahoy = DateTime.Now;
            string fechas = fechahoy.ToString("d");

            DataSet dss = new DataSet();
            //indicamos la consulta en SQL
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT nombre_platillo FROM MENU", ds);
            //se indica el nombre de la tabla
            da.Fill(dss, "Nombre_platillo");
            comboBox_platillos.DataSource = dss.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            comboBox_platillos.ValueMember = "nombre_platillo";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Primera fecha
            string var1 = fechaA.Text;
            var1 = var1.Substring(0, 2);

            string var2 = fechaA.Text;
            var2 = var2.Substring(3, 2);

            string var3 = fechaA.Text;
            var3 = var3.Substring(6, 4);

            //juntando las cadenas
            string FECHAA = string.Concat(var3, var2, var1);
            fechaa = Convert.ToInt32(FECHAA);
            //----------------

            //Segunda fecha
            var1 = fechaB.Text;
            var1 = var1.Substring(0, 2);

            var2 = fechaB.Text;
            var2 = var2.Substring(3, 2);

            var3 = fechaB.Text;
            var3 = var3.Substring(6, 4);

            //juntando las cadenas
            string FECHAB = string.Concat(var3, var2, var1);
            fechab = Convert.ToInt32(FECHAB);

            if (fechab < fechaa)
            {
                MessageBox.Show("Seleccione una fecha mayor a la primera fecha", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //----------------

                //PLATILLOS DEL COMBO
                METODOCOMBO();
                //----------------------

                //SUMAR LAS VENTAS DE LOS PLATILLOS SELECCIONADOS
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();
                try
                {
                    string select = "SELECT SUM(ORDEN.total) FROM ORDEN INNER JOIN FECHA ON ORDEN.fecha = FECHA.fecha WHERE FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

                    OleDbCommand cmd2 = new OleDbCommand(select, conexion); //Conexion es tu objeto conexion                                

                    textBox_ventas.Text = (cmd2.ExecuteScalar()).ToString();
                    if (textBox_ventas.Text == "")
                    {
                        textBox_ventas.Text = "0";
                    }

                    //...................................

                    //SUMA GASTOS
                    string select5 = "SELECT SUM(Gastos) FROM FECHA WHERE FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

                    OleDbCommand cmd5 = new OleDbCommand(select5, conexion); //Conexion es tu objeto conexion                                

                    textBox_gastos.Text = (cmd5.ExecuteScalar()).ToString();
                    if (textBox_gastos.Text == "")
                    {
                        textBox_gastos.Text = "0";
                    }
                    //--------------------------------

                    //SUMA GANANCIAS
                    string select6 = "SELECT SUM(Ganancia) FROM FECHA WHERE FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

                    OleDbCommand cmd6 = new OleDbCommand(select6, conexion); //Conexion es tu objeto conexion                                

                    textBox_ganancias.Text = (cmd6.ExecuteScalar()).ToString();
                    if (textBox_ganancias.Text == "")
                    {
                        textBox_ganancias.Text = "0";
                    }
                    //--------------------------------

                    //SUMAR LAS VENTAS DE LOS PLATILLOS SELECCIONADOS

                    string select2 = "SELECT COUNT(ORDEN.total) FROM ORDEN INNER JOIN FECHA ON ORDEN.fecha = FECHA.fecha WHERE FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

                    OleDbCommand cmd3 = new OleDbCommand(select2, conexion); //Conexion es tu objeto conexion                                

                    textBox_clientes.Text = (cmd3.ExecuteScalar()).ToString();
                    if (textBox_clientes.Text == "")
                    {
                        textBox_clientes.Text = "0";
                    }

                    //--------------------------------------------------------

                    //TOTAL DE POLLOS VENDIDAS EN EL PERIODO SELECCIONADO

                    string select3 = "SELECT SUM(PLATILLO.cantidad) FROM(FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha) INNER JOIN PLATILLO ON ORDEN.id_orden = PLATILLO.id_orden WHERE FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

                    OleDbCommand cmd4 = new OleDbCommand(select3, conexion); //Conexion es tu objeto conexion                                

                    textBox_empanadas.Text = (cmd4.ExecuteScalar()).ToString();
                    if (textBox_empanadas.Text == "")
                    {
                        textBox_empanadas.Text = "0";
                    }

                    //----------------------------------------------------------
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error con la base de datos!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //...................................

                //LLENAR LA GRAFICA CON VENTAS
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }

                string select4 = "SELECT  * FROM FECHA WHERE FECHA.id >= " + fechaa + " AND FECHA.id <= " + fechab + " ORDER BY FECHA.id";
                OleDbCommand cmd = new OleDbCommand(select4, conexion);
                try
                {
                    OleDbDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        chart1.Series["Ventas"].Points.AddXY(reader["fecha"].ToString(), reader["Venta_total"].ToString());
                        chart1.Series["Gastos"].Points.AddXY(reader["fecha"].ToString(), reader["Gastos"].ToString());
                        chart1.Series["Ganancias"].Points.AddXY(reader["fecha"].ToString(), reader["Ganancia"].ToString());

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                conexion.Close();

                //cambio de tamaño de la forma            
                label13.Visible = false;
                this.Size = new Size(1104, 521);
                label1.Visible = true;
                //---------------------------
            }
        }

        private void gastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gastos form = new Gastos(ds);
            form.ShowDialog();
        }

        private void comboBox_platillos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PLATILLOS DEL COMBO
            METODOCOMBO();
        }

        private void almacenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Primera fecha
            string var1 = fecha;
            var1 = var1.Substring(0, 2);

            string var2 = fecha;
            var2 = var2.Substring(3, 2);

            string var3 = fecha;
            var3 = var3.Substring(6, 4);

            //juntando las cadenas
            string FECHAA = string.Concat(var3, var2, var1);
            fechaa = Convert.ToInt32(FECHAA);//----------------

            //PLATILLOS DEL COMBO
            METODOCOMBO();
            //----------------------

            //SUMAR LAS VENTAS DE LOS PLATILLOS SELECCIONADOS
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();
            try
            {
                string select = "SELECT SUM(ORDEN.total) FROM ORDEN INNER JOIN FECHA ON ORDEN.fecha = FECHA.fecha WHERE FECHA.id = " + fechaa;

                OleDbCommand cmd2 = new OleDbCommand(select, conexion); //Conexion es tu objeto conexion                                

                textBox_ventas.Text = (cmd2.ExecuteScalar()).ToString();
                if (textBox_ventas.Text == "")
                {
                    textBox_ventas.Text = "0";
                }

                //...................................

                //SUMA GASTOS
                string select5 = "SELECT SUM(Gastos) FROM FECHA WHERE FECHA.id = " + fechaa;

                OleDbCommand cmd5 = new OleDbCommand(select5, conexion); //Conexion es tu objeto conexion                                

                textBox_gastos.Text = (cmd5.ExecuteScalar()).ToString();
                if (textBox_gastos.Text == "")
                {
                    textBox_gastos.Text = "0";
                }
                //--------------------------------

                //SUMA GANANCIAS
                string select6 = "SELECT SUM(Ganancia) FROM FECHA WHERE FECHA.id = " + fechaa ;

                OleDbCommand cmd6 = new OleDbCommand(select6, conexion); //Conexion es tu objeto conexion                                

                textBox_ganancias.Text = (cmd6.ExecuteScalar()).ToString();
                if (textBox_ganancias.Text == "")
                {
                    textBox_ganancias.Text = "0";
                }
                //--------------------------------

                //SUMAR LAS VENTAS DE LOS PLATILLOS SELECCIONADOS

                string select2 = "SELECT COUNT(ORDEN.total) FROM ORDEN INNER JOIN FECHA ON ORDEN.fecha = FECHA.fecha WHERE FECHA.id = " + fechaa;

                OleDbCommand cmd3 = new OleDbCommand(select2, conexion); //Conexion es tu objeto conexion                                

                textBox_clientes.Text = (cmd3.ExecuteScalar()).ToString();
                if (textBox_clientes.Text == "")
                {
                    textBox_clientes.Text = "0";
                }

                //--------------------------------------------------------

                //TOTAL DE POLLOS VENDIDAS EN EL PERIODO SELECCIONADO

                string select3 = "SELECT SUM(PLATILLO.cantidad) FROM(FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha) INNER JOIN PLATILLO ON ORDEN.id_orden = PLATILLO.id_orden WHERE FECHA.id = " + fecha;

                OleDbCommand cmd4 = new OleDbCommand(select3, conexion); //Conexion es tu objeto conexion                                

                textBox_empanadas.Text = (cmd4.ExecuteScalar()).ToString();
                if (textBox_empanadas.Text == "")
                {
                    textBox_empanadas.Text = "0";
                }

                //----------------------------------------------------------
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error con la base de datos!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //...................................

            //LLENAR LA GRAFICA CON VENTAS
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            string select4 = "SELECT  * FROM FECHA WHERE FECHA.id = " + fechaa;
            OleDbCommand cmd = new OleDbCommand(select4, conexion);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    chart1.Series["Ventas"].Points.AddXY(reader["fecha"].ToString(), reader["Venta_total"].ToString());
                    chart1.Series["Gastos"].Points.AddXY(reader["fecha"].ToString(), reader["Gastos"].ToString());
                    chart1.Series["Ganancias"].Points.AddXY(reader["fecha"].ToString(), reader["Ganancia"].ToString());

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            conexion.Close();

            //cambio de tamaño de la forma            
            label13.Visible = false;
            this.Size = new Size(1104, 521);
            label1.Visible = true;
            //---------------------------
        }

        private void METODOCOMBO()
        {
            //SUMAR LAS VENTAS DE LOS PLATILLOS SELECCIONADOS
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();


            string select = "SELECT SUM(PLATILLO.pagar) FROM(FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha) INNER JOIN PLATILLO ON ORDEN.id_orden = PLATILLO.id_orden WHERE PLATILLO.nombre_platillo = '" + comboBox_platillos.Text + "'" + "AND FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

            OleDbCommand cmd2 = new OleDbCommand(select, conexion); //Conexion es tu objeto conexion                                

            textBox_venta_platillo.Text = (cmd2.ExecuteScalar()).ToString();
            if (textBox_venta_platillo.Text == "")
            {
                textBox_venta_platillo.Text = "0";
            }
            //...................................

            //CONTAR TODOS LOS PLATILLOS QUE SE VENDIERON                       

            string select2 = "SELECT SUM(PLATILLO.cantidad) FROM(FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha) INNER JOIN PLATILLO ON ORDEN.id_orden = PLATILLO.id_orden WHERE PLATILLO.nombre_platillo = '" + comboBox_platillos.Text + "'" + "AND FECHA.id >= " + fechaa + "AND FECHA.id <= " + fechab;

            OleDbCommand cmd3 = new OleDbCommand(select2, conexion); //Conexion es tu objeto conexion                                

            textBox_platillos_vendidos.Text = (cmd3.ExecuteScalar()).ToString();
            if (textBox_platillos_vendidos.Text == "")
            {
                textBox_platillos_vendidos.Text = "0";
            }

            conexion.Close();
        }
    }
}