using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using MediatR;
using API.Persistence;

namespace API.Application.Grupos.Queries
{
    public class GetGrupoDetails
    {
        public class Query : IRequest<Grupo>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Grupo>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Grupo> Handle(Query request, CancellationToken cancellationToken)
            {
                var grupo = await _context.Grupos.FindAsync(request.Id, cancellationToken);

                if (grupo == null)
                {
                    throw new Exception("No se encontró el grupo");
                }

                return grupo;
            }
        }
    }
}