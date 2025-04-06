using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain
{
    public class Grupo
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string Descr { get; set; }
        public bool EsActivo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }

        // Relación uno a muchos con Usuario
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}