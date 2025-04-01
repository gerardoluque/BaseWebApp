using System;
using System.Threading;
using System.Threading.Tasks;
using API.Persistence;
using MediatR;


namespace API.Application.Procesos.Commands
{
    public class DeleteProceso
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
                var proceso = await _context.Procesos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (proceso == null)
                {
                    throw new Exception("No se encontró el proceso");
                }

                _context.Procesos.Remove(proceso);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}