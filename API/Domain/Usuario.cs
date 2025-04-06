using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace API.Domain
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto => $"{Nombre} {PrimerApellido} {SegundoApellido}";
        public string Telefono { get; set; }
        public bool EsActivo { get; set; } = true;
        public short TiempoInactividad { get; set; } = 30; // Tiempo en minutos
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }

        // Relación uno a uno con Grupo
        public int? GrupoId { get; set; }
        public Grupo Grupo { get; set; } = null!;

        // Relación uno a uno con Rol
        public int? RolId { get; set; }
        public Rol Rol { get; set; } = null!;
    }
}