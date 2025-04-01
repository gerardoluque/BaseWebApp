namespace API.Domain
{
    public class RolProceso
    {
        public int? RolId { get; set; }
        public Rol Rol { get; set; } = null!;

        public int? ProcesoId { get; set; }
        public Proceso Proceso { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }        
    }
}