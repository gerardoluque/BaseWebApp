using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain
{
    public class Proceso
    {
        public int Id { get; set; }
        public required string Descr { get; set; }
        public string Tipo { get; set; } // P: Proceso, S: Subproceso
        public string Icono { get; set; }
        public bool EsActivo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }      
        public ICollection<RolProceso> Roles { get; set; } = [];
    }
}