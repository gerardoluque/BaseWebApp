using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Persistence
{
    public class DbInitializer
    {
        public static async Task SeedData(AppDbContext context, UserManager<Usuario> userManager)
        {
            if (context.Grupos.Any() || context.Roles.Any())
            {
                return;
            }

            var roles = new List<Rol>
            {
                new Rol
                {
                    Nombre = "Administrador",
                    Descr = "Roles para usuarios de tipo Administradores",
                    FechaCreacion = DateTime.Now,
                    FechaUltimaActualizacion = DateTime.Now
                },
                new Rol
                {
                    Nombre = "Usuario",
                    Descr = "Roles para Usuarios",
                    FechaCreacion = DateTime.Now,
                    FechaUltimaActualizacion = DateTime.Now
                }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();

            var grupos = new List<Grupo>
            {
                new Grupo
                {
                    Nombre = "Administradores",
                    Descr = "Grupo de Administradores",
                    FechaCreacion = DateTime.Now,
                    FechaUltimaActualizacion = DateTime.Now
                },
                new Grupo
                {
                    Nombre = "Usuarios",
                    Descr = "Grupo para Usuarios",
                    FechaCreacion = DateTime.Now,
                    FechaUltimaActualizacion = DateTime.Now
                }
            };

            await context.Grupos.AddRangeAsync(grupos);
            await context.SaveChangesAsync();

            if (!userManager.Users.Any())
            {
                var users = new List<Usuario>
                {
                    new() {GrupoId = 1, RolId = 1, Nombre = "Bob", PrimerApellido = "Smith", SegundoApellido = "Jhonson", UserName = "bob@test.com", Email = "bob@test.com"},
                    new() {GrupoId = 1, RolId = 1, Nombre = "Tom", PrimerApellido = "Smith", SegundoApellido = "Jhonson", UserName = "tom@test.com", Email = "tom@test.com"},
                    new() {GrupoId = 2, RolId = 2, Nombre = "Jane", PrimerApellido = "Smith", SegundoApellido = "Jhonson", UserName = "jane@test.com", Email = "jane@test.com"}
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}