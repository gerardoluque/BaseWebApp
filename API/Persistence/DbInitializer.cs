using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class DbInitializer
    {
        public static async Task SeedData(AppDbContext context)
        {
            if (context.Grupos.Any())
            {
                return;
            }

            var grupos = new List<Grupo>
            {
                new Grupo
                {
                    Descr = "Administradores",
                    FechaCreacion = DateTime.Now,
                    FechaUltimaActualizacion = DateTime.Now
                },
                new Grupo
                {
                    Descr = "Usuarios",
                    FechaCreacion = DateTime.Now,
                    FechaUltimaActualizacion = DateTime.Now
                }
            };

            await context.Grupos.AddRangeAsync(grupos);

            await context.SaveChangesAsync();
        }
    }
}