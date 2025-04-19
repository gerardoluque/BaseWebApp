using System.Threading;
using System.Threading.Tasks;
using API.Application.Core;
using MediatR;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace API.Application.Roles.Commands
{
    public class CreateRolAzureAD
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
                var directoryRole = new UnifiedRoleDefinition
                {
                    DisplayName = request.Nombre,
                    Description = request.Descr,
                    RolePermissions = new List<UnifiedRolePermission>
                    {
                        new UnifiedRolePermission
                        {
                            AllowedResourceActions = new List<string>
                            {
                                "microsoft.directory/applications/basic/read",
                            },
                        },
                    },
                    IsEnabled = true,
                };

                var result = await _graphClient.RoleManagement.Directory.RoleDefinitions.PostAsync(directoryRole, cancellationToken: cancellationToken);
                return Result<string>.Success(result.Id);
            }
        }
    }

    public class UpdateRolAzureAD
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
                var directoryRole = new DirectoryRole
                {
                    Description = request.Descr,
                    DisplayName = request.Nombre
                };
                await _graphClient.DirectoryRoles[request.Id].PatchAsync(directoryRole, cancellationToken: cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

    public class DeleteRolAzureAD
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
                // Fix: Ensure the correct method is used for deletion
                await _graphClient.DirectoryRoles[request.Id].DeleteAsync(cancellationToken: cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}