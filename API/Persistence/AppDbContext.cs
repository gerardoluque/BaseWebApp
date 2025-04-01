using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Domain;

namespace API.Persistence
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public required DbSet<Grupo> Grupos { get; set; }
        public required DbSet<Rol> Roles { get; set; }
        public required DbSet<Proceso> Procesos { get; set; }
        public required DbSet<RolProceso> RolesProcesos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RolProceso>()
                .HasKey(rp => new { rp.RolId, rp.ProcesoId });

            builder.Entity<RolProceso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.Procesos)
                .HasForeignKey(rp => rp.RolId);

            builder.Entity<RolProceso>()
                .HasOne(rp => rp.Proceso)
                .WithMany(p => p.Roles)
                .HasForeignKey(rp => rp.ProcesoId);

            // Configuración de la relación uno a uno entre Usuario y Grupo
            builder.Entity<Usuario>()
                .HasOne(u => u.Grupo)
                .WithOne(g => g.Usuario)
                .HasForeignKey<Usuario>(u => u.GrupoId);

            // Configuración de la relación uno a uno entre Usuario y Rol
            builder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithOne(r => r.Usuario)
                .HasForeignKey<Usuario>(u => u.RolId);
        }
    }
}