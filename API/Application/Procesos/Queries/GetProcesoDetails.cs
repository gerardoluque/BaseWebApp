using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;
using API.Application.Core;

namespace API.Application.Procesos.Queries
{
    public class GetProcesoDetails
    {
        public class Query : IRequest<Result<Proceso>>
        {
            public int Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Result<Proceso>>
        {
            public async Task<Result<Proceso>> Handle(Query request, CancellationToken cancellationToken)
            {
                var proceso = await context.Procesos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (proceso == null)
                {
                    return Result<Proceso>.Failure("No se encontró el proceso", 404);
                }

                return Result<Proceso>.Success(proceso);
            }
        }
    }
}