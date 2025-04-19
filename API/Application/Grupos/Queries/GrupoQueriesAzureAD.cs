using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;
using MediatR;
using API.Application.Core;
using Microsoft.Graph.Models;

namespace API.Application.Grupos.Queries
{
    public class GetGrupoListAzureAD
    {
        public class Query : IRequest<List<Group>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Group>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<List<Group>> Handle(Query request, CancellationToken cancellationToken)
            {
                var groups = await _graphClient.Groups.GetAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                return groups?.Value?.ToList() ?? new List<Group>();
            }
        }
    }

    public class GetGrupoByIdAzureAD
    {
        public class Query : IRequest<Result<Group>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Group>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<Group>> Handle(Query request, CancellationToken cancellationToken)
            {
                var group = await _graphClient.Groups[request.Id].GetAsync(cancellationToken: cancellationToken);

                if (group == null)
                {
                    return Result<Group>.Failure("Grupo no encontrado", 404);
                }

                return Result<Group>.Success(group);
            }
        }
    }
}
