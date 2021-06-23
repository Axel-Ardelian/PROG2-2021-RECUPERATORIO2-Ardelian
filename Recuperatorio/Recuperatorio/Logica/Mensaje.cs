using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Mensaje
    {
        public bool EstaBien { get; set; } 
        public string Comentario { get; set; } 
        public Mensaje(bool ok, string mensaje)
        { this.EstaBien = ok; this.Comentario = mensaje; } 
    }    
}
