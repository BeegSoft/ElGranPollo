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

namespace empanada_2
{
    public partial class Form1 : Form, IForm2, IForm6
    {
        public Form1(string fecha, string ds,string operador)
        {
            InitializeComponent();
            this.fecha = fecha;
            this.ds = ds;
            this.operador = operador;
        }

        //CONEXIONES
        String ds;

        string fecha, total_pagar, platillo,peso,operador;
        int precio_platillo, total, cantidad,cant_disp;
        double tipo, suma,resta;        
        int id = 0;

        //para cada producto falta agregar una variable para que realice la funcion de contar cuanto se esta seleccionando de cada cosa
        //---------------------------------------------------
        int carnec,rajas,chicharronsv,chicharronsr,nopal,tinga,chinita,frijol,dillo,soda,cebada,chata,jamaica,tamarindo,cafe,otros = 0;

        //---------------------------------------------------

        //aqui van a ir los contadores de cada producto igual al de abajo asiendo doble click sobre el boton
        //------------------------------------------------

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


        private void button1_Click(object sender, EventArgs e)
        {
            
            carnec= carnec + 1;
            textBoxcarnec.Text = Convert.ToString(carnec);
            textBoxcarnec.Enabled = false;
        }

        private void INSERTAR_VISU(string platillo, int cantidad)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();
            
            try
            {
                string insertar = "INSERT INTO VISUALIZADO (id_orden, platillo, cantidad) VALUES (@id_orden, @platillo, @cantidad)";
                OleDbCommand cmd2 = new OleDbCommand(insertar, conexion);
                cmd2.Parameters.AddWithValue("@id_orden", id);
                cmd2.Parameters.AddWithValue("@platillo", platillo);
                cmd2.Parameters.AddWithValue("@cantidad", cantidad);

                cmd2.ExecuteNonQuery();
            }

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexion.Close();
        }

