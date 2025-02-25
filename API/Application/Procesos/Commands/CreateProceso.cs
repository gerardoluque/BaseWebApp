using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;
using API.Persistence; // Ensure this namespace is correct and contains AppDbContext

namespace API.Application.Procesos.Commands
{
    public class CreateProceso
    {
        public class Command : IRequest
        {
            public string Descr { get; set; }
            public string Tipo { get; set; }
            public string Icono { get; set; }
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
                var proceso = new Proceso
                {
                    Descr = request.Descr,
                    Tipo = request.Tipo,
                    Icono = request.Icono,
                    FechaCreacion = DateTime.UtcNow,
                    FechaUltimaActualizacion = DateTime.UtcNow
                };

                _context.Procesos.Add(proceso);
                await _context.SaveChangesAsync(cancellationToken);

                return;
            }
        }
    }
}