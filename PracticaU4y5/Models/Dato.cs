using System;
using System.Collections.Generic;

#nullable disable

namespace PracticaU4y5.Models
{
    public partial class Dato
    {
        public uint Id { get; set; }
        public string Descripcion { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public string Material { get; set; }

        public virtual Juguete IdNavigation { get; set; }
    }
}
