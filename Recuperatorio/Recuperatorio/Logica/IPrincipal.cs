using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    interface IPrincipal
    {
        Usuario ObtenerUsuarioPorDNI(int dni);
        List<Movimiento> CrearMovimiento(int dniEnvia, int dniRecibe, string descripcion, double monto);
        Mensaje CancelarMovimiento(int idenvia, int idrecibe);
        List<Movimiento> ObtenerHistorial(int dni);

    }
}
