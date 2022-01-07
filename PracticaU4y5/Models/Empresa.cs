using System;
using System.Collections.Generic;

#nullable disable

namespace PracticaU4y5.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Juguetes = new HashSet<Juguete>();
        }

        public int Id { get; set; }
        public string NombreEmpresa { get; set; }

        public virtual ICollection<Juguete> Juguetes { get; set; }
    }
}
