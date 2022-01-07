using PracticaU4y5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaU4y5.Areas.Admin.Models
{
    public class AgregarViewModel
    {
        public IEnumerable<Empresa> Empresas
        {
            get;
            set;
        }
        public Juguete juguete { get; set; }

        public Dato Dato { get; set; }
    }
}
