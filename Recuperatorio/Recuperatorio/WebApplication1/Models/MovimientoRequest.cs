using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logica;

namespace WebApplication1.Models
{
    public class MovimientoRequest
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public int DniEnvia { get; set; }
        public int DniRecibe { get; set; }


        public List<Movimiento> CrearMovimiento(int dnirecibe, int dnienvia)
        {
            Usuario usuarioRecibe = Principal.Instancia.ObtenerUsuarioPorDNI(dnirecibe);
            Usuario usuarioEnvia = Principal.Instancia.ObtenerUsuarioPorDNI(dnienvia);
            List<Movimiento> movimiento = Principal.Instancia.CrearMovimiento(usuarioEnvia.DNI, usuarioRecibe.DNI, this.Descripcion, this.Monto);
            return movimiento;
        }
    }
}