using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;
using MediatR;
using API.Application.Core;
using Microsoft.Graph.Models;

namespace API.Application.Usuarios.Queries
{
    public class GetUserListAzureAD
    {
        public class Query : IRequest<List<User>>
        {
        }

        public class Handler : IRequestHandler<Query, List<User>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _graphClient.Users.GetAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                return users?.Value?.ToList() ?? new List<User>();
            }
        }
    }

    public class GetUserByIdAzureAD
    {
        public class Query : IRequest<Result<User>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<User>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _graphClient.Users[request.Id].GetAsync(cancellationToken: cancellationToken);

                if (user == null)
                {
                    return Result<User>.Failure("Usuario no encontrado", 404);
                }

                return Result<User>.Success(user);
            }
        }
    }
}
