using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using Logica;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class MovimientoController : ApiController
    {
        public IPrincipal Instancia { get; set; }

        // POST: api/Envio
        public IHttpActionResult Post([FromBody]MovimientoRequest value)
        {
            List<Movimiento> movimiento = Principal.Instancia.CrearMovimiento(value.DniEnvia, value.DniRecibe, value.Descripcion, value.Monto);
            if (movimiento != null)
                return Content(HttpStatusCode.Created, value);

            return Content(HttpStatusCode.BadRequest, value);
        }
        public IHttpActionResult Delete(int dni)
        {
            Usuario usuario = Principal.Instancia.ObtenerUsuarioPorDNI(dni);
            if (usuario != null)
            {
                if (usuario.HistorialDeMovimiento != null)
                {
                    List<Movimiento> movimientos = usuario.HistorialDeMovimiento;
                    return Ok(movimientos);
                }
            }
            return NotFound();

        }
    }
}
