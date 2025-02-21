using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain
{
    public class Grupo
    {
        public int Id { get; set; }
        public required string Descr { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
    }
}