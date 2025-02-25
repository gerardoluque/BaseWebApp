using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;

namespace API.Application.Roles.Commands
{
    public class CreateRol
    {
        public class Command : IRequest
        {
            public string Descr { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var rol = new Rol
                {
                    Descr = request.Descr,
                    FechaCreacion = DateTime.UtcNow,
                    FechaUltimaActualizacion = DateTime.UtcNow
                };

                _context.Roles.Add(rol);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public class UpdateRol
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Descr { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var rol = await _context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);

                if (rol == null)
                {
                    throw new Exception("Rol not found");
                }

                rol.Descr = request.Descr;
                rol.FechaUltimaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public class DeleteRol
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var rol = await _context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);

                if (rol == null)
                {
                    throw new Exception("Rol not found");
                }

                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}