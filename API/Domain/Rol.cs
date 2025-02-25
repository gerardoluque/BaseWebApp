using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain
{
    public class Rol
    {
        public int Id { get; set; }
        public string Descr { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public ICollection<RolProceso> Procesos { get; set; } = new List<RolProceso>();
        public Usuario Usuario { get; set; }
    }
}