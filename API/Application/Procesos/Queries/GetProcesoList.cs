using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Procesos.Queries
{
    public class GetProcesoList
    {
        public class Query : IRequest<List<Proceso>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Proceso>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Proceso>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Procesos.ToListAsync(cancellationToken);
            }
        }
    }
}