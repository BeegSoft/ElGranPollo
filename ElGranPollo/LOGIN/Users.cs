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
    public partial class Users : Form,IForm5
    {
        public Users(string ds,string ds2, int band,string operador)
        {
            InitializeComponent();
            this.band = band;
            this.ds = ds;
            this.ds2 = ds2;
            this.operador = operador;
        }
        int band,banda2,id;        
        string ds,ds2,operador;


        #region Para Actualizar los datos
        public void Cargar_usuarios()
        {
            ACTUALIZADO(band);
        }
        #endregion

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            banda2 = 0;            
            Nuevo_usuario abre = new Nuevo_usuario(ds,ds2, id, banda2);
            abre.Show(this);
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 0 para root
            if (band == 0)
            {
                foreach (ListViewItem lista in listView1.SelectedItems)
                {
                    id =Convert.ToInt32(lista.Text);                    
                }
                if (id == 0)
                {
                    MessageBox.Show("No se encuentra seleccionada un id", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    band = 1;
                    Nuevo_usuario abre = new Nuevo_usuario(ds, ds2, id, band);
                    abre.Show();
                    
                }
                band = 0;
            }
            // 1 para administrador
            else if (band == 1)
            {
                foreach (ListViewItem lista in listView2.SelectedItems)
                {
                    id = Convert.ToInt32(lista.Text);                    
                }
                if (id == 0)
                {
                    MessageBox.Show("No se encuentra seleccionado un id", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    band = 1;
                    Nuevo_usuario abre = new Nuevo_usuario(ds, ds2, id, band);
                    abre.Show();
                }
                band = 1;
            }
            id = 0;
        }
        //public event EventHandler<ListViewUpdateEventArgs> ItemUpdating;

        private void AbrirProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            DateTime fechahoy = DateTime.Now;
            string fecha = fechahoy.ToString("d");

            string var1 = fecha;
            var1 = var1.Substring(0, 2);

            string var2 = fecha;
            var2 = var2.Substring(3, 2);

            string var3 = fecha;
            var3 = var3.Substring(6, 4);

            //juntando las cadenas
            string fechacompleta = string.Concat(var3, var2, var1);
            int fechanum = Convert.ToInt32(fechacompleta);
            try
            {
                OleDbConnection conexion = new OleDbConnection(ds);

                conexion.Open();
                string insertar = "INSERT INTO FECHA (fecha, id) VALUES (@fecha, @id)";
                OleDbCommand cmd = new OleDbCommand(insertar, conexion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@id", fechanum);

                cmd.ExecuteNonQuery();

                conexion.Close();
                Pricipal form = new Pricipal(fecha, ds,ds2, band);
                form.Show();
                this.Close();
            }

            catch (Exception)
            {
                DialogResult resultado = MessageBox.Show("Ya existe un historial del dia de hoy\n\n      Desea continuar el dia de hoy?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    Pricipal form = new Pricipal(fecha, ds,ds2, band);
                    form.Show();
                    this.Close();
                }
            }


        }

        private void abrirBaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:/ElGranPollo/ElGranPollo/base.mdb");
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control_acceso logeo = new Control_acceso(ds, ds2);
            logeo.Show();
            this.Close();            
        }

        private void button25_Click(object sender, EventArgs e)
        {
            BORRAR(band);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lista in listView1.SelectedItems)
            {
                id = Convert.ToInt32(lista.Text);


                OleDbConnection conexion = new OleDbConnection(ds2);
                conexion.Open();
                string select = "SELECT clave FROM USUARIOS WHERE id=" + id;
                OleDbCommand cmd6 = new OleDbCommand(select, conexion);
                try
                {
                    OleDbDataReader reader = cmd6.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clv_des = reader.GetString(0);
                            string aux = Encriptado.DesEncriptar(clv_des);
                            textBox1.Text = aux;
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
        }

        private void Users_Load(object sender, EventArgs e)
        {
            SELECT_USUARIOS(band);
        }

        private void BORRAR(int band)
        {
            // 0 para root
            if (band == 0)
            {
                foreach (ListViewItem lista in listView1.SelectedItems)
                {
                    id = Convert.ToInt32(lista.Text);

                    DialogResult resultado = MessageBox.Show("Esta seguro de borrar el usuario?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            OleDbConnection conexion = new OleDbConnection(ds2);

                            conexion.Open();
                            string borrar = "DELETE FROM USUARIOS WHERE id =" + id;
                            OleDbCommand cmd = new OleDbCommand(borrar, conexion);

                            cmd.ExecuteNonQuery();


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
                band = 0;
            }

            // 1 para administrador
            else if (band == 1)
            {
                foreach (ListViewItem lista in listView2.SelectedItems)
                {
                    id = Convert.ToInt32(lista.Text);

                    DialogResult resultado = MessageBox.Show("Esta seguro de borrar el usuario?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            OleDbConnection conexion = new OleDbConnection(ds2);

                            conexion.Open();
                            string borrar = "DELETE FROM USUARIOS WHERE id =" + id;
                            OleDbCommand cmd = new OleDbCommand(borrar, conexion);
                            cmd.ExecuteNonQuery();

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
                band = 1;
            }            
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:/ElGranPollo/ElGranPollo/Usuarios.mdb");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ACTUALIZADO(band);
        }

        public void ACTUALIZADO(int band)
        {
            if (label2.Text == "Usted Ingreso Como Root"||band==0)
            {
                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT USUARIOS.id, USUARIOS.nombre, USUARIOS.clave, USUARIOS.TIPO_USUARIO FROM USUARIOS ", ds2);

                DataSet dataset = new DataSet();
                DataTable tabla = new DataTable();

                adaptador.Fill(dataset);
                tabla = dataset.Tables[0];
                this.listView1.Items.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    DataRow filas = tabla.Rows[i];
                    ListViewItem elementos = new ListViewItem(filas["id"].ToString());
                    elementos.SubItems.Add(filas["nombre"].ToString());
                    elementos.SubItems.Add(filas["clave"].ToString());
                    elementos.SubItems.Add(filas["tipo_usuario"].ToString());
                    listView1.Items.Add(elementos);
                }
                band = 0;
            }
            else if (label2.Text == "Usted Ingreso como Administrador"||band==1)
            {
                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT USUARIOS.id,USUARIOS.nombre, USUARIOS.TIPO_USUARIO FROM USUARIOS WHERE USUARIOS.tipo_usuario <>'ROOT'", ds2);

                DataSet dataset = new DataSet();
                DataTable tabla = new DataTable();

                adaptador.Fill(dataset);
                tabla = dataset.Tables[0];
                this.listView2.Items.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    DataRow filas = tabla.Rows[i];
                    ListViewItem elementos = new ListViewItem(filas["id"].ToString());
                    elementos.SubItems.Add(filas["nombre"].ToString());
                    elementos.SubItems.Add(filas["tipo_usuario"].ToString());
                    listView2.Items.Add(elementos);
                }
                band = 1;
            }
            MessageBox.Show("La tabla se encuentra actualizada");
        }

        public void SELECT_USUARIOS(int band)
        {
            //root
            if (band == 0)
            {
                label2.Text = "Usted Ingreso Como Root";
                listView2.Visible = false;

                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT USUARIOS.id, USUARIOS.nombre, USUARIOS.clave, USUARIOS.TIPO_USUARIO FROM USUARIOS ", ds2);

                DataSet dataset = new DataSet();
                DataTable tabla = new DataTable();

                adaptador.Fill(dataset);
                tabla = dataset.Tables[0];
                this.listView1.Items.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    DataRow filas = tabla.Rows[i];
                    ListViewItem elementos = new ListViewItem(filas["id"].ToString());
                    elementos.SubItems.Add(filas["nombre"].ToString());
                    elementos.SubItems.Add(filas["clave"].ToString());
                    elementos.SubItems.Add(filas["tipo_usuario"].ToString());
                    listView1.Items.Add(elementos);
                }
                band = 0;
            }
            //admin
            else if (band == 1)
            {
                label2.Text = "Usted Ingreso Como Administrador";
                listView1.Visible = false;
                listView2.Location = new Point(162, 140);
                pictureBox1.Location = new Point(490, 175);
                
                button1.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                this.Size = new Size(638, 433);

                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT USUARIOS.id,USUARIOS.nombre, USUARIOS.TIPO_USUARIO FROM USUARIOS WHERE USUARIOS.tipo_usuario <>'ROOT'", ds2);

                DataSet dataset = new DataSet();
                DataTable tabla = new DataTable();

                adaptador.Fill(dataset);
                tabla = dataset.Tables[0];
                this.listView2.Items.Clear();
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    DataRow filas = tabla.Rows[i];
                    ListViewItem elementos = new ListViewItem(filas["id"].ToString());
                    elementos.SubItems.Add(filas["nombre"].ToString());
                    elementos.SubItems.Add(filas["tipo_usuario"].ToString());
                    listView2.Items.Add(elementos);
                }
                band = 1;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            BORRAR(band);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
