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
    public partial class pagar : Form
    {
        public pagar(int id_orden, string fecha, string ds)
        {
            InitializeComponent();
            this.id_orden = id_orden;
            this.fecha = fecha;
            this.ds = ds;
        }


        public int id_orden;
        public string total_pagar, fecha,operador, ds;

        public void SetDefaultCulture(CultureInfo culture)
        {
            Type type = typeof(CultureInfo);

            try
            {
                type.InvokeMember("s_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });

                type.InvokeMember("s_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }

            try
            {
                type.InvokeMember("m_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });

                type.InvokeMember("m_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }
        }

        private void pagar_Load(object sender, EventArgs e)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string select = "SELECT observacion FROM ORDEN WHERE id_orden=" + id_orden;
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        textBox_descripcion.Text = reader.GetString(0);
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

            SELECT_PLATILLO();

            OleDbConnection conexion4 = new OleDbConnection(ds);

            conexion4.Open();

            string sql = "select SUM(pagar) from PLATILLO WHERE id_orden=" + id_orden;
            OleDbCommand cmd2 = new OleDbCommand(sql, conexion4); //Conexion es tu objeto conexion

            total_pagar = (cmd2.ExecuteScalar()).ToString();

            textBox_total.Text = total_pagar;

            textBox_efectivo.Focus();
            conexion4.Close();
        }

        private void SELECT_PLATILLO()
        {
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT nombre_platillo,cantidad,pagar FROM PLATILLO WHERE id_orden = " + id_orden, ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_pagar.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elemntos = new ListViewItem(filas["nombre_platillo"].ToString());
                elemntos.SubItems.Add(filas["cantidad"].ToString());
                elemntos.SubItems.Add(filas["pagar"].ToString());

                listView_pagar.Items.Add(elemntos);

            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            double cambio, total, efectivo;
            SetDefaultCulture(new CultureInfo("es-MX"));

            try
            {
                total = double.Parse(textBox_total.Text, Thread.CurrentThread.CurrentCulture);
                if (textBox_efectivo.Text == "")
                {
                    efectivo = 0;
                }
                else
                {
                    efectivo = double.Parse(textBox_efectivo.Text, Thread.CurrentThread.CurrentCulture);
                }

                cambio = efectivo - total;

                if (cambio < 0)
                {
                    textBox_cambio.Text = ("Faltan $" + (cambio * (-1)));
                }
                else
                {
                    textBox_cambio.Text = cambio.ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Introdujo datos incorrectos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_efectivo.Text = "";
                textBox_efectivo.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox_efectivo.Text = textBox_total.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //QUITAR LA ORDEN DEL LISTVIEW QUE SIGNIFICA QUE YA HA PAGADO Y NO TIENE PORQUE APARECER AHI

            OleDbConnection conexion4 = new OleDbConnection(ds);

            conexion4.Open();

            string insertar = "UPDATE ORDEN SET checador = @checador WHERE id_orden=" + id_orden;
            OleDbCommand cmd3 = new OleDbCommand(insertar, conexion4);
            cmd3.Parameters.AddWithValue("@checador", 0);

            cmd3.ExecuteNonQuery();

            MessageBox.Show("Pago con exito", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conexion4.Close();

            IForm2 formInterface = this.Owner as IForm2;
            if (formInterface != null)
                formInterface.Relogear_ordenes();


            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //QUITAR LA ORDEN DEL LISTVIEW QUE SIGNIFICA QUE YA HA PAGADO Y NO TIENE PORQUE APARECER AHI

            if (textBox_efectivo == null)
            {
                MessageBox.Show("no puedes imprimir", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                OleDbConnection conexion4 = new OleDbConnection(ds);

                conexion4.Open();

                string insertar = "UPDATE ORDEN SET checador = @checador WHERE id_orden=" + id_orden;
                OleDbCommand cmd3 = new OleDbCommand(insertar, conexion4);
                cmd3.Parameters.AddWithValue("@checador", 0);

                cmd3.ExecuteNonQuery();

                MessageBox.Show("Pago con exito", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion4.Close();

                CrearTicket ticket = new CrearTicket();
                ticket.AbreCajon();
                ticket.TextoCentro("LAS EMPANADAS DE MI AMA");
                ticket.TextoIzquierda("EXPEDIDO EN: LOCAL PRINCIPAL");
                ticket.TextoIzquierda("DIREC: CALLE 16 Y AV TECNOLOGICO");
                ticket.TextoIzquierda("TELEFONO: 01 662 176 3999 ");
                ticket.TextoIzquierda("RFC XXXXXX-XXXXXXX-XXXXX");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("ATENDIO: "+operador);
                ticket.TextoExtremos("CLIENTE: ", textBox_descripcion.Text);
                ticket.TextoIzquierda("");
                ticket.TextoExtremos("FECHA:" + DateTime.Now.ToShortDateString(), "HORA:" + DateTime.Now.ToShortTimeString());
                ticket.lineasAsteriscos();
                //Articulos a vender.
                ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                ticket.lineasAsteriscos();


                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();

                string select = "SELECT descripcion FROM ORDEN WHERE id_orden=" + id_orden;
                OleDbCommand cmd = new OleDbCommand(select, conexion);
                try
                {


                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox_descripcion.Text = reader.GetString(0);

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

                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT nombre_platillo,cantidad,pagar FROM PLATILLO WHERE id_orden = " + id_orden, conexion);

                DataSet dataset = new DataSet();
                DataTable tabla = new DataTable();

                adaptador.Fill(dataset);
                tabla = dataset.Tables[0];
                //this.listView_pagar.Items.Clear();
                int c;
                c = tabla.Rows.Count;
                for (int i = 0; i < tabla.Rows.Count; i++)
                {


                    DataRow filas = tabla.Rows[i];
                    ListViewItem elemntos = new ListViewItem(filas["nombre_platillo"].ToString());
                    elemntos.SubItems.Add(filas["cantidad"].ToString());
                    elemntos.SubItems.Add(filas["pagar"].ToString());
                    decimal pagar = decimal.Parse(filas["pagar"].ToString());
                    int cantidad = int.Parse(filas["cantidad"].ToString());
                    decimal precio = pagar / cantidad;
                    string artic = filas["nombre_platillo"].ToString();

                    string arti = artic;
                    if (artic.Length != 15)
                    {
                        int canti = Convert.ToInt32(artic.Length);
                        for (int h = canti; h < 15; h++)
                        {
                            arti = arti + ".";
                        }
                    }
                    artic = arti;
                    //listView_pagar.Items.Add(elemntos);
                    ticket.AgregaArticulo(artic, int.Parse(filas["cantidad"].ToString()), precio, decimal.Parse(filas["pagar"].ToString()));

                }
                //ticket.AgregarTotales("         SUBTOTAL......$", 100);
                //ticket.AgregarTotales("         IVA...........$", 10.04M);//La M indica que es un decimal en C#
                //ticket.AgregaArticulo("Articulo B", 1, 10, 10);
                //ticket.AgregaArticulo("Este es un nombre largo del articulo, para mostrar como se bajan las lineas", 1, 30, 30);
                ticket.AgregarTotales("   TOTAL......$", System.Convert.ToDecimal(textBox_total.Text));
                ticket.TextoIzquierda("");
                ticket.AgregarTotales("   EFECTIVO...$", System.Convert.ToDecimal(textBox_efectivo.Text));
                ticket.AgregarTotales("   CAMBIO.....$", System.Convert.ToDecimal(textBox_cambio.Text));
                //Texto final del Ticket.
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("ARTICULOS VENDIDOS: " + c);
                ticket.TextoIzquierda("");
                ticket.TextoCentro("¡GRACIAS POR SU COMPRA!");
                ticket.CortaTicket();
                //ticket.ImprimirTicket("Microsoft XPS Document Writer");//Nombre de la impresora ticketera
                ticket.ImprimirTicket("POS-58(copy of 5)");


                conexion.Close();

                IForm2 formInterface = this.Owner as IForm2;
                if (formInterface != null)
                    formInterface.Relogear_ordenes();


                this.Close();
            }
        }
    }
}
