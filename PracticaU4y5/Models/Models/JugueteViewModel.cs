using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaU4y5.Models.Models
{
    public class JugueteViewModel
    {
        public IEnumerable<Juguete> juguete { get; set; }
        public IEnumerable<Dato>Datos{ get; set; }
        public IEnumerable<Empresa> Empresas { get; set; }
    }
}
