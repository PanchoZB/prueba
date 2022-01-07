using System;
using System.Collections.Generic;

#nullable disable

namespace PracticaU4y5.Models
{
    public partial class Juguete
    {
        public uint Id { get; set; }
        public string Nombre { get; set; }
        public int? Idemp { get; set; }

        public virtual Empresa IdempNavigation { get; set; }
        public virtual Dato Dato { get; set; }
    }
}
