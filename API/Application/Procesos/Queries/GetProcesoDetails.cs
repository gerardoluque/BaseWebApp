using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;

namespace API.Application.Procesos.Queries
{
    public class GetProcesoDetails
    {
        public class Query : IRequest<Proceso>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Proceso>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Proceso> Handle(Query request, CancellationToken cancellationToken)
            {
                var proceso = await _context.Procesos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (proceso == null)
                {
                    throw new Exception("No se encontró el proceso");
                }

                return proceso;
            }
        }
    }
}