        private void SELECT_PLATILLOS()
        {
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT id_platillo,nombre_platillo,cantidad,pagar FROM PLATILLO WHERE id_orden = " + id, ds);

            DataSet dataset = new DataSet();
            DataTable tabla = new DataTable();

            adaptador.Fill(dataset);
            tabla = dataset.Tables[0];
            this.listView_platillos.Items.Clear();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow filas = tabla.Rows[i];
                ListViewItem elemntos = new ListViewItem(filas["id_platillo"].ToString());
                elemntos.SubItems.Add(filas["nombre_platillo"].ToString());
                elemntos.SubItems.Add(filas["cantidad"].ToString());
                elemntos.SubItems.Add(filas["pagar"].ToString());

                listView_platillos.Items.Add(elemntos);
            }
         
        }

        private void SELECT_ORDEN()
        {
            OleDbDataAdapter adaptador5 = new OleDbDataAdapter("SELECT id_orden, descripcion, total_pagar FROM ORDEN WHERE checador = 1 and fecha = '" + fecha + "'", ds);

            DataSet dataset5 = new DataSet();
            DataTable tabla5 = new DataTable();

            adaptador5.Fill(dataset5);
            tabla5 = dataset5.Tables[0];
            this.listView_ordenes.Items.Clear();
            for (int i = 0; i < tabla5.Rows.Count; i++)
            {
                DataRow filas2 = tabla5.Rows[i];
                ListViewItem elemntos5 = new ListViewItem(filas2["id_orden"].ToString());
                elemntos5.SubItems.Add(filas2["descripcion"].ToString());
                elemntos5.SubItems.Add(filas2["total_pagar"].ToString());

                listView_ordenes.Items.Add(elemntos5);
            }
        }

        private void resetear()
        {
            textBoxcarnec.Text = "0";
            textBoxrajas.Text = "0";
            textBoxfrijol.Text = "0";
            textBoxcochinita.Text = "0";
            textBoxchicharronsv.Text = "0";
            textBoxchicharronsr.Text = "0";
            textBoxnopal.Text = "0";
            textBoxtinga.Text = "0";
            textBoxpicadillo.Text = "0";
            textBoxhorchata.Text = "0";
            textBoxjamaica.Text = "0";
            textBoxtamarindo.Text = "0";
            textBoxsoda.Text = "0";
            textBoxcafe.Text = "0";
            textBoxcebada.Text = "0";
            textBox_otros.Text = "0";
            

            carnec = 0;
            rajas = 0;
            chicharronsv = 0;
            chicharronsr = 0;
            nopal = 0;
            tinga = 0;
            chinita = 0;
            frijol = 0;
            dillo = 0;
            soda = 0;
            cebada = 0;
            chata = 0;
            jamaica = 0;
            tamarindo = 0;
            cafe = 0;
            otros = 0;
        }

        private void Insertar_datos(int cantidad, string platillo)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string select = "SELECT precio_platillo FROM MENU WHERE nombre_platillo='" + platillo + "'";
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        precio_platillo = reader.GetInt32(0);                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            total = precio_platillo * Convert.ToInt32(cantidad);

            try
            {
                string insertar = "INSERT INTO PLATILLO (id_orden, nombre_platillo, cantidad, pagar) VALUES (@id_orden, @nombre_platillo, @cantidad , @pagar)";
                OleDbCommand cmd2 = new OleDbCommand(insertar, conexion);
                cmd2.Parameters.AddWithValue("@id_orden", id);
                cmd2.Parameters.AddWithValue("@nombre_platillo", platillo);
                cmd2.Parameters.AddWithValue("@cantidad", cantidad);
                cmd2.Parameters.AddWithValue("@pagar", total);

                cmd2.ExecuteNonQuery();
                

            }

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conexion.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chinita = chinita + 1;
            textBoxcochinita.Text = Convert.ToString(chinita);            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frijol = frijol + 1;
            textBoxfrijol.Text = Convert.ToString(frijol);
      
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rajas = rajas + 1;
            textBoxrajas.Text = Convert.ToString(rajas);            
        }

        private void buttonchicarronsv_Click(object sender, EventArgs e)
        {
            chicharronsv= chicharronsv + 1;
            textBoxchicharronsv.Text = Convert.ToString(chicharronsv);            
        }

        private void buttonchicharronsr_Click(object sender, EventArgs e)
        {
            chicharronsr = chicharronsr + 1;
            textBoxchicharronsr.Text = Convert.ToString(chicharronsr);            
        }

        #region IForm2 Members

        public void Relogear_ordenes()
        {
            SELECT_ORDEN();
        }

        #endregion

        public double peso2=90;
        public string descripcion;

        private void CARGA()
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();
            peso2 = 90;
            string select = "SELECT Descripcion FROM ALMACEN WHERE Peso <=" + peso2;
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        descripcion = reader.GetString(0);

                        DialogResult resultado = MessageBox.Show(descripcion + " se encuentra  agotado ya no podras realizar ventas de este producto a menos que agreges mas en el almacen quieres abrir el almacen para Agregar mas " + descripcion, "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (resultado == DialogResult.Yes)
                        {
                            Almacen form = new Almacen(ds);
                            form.P = 1;
                            form.Show();
                        }
                        else if (resultado == DialogResult.No)
                        {
                            CANCELADO(descripcion);
                        }
                    }
                }
                else
                {

                }
                reader.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexion.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SELECT_PLATILLOS();

            SELECT_ORDEN();

            DataSet dss = new DataSet();
            //indicamos la consulta en SQL
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT nombre_platillo FROM MENU", ds);
            //se indica el nombre de la tabla
            da.Fill(dss, "Nombre_platillo");
            comboBox_otros.DataSource = dss.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            comboBox_otros.ValueMember = "nombre_platillo";

            this.listView_platillos.Items.Clear();

            //ESTE ES EL QUE VAS A QUITAR SI FUNCIONA EL PROGRESSBAR
            CARGA();                      

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu form = new Menu(ds);
            form.Show();
        }

        private void button14_Click(object sender, EventArgs e)
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxcarnec.Text) > 0)
            {
                carnec = carnec - 1;
                textBoxcarnec.Text = Convert.ToString(carnec);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxrajas.Text) > 0)
            {
                rajas = rajas - 1;
                textBoxrajas.Text = Convert.ToString(rajas);
            }
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxfrijol.Text) > 0)
            {
                frijol = frijol - 1;
                textBoxfrijol.Text = Convert.ToString(frijol);
            }
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxcochinita.Text) > 0)
            {
                chinita = chinita - 1;
                textBoxcochinita.Text = Convert.ToString(chinita);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxpicadillo.Text) > 0)
            {
                dillo = dillo - 1;
                textBoxpicadillo.Text = Convert.ToString(dillo);
            }            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxchicharronsv.Text) > 0)
            {
                chicharronsv = chicharronsv - 1;
                textBoxchicharronsv.Text = Convert.ToString(chicharronsv);
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxchicharronsr.Text) > 0)
            {
                chicharronsr = chicharronsr - 1;
                textBoxchicharronsr.Text = Convert.ToString(chicharronsr);
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxnopal.Text) > 0)
            {
                nopal = nopal - 1;
                textBoxnopal.Text = Convert.ToString(nopal);
            }            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxtinga.Text) > 0)
            {
                tinga = tinga - 1;
                textBoxtinga.Text = Convert.ToString(tinga);
            }            
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxhorchata.Text) > 0)
            {
                chata = chata - 1;
                textBoxhorchata.Text = Convert.ToString(chata);
            }
            
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxjamaica.Text) > 0)
            {
                jamaica = jamaica - 1;
                textBoxjamaica.Text = Convert.ToString(jamaica);
            }
            
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxtamarindo.Text) > 0)
            {
                tamarindo = tamarindo - 1;
                textBoxtamarindo.Text = Convert.ToString(tamarindo);
            }            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxcebada.Text) > 0)
            {
                cebada = cebada - 1;
                textBoxcebada.Text = Convert.ToString(cebada);
            }            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxcafe.Text) > 0)
            {
                cafe = cafe - 1;
                textBoxcafe.Text = Convert.ToString(cafe);
            }            
        }


        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox_algo.Checked)
            {
                textBoxcarnec.Enabled = true;
                textBoxcafe.Enabled = true;
                textBoxcebada.Enabled = true;
                textBoxchicharronsr.Enabled = true;
                textBoxchicharronsv.Enabled = true;
                textBoxcochinita.Enabled = true;
                textBoxfrijol.Enabled = true;
                textBoxhorchata.Enabled = true;
                textBoxjamaica.Enabled = true;
                textBoxnopal.Enabled = true;
                textBoxpicadillo.Enabled = true;
                textBoxrajas.Enabled = true;
                textBoxsoda.Enabled = true;
                textBoxtamarindo.Enabled = true;
                textBoxtinga.Enabled = true;
                textBox_otros.Enabled = true;
            }
            else
            {
                textBoxcarnec.Enabled = false;
                textBoxcafe.Enabled = false;
                textBoxcebada.Enabled = false;
                textBoxchicharronsr.Enabled = false;
                textBoxchicharronsv.Enabled = false;
                textBoxcochinita.Enabled = false;
                textBoxfrijol.Enabled = false;
                textBoxhorchata.Enabled = false;
                textBoxjamaica.Enabled = false;
                textBoxnopal.Enabled = false;
                textBoxpicadillo.Enabled = false;
                textBoxrajas.Enabled = false;
                textBoxsoda.Enabled = false;
                textBoxtamarindo.Enabled = false;
                textBoxtinga.Enabled = false;
                textBox_otros.Enabled = false;
            }
        }

        public void SUMA_ALMACEN(string descripcion, int total)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            cant_disp = cant_disp + total;

            string insertar = "UPDATE ALMACEN SET Rendimiento = @Rendimiento WHERE Descripcion= '" + descripcion + "'";
            OleDbCommand cmd3 = new OleDbCommand(insertar, conexion);
            cmd3.Parameters.AddWithValue("@Rendimiento", cant_disp.ToString());

            cmd3.ExecuteNonQuery();
            conexion.Close();
        }

        private void AGREGAR_ALMACEN(int id)
        {
            OleDbConnection conexion6 = new OleDbConnection(ds);

            conexion6.Open();

            #region carga de descripcion            
            string select = "SELECT nombre_platillo FROM PLATILLO WHERE id_platillo=" + id;            
            OleDbCommand cmd = new OleDbCommand(select, conexion6);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        descripcion = reader.GetString(0);
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
            #endregion            
            #region carga de la cantidad            
            string select2 = "SELECT cantidad FROM PLATILLO WHERE id_platillo=" + id;            
            OleDbCommand cmd6 = new OleDbCommand(select2, conexion6); //Conexion es tu objeto conexion

            cantidad = Convert.ToInt32((cmd6.ExecuteScalar()).ToString());
            //FDJGGHDJHDHFJDGHLHKJHFJ       

            #endregion

            #region agregando al almacen

            SetDefaultCulture(new CultureInfo("es-MX"));

            TIPO_RESTA(descripcion);

            CANTIDAD_PLATILLO(descripcion, cantidad);

            //checar cuanto es lo que tiene de peso respecto a la descripcion            

            string sql = "select Peso from ALMACEN WHERE Descripcion='" + descripcion + "'";
            OleDbCommand cmd3 = new OleDbCommand(sql, conexion6); //Conexion es tu objeto conexion

            peso = (cmd3.ExecuteScalar()).ToString();

            resta = tipo * cantidad;
            
            //Realizando la suma para aser la modificacion
            suma = ((double.Parse(peso, Thread.CurrentThread.CurrentCulture)) + (resta));
            
            //realizando la consulta            
            string insertar = "UPDATE ALMACEN SET Peso = @Peso WHERE Descripcion= '" + descripcion + "'";
            OleDbCommand cmd4 = new OleDbCommand(insertar, conexion6);
            cmd4.Parameters.AddWithValue("@Peso", suma.ToString());

            cmd4.ExecuteNonQuery();

            SUMA_ALMACEN(descripcion, cantidad);

            #endregion

            conexion6.Close();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_platillos.SelectedItems)
            {
                int id_platillo = Convert.ToInt32(lista.Text);

                DialogResult resultado = MessageBox.Show("Esta seguro de borrar el platillo del cliente?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {

                        //agregando la cantidad de empanada eliminada

                        AGREGAR_ALMACEN(id_platillo);

                        //---------------------------------------------

                        OleDbConnection conexion = new OleDbConnection(ds);

                        conexion.Open();
                        string insertar = "DELETE FROM PLATILLO WHERE id_platillo = " + id_platillo;
                        OleDbCommand cmd = new OleDbCommand(insertar, conexion);

                        cmd.ExecuteNonQuery();
                        
                        //-------------------------------------------------                        

                        string sql = "select SUM(pagar) from PLATILLO WHERE id_orden=" + id;
                        OleDbCommand cmd2 = new OleDbCommand(sql, conexion); //Conexion es tu objeto conexion                                

                        total_pagar = (cmd2.ExecuteScalar()).ToString();
                        if (total_pagar == "")
                        {
                            textBox_total.Text = "0";
                        }
                        else
                        {
                            textBox_total.Text = total_pagar;
                        }

                        //-------------------

                        //INSERTAR EL TOTAL EN LA TABLA ORDEN

                        string insertar2 = "UPDATE ORDEN SET total_pagar = @total_pagar WHERE id_orden=" + id;
                        OleDbCommand cmd3 = new OleDbCommand(insertar2, conexion);
                        cmd3.Parameters.AddWithValue("@total_pagar", textBox_total.Text);

                        cmd3.ExecuteNonQuery();
                        conexion.Close();

                        SELECT_ORDEN();
                        //MessageBox.Show("Borrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
            }

            //TOTAL DEL DIA

            OleDbConnection conexion5 = new OleDbConnection(ds);

            conexion5.Open();

            string sql5 = "SELECT SUM(ORDEN.total_pagar) FROM FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha WHERE FECHA.fecha = '" + fecha + "'";

            OleDbCommand cmd7 = new OleDbCommand(sql5, conexion5); //Conexion es tu objeto conexion                                

            int total_dia = Convert.ToInt32(cmd7.ExecuteScalar());

            //-------------------

            //INSERTAR EL TOTAL EN LA TABLA ORDEN

            string insertar6 = "UPDATE FECHA SET Venta_total = @Venta_total WHERE fecha = '" + fecha + "'";
            OleDbCommand cmd6 = new OleDbCommand(insertar6, conexion5);
            cmd6.Parameters.AddWithValue("@Venta_total", total_dia);

            cmd6.ExecuteNonQuery();

            conexion5.Close();

            GANANCIAS();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_ordenes.SelectedItems)
            {
                id = Convert.ToInt32(lista.Text);
            }
            if (id == 0)
            {
                MessageBox.Show("No se encuentra seleccionada una orden", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (ListViewItem lista in listView_ordenes.SelectedItems)
                {
                    id = Convert.ToInt32(lista.Text);

                    DialogResult resultado = MessageBox.Show("Esta seguro de borrar la orden?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            OleDbConnection conexion = new OleDbConnection(ds);

                            conexion.Open();
                            string insertar = "DELETE FROM ORDEN WHERE id_orden = " + id;
                            OleDbCommand cmd = new OleDbCommand(insertar, conexion);

                            cmd.ExecuteNonQuery();
                            conexion.Close();
                            listView_platillos.Items.Clear();
                            textBox_total.Text = "0";
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
                }

                //TOTAL DEL DIA

                OleDbConnection conexion5 = new OleDbConnection(ds);

                conexion5.Open();
                try
                {
                    string sql5 = "SELECT SUM(ORDEN.total_pagar) FROM FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha WHERE FECHA.fecha = '" + fecha + "'";

                    OleDbCommand cmd7 = new OleDbCommand(sql5, conexion5); //Conexion es tu objeto conexion                                

                    int total_dia = Convert.ToInt32(cmd7.ExecuteScalar());
                    //-------------------

                    //INSERTAR EL TOTAL EN LA TABLA ORDEN

                    string insertar6 = "UPDATE FECHA SET Venta_total = @Venta_total WHERE fecha = '" + fecha + "'";
                    OleDbCommand cmd6 = new OleDbCommand(insertar6, conexion5);
                    cmd6.Parameters.AddWithValue("@Venta_total", total_dia);

                    cmd6.ExecuteNonQuery();
                }
                catch
                {

                }

                conexion5.Close();

                GANANCIAS();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();

                string insertar = "INSERT INTO ORDEN (descripcion, total_pagar, fecha, checador) VALUES (@descripcion, @total_pagar, @fecha, @checador)";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@descripcion", "Descripcion");
                cmd.Parameters.AddWithValue("@total_pagar", 0);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@checador", 1);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Orden creada Correctamente", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
            }

            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Error de concurrencia:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            SELECT_ORDEN();

            textBox_descripcion.Focus();

            //SACAR LA ORDEN MAS ALTA PARA CREAR UNA NUEVA ORDEN

            OleDbConnection conexion4 = new OleDbConnection(ds);

            conexion4.Open();

            string maximo = "SELECT MAX(id_orden) FROM ORDEN";
            OleDbCommand cmd3 = new OleDbCommand(maximo, conexion4);
            id = Convert.ToInt32(cmd3.ExecuteScalar());
            conexion4.Close();
            //---------------------------------

            resetear();

            this.listView_platillos.Items.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView_ordenes.SelectedItems)
            {
                id = Convert.ToInt32(lista.Text);
            }

            if (id == 0)
            {
                MessageBox.Show("No se encuentra seleccionada una orden", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SELECT_PLATILLOS();

                //HACER LA SUMA DEL TOTAL A PAGAR
                OleDbConnection conexion4 = new OleDbConnection(ds);

                conexion4.Open();

                string sql = "select SUM(pagar) from PLATILLO WHERE id_orden=" + id;
                OleDbCommand cmd = new OleDbCommand(sql, conexion4);

                total_pagar = (cmd.ExecuteScalar()).ToString();

                textBox_total.Text = total_pagar;


                //PONER LA DESCRIPCION EN EL TEXTBOX

                string select = "SELECT descripcion FROM ORDEN WHERE id_orden=" + id;
                OleDbCommand cmd6 = new OleDbCommand(select, conexion4);
                try
                {
                    OleDbDataReader reader = cmd6.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string descripcion = reader.GetString(0);

                            textBox_descripcion.Text = descripcion;
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

                //---------------------------------
                conexion4.Close();
                resetear();
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            int id_orden = 0;
            foreach (ListViewItem lista in listView_ordenes.SelectedItems)
            {
                id_orden = Convert.ToInt32(lista.Text);
            }
            if (id_orden == 0)
            {
                MessageBox.Show("No se encuentra seleccionada una orden", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (ListViewItem lista in listView_ordenes.SelectedItems)
                {
                    id_orden = Convert.ToInt32(lista.Text);
                    int pagare;

                    //.....
                    OleDbConnection conexion = new OleDbConnection(ds);

                    conexion.Open();

                    string select = "SELECT total_pagar FROM ORDEN WHERE id_orden=" + id_orden;
                    OleDbCommand cmd = new OleDbCommand(select, conexion);
                    try
                    {
                        OleDbDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pagare = reader.GetInt32(0);

                                if (pagare > 0)
                                {
                                    pagar correr = new pagar(id_orden, fecha, ds,operador);
                                    correr.ShowDialog(this);
                                }
                                else
                                {
                                    MessageBox.Show("No se puede continuar porque la orden no tiene nada que pagar", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
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
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Almacen corre = new Almacen(ds);
            corre.Show(this);
        }

        private void TIPO_RESTA(string descripcion)
        {
            SetDefaultCulture(new CultureInfo("es-MX"));

            if (descripcion == "carne c/chile")
            {
                tipo = 35;
            }
            if (descripcion == "cochinita")
            {
                tipo = 30;
            }
            if (descripcion == "nopal")
            {
                tipo = 45;
            }
            if (descripcion == "tinga de pollo")
            {
                tipo = 35;
            }
            if (descripcion == "frijol c/queso")
            {
                tipo = 45;
            }
            if (descripcion == "rajas c/queso")
            {
                tipo = 45;
            }
            if (descripcion == "picadillo")
            {
                tipo = 40;
            }
            if (descripcion == "chicharron s/v")
            {
                tipo = 35;
            }
            if (descripcion == "chicharron s/r")
            {
                tipo = 35;
            }            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*progressBar1.Increment(1);
            if (progressBar1.Value == 100)            
                timer1.Stop();
                CARGA();*/
        }

        public void ACTIVADO(string descripcion)
        {
            if (descripcion == "carne c/chile")
            {
                buttoncarneconchile.Enabled = true;
                button1.Enabled = true;
            }
            if (descripcion == "cochinita")
            {
                buttoncochinita.Enabled = true;
                button4.Enabled = true;
            }
            if (descripcion == "nopal")
            {
                buttonnopal.Enabled = true;
                button8.Enabled = true;
            }
            if (descripcion == "tinga de pollo")
            {
                buttontinga.Enabled = true;
                button9.Enabled = true;
            }
            if (descripcion == "frijol c/queso")
            {
                buttonfrijol.Enabled = true;
                button3.Enabled = true;
            }
            if (descripcion == "rajas c/queso")
            {
                buttonrajas.Enabled = true;
                button2.Enabled = true;
            }
            if (descripcion == "picadillo")

            {
                buttonpicadillo.Enabled = true;
                button5.Enabled = true;
            }
            if (descripcion == "chicharron s/v")
            {
                buttonchicarronsv.Enabled = true;
                button6.Enabled = true;
            }
            if (descripcion == "chicharron s/r")
            {
                buttonchicharronsr.Enabled = true;
                button7.Enabled = true;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ds2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Empanada/BEEGSOFT/empanada_2/empanada_2/UsuariosEmpanadas.mdb";
            Control_acceso logeo = new Control_acceso(ds, ds2);
            logeo.Show();

            IForm7 formInterface = this.Owner as IForm7;
            if (formInterface != null)
                formInterface.Cerrar_sesion();

            this.Close();
        }

        private void checkBox_algo_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void CANCELADO(string descripcion)
        {            
            if (descripcion == "carne c/chile")
            {
                buttoncarneconchile.Enabled = false;
                button1.Enabled = false;                
            }
            if (descripcion == "cochinita")
            {
                buttoncochinita.Enabled = false;
                button4.Enabled = false;
            }
            if (descripcion == "nopal")
            {
                buttonnopal.Enabled = false;
                button8.Enabled = false;
            }
            if (descripcion == "tinga de pollo")
            {
                buttontinga.Enabled = false;
                button9.Enabled = false;
            }
            if (descripcion == "frijol c/queso")
            {
                buttonfrijol.Enabled = false;
                button3.Enabled = false;
            }
            if (descripcion == "rajas c/queso")
            {
                buttonrajas.Enabled = false;
                button2.Enabled = false;
            }
            if (descripcion == "picadillo")                

            {
                buttonpicadillo.Enabled = false;
                button5.Enabled = false;
            }
            if (descripcion == "chicharron s/v")
            {
                buttonchicarronsv.Enabled = false;
                button6.Enabled = false;
            }
            if (descripcion == "chicharron s/r")
            {
                buttonchicharronsr.Enabled = false;
                button7.Enabled = false;
            }
        }

        private void ALMACEN(string descripcion,int cantidad)
        {
            SetDefaultCulture(new CultureInfo("es-MX"));

            TIPO_RESTA(descripcion);
            //checar cuanto es lo que tiene de peso respecto a la descripcion
            OleDbConnection conexion4 = new OleDbConnection(ds);

            conexion4.Open();

            string sql = "select Peso from ALMACEN WHERE Descripcion='" + descripcion + "'";
            OleDbCommand cmd2 = new OleDbCommand(sql, conexion4); //Conexion es tu objeto conexion

            peso = (cmd2.ExecuteScalar()).ToString();

            conexion4.Close();

            resta = tipo * cantidad;

            //Realizando la resta para aser la modificacion
            suma = ((double.Parse(peso, Thread.CurrentThread.CurrentCulture)) - (resta));

            if (suma <= 0)
            {
                suma = 0;
            }

            if (suma <= 90)
            {
                if (suma <= 0)
                {
                    DialogResult resultado = MessageBox.Show(descripcion + " se encuentra  agotado ya no podras realizar ventas de este producto a menos que agreges mas en el almacen quieres abrir el almacen para Agregar mas " + descripcion, "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        Almacen form = new Almacen(ds);
                        form.P = 1;
                        form.Show();
                    }
                    else if (resultado == DialogResult.No)
                    {
                        CANCELADO(descripcion);
                    }
                }
                else if (suma <= 90)
                {
                    DialogResult resultado = MessageBox.Show(descripcion + " se encuentra en el limite permitido pronto te quedaras sin este producto quieres abrir el almacen para Agregar mas " + descripcion, "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        Almacen form = new Almacen(ds);
                        form.P = 1;
                        form.Show();
                    }
                }

            }
            else
            {
                //ACTIVADO(descripcion);
            }

            //realizando la consulta
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string insertar = "UPDATE ALMACEN SET Peso = @Peso WHERE Descripcion= '" + descripcion + "'";
            OleDbCommand cmd3 = new OleDbCommand(insertar, conexion);
            cmd3.Parameters.AddWithValue("@Peso", suma.ToString());

            cmd3.ExecuteNonQuery();
            conexion.Close();
        }
        public void RESTA_ALMACEN(string descripcion,int total)
        {
            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            cant_disp = cant_disp - total;

            string insertar = "UPDATE ALMACEN SET Rendimiento = @Rendimiento WHERE Descripcion= '" + descripcion + "'";
            OleDbCommand cmd3 = new OleDbCommand(insertar, conexion);
            cmd3.Parameters.AddWithValue("@Rendimiento", cant_disp.ToString());

            cmd3.ExecuteNonQuery();
            conexion.Close();
        }

        public void CANTIDAD_PLATILLO(string descripcion,int total)
        {
            //checar cuanto es lo que tiene de peso respecto a la descripcion
            OleDbConnection conexion4 = new OleDbConnection(ds);

            conexion4.Open();

            string sql = "select Rendimiento from ALMACEN WHERE Descripcion='" + descripcion + "'";
            OleDbCommand cmd2 = new OleDbCommand(sql, conexion4); //Conexion es tu objeto conexion

            cant_disp = Convert.ToInt32(cmd2.ExecuteScalar());
                                
            conexion4.Close();
        }
        private void button14_Click_1(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("No se has cargado ninguna orden", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(textBox_otros.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBox_otros.Text);
                        platillo = comboBox_otros.Text;

                        CANTIDAD_PLATILLO(platillo,cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo,cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer "+cant_disp+" empanadas de "+platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }                                               
                    }

                    if (Convert.ToInt32(textBoxcarnec.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxcarnec.Text);
                        platillo = "carne c/chile";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxrajas.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxrajas.Text);
                        platillo = "rajas c/queso";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxfrijol.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxfrijol.Text);
                        platillo = "frijol c/queso";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxcochinita.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxcochinita.Text);
                        platillo = "cochinita";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxchicharronsv.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxchicharronsv.Text);
                        platillo = "chicharron s/v";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxchicharronsr.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxchicharronsr.Text);
                        platillo = "chicharron s/r";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxnopal.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxnopal.Text);
                        platillo = "nopal";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxtinga.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxtinga.Text);
                        platillo = "tinga de pollo";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxpicadillo.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxpicadillo.Text);
                        platillo = "picadillo";

                        CANTIDAD_PLATILLO(platillo, cantidad);

                        if (cant_disp >= cantidad)
                        {
                            Insertar_datos(cantidad, platillo);
                            INSERTAR_VISU(platillo, cantidad);
                            ALMACEN(platillo, cantidad);
                            RESTA_ALMACEN(platillo, cantidad);
                        }
                        else
                        {
                            MessageBox.Show("No te Alcanza para realizar el pedido, tienes para hacer " + cant_disp + " empanadas de " + platillo, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (Convert.ToInt32(textBoxhorchata.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxhorchata.Text);
                        platillo = "horchata";

                        Insertar_datos(cantidad, platillo);
                        INSERTAR_VISU(platillo, cantidad);
                    }

                    if (Convert.ToInt32(textBoxjamaica.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxjamaica.Text);
                        platillo = "jamaica";

                        Insertar_datos(cantidad, platillo);
                        INSERTAR_VISU(platillo, cantidad);
                    }

                    if (Convert.ToInt32(textBoxtamarindo.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxtamarindo.Text);
                        platillo = "tamarindo";

                        Insertar_datos(cantidad, platillo);
                        INSERTAR_VISU(platillo, cantidad);
                    }

                    if (Convert.ToInt32(textBoxsoda.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxsoda.Text);
                        platillo = "cocacola";

                        Insertar_datos(cantidad, platillo);
                        INSERTAR_VISU(platillo, cantidad);
                    }

                    if (Convert.ToInt32(textBoxcafe.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxcafe.Text);
                        platillo = "cafe";

                        Insertar_datos(cantidad, platillo);
                        INSERTAR_VISU(platillo, cantidad);
                    }

                    if (Convert.ToInt32(textBoxcebada.Text) != 0)
                    {
                        cantidad = Convert.ToInt32(textBoxcebada.Text);
                        platillo = "cebada";

                        Insertar_datos(cantidad, platillo);
                        INSERTAR_VISU(platillo, cantidad);
                    }


                    //MOSTRAR EL TOTAL A PAGAR DEL PEDIDO

                    OleDbConnection conexion4 = new OleDbConnection(ds);

                    conexion4.Open();

                    string sql = "select SUM(pagar) from PLATILLO WHERE id_orden=" + id;
                    OleDbCommand cmd = new OleDbCommand(sql, conexion4); //Conexion es tu objeto conexion                                

                    total_pagar = (cmd.ExecuteScalar()).ToString();

                    textBox_total.Text = total_pagar;
                    //-------------------

                    //INSERTAR EL TOTAL EN LA TABLA ORDEN
                    try
                    {
                        string insertar = "UPDATE ORDEN SET total_pagar = @total_pagar WHERE id_orden=" + id;
                        OleDbCommand cmd3 = new OleDbCommand(insertar, conexion4);
                        cmd3.Parameters.AddWithValue("@total_pagar", textBox_total.Text);

                        cmd3.ExecuteNonQuery();

                        // INSERTAR DESCRIPCION A LA TABLA ORDEN

                        string descripcion = "UPDATE ORDEN SET descripcion = @descripcion WHERE id_orden=" + id;
                        OleDbCommand cmd5 = new OleDbCommand(descripcion, conexion4);
                        cmd5.Parameters.AddWithValue("@descripcion", textBox_descripcion.Text);

                        cmd5.ExecuteNonQuery();
                    }
                    catch { }
                    conexion4.Close();
                    //-------------------

                    SELECT_PLATILLOS();

                    //RELOGEAR EL LISTVIEW ORDENES PARA QUE APARESCA EL TOTAL


                    SELECT_ORDEN();
                    //--------------------------------
                    //RESETEAR LAS CANTIDADES DE LOS TEXTBOX 
                    resetear();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Tipo de dato incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox_otros.Text = "0";
                    textBox_otros.Focus();
                }


                string txt = textBox_descripcion.Text;

                IForm formInterface = this.Owner as IForm;

                if (formInterface != null)
                    formInterface.ChangeTextBoxText(txt, id);

                //TOTAL DEL DIA

                OleDbConnection conexion5 = new OleDbConnection(ds);

                conexion5.Open();
                try
                {
                    string sql5 = "SELECT SUM(ORDEN.total_pagar) FROM FECHA INNER JOIN ORDEN ON FECHA.fecha = ORDEN.fecha WHERE FECHA.fecha = '" + fecha + "'";

                    OleDbCommand cmd7 = new OleDbCommand(sql5, conexion5); //Conexion es tu objeto conexion                                

                    int total_dia = Convert.ToInt32(cmd7.ExecuteScalar());

                    //-------------------

                    //INSERTAR EL TOTAL EN LA TABLA ORDEN

                    string insertar6 = "UPDATE FECHA SET Venta_total = @Venta_total WHERE fecha = '" + fecha + "'";
                    OleDbCommand cmd6 = new OleDbCommand(insertar6, conexion5);
                    cmd6.Parameters.AddWithValue("@Venta_total", total_dia);

                    cmd6.ExecuteNonQuery();
                }
                catch
                {

                }
                conexion5.Close();
            }

            GANANCIAS();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string txt = textBox_descripcion.Text;

            IForm formInterface = this.Owner as IForm;

            if (formInterface != null)
                formInterface.ChangeTextBoxText(txt, id);
        }

        private void button23_Click_2(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox_otros.Text) > 0)
            {
                otros = otros - 1;
                textBox_otros.Text = Convert.ToString(otros);
            }
        }

        private void estadisticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Historial form = new Historial(ds);
            form.Show();
        }

        private void orden1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminar_orden formInterface = this.Owner as Terminar_orden;
            if (formInterface != null)
                formInterface.orden1();
        }

        private void orden2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminar_orden formInterface = this.Owner as Terminar_orden;
            if (formInterface != null)
                formInterface.orden2();
        }

        private void orden3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminar_orden formInterface = this.Owner as Terminar_orden;
            if (formInterface != null)
                formInterface.orden3();
        }

        private void orden4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminar_orden formInterface = this.Owner as Terminar_orden;
            if (formInterface != null)
                formInterface.orden4();
        }

        private void orden5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminar_orden formInterface = this.Owner as Terminar_orden;
            if (formInterface != null)
                formInterface.orden5();
        }

        private void gastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gastos form = new Gastos(ds);
            form.ShowDialog();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            otros = otros + 1;
            textBox_otros.Text = Convert.ToString(otros);            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Terminar_orden formInterface = this.Owner as Terminar_orden;
                if (formInterface != null)
                    formInterface.orden1();
            }

            if (e.KeyCode == Keys.F2)
            {
                Terminar_orden formInterface = this.Owner as Terminar_orden;
                if (formInterface != null)
                    formInterface.orden2();
            }

            if (e.KeyCode == Keys.F3)
            {
                Terminar_orden formInterface = this.Owner as Terminar_orden;
                if (formInterface != null)
                    formInterface.orden3();
            }

            if (e.KeyCode == Keys.F4)
            {
                Terminar_orden formInterface = this.Owner as Terminar_orden;
                if (formInterface != null)
                    formInterface.orden4();
            }

            if(e.KeyCode == Keys.F5)
            {
                Terminar_orden formInterface = this.Owner as Terminar_orden;
                if (formInterface != null)
                    formInterface.orden5();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxsoda.Text)>0)
            {
                soda = soda - 1;
                textBoxsoda.Text = Convert.ToString(soda);
            }                        
        }

        private void buttonnopal_Click(object sender, EventArgs e)
        {
            nopal = nopal + 1;
            textBoxnopal.Text = Convert.ToString(nopal);            
        }

        private void buttontinga_Click(object sender, EventArgs e)
        {
            tinga = tinga + 1;
            textBoxtinga.Text = Convert.ToString(tinga);            
        }

        private void buttonpicadillo_Click(object sender, EventArgs e)
        {
            dillo = dillo + 1;
            textBoxpicadillo.Text = Convert.ToString(dillo);            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            jamaica= jamaica + 1;
            textBoxjamaica.Text = Convert.ToString(jamaica);            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tamarindo = tamarindo + 1;
            textBoxtamarindo.Text = Convert.ToString(tamarindo);            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cafe = cafe + 1;
            textBoxcafe.Text = Convert.ToString(cafe);            
        }

        private void buttonhorchata_Click(object sender, EventArgs e)
        {
            chata = chata + 1;
            textBoxhorchata.Text = Convert.ToString(chata);            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            soda = soda + 1;
            textBoxsoda.Text = Convert.ToString(soda);            
        }

        private void buttoncebada_Click(object sender, EventArgs e)
        {
            cebada = cebada + 1;
            textBoxcebada.Text = Convert.ToString(cebada);            
        }
    }
}  