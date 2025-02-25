using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;

namespace API.Application.Procesos.Commands
{
    public class UpdateProceso
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
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
                var proceso = await _context.Procesos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (proceso == null)
                {
                    throw new Exception("No se encontró el proceso");
                }

                proceso.Descr = request.Descr;
                proceso.Tipo = request.Tipo;
                proceso.Icono = request.Icono;
                proceso.FechaUltimaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}