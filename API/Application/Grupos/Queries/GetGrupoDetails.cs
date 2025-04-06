using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using MediatR;
using API.Persistence;
using API.Application.Core;

namespace API.Application.Grupos.Queries
{
    public class GetGrupoDetails
    {
        public class Query : IRequest<Result<Grupo>>
        {
            public required int Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Result<Grupo>>
        {
            public async Task<Result<Grupo>> Handle(Query request, CancellationToken cancellationToken)
            {
                var grupo = await context.Grupos.FindAsync(request.Id, cancellationToken);

                if (grupo == null)
                {
                    return Result<Grupo>.Failure("Grupo no encontrado", 404);
                }

                return Result<Grupo>.Success(grupo);
            }
        }
    }
}