using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElGranPollo
{
    public interface IForm
    {
        void ChangeTextBoxText(string text, int id);
    }

    public interface IForm2
    {
        void Relogear_ordenes();
    }

    public interface IForm5
    {
        void Cargar_usuarios();
    }

    public interface IForm6
    {
        void ACTIVADO(string descripcion);
    }
    public interface IForm7
    {
        void Cerrar_sesion();
    }

    public interface Terminar_orden
    {
        void orden1();
        void orden2();
        void orden3();
        void orden4();
        void orden5();
    }
}
