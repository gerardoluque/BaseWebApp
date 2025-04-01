using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Roles.Queries
{
    public class GetRolList
    {
        public class Query : IRequest<List<Rol>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Rol>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Rol>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Roles.ToListAsync(cancellationToken);
            }
        }
    }

    public class GetRolById
    {
        public class Query : IRequest<Rol>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Rol>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Rol> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);
            }
        }
    }
}