using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public sealed class Principal : IPrincipal
    {
        public List<Usuario> Usuarios { get; set; }
        public List<Movimiento> Movimientos { get; set; }


        private readonly static Principal _instance = new Principal();

        private Principal()
        {
            if (Usuarios == null)
            {
                Usuarios = new List<Usuario>();
            }
        }

        public static Principal Instancia
        {
            get
            {
                return _instance;
            }
        }

        //METODO NO NECESARIO, PUEDE SER RESUELTO EN UNA SOLA LINEA
        public Usuario ObtenerUsuarioPorDNI(int dni)
        {
            Usuario nuevousuario = Usuarios.Find(x => x.DNI == dni);
            if (nuevousuario == null)
            {
                return null;
            }
            return nuevousuario;
        }

        public List<Movimiento> CrearMovimiento(int dniEnvia, int dniRecibe, string descripcion, double monto)
        {
            Usuario usuarioenvia = ObtenerUsuarioPorDNI(dniEnvia);
            Usuario usuariorecibe = ObtenerUsuarioPorDNI(dniRecibe);
            if (usuarioenvia == null && usuariorecibe == null)
            {
                return null;
            }
            if (usuarioenvia.Saldo > monto)
            {
                usuarioenvia.Saldo -= monto;
                usuariorecibe.Saldo += monto;
                Movimiento nuevo = new Movimiento(descripcion, monto * -1);
                Movimiento nuevo2 = new Movimiento(descripcion, monto);
                usuarioenvia.HistorialDeMovimiento.Add(nuevo);
                usuariorecibe.HistorialDeMovimiento.Add(nuevo2);
                Movimientos.Add(nuevo);
                Movimientos.Add(nuevo2);

                //PROBLEMA DE DISEÑO, ESTA LOGICA ES DEL USUARIO. CON LO CUAL EN LA CLASE USUARIO PODRIA HABER UN METODO
                //CREARTRANSACCION O CREARMOVIMIENTO QUE TENGA LAS DOS ACCIONES, AGREGAR A LA LISTA Y MODIFICAR EL SALDO.

                return Movimientos;
            }
            else
            {
                return null;
            }          

            //PORQUE NO USAR LA CLASE MENSAJE PARA RETORNAR? SE PODRIA USAR IGUAL QUE MOVIMIENTO
        }

        public Mensaje CancelarMovimiento(int idenvia, int idrecibe)
        {
            Movimiento cancelarmovimiento = Movimientos.Find(x => x.ID == idenvia);
            if (cancelarmovimiento != null)
            {
                Movimiento movimiento = Movimientos.Find(x => x.ID == idrecibe);
                if (movimiento != null)
                {
                    cancelarmovimiento = new Movimiento(cancelarmovimiento.Descripcion, cancelarmovimiento.Monto * -1);
                    movimiento = new Movimiento(movimiento.Descripcion, movimiento.Monto * -1);
                    Movimientos.Add(cancelarmovimiento);
                    Movimientos.Add(movimiento);
                    return new Mensaje(true, "Se cancelo con exito");
                }
                else
                {
                    //ESPECIFICAR LOS MENSAJES DE ERROR
                    return new Mensaje(true, "Error");
                }
            }
            else
            {
                //ESPECIFICAR LOS MENSAJES DE ERROR
                return new Mensaje(true, "Error");
            }
        }

        public List<Movimiento> ObtenerHistorial(int dni)
        {
            Usuario historial = ObtenerUsuarioPorDNI(dni);
            if (historial != null)
            {
                historial.HistorialDeMovimiento.OrderByDescending(x => x.Fecha);
                return historial.HistorialDeMovimiento;
            }
            else
            {
                return null;
            }
        }
    }
}
