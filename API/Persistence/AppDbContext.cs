using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Domain;

namespace API.Persistence
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Grupo> Grupos { get; set; }
        public required DbSet<Rol> Roles { get; set; }
        public required DbSet<Proceso> Procesos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}