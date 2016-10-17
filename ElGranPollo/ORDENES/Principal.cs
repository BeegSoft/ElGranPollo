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
        public Pricipal(string fecha,string ds,string ds2,int band)
        {
            InitializeComponent();
            this.fecha = fecha;
            this.ds = ds;
            this.ds2 = ds2;
            this.band = band;

            label10.Visible = false;
            label9.Visible = false;
            dateTimePicker1.Visible = false;
            listView_fechas.Visible = false;
            button5.Visible = false;
            button6.Visible = false;

            groupBox5.Enabled = false;
        }

        public string fecha,fecha_ale, ds,ds2, nombre_platillo, opcion_cantidad_pollo, opcion_tipo_pollo, observacion;
        public int band, id_orden, precio_pagar, cantidad_extras, extras_neto, cobro_extra, precio_extra, total_pagar;
        public bool bandera_domicilio = false,banda;

        private void Pricipal_Load(object sender, EventArgs e)
        {
            if (band == 3)
            {
                estadisticasToolStripMenuItem.Visible = false;
                gastosToolStripMenuItem.Visible = false;
                menuToolStripMenuItem.Visible = false;

                button12.Visible = false;
                groupBox4.Visible = false;
                listView_Historial.Location = new Point(123, 68);
            }

           SELECT_HISTORIAL(fecha);

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

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(fecha);
            LIMPIEZA();
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radio_dos_CheckedChanged(object sender, EventArgs e)
        {
            if(radio_dos.Checked == true)
            {
                extra_tortilla.Value = 2;
                extra_salsa.Value = 2;
                extra_ensalada.Value = 2;
                extra_frijol.Value = 2;
                extra_cebolla.Value = 2;
            }
        }

        private void radio_unomedio_CheckedChanged(object sender, EventArgs e)
        {
            if(radio_unomedio.Checked == true)
            {
                extra_tortilla.Value = 1;
                extra_salsa.Value = 1;
                extra_ensalada.Value = 1;
                extra_frijol.Value = 1;
                extra_cebolla.Value = 1;
            }
        }

        private void radio_uno_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_uno.Checked == true)
            {
                extra_tortilla.Value = 1;
                extra_salsa.Value = 1;
                extra_ensalada.Value = 1;
                extra_frijol.Value = 1;
                extra_cebolla.Value = 1;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string selet = "SELECT nombre_cliente,calle_y_numero,entre_1,entre_2,descripcion_casa FROM DOMICILIO WHERE telefono='" + textBox1.Text + "'";
            OleDbCommand cm = new OleDbCommand(selet, conexion);
            try
            {
                OleDbDataReader reader = cm.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                        

                        textBox2.Text = reader.GetString(0);                        

                        textBox4.Text = reader.GetString(1);                                                     

                        textBox5.Text = reader.GetString(2);                                                      

                        textBox6.Text = reader.GetString(3);                                                    

                        textBox3.Text = reader.GetString(4);
                        //--------------------------------
                        banda = true;                        
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    banda = false;                    
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            conexion.Close();
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();
            if (banda == true)
            {
                banda = false;
                string insertar = "UPDATE DOMICILIO SET telefono= @telefono, nombre_cliente = @nombre_cliente, calle_y_numero= @calle_y_numero, entre_1 = @entre_1, entre_2= @entre_2, descripcion_casa = @descripcion_casa WHERE telefono='" + textBox1.Text + "'";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@telefono", textBox1.Text);
                cmd.Parameters.AddWithValue("@nombre_cliente", textBox2.Text);
                cmd.Parameters.AddWithValue("@calle_y_numero", textBox4.Text);
                cmd.Parameters.AddWithValue("@entre_1", textBox5.Text);
                cmd.Parameters.AddWithValue("@entre_2", textBox6.Text);
                cmd.Parameters.AddWithValue("@descripcion_casa", textBox3.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Datos modificados correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            else
            {
                if (textBox1.Text==""||textBox2.Text==""|| textBox3.Text == "" || textBox4.Text == ""|| textBox5.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Algunos campos estan vacios", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string insertar = "INSERT INTO DOMICILIO(telefono,nombre_cliente,calle_y_numero,entre_1,entre_2,descripcion_casa) VALUES (@telefono, @nombre_cliente, @calle_y_numero, @entre_1, @entre_2, @descripcion_casa)";
                    OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                    cmd.Parameters.AddWithValue("@telefono", textBox1.Text);
                    cmd.Parameters.AddWithValue("@nombre_cliente", textBox2.Text);
                    cmd.Parameters.AddWithValue("@calle_y_numero", textBox4.Text);
                    cmd.Parameters.AddWithValue("@entre_1", textBox5.Text);
                    cmd.Parameters.AddWithValue("@entre_2", textBox6.Text);
                    cmd.Parameters.AddWithValue("@descripcion_casa", textBox3.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datos agregados correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }                        
            conexion.Close();
        }

        private void estadisticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Historial corre = new Historial(ds,fecha);
            corre.Show();
        }

        private void gastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gastos corre = new Gastos(ds);
            corre.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control_acceso corre = new Control_acceso(ds, ds2);
            corre.Show();
            this.Hide();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu corre = new Menu(ds);
            corre.Show();
        }

        private void radio_medio_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_medio.Checked == true)
            {
                extra_tortilla.Value = 1;
                extra_salsa.Value = 1;
                extra_ensalada.Value = 1;
                extra_frijol.Value = 1;
                extra_cebolla.Value = 1;
            }
        }

        //BOTON DE TERMINAR
        private void button7_Click(object sender, EventArgs e)
        {
            //crear la orden
            OleDbConnection conexion2 = new OleDbConnection(ds);

            conexion2.Open();

            string maximo2 = "SELECT MAX(id_orden) FROM ORDEN";
            OleDbCommand cmd4 = new OleDbCommand(maximo2, conexion2);
            id_orden = 1 + Convert.ToInt32(cmd4.ExecuteScalar());

            conexion2.Close();

            //Obtener el numero de orden
            /*OleDbConnection conexion2 = new OleDbConnection(ds);

            conexion2.Open();

            string maximo = "SELECT MAX(id_orden) FROM ORDEN";
            OleDbCommand cmd3 = new OleDbCommand(maximo, conexion2);
            id_orden = Convert.ToInt32(cmd3.ExecuteScalar());
            conexion2.Close();*/

            /*===============Seleccion de cantidad de pollo=====================*/
            if (radio_medio.Checked == true) opcion_cantidad_pollo = "Medio";

            else if (radio_uno.Checked == true) opcion_cantidad_pollo = "Uno";

            else if (radio_unomedio.Checked == true) opcion_cantidad_pollo = "Uno_y_medio";

            else if (radio_dos.Checked == true) opcion_cantidad_pollo = "Dos";

            /*==========Seleccionar el precio del pollo=========================*/
            OleDbConnection conexion3 = new OleDbConnection(ds);

            conexion3.Open();

            string select = "SELECT precio_platillo FROM MENU WHERE nombre_platillo='" + opcion_cantidad_pollo + "'";
            OleDbCommand cmd2 = new OleDbCommand(select, conexion3);
            try
            {
                OleDbDataReader reader = cmd2.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        precio_pagar = reader.GetInt32(0);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error orden" + ex);
            }

            /*==============Seleccion de tipo de pollo==========================*/
            if (radio_adobado.Checked == true) opcion_tipo_pollo = "adobado";

            else if (radio_chiltepin.Checked == true) opcion_tipo_pollo = "chiltepin";

            else if (radio_chipotle.Checked == true) opcion_tipo_pollo = "chipotle";


            /*=================SELECCION DE EXTRAS==================================*/

            cantidad_extras = Convert.ToInt32(extra_cebolla.Value) + Convert.ToInt32(extra_ensalada.Value) + Convert.ToInt32(extra_frijol.Value) + Convert.ToInt32(extra_salsa.Value) + Convert.ToInt32(extra_tortilla.Value);

            cobro_extra = 0;
            if (cantidad_extras > 5)
            {
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();

                string select2 = "SELECT precio_platillo FROM MENU WHERE nombre_platillo='extra'";
                OleDbCommand cmd = new OleDbCommand(select2, conexion);
                try
                {
                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            precio_extra = reader.GetInt32(0);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error orden" + ex);
                }

                if (radio_dos.Checked == true)
                {
                    if(cantidad_extras > 10)
                    {
                        extras_neto = cantidad_extras - 10;
                        cobro_extra = extras_neto * precio_extra;

                        total_pagar = cobro_extra + precio_pagar;
                    }
                }
                
                extras_neto = cantidad_extras - 5;
                cobro_extra = extras_neto * precio_extra;

                total_pagar = cobro_extra + precio_pagar;
            }

            //TIPO DE PAGO

            if (radio_efectivo.Checked == true) observacion = "Efectivo";
            else if (radioCredito.Checked == true) observacion = "Credito";

            //Preguntar si desea terminar la orden
            DialogResult resultado = MessageBox.Show("Esta seguro de TERMINAR la orden?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                //verificar si no ha guardado los datos del domicilio
                if(bandera_domicilio == true)
                {
                    //METODO PARA GUARDAR EL DOMICILIO
                }

                //Insertar todos los datos del pedido a la base de datos
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();
                if (cantidad_extras > 5)
                {
                    string insertar10 = "INSERT INTO ORDEN (total, fecha, observaciones) VALUES (@total, @fecha, @observacion)";
                    OleDbCommand cmd10 = new OleDbCommand(insertar10, conexion);
                    cmd10.Parameters.AddWithValue("@total", total_pagar);
                    cmd10.Parameters.AddWithValue("@fecha", fecha);
                    cmd10.Parameters.AddWithValue("@observaciones", observacion);

                    cmd10.ExecuteNonQuery();
                }
                else
                {
                    string insertar10 = "INSERT INTO ORDEN (total, fecha, observaciones) VALUES (@total, @fecha, @observacion)";
                    OleDbCommand cmd10 = new OleDbCommand(insertar10, conexion);
                    cmd10.Parameters.AddWithValue("@total", precio_pagar);
                    cmd10.Parameters.AddWithValue("@fecha", fecha);
                    cmd10.Parameters.AddWithValue("@observaciones", observacion);

                    cmd10.ExecuteNonQuery();
                }

                string insertar = "INSERT INTO PLATILLO (id_orden, nombre_platillo, cantidad, pagar) VALUES (@id_orden, @nombre_platillo, @cantidad, @pagar)";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@id_orden", Convert.ToInt32(id_orden));
                cmd.Parameters.AddWithValue("@nombre_platillo", opcion_tipo_pollo);
                cmd.Parameters.AddWithValue("@cantidad", opcion_cantidad_pollo);
                cmd.Parameters.AddWithValue("@pagar", Convert.ToInt32(precio_pagar));

                cmd.ExecuteNonQuery();

                //Si hay extras insertará que haya pedido extras
                if(cobro_extra != 0)
                {
                    string insertar2 = "INSERT INTO PLATILLO (id_orden, nombre_platillo, cantidad, pagar) VALUES (@id_orden, @nombre_platillo, @cantidad, @pagar)";
                    OleDbCommand cmd22 = new OleDbCommand(insertar2, conexion);
                    cmd22.Parameters.AddWithValue("@id_orden", id_orden);
                    cmd22.Parameters.AddWithValue("@nombre_platillo", "Extra");
                    cmd22.Parameters.AddWithValue("@cantidad", extras_neto);
                    cmd22.Parameters.AddWithValue("@pagar", cobro_extra);

                    cmd22.ExecuteNonQuery();
                }
                
                conexion.Close();

                SELECT_HISTORIAL(fecha);
                LIMPIEZA();
                pagar form = new pagar(id_orden, fecha, ds);
                form.Show();
                SELECT_HISTORIAL(fecha);
            }
        }

        //Metodo para poner las opciones del pedido en DEFAULT
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
            extra_tortilla.Value = 1;
            extra_salsa.Value = 1;
            extra_ensalada.Value = 1;
            extra_frijol.Value = 1;
            extra_cebolla.Value = 1;

            //TIPO DE PAGO
            radio_efectivo.Checked = true;
            radioCredito.Checked = false;

            //DOMICILIO
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            groupBox5.Enabled = false;

        }

        //Boton de fecha actual
        private void button12_Click(object sender, EventArgs e)
        {
            SELECT_HISTORIAL(fecha);
        }

        //Radio de buscar fecha
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

        //Radio de TABLA DE FECHAS
        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                OleDbDataAdapter adaptador5 = new OleDbDataAdapter("SELECT fecha FROM FECHA", ds);

                DataSet dataset5 = new DataSet();
                DataTable tabla5 = new DataTable();

                adaptador5.Fill(dataset5);
                tabla5 = dataset5.Tables[0];
                this.listView_fechas.Items.Clear();
                for (int i = 0; i < tabla5.Rows.Count; i++)
                {
                    DataRow filas2 = tabla5.Rows[i];
                    ListViewItem elemntos5 = new ListViewItem(filas2["fecha"].ToString());

                    listView_fechas.Items.Add(elemntos5);
                }

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

        //Boton para habilitar campos de domicilio
        private void button9_Click(object sender, EventArgs e)
        {
            groupBox5.Enabled = true;
        }
        
        private void SELECT_HISTORIAL(string date)
        {
            OleDbDataAdapter adaptador5 = new OleDbDataAdapter("SELECT id_orden, observaciones, total FROM ORDEN WHERE fecha = '" + date + "'", ds);

            DataSet dataset5 = new DataSet();
            DataTable tabla5 = new DataTable();

            adaptador5.Fill(dataset5);
            tabla5 = dataset5.Tables[0];
            this.listView_Historial.Items.Clear();
            for (int i = 0; i < tabla5.Rows.Count; i++)
            {
                DataRow filas2 = tabla5.Rows[i];
                ListViewItem elemntos5 = new ListViewItem(filas2["id_orden"].ToString());
                elemntos5.SubItems.Add(filas2["observaciones"].ToString());
                elemntos5.SubItems.Add(filas2["total"].ToString());

                listView_Historial.Items.Add(elemntos5);
            }

            label3.Text = date;
        }        
    }
}
