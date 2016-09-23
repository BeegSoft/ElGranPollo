using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElGranPollo
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]

        static void Main()
        {
            //CONEXION PARA LA BASE DE DATOS
            string ds = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/ElGranPollo/ElGranPollo/base.mdb";
            

            //CONEXION PARA LOS USUARIOS
            string ds2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/ElGranPollo/ElGranPollo/Usuarios.mdb";
            
            //  h   ola
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Control_acceso(ds,ds2));
            //Algo
        }
    }
}
