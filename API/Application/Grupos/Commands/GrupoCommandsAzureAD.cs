using System.Threading;
using System.Threading.Tasks;
using API.Application.Core;
using MediatR;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace API.Application.Grupos.Commands
{
    public class CreateGrupoAzureAD
    {
        public class Command : IRequest<Result<string>>
        {
            public string Nombre { get; set; }
            public string Descr { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var group = new Group
                {
                    DisplayName = request.Nombre,
                    Description = request.Descr,
                    MailEnabled = false,
                    MailNickname = request.Nombre.Replace(" ", "").ToLower(),
                    SecurityEnabled = true,                    
                };

                var result = await _graphClient.Groups.PostAsync(group, cancellationToken: cancellationToken);
                return Result<string>.Success(result.Id);
            }
        }
    }

    public class UpdateGrupoAzureAD
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public string Descr { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var group = new Group
                {
                    DisplayName = request.Nombre,
                    Description = request.Descr
                };

                await _graphClient.Groups[request.Id].PatchAsync(group, cancellationToken: cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

    public class DeleteGrupoAzureAD
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _graphClient.Groups[request.Id].DeleteAsync(cancellationToken: cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
