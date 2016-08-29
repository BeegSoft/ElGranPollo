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
using System.Threading;


namespace ElGranPollo
{
    public partial class Inicio : Form
    {
        public Inicio(string ds)
        {
            //---------
            Thread t = new Thread(new ThreadStart(splashtart));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
            //---------
            this.ds = ds;            
        }

        private void splashtart()
        {
            Application.Run(new Form2(ds));
        }

        //CONEXION
        string ds;
        private void button1_Click(object sender, EventArgs e)
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
                Pricipal form = new Pricipal(fecha, ds);
                form.Show();
                this.Close();
            }

            catch (Exception)
            {
                DialogResult resultado = MessageBox.Show("Ya existe un historial del dia de hoy\n\n      Desea continuar el dia de hoy?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    Pricipal form = new Pricipal(fecha, ds);
                    form.Show();
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            string fecha = fechahoy.ToString("d");

            OleDbConnection conexion = new OleDbConnection(ds);

            conexion.Open();

            string select = "SELECT COUNT(fecha) FROM FECHA WHERE fecha='" + fecha + "'";
            OleDbCommand cmd = new OleDbCommand(select, conexion);
            try
            {
                string compro = (cmd.ExecuteScalar()).ToString();

                if (Convert.ToInt32(compro) != 0)
                {
                    Pricipal form = new Pricipal(fecha, ds);
                    form.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No hay un dia en el historial", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error orden" + ex, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
           
        }

        /*private void button4_Click(object sender, EventArgs e)
        {
            Menu form = new Menu(ds);
            form.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Historial form = new Historial(ds);
            form.Show();
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            /*Almacen corre = new Almacen(ds);
            corre.P = 0;
            corre.Show();
        }*/
    }
}
