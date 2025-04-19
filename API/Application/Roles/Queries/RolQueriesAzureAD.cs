using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using MediatR;
using API.Application.Core;
using Microsoft.Graph.Models;
using API.Domain;

namespace API.Application.Roles.Queries
{
    public class GetRolListAzureAD
    {
        public class Query : IRequest<List<DirectoryRole>>
        {
        }

        public class Handler : IRequestHandler<Query, List<DirectoryRole>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<List<DirectoryRole>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roles = await _graphClient.DirectoryRoles.GetAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                return roles?.Value?.ToList() ?? new List<DirectoryRole>();
            }
        }
    }

    public class GetRolByIdAzureAD
    {
        public class Query : IRequest<Result<DirectoryRole>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DirectoryRole>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<DirectoryRole>> Handle(Query request, CancellationToken cancellationToken)
            {
                var role = await _graphClient.DirectoryRoles[request.Id].GetAsync(cancellationToken: cancellationToken);

                if (role == null)
                {
                    return Result<DirectoryRole>.Failure("Rol not encontrado", 404);
                }

                return Result<DirectoryRole>.Success(role);
            }
        }
    }
}