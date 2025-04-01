using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Persistence;
using API.Domain;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading;

namespace API.Application.Grupos.Queries
{
    public class GetGrupoList
    {
        public class Query : IRequest<List<Grupo>>
        {
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Grupo>>
        {
            public async Task<List<Grupo>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Grupos.ToListAsync(cancellationToken);
            }
        }   
    }
}