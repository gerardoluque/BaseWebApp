using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using API.Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Roles.Queries
{
    public class GetRolList
    {
        public class Query : IRequest<List<Rol>>
        {
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Rol>>
        {
            public async Task<List<Rol>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Roles.ToListAsync(cancellationToken);
            }
        }
    }

    public class GetRolById
    {
        public class Query : IRequest<Result<Rol>>
        {
            public int Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Result<Rol>>
        {
            public async Task<Result<Rol>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);

                if (result == null)
                {
                    return Result<Rol>.Failure("Rol not encontrado", 404);
                }

                return Result<Rol>.Success(result);
            }
        }
    }
}